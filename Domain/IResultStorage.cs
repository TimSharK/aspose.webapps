using System.IO;
using System.Threading.Tasks;

namespace Domain
{
    public interface IResultStorage
    {
        /// <summary>
        /// Saves result file to the underlying storage
        /// </summary>
        /// <param name="stream">File data to be saved</param>
        /// <returns>Unique ID of the result</returns>
        Task<string> SaveResultAsync(Stream stream);

        /// <summary>
        /// Retrieves the result from underlying storage
        /// </summary>
        /// <param name="id">ID of the result</param>
        /// <returns>Stream with result file data</returns>
        Stream GetResult(string id);
    }
}
