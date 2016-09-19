using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Outsurance.Contracts;

namespace Outsurance.Stubs
{
    public class ProcessorStub : IFileProcessor
    {
        public string Path { get; set; }
        public List<Person> Persons { get; set; }

        public ProcessorStub(string path)
        {
            Path = path;
            Persons = new List<Person>();
        }

        public List<Person> GetRecords()
        {
            return Persons;
        }

        public bool Write(string path, List<string> content)
        {
            return true;
        }

    }
}
