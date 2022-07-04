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
            string trainFolder = Path.Combine(resourcePath, "Train");
            string testFolder = Path.Combine(resourcePath, "Test");

            CreateTrainTestFolder(trainFolder, testFolder);

            List<string> listOfFiles = Directory.GetFiles(extractionPath).ToList();

            int numberOfSample = int.Parse(args[0]);
            for (int i = 0; i < numberOfSample; i++)
            {
                string randomFile = listOfFiles.PopRandom();
                FileInfo file = new FileInfo(randomFile);

                if(i <= numberOfSample * 0.8)
                {
                    File.Copy(file.FullName, Path.Combine(trainFolder, $"{i}_{file.Name}.png"));
                }
                else
                {
                    File.Copy(file.FullName, Path.Combine(testFolder, $"{i}_{file.Name}.png"));
                }
            }
        }

        private static void CreateTrainTestFolder(params string[] extractPaths)
        {
            foreach (string path in extractPaths)
            {
                if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                }

                Directory.CreateDirectory(path);
            }
        }
    }
}
