using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;

namespace FealtedByEsra.BAL.Extensions
{
    public class FileBaseUploader
    {
        private const int ThumbnailWidth = 300;
        private const int FullscreanWidth = 1000;

        public async Task UploadAsync(IFormFile file)
        {
            var imageResult = await Image.LoadAsync(file.ContentDisposition);

            var width = imageResult.Width;
            var heigth= imageResult.Height;

            if(width > FullscreanWidth)
            {
                heigth = (int)((double)FullscreanWidth / width * heigth);
                width = FullscreanWidth;
            }

            imageResult.Mutate(i => i.Resize(new Size(width, heigth)));

            imageResult.Metadata.ExifProfile = null;

            await imageResult.SaveAsJpegAsync(file.Name, new JpegEncoder
            {
                Quality = 75
            });


            //if (file == null || file.Length == 0)
            //    throw new FileNotFoundException();

            //var extension = Path.GetExtension(file.FileName).Trim('.').ToLower();

            //if (!(new[] { "jpg", "png", "jpeg" }.Contains(extension)))
            //    throw new FileNotFoundException();

            //string imageName = Guid.NewGuid() + extension;

            //string location = $"{sourcePath}";

            //if (!Directory.Exists(Path.Combine(location)))
            //    Directory.CreateDirectory(Path.Combine(location));

            //using (Stream stream = new FileStream(location, FileMode.Create))
            //    await file.CopyToAsync(stream);
        }
    }
}
