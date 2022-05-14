using MathNet.Numerics.LinearAlgebra;

namespace LinearRegressionBackend
{
    /// <summary>
    /// Responsibles for reducing the losses by changing the parameters of the machine learning model.
    /// </summary>
    public interface IOptimizer
    {
        // TODO: Remove this method.
        /// <summary>
        /// Calculates a step of gradient.
        /// </summary>
        /// <param name="lossFunction">The loss function to calcualte.</param>
        /// <param name="thetas">The parameters of a machine learning model.</param>
        /// <param name="inputData">Matrix representation of the input data.</param>
        /// <param name="targetData">Array representation of the target data.</param>
        /// <returns>Returns with the step of gradients.</returns>
        double[] Minimize(ILossFunction lossFunction, double[] thetas, double[][] inputData, double[] targetData);

        /// <summary>
        /// Updates the parameters.
        /// </summary>
        /// <param name="weights">The parameters of the weight.</param>
        /// <param name="bias">The parameter of the bias.</param>
        /// <param name="dWeights">The gradient of the weights.</param>
        /// <param name="dBias">The gradient of the bias.</param>
        /// <returns>Returns with the updated parameters (weights, biases).</returns>
        (Matrix<double>, double) UpdateParameters(Matrix<double> weights, double bias, Matrix<double> dWeights, double dBias);
    }
}
