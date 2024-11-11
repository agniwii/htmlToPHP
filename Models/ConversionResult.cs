namespace fileConverter.Models
{
    public class ConversionResult
    {
        public string OriginalPath { get; set; }
        public string? NewPath { get; set; }
        public string? BackupPath { get; set; }
        public string? RelativePath { get; set; }
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }

        public ConversionResult(string originalPath)
        {
            OriginalPath = originalPath;
            Success = true;

            NewPath = string.Empty;
            BackupPath = string.Empty;
            RelativePath = string.Empty;
            ErrorMessage = string.Empty;
        }
    }
}