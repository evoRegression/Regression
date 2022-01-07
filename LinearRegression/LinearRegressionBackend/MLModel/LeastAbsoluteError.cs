using LinearRegressionBackend.MLCommmons;
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

        public double[] LossDerivates(double[] thetas, double[][] inputData, double[] targetData)
        {
            throw new NotImplementedException();
        }
    }
}
