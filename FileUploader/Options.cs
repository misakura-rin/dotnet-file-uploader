namespace FileUploader
{
    public interface IOptions
    {
        string Token { get; }
        string OutputDir { get; }
        IEnumerable<string> AllowedFileExtensions { get; }
        string HostUrl { get; }
    }

    public class Options : IOptions
    {
        public string Token { get; }
        public string OutputDir { get; }
        public IEnumerable<string> AllowedFileExtensions { get; }
        public string HostUrl { get; }
        public Options()
        {
            Token = Environment.GetEnvironmentVariable("API_TOKEN") ?? throw new ArgumentException("Environment does not contain a value for 'API_TOKEN'!");
            OutputDir = Environment.GetEnvironmentVariable("OUTPUT_DIR") ?? throw new ArgumentException("Environment does not contain a value for 'OUTPUT_DIR'!");
            AllowedFileExtensions = ParseFileExtensions();
            HostUrl = Environment.GetEnvironmentVariable("HOST_URL") ?? throw new ArgumentException("Environment does not contain a value for 'HOST_URL'!");
        }

        private IEnumerable<string> ParseFileExtensions()
        {
            var rawExtensions = Environment.GetEnvironmentVariable("ALLOWED_FILE_EXTENSIONS") ?? throw new ArgumentException("Environment does not contain a value for 'ALLOWED_FILE_EXTENSIONS'!");
            return rawExtensions.Split(",");
        }
    }
}
