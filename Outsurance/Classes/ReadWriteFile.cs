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
    public class ReadWriteFile
    {
        public IFileProcessor Processor { get; set; }
        public List<string> Names { get; set; }
        public List<string> Addresses { get; set; }

        public ReadWriteFile(IFileProcessor processor)
        {
            Processor = processor;
            Names = new List<string>();
            Addresses = new List<string>();
        }

        public bool IsValidFileName(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentException("File is required");
            }
            if (!fileName.EndsWith("csv", StringComparison.CurrentCultureIgnoreCase))
            {
                return false;
            }
            return true;
        }

        public bool WriteNames(string fileName)
        {
            if (!IsValidFileName(fileName))
            {
                throw new ArgumentException("Please provide a valid file");
            }
            var items = GetNames();
            if (!Processor.Write(fileName,items))
            {
                return false;
            }
            return true;
        }

        public bool WriteAddress(string fileName)
        {
            if (!IsValidFileName(fileName))
            {
                throw new ArgumentException("Please provide a valid file");
            }
            var items = GetAddress();
            if (!Processor.Write(fileName, items))
            {
                return false;
            }
            return true;
        }

        public List<string> GetAddress()
        {
            List<Person> records = Processor.GetRecords();

            foreach (var record in records)
            {
                Addresses.Add(record.Address);
            }
            return Addresses;
        }

        public List<string> GetNames()
        {
            List<Person> records = Processor.GetRecords();

            foreach (var record in records)
            {
                Names.Add(record.FirstName);
                Names.Add(record.LastName);
            }
            var items = OrderNames(Names);
            return items;
        }

        public List<string> OrderNames(List<string> names )
        {
            List<string> items = new List<string>();

            var query = names.GroupBy(x => x)
                .Select(g => new {Value = g.Key, Count = g.Count()})
                .OrderByDescending(x => x.Count).ToList();
            
            foreach (var item in query)
            {
                string line = item.Value + ", " + item.Count;
                items.Add(line);
            }
            return items;
        }
    }
}
