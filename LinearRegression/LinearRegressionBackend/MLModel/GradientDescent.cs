using LinearRegressionBackend.MLCommmons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearRegressionBackend.MLModel
{
    public class GradientDescent : IOptimizer
    {
        internal double _learningRate { get; set; }
        public GradientDescent(double learningRate)
        {
            _learningRate = learningRate;
        }
        public double[] Minimize(ILossFunction lossFunction, double[] thetas, double[][] inputData, double[] targetData)
        {
            double[] derivateThetas = lossFunction.LossDerivates(thetas, inputData, targetData);
            return new double[] { 
                 - _learningRate * derivateThetas[MLCommons.SLOPE_INDEX], 
                 - _learningRate * derivateThetas[MLCommons.INTERCEPT_INDEX] };
        }
    }
}
