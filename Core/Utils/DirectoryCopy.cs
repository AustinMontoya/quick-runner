using System.IO;

namespace QuickRunner.Core.Utils
{
    public static class DirectoryUtils
    {
        /// <summary>
        /// Recursive copy from msdn example
        /// http://msdn.microsoft.com/en-us/library/bb762914.aspx
        /// </summary>
        public static void CopyRecursive(string sourceDirName, string destDirName)
        {
            // Get the subdirectories for the specified directory.
            var dir = new DirectoryInfo(sourceDirName);
            var dirs = dir.GetDirectories();

            // If the destination directory doesn't exist, create it. 
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            var files = dir.GetFiles();
            foreach (var file in files)
            {
                var temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, false);
            }

            foreach (var subdir in dirs)
            {
                var temppath = Path.Combine(destDirName, subdir.Name);
                CopyRecursive(subdir.FullName, temppath);
            }
        }
    }
}