using CommandLine;
using CommandLine.Text;

namespace TagCloudTask
{
    public class CommandLineOptions : IAlgorithmConfig
    {
        [Option('i', "input", Required = true, HelpText = "Input file to read.")]
        public string InputFile { get; set; }

        [Option('o', "output", DefaultValue = "output", HelpText = "Output file to write.")]
        public string OutputFile { get; set; }

        [Option("output_format", DefaultValue = ".png" , HelpText = "Format for output file.")]
        public string OutputFormat { get; set; }

        [Option('w', "width", DefaultValue = 500, HelpText = "Width of picture.")]
        public int Width { get; set; }

        [Option('h', "height", DefaultValue = 500, HelpText = "Height of picture.")]
        public int Height { get; set; }

        [Option('f', "font_size", DefaultValue = 20, HelpText = "Font size of words in could")]
        public int FontSize { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this).ToString();
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
    }
}
