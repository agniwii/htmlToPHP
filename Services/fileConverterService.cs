using System;
using System.IO;
using System.Text;
using fileConverter.Models;
using fileConverter.Helpers;

namespace fileConverter.Services
{
    public class fileConverterService
    {
        private readonly ConverterConfig _config;
        private readonly fileHelper _fileHelper; 

        public int filesProcessed { get; private set;}

        public fileConverterService(ConverterConfig config)
        {
            _config = config;
            _fileHelper = new fileHelper();
            filesProcessed = 0;
        }

        public void ProcessFiles()
        {
            if (!_fileHelper.VerifyConfigFile(_config.RootDirectory, _config.ConfigPath))
            {
                throw new FileNotFoundException("config.php not found");
            }

            ProcessDirectory(_config.RootDirectory);
        }

        private void ProcessDirectory(string currentDirectory)
        {
            // Skip the 'lib' directory and its subdirectories
            if (Path.GetFileName(currentDirectory).Equals("lib", StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            string searchPattern = "index.html"; // Only search for index.html
            foreach (string filePath in Directory.GetFiles(currentDirectory, searchPattern))
            {
                var result = ConvertFile(filePath);
                if (result.Success)
                {
                    ConsoleHelper.PrintConversionResult(result);
                    filesProcessed++;
                }
                else
                {
                    ConsoleHelper.PrintError(result.ErrorMessage ?? "Unknown error occurred.");
                }
            }

            foreach (string subDirectory in Directory.GetDirectories(currentDirectory))
            {
                ProcessDirectory(subDirectory);
            }
        }

        private ConversionResult ConvertFile(string filePath)
        {
            var result = new ConversionResult(filePath);

            try
            {
                string directory = Path.GetDirectoryName(filePath) ?? throw new ArgumentNullException(nameof(filePath), "File path cannot be null");
                result.NewPath = Path.Combine(directory, Path.GetFileNameWithoutExtension(filePath) + _config.TargetExtension);
                result.RelativePath = _fileHelper.CalculateRelativePath(directory, _config.RootDirectory);
                string newContent = GeneratePhpContent(result.RelativePath);

                File.WriteAllText(result.NewPath, newContent, Encoding.UTF8);

                File.Delete(filePath);
            }
            catch (Exception e)
            {
                result.Success = false;
                result.ErrorMessage = e.Message;
            }
            return result;
        }

        private string GeneratePhpContent(string relativePath)
        {
            return $@"<?php
require_once(""{relativePath}{_config.ConfigPath}"");
?>
<script>
    window.location.href = ""<?php echo $baseurl?>"";
</script>";
        }
    }
}