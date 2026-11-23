using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Threading;
using System.Windows.Forms;

namespace SimpleScreenRecorder
{
    internal static class Program
    {
        private const string TempFolderPrefix = "SimpleScreenRecorder_DLLs_";
        private const string DllFileName = "ScreenRecorderLib.dll";
        private const string ResourceName = "SimpleScreenRecorder.Resources.ScreenRecorderLib.dll";
        private static Assembly _recorderLibrary;

        [STAThread]
        static void Main()
        {
            using (var mutex = new Mutex(true, @"Global\SimpleScreenRecorder", out bool createdNew))
            {
                if (!createdNew)
                    return;

                string tempFolder = null;
                FileStream dllFile = null;
                try
                {
                    try
                    {
                        using (Stream resource = OpenLibraryResource())
                        {
                            byte[] expectedHash = ComputeHash(resource);
                            CleanupTempFolders(expectedHash);
                            tempFolder = CreatePrivateTempFolder();
                            string dllPath = Path.Combine(tempFolder, DllFileName);

                            // Never reuse a DLL already on disk. Close the write handle
                            // before loading: the Windows loader requires read sharing.
                            using (var output = new FileStream(dllPath, FileMode.CreateNew,
                                FileAccess.Write, FileShare.None))
                            {
                                resource.Position = 0;
                                resource.CopyTo(output);
                            }

                            // Verify through the same read handle that prevents writes
                            // and deletion for the lifetime of the application.
                            dllFile = new FileStream(dllPath, FileMode.Open, FileAccess.Read, FileShare.Read);
                            if (!HashesEqual(expectedHash, ComputeHash(dllFile)))
                                throw new IOException("The extracted recording library failed its integrity check.");

                            // This is a mixed native/managed assembly and needs a file on
                            // disk. Load it before any recorder types are JIT-compiled.
                            _recorderLibrary = Assembly.LoadFrom(dllPath);
                            AppDomain.CurrentDomain.AssemblyResolve += ResolveRecorderLibrary;
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex);
                        MessageBox.Show("DLL load failed:\n" + ex.Message, "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    RunApplication();
                }
                finally
                {
                    AppDomain.CurrentDomain.AssemblyResolve -= ResolveRecorderLibrary;
                    if (dllFile != null)
                        dllFile.Dispose();
                    if (tempFolder != null)
                        DeleteLibraryFolder(tempFolder);
                }
            }
        }

        private static Assembly ResolveRecorderLibrary(object sender, ResolveEventArgs args)
        {
            // The CLR can request this assembly in the default load context after
            // LoadFrom. Only resolve the exact identity already verified above.
            return _recorderLibrary != null &&
                string.Equals(args.Name, _recorderLibrary.FullName, StringComparison.OrdinalIgnoreCase)
                ? _recorderLibrary : null;
        }

        // Keep recorder type resolution out of Main until the embedded library is loaded.
        [MethodImpl(MethodImplOptions.NoInlining)]
        private static void RunApplication()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ScreenRecorder());
        }

        private static Stream OpenLibraryResource()
        {
            return Assembly.GetExecutingAssembly().GetManifestResourceStream(ResourceName)
                ?? throw new FileNotFoundException("Embedded DLL not found: " + ResourceName);
        }

        private static string CreatePrivateTempFolder()
        {
            SecurityIdentifier user = WindowsIdentity.GetCurrent().User;
            var security = new DirectorySecurity();
            security.SetOwner(user);
            security.SetAccessRuleProtection(true, false);
            const InheritanceFlags inheritance = InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit;
            security.AddAccessRule(new FileSystemAccessRule(user, FileSystemRights.FullControl,
                inheritance, PropagationFlags.None, AccessControlType.Allow));
            security.AddAccessRule(new FileSystemAccessRule(
                new SecurityIdentifier(WellKnownSidType.LocalSystemSid, null), FileSystemRights.FullControl,
                inheritance, PropagationFlags.None, AccessControlType.Allow));

            string folder = Path.Combine(Path.GetTempPath(), TempFolderPrefix + Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(folder, security);
            return folder;
        }

        private static byte[] ComputeHash(Stream stream)
        {
            using (var sha256 = SHA256.Create())
                return sha256.ComputeHash(stream);
        }

        private static bool HashesEqual(byte[] first, byte[] second)
        {
            if (first.Length != second.Length)
                return false;
            for (int i = 0; i < first.Length; i++)
                if (first[i] != second[i])
                    return false;
            return true;
        }

        private static void CleanupTempFolders(byte[] expectedHash)
        {
            try
            {
                SecurityIdentifier user = WindowsIdentity.GetCurrent().User;
                foreach (string folder in Directory.GetDirectories(Path.GetTempPath(), TempFolderPrefix + "*"))
                {
                    try
                    {
                        // Only the new GUID layout is ours. Leave legacy PID folders alone.
                        string suffix = Path.GetFileName(folder).Substring(TempFolderPrefix.Length);
                        if (!Guid.TryParseExact(suffix, "N", out _) ||
                            (File.GetAttributes(folder) & FileAttributes.ReparsePoint) != 0 ||
                            !user.Equals(Directory.GetAccessControl(folder).GetOwner(typeof(SecurityIdentifier))))
                            continue;

                        string[] entries = Directory.GetFileSystemEntries(folder);
                        string dllPath = Path.Combine(folder, DllFileName);
                        if (entries.Length != 1 || !string.Equals(entries[0], dllPath, StringComparison.OrdinalIgnoreCase) ||
                            (File.GetAttributes(dllPath) & (FileAttributes.Directory | FileAttributes.ReparsePoint)) != 0)
                            continue;

                        // An active instance holds a conflicting handle. Only remove a
                        // closed file whose contents match our embedded dependency.
                        using (var file = new FileStream(dllPath, FileMode.Open, FileAccess.Read, FileShare.None))
                            if (!HashesEqual(expectedHash, ComputeHash(file)))
                                continue;

                        DeleteLibraryFolder(folder);
                    }
                    catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException || ex is System.Security.SecurityException)
                    {
                        Debug.WriteLine("Temporary library cleanup skipped: " + ex.Message);
                    }
                }
            }
            catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException || ex is System.Security.SecurityException)
            {
                Debug.WriteLine("Temporary library cleanup skipped: " + ex.Message);
            }
        }

        private static void DeleteLibraryFolder(string folder)
        {
            try
            {
                // Never recursively delete a temporary directory. Native images may
                // remain locked until process exit; retry verified leftovers next launch.
                File.Delete(Path.Combine(folder, DllFileName));
                Directory.Delete(folder, false);
            }
            catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException || ex is System.Security.SecurityException)
            {
                Debug.WriteLine("Temporary library cleanup deferred: " + ex.Message);
            }
        }
    }
}
