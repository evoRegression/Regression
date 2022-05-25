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
        /// Calculates the derivative of the activation function.
        /// </summary>
        /// <param name="weightedSum">The weighted sum of the neuron inputs.</param>
        /// <returns>Returns a scalar value. The derivative of the activation function.</returns>
        double Derivative(double weightedSum);
    }
}
