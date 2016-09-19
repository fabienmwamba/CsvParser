using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using Outsurance.Contracts;

namespace Outsurance
{
    public class Processor : IFileProcessor
    {
        public string FileName { get; set; }
        public List<Person> Persons { get; set; }

        public Processor(string filename)
        {
            FileName = filename;
            Persons = new List<Person>();
        }

        public List<Person> GetRecords()
        {
            using (StreamReader sr = new StreamReader(FileName))
            {
                var reader = new CsvReader(sr);

                IEnumerable<Person> records = reader.GetRecords<Person>();
                foreach (var record in records)
                {
                    Persons.Add(record);
                }
            }
            return Persons;
        }

        public bool Write(string path, List<string> content)
        {
            try
            {
                File.WriteAllLines(path, content);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}