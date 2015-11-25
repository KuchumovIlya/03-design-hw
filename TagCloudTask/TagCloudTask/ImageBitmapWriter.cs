using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace TagCloudTask
{
    public class ImageBitmapWriter : IBitmapWriter
    {
        private readonly string outputPath;
        private readonly string outputFormat;

        public ImageBitmapWriter(string outputPath, string outputFormat)
        {
            this.outputPath = outputPath;
            this.outputFormat = outputFormat;
        }

        public void WriteBitmap(Bitmap bitmap)
        {
            var toImageFormatConverter = new Dictionary<string, ImageFormat>
            {
                {".png", ImageFormat.Png},
                {".jpeg", ImageFormat.Jpeg},
                {".bmp", ImageFormat.Bmp}
            };
            bitmap.Save(outputPath + outputFormat, toImageFormatConverter[outputFormat]);
        }
    }
}
