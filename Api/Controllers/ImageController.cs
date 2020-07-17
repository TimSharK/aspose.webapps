using System;
using System.IO;
using System.Threading.Tasks;
using Api.Models;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageConverterService _imageConverter;

        public ImageController(IImageConverterService imageConverter)
        {
            _imageConverter = imageConverter;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] ConvertImageData data)
        {
            var bytes = Convert.FromBase64String(data.FileData);
            var id = await _imageConverter.ConvertAsync(bytes, data.Format);
            var fileName = GetOutputFileName(data.FileName, data.Format);

            Response.Headers.Add("Access-Control-Allow-Origin", "*");

            return new JsonResult(new { id, fileName });
        }

        private string GetOutputFileName(string originalName, string format)
        {
            var fileWithoutExt = Path.GetFileNameWithoutExtension(originalName);
            var newExt = $".{format.ToLower()}";
            var fileName = fileWithoutExt + newExt;

            return fileName;
        }
    }
}
