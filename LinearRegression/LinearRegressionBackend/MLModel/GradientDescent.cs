using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearRegressionBackend.MLModel
{
    public class GradientDescent : IOptimizer
    {
        public double[] Minimize(ILossFunction lossFunction, double[] thetas, double[][] inputData, double[] targetData)
        {
            double[] derivateThetas = lossFunction.LossDerivates(thetas, inputData, targetData);
            double learningRate = 0.01;
            return new double[] { 
                 - learningRate * derivateThetas[0], 
                 - learningRate * derivateThetas[1] };
        }
    }
}
