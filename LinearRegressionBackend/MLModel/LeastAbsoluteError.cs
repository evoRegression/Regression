using LinearRegressionBackend.MLCommmons;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearRegressionBackend.MLModel
{
    public class LeastAbsoluteError : ILossFunction
    {
        public double Loss(double[] thetas, double[][] inputData, double[] targetData)
        {
            if (inputData.Length == 0)
                return 0;

            double[] xAxis = inputData.Select(array => (double)array.GetValue(0)).ToArray();
            return xAxis.Zip(targetData, (x, target) => Math.Abs(target - (x * thetas[MLCommons.SLOPE_INDEX] + thetas[MLCommons.INTERCEPT_INDEX]) )).Sum() / xAxis.Length;
        }

        //Based on this article: https://github.com/chenxingwei/machine_learning_from_scratch/blob/master/algorithm/2.linearRegressionGradientDescent.md
        public double[] LossDerivates(double[] thetas, double[][] inputData, double[] targetData)
        {
            double[] xAxis = inputData.Select(array => (double)array.GetValue(0)).ToArray();
            Vector<double> signs = Vector.Build.DenseOfArray(xAxis.Zip(targetData, (x, target) => (double)Math.Sign((x * thetas[MLCommons.SLOPE_INDEX] + thetas[MLCommons.INTERCEPT_INDEX])-target)).ToArray());
            Matrix<double> matrix = Matrix.Build.Dense(targetData.Length, 2, 1.0);
            matrix.SetColumn(0, xAxis);
            int n = targetData.Length;
            foreach (var (index, row) in matrix.EnumerateRowsIndexed())
            {
                matrix.SetRow(index, row.Multiply(signs.At(index)).Divide(n));
            }
            return matrix.ColumnSums().ToArray();
        }
    }
}
