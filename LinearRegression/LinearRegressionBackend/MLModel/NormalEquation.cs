using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra.Double;

namespace LinearRegressionBackend.MLModel
{
    class NormalEquation : IOptimizer
    {
        public double[] Minimize(ILossFunction lossFunction, double[] thetas, double[][] inputData, double[] targetData)
        {
           
            Vector<double> y = Vector.Build.DenseOfArray(targetData);

            Matrix<double> matrix = Matrix.Build.Dense(targetData.Length, 2, 1);
            matrix.SetColumn(1, inputData.Select(array => (double)array.GetValue(0)).ToArray());

            Matrix<double> transpose = matrix.Transpose();

            return matrix.TransposeThisAndMultiply(matrix).Inverse().Multiply(transpose).Multiply(y).Reverse().ToArray();
        }

    }
}
