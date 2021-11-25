using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace DataProvider
{
    public class DataProvider : IDataProvider
    {
        private double[][] data;

        public double Mean()
        {
            if (data == null || data.Length == 0)
                throw new ArgumentNullException("Data array is empty");
            double[] yAxis = data.Select(array => (double)array.GetValue(1)).ToArray();
            return (double)yAxis.Sum() / yAxis.Length;
        }

        public double Median()
        {
            if (data == null || data.Length == 0)
                throw new Exception("Data array is empty");
            double[] yAxis = data.Select(array => (double)array.GetValue(1)).ToArray();
            Array.Sort(yAxis);
            int midIndex = yAxis.Length / 2;
            if (yAxis.Length % 2 == 0)
            {
                return (yAxis[midIndex] + yAxis[midIndex - 1]) / 2.0;
            }
            else
            {
                return yAxis[midIndex];
            }
        }

        public double[][] Import(string filePath)
        {
            string[] lines = System.IO.File.ReadAllLines(filePath);
            List<double[]> dataList = new List<double[]>();
            for (int i = 0; i < lines.Length; i++)
            {
                String line = lines[i];
                if (Check(line))
                {
                    String[] numbers = line.Split(",");
                    double[] XandY = new double[2];
                    for (int j = 0; j < numbers.Length; j++)
                    {
                        double nextData;
                        if (double.TryParse(numbers[j], NumberStyles.Any, CultureInfo.InvariantCulture, out nextData))
                        {
                            XandY[j] = nextData;
                        }
                        else
                        {
                            Console.WriteLine($"Incorrect Data in \"{ filePath}\" file at line: {i + 1} : {lines[i]}");
                        }
                    }
                    dataList.Add(XandY);
                }
                else
                {
                    Console.WriteLine($"Incorrect Data  in \"{ filePath}\" file at line: {i + 1} : {lines[i]}");
                }
            }
            data = dataList.ToArray();
            return data;
        }

        public double StandardDeviation()
        {
            return Math.Sqrt(Variance());
        }

        public double Variance()
        {
            if (data == null || data.Length == 0)
                throw new Exception("Data array is empty");
            double[] yAxis = data.Select(array => (double)array.GetValue(1)).ToArray();
            return yAxis.Select(num => (num - Mean()) * (num - Mean())).Sum() / data.Length;
        }
        private bool Check(string line)
        {
            Regex rg = new Regex(@"^-?[\d]+(\.?[\d]+)?,-?[\d]+(\.?[\d]+)?$");
            return rg.IsMatch(line);
        }
    }
}
