using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp.Formats.Webp;
using System;
using System.Collections.Generic;
using SixLabors.ImageSharp;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SixLabors.ImageSharp.Processing;

namespace Infrastructure.MyShop.Helpers
{
    public static class ImageHelper
    {
        public static async Task<string> SaveImageAsync(IFormFile image, string directoryPath)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                await image.CopyToAsync(ms);
                string fileName = await SaveBytesCompresAsync(ms.ToArray(), directoryPath);
                return fileName;
            }
        }
        public static byte[] Base64ToBytesArray(this string base64)
        {
            if (base64.Contains(","))
                base64 = base64.Split(',')[1];
            return Convert.FromBase64String(base64);
        }

        public static async Task<string> SaveImageAsync(byte[] bytes, string directoryPath)
        {
            return await SaveBytesCompresAsync(bytes, directoryPath);
        }

        public static void DeleteImage(string imageName, string directoryPath)
        {
            string dirSaveImage = Path.Combine(Directory.GetCurrentDirectory(), directoryPath, imageName);
            if (File.Exists(dirSaveImage))
            {
                File.Delete(dirSaveImage);
            }
            //File.Delete(dirSaveImage);
        }

        private static async Task<string> SaveBytesCompresAsync(byte[] bytes, string directoryPath)
        {
            string imageName = Path.GetRandomFileName() + ".jpg";
            string dirSaveImage = Path.Combine(Directory.GetCurrentDirectory(), directoryPath, imageName);
            using (var image = Image.Load(bytes))
            {
                image.Mutate(x =>
                {
                    x.Resize(new ResizeOptions
                    {
                        Size = new Size(1200, 1200),
                        Mode = ResizeMode.Max
                    });
                });

                using (var stream = System.IO.File.Create(dirSaveImage))
                {
                    await image.SaveAsync(stream, new WebpEncoder());
                }
            }
            return imageName;
        }
    }
}
