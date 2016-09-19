using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using Outsurance.Contracts;

namespace Outsurance
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileName = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + Path.DirectorySeparatorChar +
                          "data.csv";
            string name = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + Path.DirectorySeparatorChar +
                          "names.csv";
            string addresses = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + Path.DirectorySeparatorChar +
                          "addresses.csv";
            IFileProcessor fileProcessor = new Processor(fileName);
            ReadWriteFile rw = new ReadWriteFile(fileProcessor);
            rw.WriteNames(name);
            rw.WriteAddress(addresses);

        }
    }
}
