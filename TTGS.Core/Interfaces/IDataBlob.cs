using System.IO;
using System.Threading.Tasks;

namespace TTGS.Core.Interfaces
{
    public interface IDataBlob
    {
        Task<string> UploadImageAsync(string fileName, Stream stream, string contentType, string directory);
        Task RemoveImageAsync(string fileName, string directory);
        Task<byte[]> DownloadFileAsync(string uri);
    }
}
