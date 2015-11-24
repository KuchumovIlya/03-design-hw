using System;
using System.IO;

namespace TagCloudTask
{
    public class WordsReader : IWordsReader
    {
        private readonly string path;

        public WordsReader(string path)
        {
            this.path = path;
        }

        public string[] ReadWords()
        {
            return File.ReadAllText(path)
                .Split(new[] { ' ', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
