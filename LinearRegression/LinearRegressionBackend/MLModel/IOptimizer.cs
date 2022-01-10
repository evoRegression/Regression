using System;

namespace LinearRegressionBackend.MLModel
{
    /// <summary>
    /// Responsibles for reducing the losses by changing the parameters of the machine learning model.
    /// </summary>
    public interface IOptimizer
    {
        /// <summary>
        /// Calculates a step of gradient.
        /// </summary>
        /// <param name="lossFunction">The loss function to calcualte.</param>
        /// <param name="thetas">The parameters of a machine learning model.</param>
        /// <param name="inputData">Matrix representation of the input data.</param>
        /// <param name="targetData">Array representation of the target data.</param>
        /// <returns>Returns with the step of gradients.</returns>
        double[] Minimize(ILossFunction lossFunction, double[] thetas, double[][] inputData, double[] targetData);
    }
}
