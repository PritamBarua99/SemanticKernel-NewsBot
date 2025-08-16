using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SemanticKernel;

namespace ArchivePlugin
{
    public class ArchivePlugin
    {
        public async Task WriteData(Kernel kernel, string fileName, string data)
        {
            await File.WriteAllTextAsync
                ($@"C:\Users\prita\source\repos\SemanticKernel\{fileName}.txt", data);
        }
    }
}
