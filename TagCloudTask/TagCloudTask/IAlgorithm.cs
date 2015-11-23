using System.Drawing;

namespace TagCloudTask
{
    public interface IAlgorithm
    {
        Bitmap BuildTagCloudBitmap(string[] words);
    }
}
