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

        // можно вернуть IEnumerable
        public string[] ReadWords()
        {
            // давай WordsReader будет только читать, а логику по фильтрации и нормализации вынесем в TagCloudBuilder (см Program.cs)
            
            // мне кажется интерфейса IWordsFileReader быть не должно, вполне достаточно, если логику чтения скрыть за WordsReader
            // потому что вдруг мы из сети захотим читать, или из памяти (то есть не из файла) 
            return fileReader
                .ReadWordsFromFile()
                .Select(normalizer.NormalizeWord)
                .Where(filter.IsInterestingWord)
                .ToArray();
        }
    }
}
