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

            kernel.Bind<IWordsReader>().To<TxtWordsReader>().WithConstructorArgument("path", options.GetInputFile());
            kernel.Bind<IWordNormalizer>().To<NullWordNormalizer>();
            kernel.Bind<IWordFilter>().To<NullWordFilter>();
            kernel.Bind<ITagCloudBuilder>().To<TagCloudBuilder>();
            kernel.Bind<IAlgorithm>().To<Algorithm>().WithConstructorArgument("config", options);
            kernel.Bind<IBitmapWriter>().To<ImageBitmapWriter>()
                .WithConstructorArgument("outputPath", options.GetOutputFile())
                .WithConstructorArgument("outputFormat", options.GetOutputFormat());

            var tagCloudBuilder = kernel.Get<ITagCloudBuilder>();
            var bitmap = tagCloudBuilder.Build();
            var writer = kernel.Get<IBitmapWriter>();
            writer.WriteBitmap(bitmap);
        }
    }
}
