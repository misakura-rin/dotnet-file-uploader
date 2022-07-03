namespace FileUploader
{
    public interface IOptions
    {
        string Token { get; }
        string OutputDir { get; set; }
        IEnumerable<string> AllowedFileExtensions { get; set; }
    }

    public class Options : IOptions
    {
        public string Token { get; }
        public string OutputDir { get; set; }
        public IEnumerable<string> AllowedFileExtensions { get; set; }

        public Options()
        {
            Token = Environment.GetEnvironmentVariable("API_TOKEN") ?? throw new ArgumentException("Environment does not contain a value for 'API_TOKEN'!");
            OutputDir = Environment.GetEnvironmentVariable("OUTPUT_DIR") ?? throw new ArgumentException("Environment does not contain a value for 'OUTPUT_DIR'!");
            AllowedFileExtensions = ParseFileExtensions();
        }

        private IEnumerable<string> ParseFileExtensions()
        {
            var rawExtensions = Environment.GetEnvironmentVariable("ALLOWED_FILE_EXTENSIONS") ?? throw new ArgumentException("Environment does not contain a value for 'ALLOWED_FILE_EXTENSIONS'!");
            return rawExtensions.Split(",");
        }
    }
}
