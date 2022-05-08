namespace LinearRegressionBackend
{
    /// <summary>
    /// Activation function to transform the neuron weighted sum.
    /// </summary>
    public interface IActivationFunction
    {
        /// <summary>
        /// Activation to get the output of the current neuron.
        /// </summary>
        /// <param name="weightedSum">The weighted of the neuron inputs.</param>
        /// <returns>Returns with a scalar value. The output of the current neuron.</returns>
        double Activation(double weightedSum);
    }
}
