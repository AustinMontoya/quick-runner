using System.IO;

namespace QuickRunner.Core.Utils
{
    public static class DirectoryUtils
    {
        /// <summary>
        /// Recursive copy from msdn example
        /// http://msdn.microsoft.com/en-us/library/bb762914.aspx
        /// </summary>
        public static void CopyRecursive(string sourcePath, string destPath)
        {
            if (!Directory.Exists(destPath))
            {
                Directory.CreateDirectory(destPath);
            }

            foreach (var file in Directory.GetFiles(sourcePath))
            {
                var dest = Path.Combine(destPath, Path.GetFileName(file));
                File.Copy(file, dest, true);
            }

            foreach (var folder in Directory.GetDirectories(sourcePath))
            {
                var dest = Path.Combine(destPath, Path.GetFileName(folder));
                CopyRecursive(folder, dest);
            }
        }

        public static void Delete(string targetDir)
        {
            File.SetAttributes(targetDir, FileAttributes.Normal);

            var files = Directory.GetFiles(targetDir);
            var dirs = Directory.GetDirectories(targetDir);

            foreach (var file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            foreach (var dir in dirs)
            {
                Delete(dir);
            }

            Directory.Delete(targetDir, false);
        }
    }
}