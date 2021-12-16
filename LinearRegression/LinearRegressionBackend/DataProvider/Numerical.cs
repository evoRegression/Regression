using System;

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
            double[] array = new double [(int)((stop-start)/step)];
            for (double i=start;i<stop;i+=step)
            {
                for (int j = 0;; j++)
                {
                    array[j] = i;
                }
            }
            return array;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="delimeter"></param>
        /// <returns></returns>
        public static double[][] LoadText(string filePath, char delimeter = ',')
        {
            double[][] matrix = null;

            return matrix;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="matrix"></param>
        /// <param name="delimeter"></param>
        public static void SaveText(string filePath, double[][] matrix, char delimeter = ',')
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="axis"></param>
        public static double Mean(double[][] matrix, int axis)
        {
            int[] matrix_array;
            if (axis == 0)
            {
                for (int i = 0; i < matrix.GetLength(0) / 2; i++)
                {

                }
            }
            return 0;
            /*
            int middle = matrix.GetLength(0)/2;
            if (middle % 2 == 0)
            {
                if (axis == 0)
                {
                    return ( (matrix[middle][0]+ matrix[middle+1][0]) / 2 );
                }
                else
                {
                    return ((matrix[middle][0] + matrix[middle + 1][0]) / 2);
                }
            }
            else
            {
                
                if (axis == 0)
                {
                    return matrix[middle][0];
                }
                else
                {
                    return matrix[middle][1];
                }
            }*/
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="axis"></param>
        public static double Median(double[][] matrix, int axis)
        {
            double result = 0;

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="axis"></param>
        public static double StandardDeviation(double[][] matrix, int axis)
        {
            double result = 0;

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="axis"></param>
        public static double Variance(double[][] matrix, int axis)
        {
            double result = 0;

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="axis"></param>
        public static double Min(double[][] matrix, int axis)
        {
            double result = 0;

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="axis"></param>
        public static double Max(double[][] matrix, int axis)
        {
            double result = 0;

            return result;
        }
        public static double[] SortArray(double[] array)
        {
            return SortArray(array);
        }
    }
}
