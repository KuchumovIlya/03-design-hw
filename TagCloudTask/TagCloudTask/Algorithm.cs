using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TagCloudTask
{
    public class Algorithm : IAlgorithm
    {
        private readonly ICommandLineOptions config;

        public Algorithm (ICommandLineOptions config)
        {
            this.config = config;
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
            var bitmap = new Bitmap(config.GetBitmapWidth(), config.GetBitmapHeight());
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
            var wordWidth = word.Length * config.GetFontSize();
            var wordHeight = config.GetFontSize();
            var position = GetWordPosition(ref currentX, ref currentY, wordWidth, wordHeight);
            if (position.Item1 == -1)
                return;
            var font = new Font(FontFamily.GenericMonospace, config.GetFontSize() * (float)Math.Pow(rate, 0.07));
            var color = ControlPaint.Dark(Color.Aqua, (float)Math.Pow(rate, 0.2));
            var pen = new Pen(color);
            g.DrawString(word, font, pen.Brush, position.Item1, position.Item2);
        }

        private Tuple<int, int> GetWordPosition(ref int currentX, ref int currentY, int wordWidth, int wordHeight)
        {
            currentX += wordWidth;
            
            if (currentX > config.GetBitmapWidth())
            {
                currentX = wordWidth + new Random(currentY).Next(wordWidth);
                currentY += wordHeight;
            }
            
            if (currentY + wordHeight >= config.GetBitmapHeight())
                return Tuple.Create(-1, -1);

            return Tuple.Create(currentX - wordWidth, currentY);
        }
    }
}
