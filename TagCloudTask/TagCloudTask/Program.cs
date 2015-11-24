using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;

namespace TagCloudTask
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new CommandLineOptions();
            if (!CommandLine.Parser.Default.ParseArguments(args, options))
                return;

            var kernel = new StandardKernel();
            kernel.Bind<IWordsReader>().To<WordsReader>().WithConstructorArgument("path", options.InputFile);
            kernel.Bind<IWordNormalizer>().To<NullWordNormalizer>();
            kernel.Bind<IWordFilter>().To<NullWordFilter>();
            kernel.Bind<ITagCloudBuilder>().To<TagCloudBuilder>();
            kernel.Bind<IAlgorithm>().To<Algorithm>().WithConstructorArgument("config", options);
            kernel.Bind<IImageWriter>().To<ImageWriter>()
                .WithConstructorArgument("outputPath", options.OutputFile)
                .WithConstructorArgument("outputFormat", options.OutputFormat);

            var tagCloudBuilder = kernel.Get<ITagCloudBuilder>();
            var bitmap = tagCloudBuilder.Build();
            var imageWriter = kernel.Get<IImageWriter>();
            imageWriter.WriteImage(bitmap);
        }
    }
}
