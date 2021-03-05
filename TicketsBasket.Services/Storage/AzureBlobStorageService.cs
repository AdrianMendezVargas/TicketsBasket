using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TicketsBasket.Infrastructure.Options;

namespace TicketsBasket.Services.Storage {
    public class AzureBlobStorageService : IStorageService {

        private readonly AzureStorageOptions _options;
        private readonly BlobServiceClient _blobServiceClient;

        public AzureBlobStorageService(AzureStorageOptions options) {
            _options = options;
            _blobServiceClient = new BlobServiceClient(options.StorageConnectionString);
        }

        public string GetProtectedUrl(string containerName , string blobName , DateTimeOffset expireDate) {
            var container = _blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = container.GetBlobClient(Path.GetFileName(blobName));
            var token = blobClient.GenerateSasUri(Azure.Storage.Sas.BlobSasPermissions.Read , expireDate);

            return token.AbsoluteUri;
        }

        public async Task RemoveBlobAsync(string containerName , string blobName) {

            var container = _blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = container.GetBlobClient(Path.GetFileName(blobName));

            await blobClient.DeleteIfExistsAsync();
        }

        public async Task<string> SaveBlobAsync(string containerName , IFormFile file, BlobType blobType) {

            if (file == null) {
                return null;
            }

            string fileName = file.FileName;
            string extension = Path.GetExtension(fileName);

            ValidateExtension(blobType, extension);

            string newFileName = $"{Path.GetFileNameWithoutExtension(fileName)}-{Guid.NewGuid()}{extension}";

            using(var stream = file.OpenReadStream()) {

                var container = _blobServiceClient.GetBlobContainerClient(containerName);
                await container.CreateIfNotExistsAsync();
                var blob = container.GetBlobClient(newFileName);

                await blob.UploadAsync(stream);

                return $"{_options.AccountUrl}/{containerName}/{newFileName}";
            }
        }

        private void ValidateExtension(BlobType blobType, string extension) {

            var allowedImageExtensions = new[] { ".jpg", ".png", ".bmp", ".svg" };

            var allowedDocumentsExtensions = new[] { ".pdf" , ".doc" , ".docx" , ".xls" , ".xlsx" , ".txt" };

            switch (blobType) {
                case BlobType.Image:
                    if (!allowedImageExtensions.Contains(extension)) {
                        throw new BadImageFormatException();
                    }
                    break;
                case BlobType.Document:
                    if (!allowedDocumentsExtensions.Contains(extension)) {
                        throw new NotSupportedException($"Document not supported for the extension {extension}");
                    }
                    break;
                default:
                    break;
            }
            
                
        }

    }

}
