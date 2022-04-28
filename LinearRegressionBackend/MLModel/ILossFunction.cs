using System;

namespace LinearRegressionBackend.MLModel
{
    /// <summary>
    /// Computes the quantity that a model should seek to minimize during training.
    /// </summary>
    public interface ILossFunction
    {
        /// <summary>
        /// Calculates the loss for the given input and target data.
        /// </summary>
        /// <param name="thetas">The parameters of a machine learning model.</param>
        /// <param name="inputData">The matrix representation of the input data.</param>
        /// <param name="targetData">The array representation of the target data.</param>
        /// <returns>Returns with the loss value.</returns>
        double Loss(double[] thetas, double[][] inputData, double[] targetData);

        /// <summary>
        /// Calculates the loss function derivates for the given input and target data based on <paramref name="thetas"/>.
        /// </summary>
        /// <param name="thetas">The parameters of a machine learning model.</param>
        /// <param name="inputData">The matrix representation of the input data.</param>
        /// <param name="targetData">The array representation of the target data.</param>
        /// <returns>Returns with the gradients on the given <paramref name="thetas"/>.</returns>
        double[] LossDerivates(double[] thetas, double[][] inputData, double[] targetData);
    }
}
