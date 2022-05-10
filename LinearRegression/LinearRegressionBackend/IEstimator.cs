using MathNet.Numerics.LinearAlgebra;

namespace LinearRegressionBackend
{
    /// <summary>
    /// Responsibles for estimating the parameters from the input and output data.
    /// </summary>
    public interface IEstimator
    {
        /// <summary>
        /// Estimates the unknow parameters in regression model.
        /// </summary>
        /// <param name="inputData">The input (independent) variables.</param>
        /// <param name="outputData">The output (dependent) variables.</param>
        /// <returns>Returns with the estimated parameters.</returns>
        (Matrix<double>, double) Estimate(Matrix<double> inputData, Vector<double> outputData);
    }
}
