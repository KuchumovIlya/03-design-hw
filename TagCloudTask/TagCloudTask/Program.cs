using Ninject;

namespace TagCloudTask
{
    class Program
    {
        static void Main(string[] args)
        {
            var kernel = new StandardKernel();
            kernel.Bind<ICommandLineOptions>().To<CommandLineOptions>();

            var options = kernel.Get<ICommandLineOptions>();
            if (!CommandLine.Parser.Default.ParseArguments(args, options))
                return;

            kernel.Bind<IMorphologicalParser>().To<MystemToJsonParser>().WithConstructorArgument("options", options);
            kernel.Bind<IWordsReader>().To<TxtWordsReader>().WithConstructorArgument("path", options.GetInputFile());
            kernel.Bind<IWordNormalizer>().To<MorphologicalWordNormalizer>();
            kernel.Bind<IWordFilter>().To<MorphologicalWordFilter>();
            kernel.Bind<ITagCloudBuilder>().To<TagCloudBuilder>();
            kernel.Bind<IAlgorithm>().To<Algorithm>().WithConstructorArgument("options", options);
            kernel.Bind<IBitmapWriter>().To<ImageBitmapWriter>()
                .WithConstructorArgument("outputPath", options.GetOutputFile())
                .WithConstructorArgument("outputFormat", options.GetOutputFormat());
            kernel.Bind<IClient>().To<ConsoleClient>();

            kernel.Get<IClient>().Run();
        }
    }
}
