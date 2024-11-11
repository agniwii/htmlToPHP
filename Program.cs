using System;
using fileConverter.Services;
using fileConverter.Models;
using fileConverter.Helpers;

namespace fileConverter{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ConsoleHelper.PrintHeader();

                var config = ConsoleHelper.GetUserInput();

                var converter = new fileConverterService(config);
                converter.ProcessFiles();

                ConsoleHelper.PrintSummary(converter.filesProcessed);
            }
            catch (Exception e)
            {
                ConsoleHelper.PrintError(e.Message);
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}