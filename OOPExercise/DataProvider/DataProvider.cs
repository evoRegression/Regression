using System;
using System.Collections.Generic;
using System.Linq;

namespace DataProvider
{
    public class DataProvider : IDataProvider
    {
        private int[] data;
        public double Mean()
        {
            if (this.data == null || this.data.Length == 0)
                throw new Exception("Data array is empty");

            return (double)this.data.Sum() / this.data.Length;
        }

        public double Median()
        {
            if (this.data == null || this.data.Length == 0)
                throw new Exception("Data array is empty");
            int[] sortedData = (int[])this.data.Clone();
            int midIndex = sortedData.Length / 2 + 1;
            if( sortedData.Length % 2 == 0)
            {
                return (sortedData[midIndex] + sortedData[midIndex - 1]) / 2.0;
            }
            else
            {
                return sortedData[midIndex];
            }
        }

        public int[] Read(string filePath)
        {
            string[] lines = System.IO.File.ReadAllLines(filePath);
            List<int> dataList = new List<int>();
            for (int i = 0; i < lines.Length; i++)
            {
                try
                {
                    dataList.Add(int.Parse(lines[i]));
                }
                catch (FormatException)
                {
                    throw new Exception($"Incorrect Data in \"{ filePath}\" file at line: {i+1} : {lines[i]}");
                }
            }
            this.data = dataList.ToArray();
            return data;
        }

        public double StandardDeviation()
        {
            return Math.Sqrt(this.Variance());
        }

        public double Variance()
        {
            if (this.data == null || this.data.Length == 0)
                throw new Exception("Data array is empty");
            double mean = this.Mean();
            double sum = 0;
            foreach (int i in this.data)
            {
                sum += Math.Pow(i - mean, 2.0);
            }
            return sum / this.data.Length;
        }
    }
}
