using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace TagCloudTask
{
    public class MystemToJsonParser : IMorphologicalParser
    {
        private readonly Process process;

        public MystemToJsonParser(ICommandLineOptions options)
        {
            var processStartInfo = new ProcessStartInfo
            {
                Arguments = "-nig --format json -e utf-8",
                RedirectStandardOutput = true,
                RedirectStandardInput = true,
                UseShellExecute = false,
                FileName = options.GetMorphologicalApplicationPath()
            };
            process = new Process {StartInfo = processStartInfo};
            process.Start();
        }

        public string GetFormatedMorphologicalDataInJson(string word)
        {
            WriteText(word);
            return ReadText();
        }

        private void WriteText(string text)
        {
            var b = Encoding.UTF8.GetBytes(text + "\n");
            process.StandardInput.BaseStream.Write(b, 0, b.Length);
            process.StandardInput.Flush();
        }

        private string ReadText()
        {
            var bytes = new List<byte>();
            while (true)
            {
                var bb = process.StandardOutput.BaseStream.ReadByte();
                bytes.Add((byte)bb);
                if (bb == 10)
                    break;
            }
            return Encoding.UTF8.GetString(bytes.ToArray());
        }
    }
}
