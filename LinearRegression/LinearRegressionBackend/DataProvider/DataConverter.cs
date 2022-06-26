using System;
using System.Drawing;
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

        public static Tuple<Matrix<double>, Matrix<double>> ProcessInputImages(DirectoryInfo directory, IImageConverter converter)
        {
            Vector<double> pixelVector;
            Vector<double> labelVector;
            Matrix<double> matrixOfPixels = null;
            Matrix<double> matrixOfLabels = null;
       
            int i = 0;
            foreach (var file in directory.GetFiles("*.png")) 
            {
                using (var image = Image.FromFile(file.FullName))
                {
                    using (var newImage = converter.Scale((Bitmap)image, 4, 4)) //scale 4*4 
                    {
                        try
                        {
                            pixelVector = converter.GrayScale(newImage);
                            labelVector = converter.CreateLabel(file.Name);

                            // Matrix of pixel vectors 
                            matrixOfPixels.SetRow(i, pixelVector);
                            // Matrix of label vectors 
                            matrixOfLabels.SetRow(i, labelVector);

                            i++;
                        }
                        catch
                        {
                            continue;
                        }
                    }

                }
            }
            return Tuple.Create(matrixOfPixels, matrixOfLabels);
        }
    }
}
