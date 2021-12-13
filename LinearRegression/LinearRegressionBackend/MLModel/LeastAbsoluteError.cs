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
            double[] yAxis = inputData.Select(array => (double)array.GetValue(1)).ToArray();
            return xAxis.Zip(yAxis, (x, y) => Math.Abs(y - (x * thetas[0] + thetas[1]) )).Sum() / xAxis.Length;
        }

        public double[] LossDerivates(double[] thetas, double[][] inputData, double[] targetData)
        {
            throw new NotImplementedException();
        }
    }
}
