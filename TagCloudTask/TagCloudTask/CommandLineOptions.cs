using CommandLine;
using CommandLine.Text;

namespace TagCloudTask
{
    public class CommandLineOptions : ICommandLineOptions
    {
        [Option('i', "input", DefaultValue = "..//../input_small.txt", HelpText = "Input file to read.")]
        public string InputFilePath { get; set; }

        [Option('o', "output", DefaultValue = "../../output", HelpText = "Output file to write.")]
        public string OutputFilePath { get; set; }

        [Option("output_format", DefaultValue = ".png" , HelpText = "Format for output file.")]
        public string OutputFormat { get; set; }

        [Option('w', "width", DefaultValue = 500, HelpText = "Width of picture.")]
        public int Width { get; set; }

        [Option('h', "height", DefaultValue = 500, HelpText = "Height of picture.")]
        public int Height { get; set; }

        [Option('f', "font_size", DefaultValue = 30, HelpText = "Font size of words in could")]
        public int FontSize { get; set; }

        [Option('m', "morph", DefaultValue = "../../mystem.exe", HelpText = "Path to morphological application")]
        public string MorphologicalApplicationPath { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this) + "Use utf-8 encoding.\n";
        }

        public string GetInputFile()
        {
            return InputFilePath;
        }

        public string GetOutputFile()
        {
            return OutputFilePath;
        }

        public string GetOutputFormat()
        {
            return OutputFormat;
        }

        public int GetBitmapWidth()
        {
            return Width;
        }

        public int GetBitmapHeight()
        {
            return Height;
        }

        public int GetFontSize()
        {
            return FontSize;
        }

        public string GetMorphologicalApplicationPath()
        {
            return MorphologicalApplicationPath;
        }
    }
}
