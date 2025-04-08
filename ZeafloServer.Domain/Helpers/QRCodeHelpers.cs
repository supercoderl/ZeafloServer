using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Domain.Helpers
{
    public class QRCodeGenerated
    {
        public Guid UserId { get; set; }
        public string QrBase64 { get; set; } = string.Empty;
    }

    public sealed class QRCodeHelpers
    {
        [SupportedOSPlatform("windows")]
        public Bitmap GenerateQRCode(string content)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            using var qrCodeData = qrGenerator.CreateQrCode(content, QRCodeGenerator.ECCLevel.Q);
            using var qrCode = new PngByteQRCode(qrCodeData);
            byte[] qrCodeAsPngByteArr = qrCode.GetGraphic(250, Color.DarkGreen, Color.Beige, true);
            Bitmap bmp;
            using (var ms = new MemoryStream(qrCodeAsPngByteArr))
            {
                bmp = new Bitmap(ms);
            }

            return bmp;
        }

        [SupportedOSPlatform("windows")]
        public async Task<Bitmap> AddLogoToQRCode(Bitmap qrBitmap, string logoPath)
        {
            if (string.IsNullOrEmpty(logoPath)) return qrBitmap; // return default qr if logo is not exists

            Bitmap logo;
            if (Uri.IsWellFormedUriString(logoPath, UriKind.Absolute)) // Check is it a URL
            {
                using HttpClient client = new HttpClient();
                byte[] imageBytes = await client.GetByteArrayAsync(logoPath);
                using MemoryStream ms = new MemoryStream(imageBytes);
                logo = new Bitmap(ms);
            }
            else // If local path
            {
                logo = new Bitmap(logoPath);
            }

            Bitmap qrWithLogo = new Bitmap(qrBitmap);

            using (Graphics graphics = Graphics.FromImage(qrWithLogo))
            {
                int logoSize = qrWithLogo.Width / 2;
                int logoX = (qrWithLogo.Width - logoSize) / 2;
                int logoY = (qrWithLogo.Height - logoSize) / 2;
                Rectangle logoRect = new Rectangle(logoX, logoY, logoSize, logoSize);

                graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
                graphics.DrawImage(logo, logoRect);
            }

            return qrWithLogo;
        }

        [SupportedOSPlatform("windows")]
        public string ConvertBitmapToBase64(Bitmap bitmap)
        {
            using MemoryStream ms = new MemoryStream();
            bitmap.Save(ms, ImageFormat.Png); // Store Bitmap int MemoryStream as PNG type
            byte[] imageBytes = ms.ToArray(); // Convert MemoryStream to byte array
            return Convert.ToBase64String(imageBytes); // Convert byte[] to Base64 string
        }

        [SupportedOSPlatform("windows")]
        public Bitmap ConvertBase64ToBitmap(string base64String)
        {
            if (base64String.Contains(","))
            {
                base64String = base64String.Substring(base64String.IndexOf(",") + 1);
            }
            byte[] imageBytes = Convert.FromBase64String(base64String); // Convert Base64 to byte array
            using MemoryStream ms = new MemoryStream(imageBytes); // Put byte into MemoryStream
            return new Bitmap(ms); // Convert to Bitmap
        }
    }
}
