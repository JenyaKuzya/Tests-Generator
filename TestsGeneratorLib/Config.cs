using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsGeneratorLib
{
    public class Config
    {
        public Config(int countOfReadThreads, int countOfProcessThreads, int countOfWriteThreads)
        {
            CountOfReadThreads = countOfReadThreads;
            CountOfProcessThreads = countOfProcessThreads;
            CountOfWriteThreads = countOfWriteThreads;
        }

        public int CountOfReadThreads { get; set; }
        public int CountOfProcessThreads { get; set; }
        public int CountOfWriteThreads { get; set; }
    }
}
