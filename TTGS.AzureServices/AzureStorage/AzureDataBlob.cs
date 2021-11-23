using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using TTGS.Core.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;

namespace TTGS.AzureServices.AzureStorage
{
    public class AzureDataBlob : IDataBlob
    {
        private readonly BlobContainerClient _blobContainerClient;

        public AzureDataBlob(string connectionString, string containerName)
        {
            _blobContainerClient = new BlobContainerClient(connectionString, containerName);
        }

        public async Task<string> UploadImageAsync(string fileName, Stream stream, string contentType, string directory)
        {
            var blob = _blobContainerClient.GetBlobClient($"{directory}/{fileName.ToLower()}");
            await blob.UploadAsync(stream);
            return blob.Uri.AbsoluteUri;
        }

        public async Task RemoveImageAsync(string fileName, string directory)
        {
            var blob = _blobContainerClient.GetBlobClient($"{directory}/{fileName.ToLower()}");
            await blob.DeleteIfExistsAsync();
        }

        public async Task<byte[]> DownloadFileAsync(string uri)
        {
            var blob = new BlobClient(new Uri(uri));
            using var stream = new MemoryStream();
            {
                var response = await blob.DownloadAsync();
                using (var memoryStream = new MemoryStream())
                {
                    response.Value.Content.CopyTo(memoryStream);
                    return memoryStream.ToArray();
                }
            }
        }
    }
}
