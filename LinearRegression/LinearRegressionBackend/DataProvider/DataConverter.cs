using System;
using System.IO;

using MathNet.Numerics.LinearAlgebra;

namespace LinearRegressionBackend.DataProvider
{
    public static class DataConverter
    {
        public static Vector<double> GetVector(double[,] imgPixelsMatrix)
        {
            int index = 0;
            double[] arrayOfPixels = new double[imgPixelsMatrix.Length];
            for (int i = 0; i < imgPixelsMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < imgPixelsMatrix.GetLength(1); j++)
                {
                    arrayOfPixels[index] = imgPixelsMatrix[i, j];
                    index++;
                }
            }

            Vector<double> pixelVector = Vector<double>.Build.DenseOfArray(arrayOfPixels);

            return pixelVector;
        }

        public static Matrix<double> ProcessInputImages(DirectoryInfo directory, IImageConverter converter)
        {
            throw new NotImplementedException();
        }
    }
}
