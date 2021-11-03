using System;
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
            this.data = new int[] { 99, 34, 634, 674, 34, 56, 23, 4 };
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
