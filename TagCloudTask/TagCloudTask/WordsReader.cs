using System.Linq;

namespace TagCloudTask
{
    public class WordsReader : IWordsReader
    {
        private readonly IWordsFileReader fileReader;
        private readonly IWordNormalizer normalizer;
        private readonly IWordFilter filter;

        public WordsReader(IWordsFileReader fileReader, IWordNormalizer normalizer, IWordFilter filter)
        {
            this.fileReader = fileReader;
            this.normalizer = normalizer;
            this.filter = filter;
        }

        public string[] ReadWords()
        {
            return fileReader
                .ReadWordsFromFile()
                .Select(normalizer.NormalizeWord)
                .Where(filter.IsInterestingWord)
                .ToArray();
        }
    }
}
