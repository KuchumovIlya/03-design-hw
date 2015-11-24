using System.Drawing;
using System.Linq;

namespace TagCloudTask
{
    public class TagCloudBuilder : ITagCloudBuilder
    {
        private readonly IWordsReader reader;
        private readonly IWordNormalizer normalizer;
        private readonly IWordFilter filter;
        private readonly IAlgorithm algorithm;

        public TagCloudBuilder(IWordsReader reader, IWordNormalizer normalizer, IWordFilter filter, IAlgorithm algorithm)
        {
            this.reader = reader;
            this.normalizer = normalizer;
            this.filter = filter;
            this.algorithm = algorithm;
        }

        public Bitmap Build()
        {
            var words = reader
                .ReadWords()
                .Select(normalizer.NormalizeWord)
                .Where(filter.IsInterestingWord)
                .ToArray();
            return algorithm.BuildTagCloudBitmap(words);
        }
    }
}
