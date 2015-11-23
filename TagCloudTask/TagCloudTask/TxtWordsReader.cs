using System;
using System.IO;

namespace TagCloudTask
{
    public class TxtWordsReader : IWordsFileReader
    {
        private readonly string path;

        public TxtWordsReader(string path)
        {
            this.path = path;
        }

        public string[] ReadWordsFromFile()
        {
            return File.ReadAllText(path)
                .Split(new[] {' ', '\n', '\r', '\t'}, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
