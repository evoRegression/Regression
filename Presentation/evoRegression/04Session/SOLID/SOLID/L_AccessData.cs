using System;
using System.Collections.Generic;
using System.Text;

namespace SOLID
{
    #region Before
    public class AccessDataFile : IFileReader, IFileWriter
    {
        public string FilePath { get; set; }

        public virtual void ReadFile()
        {
            // Read File logic  
            Console.WriteLine($"Base File {FilePath} has been read");
        }

        public virtual void WriteFile()
        {
            //Write File Logic  
            Console.WriteLine($"Base File {FilePath} has been written");
        }
    }

    public class AdminDataFileUser : IFileReader, IFileWriter
    {
        public string AdminFilePath { get; set; }

        public void ReadFile()
        {
            // Read File logic  
            Console.WriteLine($"File {AdminFilePath} has been read");
        }

        public void WriteFile()
        {
            //Write File Logic  
            Console.WriteLine($"File {AdminFilePath} has been written");
        }
    }

    public class RegularDataFileUser : IFileReader
    {
        public string RegularFilePath { get; set; }

        public void ReadFile()
        {
            // Read File logic  
            Console.WriteLine($"File {RegularFilePath} has been read");
        }
    }
    #endregion

    #region After
    public interface IFileReader
    {
        void ReadFile();
    }

    public interface IFileWriter
    {
        void WriteFile();
    }
    #endregion
}
