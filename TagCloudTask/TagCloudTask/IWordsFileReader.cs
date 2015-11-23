using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloudTask
{
    public interface IWordsFileReader
    {
        string[] ReadWordsFromFile();
    }
}
