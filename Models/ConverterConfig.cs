namespace fileConverter.Models
{
    public class ConverterConfig
    {
     public string RootDirectory { get; set; }
     public string ConfigPath { get; set; }
     public string SourceExtension { get; set; } = ".html";
    public string TargetExtension { get; set; } = ".php";

    public bool createBackup { get; set; } = true;

    public ConverterConfig(string rootDirectory, string configPath)
    {
        RootDirectory = rootDirectory;
        ConfigPath = configPath;
    }
    }
}