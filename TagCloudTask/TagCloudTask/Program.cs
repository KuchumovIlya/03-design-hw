﻿using System;
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
            kernel.Bind<IWordsFileReader>().To<TxtWordsReader>().WithConstructorArgument("path", options.InputFile);
            kernel.Bind<IWordNormalizer>().To<NullWordNormalizer>();
            kernel.Bind<IWordFilter>().To<NullWordFilter>();
            kernel.Bind<IWordsReader>().To<WordsReader>();

            kernel.Bind<IAlgorithm>().To<Algorithm>().WithConstructorArgument("config", options);

            var dict = new Dictionary<string, ImageFormat>()
            {
                {".png", ImageFormat.Png},
                {".jpeg", ImageFormat.Jpeg},
                {".bmp", ImageFormat.Bmp}
            };

            // этот способ сборки все равно кажется сложным
            var algorithm = kernel.Get<IAlgorithm>();
            var words = kernel.Get<WordsReader>().ReadWords();
            var bitmap = algorithm.BuildTagCloudBitmap(words);
            bitmap.Save(options.OutputFile + options.OutputFormat, dict[options.OutputFormat]);

            // мне кажется должно выглядеть как то так
            // ITagCloudBuilder tagCloudBuilder =  kernel.Get<ITagCloudBuilder>();
            // var bitmap = tagCloudBuilder.Build();
            // тогда всю цепочку преобразований можно вынести в TagCloudBuilder

            // строчку bitmap.Save(options.OutputFile + options.OutputFormat, dict[options.OutputFormat]);
            // я бы вынес в PictureBuilder с интерфейсом IOutputBuilder
            // а то вдргуг захотим в pdf выводить
            // то есть штука преобразующая bitmap в файл должна быть за интерфейсом 
        }
    }
}
