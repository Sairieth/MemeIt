using Azure.Storage.Blobs.Models;

namespace MemeIt.Core;

public interface IBlobService
{
    public Task<string> GetBlobUriAsync(string fileName, string containerName);
    public Task<bool> UploadBlobAsync(string fileName,IFormFile file, string containerName);
    public Task<bool> DeleteBlobAsync(string fileName, string containerName);

}