namespace TagCloudTask
{
    public interface IWordFilter
    {
        // название не очень подходящее, попробуй придумать другое (ну или забей, это не очень важно)
        bool IsInterestingWord(string word);
    }
}
