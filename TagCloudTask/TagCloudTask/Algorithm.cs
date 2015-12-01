using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TagCloudTask
{
    public class Algorithm : IAlgorithm
    {
        private readonly ICommandLineOptions options;

        public Algorithm (ICommandLineOptions options)
        {
            this.options = options;
        }

        private static IEnumerable<Tuple<string, double>> GetOrderedWordsWithRate(IReadOnlyCollection<string> words)
        {
            var count = words
                .GroupBy(x => x)
                .ToDictionary(g => g.Key, g => g.Count());
            var maxCount = count.Max(t => t.Value);
            return words
                .RandomShuffle(new Random())
                .Distinct()
                .Select(s => Tuple.Create(s, (double) count[s] / maxCount));
        }

        public Bitmap BuildTagCloudBitmap(string[] words)
        {
            var orderedWordsWithRate = GetOrderedWordsWithRate(words).ToArray();
            var bitmap = new Bitmap(options.GetBitmapWidth(), options.GetBitmapHeight());
            var currentX = 0;
            var currentY = 0;

            using (var g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.AliceBlue);
                foreach (var tuple in orderedWordsWithRate)
                    AddWordOnBitmap(g, tuple.Item1, tuple.Item2, ref currentX, ref currentY);        
            }
            return bitmap;
        }

        private void AddWordOnBitmap(Graphics g, string word, double rate, ref int currentX, ref int currentY)
        {
            var wordWidth = word.Length * options.GetFontSize();
            var wordHeight = options.GetFontSize();
            if (wordWidth > options.GetBitmapWidth())
                return;
            var position = GetWordPosition(ref currentX, ref currentY, wordWidth, wordHeight);
            if (position.Item1 == -1)
                return;
            var font = new Font(FontFamily.GenericMonospace, options.GetFontSize() * (float)Math.Pow(rate, 0.07));
            var color = ControlPaint.Dark(Color.Blue, (float)Math.Pow(rate, 0.2) + (float)0.5);
            var pen = new Pen(color);
            g.DrawString(word, font, pen.Brush, position.Item1, position.Item2);
        }

        private Tuple<int, int> GetWordPosition(ref int currentX, ref int currentY, int wordWidth, int wordHeight)
        {
            currentX += wordWidth;
            
            if (currentX > options.GetBitmapWidth())
            {
                var canAdd = Math.Min(options.GetBitmapWidth() - wordWidth, wordWidth);
                currentX = wordWidth + new Random(currentY).Next(canAdd);
                currentY += wordHeight;
            }
            
            if (currentY + wordHeight >= options.GetBitmapHeight())
                return Tuple.Create(-1, -1);

            return Tuple.Create(currentX - wordWidth, currentY);
        }
    }
}
