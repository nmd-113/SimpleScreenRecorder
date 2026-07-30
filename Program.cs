using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace SimpleScreenRecorder
{
    internal static class Program
    {
        private static readonly string TempFolderPath =
            Path.Combine(Path.GetTempPath(),
            "SimpleScreenRecorder_DLLs_" + Process.GetCurrentProcess().Id);

        private static readonly string DllPath =
            Path.Combine(TempFolderPath, "ScreenRecorderLib.dll");

        [STAThread]
        static void Main()
        {
            using (var mutex = new Mutex(true, @"Global\SimpleScreenRecorder", out bool createdNew))
            {
                if (!createdNew)
                    return;

                AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;

                CleanupTempFolders();

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new ScreenRecorder());
            }
        }

        private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            string requestedName = new AssemblyName(args.Name).Name;

            if (!requestedName.Equals("ScreenRecorderLib", StringComparison.OrdinalIgnoreCase))
                return null;

            try
            {
                Directory.CreateDirectory(TempFolderPath);

                if (!File.Exists(DllPath))
                {
                    ExtractResourceToFile(
                        "SimpleScreenRecorder.Resources.ScreenRecorderLib.dll",
                        DllPath);
                }

                return Assembly.LoadFrom(DllPath);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);

                MessageBox.Show(
                    "DLL load failed:\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return null;
            }
        }

        private static void ExtractResourceToFile(string resourceName, string targetPath)
        {
            Assembly asm = Assembly.GetExecutingAssembly();

            Stream stream = asm.GetManifestResourceStream(resourceName);
            if (stream == null)
                throw new FileNotFoundException("Embedded DLL not found: " + resourceName);

            using (stream)
            using (FileStream fs = new FileStream(targetPath, FileMode.Create, FileAccess.Write))
            {
                stream.CopyTo(fs);
            }
        }

        private static void CleanupTempFolders()
        {
            try
            {
                string baseTemp = Path.GetTempPath();
                string currentFolderName = "SimpleScreenRecorder_DLLs_" + Process.GetCurrentProcess().Id;

                foreach (var dir in Directory.GetDirectories(baseTemp, "SimpleScreenRecorder_DLLs_*"))
                {
                    try
                    {
                        if (Path.GetFileName(dir) == currentFolderName)
                            continue;

                        Directory.Delete(dir, true);
                    }
                    catch
                    {
                        // ignore
                    }
                }

                Directory.CreateDirectory(TempFolderPath);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Startup cleanup failed: " + ex.Message);
            }
        }
    }
}
