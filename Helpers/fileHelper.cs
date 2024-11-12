using System;
using System.Collections.Generic;
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

        // Reads the URL from the HTML file
        public string? ReadUrlFromHtml(string filePath)
        {
            List<string> validUrls = new List<string> { "http://localhost/", "https://protrax.formulatrix.com/" };
            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                if (line.Contains("window.location.href"))
                {
                    var startIndex = line.IndexOf('"') + 1;
                    var endIndex = line.LastIndexOf('"');
                    if (startIndex > 0 && endIndex > startIndex)
                    {
                        var url = line.Substring(startIndex, endIndex - startIndex);
                        if (validUrls.Contains(url))
                        {
                            return url;
                        }
                    }
                }
            }
            return null;
        }
    }
}