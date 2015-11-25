namespace TagCloudTask
{
    public interface ICommandLineOptions
    {
        string GetInputFile();
        string GetOutputFile();
        string GetOutputFormat();
        int GetBitmapWidth();
        int GetBitmapHeight();
        int GetFontSize();
    }
}
