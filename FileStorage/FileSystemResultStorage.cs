using System;
using System.IO;
using System.Threading.Tasks;
using Domain;

namespace FileStorage
{
    public class FileSystemResultStorage : IResultStorage
    {
        private readonly string _rootPath;

        public FileSystemResultStorage(string rootPath)
        {
            _rootPath = rootPath;
        }

        public async Task<string> SaveResultAsync(Stream stream)
        {
            var id = Guid.NewGuid().ToString();
            var path = Path.Combine(_rootPath, id);

            await using (var fileStream = File.Create(path))
            {
                stream.Flush();
                stream.Seek(0, SeekOrigin.Begin);
                await stream.CopyToAsync(fileStream);
            }

            return id;
        }

        public Stream GetResult(string id)
        {
            var path = Path.Combine(_rootPath, id);

            return File.OpenRead(path);
        }
    }
}
