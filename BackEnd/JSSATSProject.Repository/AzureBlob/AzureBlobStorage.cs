using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;


namespace JSSATSProject.Repository.AzureBlob
{
    public class AzureBlobStorage
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string _containerName = "productimage";

        public AzureBlobStorage(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        public async Task<string> UploadImageAsync(Stream imageStream, string fileName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
            await containerClient.CreateIfNotExistsAsync();
            var blobClient = containerClient.GetBlobClient(fileName);

            // Get the file extension to determine the content type
            var extension = Path.GetExtension(fileName).ToLowerInvariant();
            string contentType;

            switch (extension)
            {
                case ".png":
                    contentType = "image/png";
                    break;
                case ".jpg":
                case ".jpeg":
                    contentType = "image/jpeg";
                    break;
                case ".gif":
                    contentType = "image/gif";
                    break;
                case ".bmp":
                    contentType = "image/bmp";
                    break;
                default:
                    contentType = "application/octet-stream"; // Default content type
                    break;
            }

            // Upload the image with the correct content type
            await blobClient.UploadAsync(imageStream, new BlobHttpHeaders { ContentType = contentType });

            return blobClient.Uri.ToString();
        }

    }
}

