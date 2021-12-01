using LinearRegressionBackend.DataProvider.Exceptions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace DataProvider
{
    public class DataProvider : IDataProvider
    {
        
        private double[][] _data;

        public DataProvider() { }

        public DataProvider(String path)
        {
            Import(path);
        }
        
        private double Mean(double[] data)
        {
            if (data == null || data.Length == 0)
                throw new ArgumentNullException("Data array is empty");
            return (double)data.Sum() / data.Length;
        }

        public double MeanXAxis()
        {
            if (_data == null || _data.Length < 2)
                return 0;
            return Mean(_data.Select(array => (double)array.GetValue(0)).ToArray());
        }

        public double MeanYAxis()
        {
            if (_data == null || _data.Length < 2)
                return 0;
            return Mean(_data.Select(array => (double)array.GetValue(1)).ToArray());
        }

        private double Median(double[] data)
        {
            if (data == null || data.Length == 0)
                throw new Exception("Data array is empty");
            double[] sortedData = (double[])data.Clone();
            Array.Sort(sortedData);
            int midIndex = sortedData.Length / 2;
            if (sortedData.Length % 2 == 0)
            {
                return (sortedData[midIndex] + sortedData[midIndex - 1]) / 2.0;
            }
            else
            {
                return sortedData[midIndex];
            }
        }

        public double MedianXAxis()
        {
            if (_data == null || _data.Length < 2)
                return 0;
            return Median(_data.Select(array => (double)array.GetValue(0)).ToArray());
        }

        public double MedianYAxis()
        {
            if (_data == null || _data.Length < 2)
                return 0;
            return Median(_data.Select(array => (double)array.GetValue(1)).ToArray());
        }

        public double[][] Import(string filePath)
        {
            string[] lines = System.IO.File.ReadAllLines(filePath);
            List<double[]> dataList = new List<double[]>();
            for (int i = 0; i < lines.Length; i++)
            {
                String line = lines[i];
                String[] numbers = line.Split(",");
                if (numbers.Length != 2)
                {
                    throw new InvalidDataException($"Incorrect Data in \"{ filePath}\" file at line: {i + 1} : {line}");
                }
                double[] XandY = new double[2];
                for (int j = 0; j < numbers.Length; j++)
                {
                    double nextData;
                    if (double.TryParse(numbers[j], NumberStyles.Any, CultureInfo.InvariantCulture, out nextData))
                        XandY[j] = nextData;
                    else
                        throw new InvalidDataException($"Incorrect Data in \"{ filePath}\" file at line: {i + 1} : {line}");
                }
                dataList.Add(XandY);
            }
            _data = dataList.ToArray();
            return _data;
        }

        public double StandardDeviation()
        {
            return Math.Sqrt(Variance());
        }

        public double Variance()
        {
            if (_data == null || _data.Length == 0)
                throw new Exception("Data array is empty");
            double[] yAxis = _data.Select(array => (double)array.GetValue(1)).ToArray();
            return yAxis.Select(num => (num - MeanXAxis()) * (num - MeanXAxis())).Sum() / _data.Length;
        }
      
    }
}
