using LinearRegressionBackend.MLCommmons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearRegressionBackend.MLModel
{
    public class GradientDescent : IOptimizer, IIterable
    {
        public double learningRate { get ; set; }

        public GradientDescent(double learningRate)
        {
            this.learningRate = learningRate;
        }

        public GradientDescent()
        {
            learningRate = 1;
        }

        public double[] Minimize(ILossFunction lossFunction, double[] thetas, double[][] inputData, double[] targetData)
        {
            double[] derivateThetas = lossFunction.LossDerivates(thetas, inputData, targetData);
            return new double[] { 
                 - learningRate * derivateThetas[MLCommons.SLOPE_INDEX], 
                 - learningRate * derivateThetas[MLCommons.INTERCEPT_INDEX] };
        }
    }
}
