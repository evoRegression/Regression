using System;
using System.Linq;

using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace LinearRegressionBackend.MLModel
{
    public class NormalEquation : IEstimator
    {
        public double[] Minimize(double[][] inputData, double[] targetData)
        {
            Vector<double> y = Vector.Build.DenseOfArray(targetData);

            Matrix<double> matrix = Matrix.Build.Dense(targetData.Length, 2, 1);
            matrix.SetColumn(1, inputData.Select(array => (double)array.GetValue(0)).ToArray());

            Matrix<double> transpose = matrix.Transpose();

            return matrix.TransposeThisAndMultiply(matrix).Inverse().Multiply(transpose).Multiply(y).Reverse().ToArray();
        }

        // TODO: Replace the old implementation with this.
        public (Matrix<double>, double) Estimate(Matrix<double> inputData, Vector<double> outputData)
        {
            throw new NotImplementedException();
        }
    }
}
