using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RandomShuffle
{
    internal class Program
    {
        internal static void Main(string[] args)
        {
            string resourcePath = Path.GetFullPath(@"..\..\..\..\Resources");

            string extractionPath = Path.Combine(resourcePath, "Output");
            string randomSampleFolder = Path.Combine(resourcePath, "Random");

            CreateRandomSampleFolder(randomSampleFolder);

            List<string> listOfFiles = Directory.GetFiles(extractionPath).ToList();

            int numberOfSample = int.Parse(args[0]);
            for (int i = 0; i < numberOfSample; i++)
            {
                string randomFile = listOfFiles.PopRandom();
                FileInfo file = new FileInfo(randomFile);

                File.Copy(file.FullName, Path.Combine(randomSampleFolder, $"{i}.png"));
            }
        }

        private static void CreateRandomSampleFolder(string extractPath)
        {
            if (Directory.Exists(extractPath))
            {
                Directory.Delete(extractPath, true);
            }

            Directory.CreateDirectory(extractPath);
        }
    }
}
