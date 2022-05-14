using System;
using System.Linq;

using MathNet.Numerics.LinearAlgebra;

namespace LinearRegressionBackend.MLModel
{
    public class QuadraticOrdinaryLeastSquare : IEstimator
    {
        public double[] Minimize(double[][] inputData, double[] targetData)
        {
            double[] xAxis = inputData.Select(array => (double)array.GetValue(0)).ToArray();
            double[] yAxis = targetData;

            int N = yAxis.Length;
            double avgX = xAxis.Average();
            double avgY = yAxis.Average();

            double numenator = xAxis.Zip(yAxis, (x, y) => (x - avgX) * (y - avgY)).Sum();
            double denominator = xAxis.Select(x => (x - avgX) * (x - avgX)).Sum();

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
