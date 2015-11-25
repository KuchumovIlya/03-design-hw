using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace TagCloudTask
{
    public class TxtWordsReader : IWordsReader
    {
        private readonly string path;

        public TxtWordsReader(string path)
        {
            this.path = path;
        }

        public string[] ReadWords()
        {
            var text = File.ReadAllText(path).ToLower();
            var regExp = new Regex(@"[^\w\d]");
            return regExp.Split(text).Where(s => s != "").ToArray();
        }
    }
}
