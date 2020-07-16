using System.IO;
using System.Threading.Tasks;

namespace Domain
{
    public interface IImageConverterService
    {
        /// <summary>
        /// Convert and save to <see cref="IResultStorage"/>
        /// </summary>
        /// <param name="bytes">Document to be converted</param>
        /// <param name="format">PDF, PSD or PNG</param>
        /// <returns>Unique ID of the file that is used by <see cref="IResultStorage"/>.GetResult</returns>
        Task<string> ConvertAsync(byte[] bytes, string format);
    }
}