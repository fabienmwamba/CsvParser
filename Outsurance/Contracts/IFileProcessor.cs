using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outsurance.Contracts
{
    public interface IFileProcessor
    {
        List<Person> GetRecords();

        bool Write(string path, List<string> content);
    }
}
