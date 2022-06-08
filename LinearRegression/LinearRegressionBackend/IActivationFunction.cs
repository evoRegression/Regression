using MathNet.Numerics.LinearAlgebra;

namespace LinearRegressionBackend
{
    /// <summary>
    /// Activation function to transform the neuron weighted sum.
    /// </summary>
    public interface IActivationFunction
    {
        /// <summary>
        /// Calculates the activation of a neuron.
        /// </summary>
        /// <param name="weightedSum">The weighted sum of the neuron inputs.</param>
        /// <returns>Returns a scalar value. The output of the current neuron.</returns>
        double Activation(double weightedSum);

        /// <summary>
        /// Calculates the activation of a layer.
        /// </summary>
        /// <param name="weightedSum">The weighted sum of the layer inputs.</param>
        /// <returns>Returns a vector. The output of the current layer.</returns>
        Vector<double> Activation(Vector<double> weightedSum)
        {
            return Vector<double>.Build.Dense(
                weightedSum.Count,
                i => Activation(weightedSum[i]));
        }

        /// <summary>
        /// Calculates the derivative of the activation function.
        /// </summary>
        /// <param name="weightedSum">The weighted sum of the neuron inputs.</param>
        /// <returns>Returns a scalar value. The derivative of the activation function.</returns>
        double Derivative(double weightedSum);

        /// <summary>
        /// Calculates the derivative of the activation function.
        /// </summary>
        /// <param name="weightedSum">The weighted sum of the layer inputs.</param>
        /// <returns>Returns a vector. The derivative of the activation function.</returns>
        Vector<double> Derivative(Vector<double> weightedSum)
        {
            return Vector<double>.Build.Dense(
                weightedSum.Count,
                i => Derivative(weightedSum[i]));
        }
    }
}
