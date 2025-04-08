using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Domain.Helpers
{
    public sealed class UploadHelpers
    {
        private readonly Cloudinary _cloudinary;

        public UploadHelpers(Cloudinary cloudinary)
        {
            _cloudinary = cloudinary;
        }

        public async Task<bool> DeleteImageAsync(string publicId)
        {
            if (string.IsNullOrWhiteSpace(publicId))
                throw new ArgumentException("Public ID is required.");

            var deleteParams = new DeletionParams(publicId);
            var result = await _cloudinary.DestroyAsync(deleteParams);

            return result.Result == "ok";
        }

        [SupportedOSPlatform("windows")]
        public async Task<string> UploadImageAsync(Bitmap image, string fileName, string folder)
        {
            using var stream = new MemoryStream();

            // Clone bitmap to void GDI+
            using var cloned = new Bitmap(image);
            cloned.Save(stream, ImageFormat.Png);
            stream.Position = 0; // Reset position

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(fileName, stream),
                Transformation = new Transformation().Quality(80).FetchFormat("auto"),
                Folder = folder
            };
            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            return uploadResult?.SecureUrl?.ToString() ?? throw new Exception("Upload failed.");
        }
    }
}
