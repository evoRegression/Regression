using System;
using System.Linq;

using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

using LinearRegressionBackend.MLCommmons;

namespace LinearRegressionBackend.MLModel
{
    public class LeastAbsoluteError : ILossFunction
    {
        public double Loss(double[] thetas, double[][] inputData, double[] targetData)
        {
            if (inputData.Length == 0)
            {
                return 0;
            }

            double[] xAxis = inputData.Select(array => (double)array.GetValue(0)).ToArray();
            return xAxis.Zip(targetData, (x, target) => Math.Abs(target - (x * thetas[MLCommons.SLOPE_INDEX] + thetas[MLCommons.INTERCEPT_INDEX]))).Sum() / xAxis.Length;
        }

        public double[] LossDerivates(double[] thetas, double[][] inputData, double[] targetData)
        {
            double[] xAxis = inputData.Select(array => (double)array.GetValue(0)).ToArray();
            Vector<double> signs = Vector.Build.DenseOfArray(xAxis.Zip(targetData, (x, target) => (double)Math.Sign(x * thetas[MLCommons.SLOPE_INDEX] + thetas[MLCommons.INTERCEPT_INDEX] - target)).ToArray());
            Matrix<double> matrix = Matrix.Build.Dense(targetData.Length, 2, 1.0);
            matrix.SetColumn(0, xAxis);
            int n = targetData.Length;
            foreach ((int index, Vector<double> row) in matrix.EnumerateRowsIndexed())
            {
                matrix.SetRow(index, row.Multiply(signs.At(index)).Divide(n));
            }
            return matrix.ColumnSums().ToArray();
        }

        // TODO: Replace the old implementation with this.
        public double Loss(Vector<double> actualValue, Vector<double> expectedValue)
        {
            throw new NotImplementedException();
        }
        // TODO: Replace the old implementation with this.
        public (Matrix<double>, double) LossDerivates(Vector<double> actualValue, Vector<double> expectedValue)
        {
            throw new NotImplementedException();
        }
    }
}
