using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace TagCloudTask
{
    public class ImageWriter : IImageWriter
    {
        private readonly string outputPath;
        private readonly string outputFormat;

        public ImageWriter(string outputPath, string outputFormat)
        {
            this.outputPath = outputPath;
            this.outputFormat = outputFormat;
        }

        public void WriteImage(Bitmap bitmap)
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
