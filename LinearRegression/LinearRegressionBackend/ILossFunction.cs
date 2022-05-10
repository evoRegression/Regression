using MathNet.Numerics.LinearAlgebra;

namespace LinearRegressionBackend
{
    /// <summary>
    /// Computes the quantity that a model should seek to minimize during training.
    /// </summary>
    public interface ILossFunction
    {
        // TODO: Remove this method.
        /// <summary>
        /// Calculates the loss for the given input and target data.
        /// </summary>
        /// <param name="thetas">The parameters of a machine learning model.</param>
        /// <param name="inputData">The matrix representation of the input data.</param>
        /// <param name="targetData">The array representation of the target data.</param>
        /// <returns>Returns with the loss value.</returns>
        double Loss(double[] thetas, double[][] inputData, double[] targetData);

        /// <summary>
        /// Calculates the loss for the actual and expected value.
        /// </summary>
        /// <param name="actualValue">The vector representation of the actual value.</param>
        /// <param name="expectedValue">The vector representation of the expected value.</param>
        /// <returns>Returns with the loss value.</returns>
        double Loss(Vector<double> actualValue, Vector<double> expectedValue);

        // TODO: Remove this method.
        /// <summary>
        /// Calculates the loss function derivates for the given input and target data based on <paramref name="thetas"/>.
        /// </summary>
        /// <param name="thetas">The parameters of a machine learning model.</param>
        /// <param name="inputData">The matrix representation of the input data.</param>
        /// <param name="targetData">The array representation of the target data.</param>
        /// <returns>Returns with the gradients on the given <paramref name="thetas"/>.</returns>
        double[] LossDerivates(double[] thetas, double[][] inputData, double[] targetData);

        /// <summary>
        /// Calculates the loss function derivates based on the <paramref name="actualValue"/> and the <paramref name="expectedValue"/>.
        /// </summary>
        /// <param name="actualValue">The vector representation of the actual value.</param>
        /// <param name="expectedValue">The vector representation of the expected value.</param>
        /// <returns></returns>
        (Matrix<double>, double) LossDerivates(Vector<double> actualValue, Vector<double> expectedValue);
    }
}
