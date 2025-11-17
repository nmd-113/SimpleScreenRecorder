using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace SimpleScreenRecorder
{
    internal static class Program
    {
        private static readonly string TempFolderPath = Path.Combine(Path.GetTempPath(), "SimpleScreenRecorder_DLLs");

        [STAThread]
        static void Main()
        {
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;

            CleanupTempFolder();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ScreenRecorder());

            CleanupTempFolder();
        }

        private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            string assemblyName = new AssemblyName(args.Name).Name + ".dll";

            if (assemblyName.Equals("ScreenRecorderLib.dll", StringComparison.OrdinalIgnoreCase))
            {
                string resourceName = "SimpleScreenRecorder.Resources." + assemblyName;

                string tempFilePath = Path.Combine(TempFolderPath, assemblyName);

                try
                {
                    Directory.CreateDirectory(TempFolderPath);

                    if (!File.Exists(tempFilePath))
                    {
                        ExtractResourceToFile(resourceName, tempFilePath);
                    }

                    return Assembly.LoadFrom(tempFilePath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Eroare la extragerea/încărcarea {assemblyName}:\n{ex.Message}", "Eroare de Încărcare DLL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }

            return null;
        }

        private static void ExtractResourceToFile(string resourceName, string targetPath)
        {
            Assembly executingAssembly = Assembly.GetExecutingAssembly();

            string actualResourceName = executingAssembly.GetManifestResourceNames()
                .FirstOrDefault(name => name.EndsWith(resourceName.Split('.').Last(), StringComparison.OrdinalIgnoreCase)) ?? throw new FileNotFoundException($"Resursa încorporată '{resourceName}' nu a putut fi găsită în asamblare.");
            using (Stream stream = executingAssembly.GetManifestResourceStream(actualResourceName))
            using (FileStream fileStream = new FileStream(targetPath, FileMode.Create, FileAccess.Write))
            {
                if (stream == null)
                {
                    throw new FileNotFoundException($"Stream-ul resursei '{actualResourceName}' este null.");
                }

                stream.CopyTo(fileStream);
            }
        }

        private static void CleanupTempFolder()
        {
            try
            {
                if (Directory.Exists(TempFolderPath))
                {
                    Directory.Delete(TempFolderPath, true);
                }
            }
            catch (Exception)
            {
                /// Ignore any exceptions during cleanup
            }
        }
    }
}