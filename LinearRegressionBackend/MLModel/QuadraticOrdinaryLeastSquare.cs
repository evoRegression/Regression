using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearRegressionBackend.MLModel
{
    public class QuadraticOrdinaryLeastSquare : IOptimizer
    {
        public double[] Minimize(ILossFunction lossFunction, double[] thetas, double[][] inputData, double[] targetData)
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
    }
}
