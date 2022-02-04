using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra.Double;

namespace LinearRegressionBackend.MLModel
{
    public class NormalEquation : IOptimizer
    {
        public double[] Minimize(ILossFunction lossFunction, double[] thetas, double[][] inputData, double[] targetData)
        {
            double[][] rowArray = new double[inputData.Length][];
            
            for (int i = 0; i < inputData.Length; i++)
            {
                rowArray[i] = new double[2];
                rowArray[i][0] = 1.0;
                rowArray[i][1] = inputData[i][0];
            }

            Vector<double> y = Vector.Build.DenseOfArray(targetData);

            Matrix<double> matrix = Matrix.Build.DenseOfRowArrays(rowArray);

            Matrix<double> transpose = matrix.Transpose();

            return transpose.Multiply(matrix).Inverse().Multiply(transpose).Multiply(y).Reverse().ToArray();
        }

    }
}
