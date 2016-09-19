using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Outsurance;
using Outsurance.Contracts;
using Outsurance.Stubs;

namespace OutsuranceTest
{
    [TestFixture]
    public class ReadWriteFileTest
    {
        [TestCase("myfile.csv", true)]
        [TestCase("myfile.foo", false)]
        public void IsValidFileName_ValidExtensions_ReturnsTrue(string file, bool expected)
        {
            //Arrange
            ReadWriteFile readWriteFile = MakeParser();

            //Act
            bool result = readWriteFile.IsValidFileName(file);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void IsValidFileName_EmptyFile_Throws()
        {
            ReadWriteFile readWriteFile = MakeParser();

            var ex = Assert.Catch<Exception>(() => readWriteFile.IsValidFileName(""));

            StringAssert.Contains("File is required", ex.Message);
        }

        [Test]
        public void GetRecords_WhenCalled_Returns_ListOfPerson()
        {
            //Arrange
            string path = "mydata.csv";
            IFileProcessor fp = new ProcessorStub(path); 
      
            //Act
            List<Person> items = fp.GetRecords();
            List<Person> expected = new List<Person>();

            //Assert
            Assert.AreEqual(expected, items);
        }

        [Test]
        public void GetNames_WhenCalled_Returns_ListOfStringNames()
        {
            //Arrange
            List<string> expected = new List<string>();
            ReadWriteFile readWriteFile = MakeParser();
            
            //Act
            List<string> actual = readWriteFile.GetNames();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void OrderNames_WhenCalled_ReturnsListOfitemsOrderByCountDescendingAndAlphabeticallyAscending()
        {
            //Arrange
            string path = "mypath.csv";
            List<string> expected = new List<string>()
            {
                "Smith, 2", "Clive, 2", "Owen, 2", "James, 2", "Brown, 2", "Graham, 2", "Howe, 2", "Jimmy, 1", "John, 1"
            };
            List<string> items = new List<string>()
            {
                "Jimmy", "Smith", "Clive", "Owen", "James", "Brown", "Graham", "Howe", "John", "Howe", "Clive", "Smith", "James", "Owen", "Graham", "Brown"
            };
            ReadWriteFile readWriteFile = MakeParser();
           
            //Act
            List<string> actual = readWriteFile.OrderNames(items);
            
            //Assert
            Assert.AreEqual(expected, actual);

        }

        private ReadWriteFile MakeParser()
        {
            string path = "mypath.csv";
            IFileProcessor fileProcessor = new ProcessorStub(path);
            ReadWriteFile readWriteFile = new ReadWriteFile(fileProcessor);
            return readWriteFile;
        }
    }
}
