using System;
using System.Linq;

using MathNet.Numerics.LinearAlgebra;

namespace LinearRegressionBackend.MLModel
{
    public class SimpleOrdinaryLeastSquare : IEstimator
    {
        public double[] Minimize(double[][] inputData, double[] targetData)
        {
            double[] xAxis = inputData.Select(array => (double)array.GetValue(0)).ToArray();
            double[] yAxis = targetData;

            int N = yAxis.Length;
            double sumX = xAxis.Sum();
            double sumY = yAxis.Sum();
            double avgX = sumX / N;
            double avgY = sumY / N;

            double numenator = N * xAxis.Zip(yAxis, (x, y) => x * y).Sum() - sumX * sumY;
            double denominator = N * xAxis.Select(x => x * x).Sum() - sumX * sumX;

            double slope = numenator / denominator;
            double intercept = avgY - slope * avgX;

            return new double[] { slope, intercept };
        }

        // TODO: Replace the old implementation with this.
        public (Matrix<double>, double) Estimate(Matrix<double> inputData, Vector<double> outputData)
        {
            throw new NotImplementedException();
        }
    }
}
