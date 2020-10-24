namespace Scio.Services.Data
{
    using System.IO;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Http;

    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary cloudinary;

        public CloudinaryService(Cloudinary cloudinary)
        {
            this.cloudinary = cloudinary;
        }

        public async Task<string> UploadAsync(IFormFile file, string fileName)
        {
            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);

            var dstFile = memoryStream.ToArray();

            using var dstStream = new MemoryStream(dstFile);

            var result = await this.cloudinary.UploadAsync(new ImageUploadParams()
            {
                File = new FileDescription(fileName, dstStream),
                PublicId = fileName,
            });

            return result.Uri.AbsoluteUri;
        }
    }
}
