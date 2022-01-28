using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using MemeIt.Core;
using MemeIt.Models.DTOs;

namespace MemeIt.Data.Services;

public class BlobService : IBlobService
{
    private readonly BlobServiceClient _blobServiceClient;

    public BlobService()
    {
        _blobServiceClient = new BlobServiceClient(ConfigurationService.Configuration.AzureBlobStorageConnectionStrings);
    }

    public async Task<string> GetBlobUriAsync(string fileName, string containerName)
    {
        var containerClient = await Task.Run(() => _blobServiceClient.GetBlobContainerClient(containerName));
        var blobClient = await Task.Run(() => containerClient.GetBlobClient(fileName));

        return blobClient.Uri.AbsoluteUri;
    }

    public async Task<bool> UploadBlobAsync(string fileName, IFormFile file, string containerName)
    {
        var containerClient = await Task.Run(() => _blobServiceClient.GetBlobContainerClient(containerName));
        var blobClient = await Task.Run(() => containerClient.GetBlobClient(fileName));
        var httpHeaders = new BlobHttpHeaders()
        {
            ContentType = file.ContentType
        };

        var res = await blobClient.UploadAsync(file.OpenReadStream(), httpHeaders);

        if (res != null) return true;

        return false;
    }

    public async Task<bool> DeleteBlobAsync(string fileName, string containerName)
    {
        var containerClient = await Task.Run(() => _blobServiceClient.GetBlobContainerClient(containerName));
        var blobClient = await Task.Run(() => containerClient.GetBlobClient(fileName));
        return await blobClient.DeleteIfExistsAsync();
    }
}