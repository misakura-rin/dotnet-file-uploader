using Microsoft.AspNetCore.Mvc;

namespace FileUploader.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UploadController : Controller
    {
        private readonly IOptions _options;
        public UploadController(IOptions options)
        {
            _options = options;
        }

        [HttpPost]
        public IActionResult Index(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                return StatusCode(StatusCodes.Status403Forbidden);
            }

            if (_options.Token != token)
            {
                return StatusCode(StatusCodes.Status403Forbidden);
            }

            var file = HttpContext.Request.Form.Files.First();
            if(file is null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            var extension = Path.GetExtension(file.FileName);
            
            if (!_options.AllowedFileExtensions.Contains(extension))
            {
                return StatusCode(StatusCodes.Status403Forbidden);
            }

            var filename = Guid.NewGuid().ToString().Replace("-", "") + extension;

            var path = Path.Combine(_options.OutputDir, filename);

            using var stream = new FileStream(path, FileMode.Create);
            file.CopyTo(stream);

            return Ok(_options.HostUrl, filename));
        }
    }
}
