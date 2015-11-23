namespace TagCloudTask
{
    public class NullWordFilter : IWordFilter
    {
        public bool IsInterestingWord(string word)
        {
            return true;
        }
    }
}
