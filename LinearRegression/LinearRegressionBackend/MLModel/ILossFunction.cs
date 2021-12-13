using System;

namespace LinearRegressionBackend.MLModel
{
    /// <summary>
    /// Computes the quantity that a model should seek to minimize during training.
    /// </summary>
    interface ILossFunction
    {
        /// <summary>
        /// Calculates the loss for the given input and target data.
        /// </summary>
        /// <param name="thetas">The coefficients for the given model.</param>
        /// <param name="inputData">Matrix representation of the input data.</param>
        /// <param name="targetData">Array representation of the target data.</param>
        /// <returns>Returns with loss value.</returns>
        double Loss(double[] thetas, double[][] inputData, double[] targetData);

        /// <summary>
        /// Calculates the loss function derivates for the given input and target data based on <paramref name="thetas"/>.
        /// </summary>
        /// <param name="thetas">The coefficients for the given model.</param>
        /// <param name="inputData">Matrix representation of the input data.</param>
        /// <param name="targetData">Array representation of the target data.</param>
        /// <returns>Returns an array of gradient step for <paramref name="thetas"/>.</returns>
        double[] LossDerivates(double[] thetas, double[][] inputData, double[] targetData);
    }
}
