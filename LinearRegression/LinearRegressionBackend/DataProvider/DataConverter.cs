using MathNet.Numerics.LinearAlgebra;


namespace LinearRegressionBackend.DataProvider
{
   public class DataConverter
    {
        public Vector<double> getVector(double[,] imgPixelsMatrix)
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
            var V = Vector<double>.Build;

            var v = V.DenseOfArray(arrayOfPixels);

            return v;
        }
    }
}
