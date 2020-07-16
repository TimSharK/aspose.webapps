using System.IO;
using System.Threading.Tasks;
using Aspose.Imaging;
using Aspose.Imaging.ImageOptions;
using Domain;
using Domain.Exceptions;

namespace Services
{
    public class AsposeImageConverterService : IImageConverterService
    {
        private readonly IResultStorage _resultStorage;

        public AsposeImageConverterService(IResultStorage resultStorage)
        {
            _resultStorage = resultStorage;
        }

        public async Task<string> ConvertAsync(byte[] bytes, string format)
        {
            ImageOptionsBase exportOptions;

            switch (format)
            {
                case "PDF":
                    exportOptions = new PdfOptions
                    {
                        VectorRasterizationOptions = new SvgRasterizationOptions
                        {
                            PageWidth = 400,
                            PageHeight = 400
                        }
                    };
                    break;
                case "PSD":
                    exportOptions = new PsdOptions();
                    break;
                case "PNG":
                    exportOptions = new PngOptions();
                    break;
                default:
                    throw new ConvertException($"'{format}' is not supported yet");
            }

            using var outputStream = new MemoryStream();
            await using var stream = new MemoryStream(bytes);

            using (var image = Image.Load(stream))
            {
                image.Save(outputStream, exportOptions);
            }

            var id = await _resultStorage.SaveResultAsync(outputStream);

            return id;
        }
    }
}
