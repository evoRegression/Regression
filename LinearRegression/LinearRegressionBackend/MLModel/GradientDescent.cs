using MathNet.Numerics.LinearAlgebra;

using LinearRegressionBackend.MLCommmons;

namespace LinearRegressionBackend.MLModel
{
    public class GradientDescent : IOptimizer, IIterable
    {
        public double LearningRate { get; set; }

        public GradientDescent(double learningRate)
        {
            LearningRate = learningRate;
        }

        public double[] Minimize(ILossFunction lossFunction, double[] thetas, double[][] inputData, double[] targetData)
        {
            double[] derivateThetas = lossFunction.LossDerivates(thetas, inputData, targetData);
            return new double[] {
                 - LearningRate * derivateThetas[MLCommons.SLOPE_INDEX],
                 - LearningRate * derivateThetas[MLCommons.INTERCEPT_INDEX] };
        }

        // TODO: Replace the old Minimize method with this Minimize method.
        public (Matrix<double>, double) UpdateParameters(Matrix<double> weights, double bias, Matrix<double> dWeights, double dBias)
        {
            Matrix<double> updatedWeights = weights - LearningRate * dWeights;
            double updatedBias = bias - LearningRate * dBias;

            return (updatedWeights, updatedBias);
        }
    }
}
