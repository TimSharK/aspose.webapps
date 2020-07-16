using System.IO;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultController : ControllerBase
    {
        private readonly IResultStorage _resultStorage;

        public ResultController(IResultStorage resultStorage)
        {
            _resultStorage = resultStorage;
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id, string fileName)
        {
            try
            {
                var fileStream = _resultStorage.GetResult(id);

                return new FileStreamResult(fileStream, GetContentType(fileName))
                {
                    FileDownloadName = fileName
                };
            }
            catch (IOException)
            {
                return BadRequest("Looks like your result is expired");
            }
        }

        private string GetContentType(string fileName)
        {
            var provider = new FileExtensionContentTypeProvider();

            if (!provider.TryGetContentType(fileName, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            return contentType;
        }
    }
}