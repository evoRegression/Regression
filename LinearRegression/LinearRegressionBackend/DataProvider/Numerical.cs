using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

using MathNet.Numerics.LinearAlgebra;

namespace LinearRegressionBackend.DataProvider
{
    /// <summary>
    /// Helper class for calculating numerical operations on matrixes.
    /// </summary>
    public static class Numerical
    {
        /// <summary>
        /// Generates a vector from <paramref name="start"/> inclusively to <paramref name="stop"/> exclusively.
        /// </summary>
        /// <param name="start">Inclusive starting value.</param>
        /// <param name="stop">Exclusive stoping value.</param>
        /// <param name="step">Number of steps go from <paramref name="start"/> to <paramref name="stop"/>.</param>
        /// <returns>Returns with an array.</returns>
        public static double[] Arange(double start, double stop, double step)
        {
            decimal stepD = (decimal)step;
            decimal currentValue = (decimal)start;

            int arrayLength = (int)((stop - start) / step);
            double[] array = new double[arrayLength];

            // TODO: Replace with linq enumarate-range
            for (int j = 0; j < arrayLength; j++)
            {
                array[j] = (double)currentValue;
                currentValue += stepD;
            }

            return array;
        }

        /// <summary>
        /// Loads a correctly formated string file, into a two dimensional double matrix. The format must be "number,number", 
        /// every line can only contain 1 pair of values, the file must contain at least one full pair.
        /// </summary>
        /// <param name="filePath">The path to the formated file, from which we want to get the values</param>
        /// <param name="delimeter">The char symbol, we used to sepparate the number pairings during the formating process.</param>
        /// <returns>Returns with a two dimensional double matrix.</returns>
        public static double[][] LoadText(string filePath, char delimeter = ',')
        {
            string[] lines = File.ReadAllLines(filePath);
            List<double[]> dataList = new List<double[]>();
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                string[] numbers = line.Split(delimeter);

                double[] XandY = new double[2];
                for (int j = 0; j < numbers.Length; j++)
                {
                    if (double.TryParse(numbers[j], NumberStyles.Any, CultureInfo.InvariantCulture, out double nextData))
                        XandY[j] = nextData;
                    else
                        throw new InvalidDataException($"Incorrect Data in \"{filePath}\" file at line: {i + 1} : {line}");
                }

                dataList.Add(XandY);
            }
            return dataList.ToArray();
        }

        // TODO: Replace the old method with this.
        public static Matrix<double> LoadText1(string filePath, char delimeter = ',')
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Saves, and formats a two dimensional double matrix, into a txt file.
        /// </summary>
        /// <param name="filePath">The path to the txt file we want to save our matrix to.</param>
        /// <param name="matrix">The matrix we wish to save</param>
        /// <param name="delimeter">The symbol in the txt that will be used to separate the value pairs.</param>
        public static void SaveText(string filePath, double[][] matrix, char delimeter = ',')
        {
            string outputstring = matrix[0][0].ToString() + delimeter.ToString() + matrix[0][1].ToString() + Environment.NewLine;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                outputstring += matrix[i][0].ToString() + delimeter.ToString() + matrix[i][1].ToString() + Environment.NewLine;
            }

            File.WriteAllText(filePath, outputstring);
        }

        public static void SaveText(string filePath, Matrix<double> matrix, char delimeter = ',')
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Calculates the mean of all values on an axis.
        /// </summary>
        /// <param name="matrix">The matrix, containing the values.</param>
        /// <param name="axis">The axis whose mean we want to calculate. 0 for X, 1 for Y.</param>
        /// <returns>Returns the mean of all values, of the given axis.</returns>
        public static double Mean(double[][] matrix, int axis)
        {
            double result = 0;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                result += matrix[i][axis];
            }

            return result / matrix.GetLength(0);
        }

        public static double Mean(Matrix<double> matrix, int axis)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Calculates the Median of all values on an axis.
        /// </summary>
        /// <param name="matrix">The matrix, containing the values.</param>
        /// <param name="axis">The axis whose median we want to calculate. 0 for X, 1 for Y.</param>
        /// <returns>Returns the Median of all values, from the given axis.</returns>
        public static double Median(double[][] matrix, int axis)
        {
            double[] matrix_array = new double[matrix.GetLength(0)];
            int middle = matrix_array.GetLength(0) / 2;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                matrix_array[i] = matrix[i][axis];
            }

            Array.Sort(matrix_array);

            if (matrix_array.Length % 2 == 0)
            {
                return ((matrix_array[middle - 1] + matrix_array[middle]) / 2);
            }

            return (matrix_array[middle]);
        }

        public static double Median(Matrix<double> matrix, int axis)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Calculates the standard deviation of all values on an axis.
        /// </summary>
        /// <param name="matrix">The matrix, containing the values.</param>
        /// <param name="axis">The axis whose standard deviation we want to calculate. 0 for X, 1 for Y.</param>
        /// <returns>Returns the Standard deviation of all values, from the given axis.</returns>
        public static double StandardDeviation(double[][] matrix, int axis)
        {
            double mean = Mean(matrix, axis);
            double result = 0;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                result += Math.Pow(matrix[i][axis] - mean, 2);
            }

            result /= matrix.Length;

            return Math.Pow(result, 0.5);
        }

        public static double StandardDeviation(Matrix<double> matrix, int axis)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Calculates the variance of all values on an axis.
        /// </summary>
        /// <param name="matrix">The matrix, containing the values.</param>
        /// <param name="axis">The axis whose variance we want to calculate. 0 for X, 1 for Y.</param>
        /// <returns>Returns the variance of all values, from the given axis.</returns>
        public static double Variance(double[][] matrix, int axis)
        {
            return Max(matrix, axis) - Min(matrix, axis);
        }

        public static double Variance(Matrix<double> matrix, int axis)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Calculates the minimum value on an axis.
        /// </summary>
        /// <param name="matrix">The matrix, containing the values.</param>
        /// <param name="axis">The axis whose minumim we want to calculate. 0 for X, 1 for Y.</param>
        /// <returns>Returns the lowest value, from the given axis.</returns>
        public static double Min(double[][] matrix, int axis)
        {
            double result = matrix[0][axis];
            for (int i = 1; i < matrix.GetLength(0); i++)
            {
                if (matrix[i][axis] < result)
                {
                    result = matrix[i][axis];
                }
            }

            return result;
        }

        public static double Min(Matrix<double> matrix, int axis)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Calculates the maximum value on an axis.
        /// </summary>
        /// <param name="matrix">The matrix, containing the values.</param>
        /// <param name="axis">The axis whose maximum we want to calculate. 0 for X, 1 for Y.</param>
        /// <returns>Returns the largest value, from the given axis.</returns>
        public static double Max(double[][] matrix, int axis)
        {
            double result = matrix[0][axis];
            for (int i = 1; i < matrix.GetLength(0); i++)
            {
                if (matrix[i][axis] > result)
                {
                    result = matrix[i][axis];
                }
            }

            return result;
        }

        public static double Max(Matrix<double> matrix, int axis)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Computes the dot product of two vectors.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        /// <returns>Returns the dot product of the two vectors.</returns>
        public static double DotProduct(Vector<double> a, Vector<double> b)
        {
            return (a.ToRowMatrix() * b.ToColumnMatrix()).Row(0)[0];
        }
    }
}
