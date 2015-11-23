namespace TagCloudTask
{
    public class NullWordNormalizer : IWordNormalizer
    {
        public string NormalizeWord(string word)
        {
            return word;
        }
    }
}
