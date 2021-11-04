using System;
using System.Collections.Generic;
using System.Text;

namespace SOLID
{
    #region Before
    public class AccessDataFile
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

    public class AdminDataFileUser : AccessDataFile
    {
        public string AdminFilePath { get; set; }

        public override void ReadFile()
        {
            // Read File logic  
            Console.WriteLine($"File {AdminFilePath} has been read");
        }

        public override void WriteFile()
        {
            //Write File Logic  
            Console.WriteLine($"File {AdminFilePath} has been written");
        }
    }

    public class RegularDataFileUser : AccessDataFile
    {
        public string RegularFilePath { get; set; }

        public override void ReadFile()
        {
            // Read File logic  
            Console.WriteLine($"File {RegularFilePath} has been read");
        }

        public override void WriteFile()
        {
            //Write File Logic  
            throw new NotImplementedException();
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
