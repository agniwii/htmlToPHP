using System;
using System.IO.Pipes;
using fileConverter.Models;
using fileConverter.Services;

namespace fileConverter.Helpers
{
    public static class ConsoleHelper
    {
        public static void PrintHeader()
        {
            Console.WriteLine("HTML to PHP Converter");
            Console.WriteLine("---------------------");
        }

        public static ConverterConfig GetUserInput()
        {
            Console.Write("Enter the root directory: ");
            string rootDirectory = Console.ReadLine() ?? throw new ArgumentNullException("rootDirectory cannot be null");

            // Directly use 'config.php' without asking for input
            string configPath = "Config.php";

            return new ConverterConfig(rootDirectory, configPath);
        }

        public static void PrintConversionResult(ConversionResult result)
        {
            Console.WriteLine($"\nProcessed directory: {Path.GetDirectoryName(result.OriginalPath)}");
            Console.WriteLine($"Converted: {Path.GetFileName(result.OriginalPath)} → {Path.GetFileName(result.NewPath)}");
            Console.WriteLine($"Relative Path used: {result.RelativePath}");

            if(!String.IsNullOrEmpty(result.BackupPath))
            {
                Console.WriteLine($"Backup created: {Path.GetFileName(result.BackupPath)}");
            }
            Console.WriteLine(new string('-', 40));
        }

        public static void PrintSummary(int filesProcessed)
        {
            Console.WriteLine($"\nTotal files processed: {filesProcessed}");
        }

        public static void PrintError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\nError: {message}");
            Console.ResetColor();
        }
    }
}