using System;
using System.IO;
using System.Linq;

namespace fileConverter.Helpers
{
    public class fileHelper
    {
        // Verifies if the config file exists in the specified root directory
        public bool VerifyConfigFile(string rootDirectory, string configPath)
        {
            string configFullPath = Path.Combine(rootDirectory, configPath);
            return File.Exists(configFullPath);
        }

        // Calculates the relative path from the current directory to the root directory
        public string CalculateRelativePath(string currentDirectory, string rootDirectory)
        {
            string relativePath = Path.GetRelativePath(currentDirectory, rootDirectory);
            int depth = relativePath.Split(Path.DirectorySeparatorChar).Length;
            return string.Concat(Enumerable.Repeat("../", depth));
        }
    }
}