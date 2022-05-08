using System.Collections.Generic;

using MathNet.Numerics.LinearAlgebra;

namespace LinearRegressionBackend
{
    /// <summary>
    /// Provides an algorithm that can be uses to reason over and learn from a set of data.
    /// </summary>
    public interface IMLModel
    {
        // TODO: Remove this method.
        /// <summary>
        /// Generates output predictions for the input data.
        /// </summary>
        /// <param name="inputData">The matrix representation of the input data.</param>
        /// <returns>Returns the prediction at a given point.</returns>
        double Predict(double[] inputData);

        /// <summary>
        /// Generates output predictions for the input data.
        /// </summary>
        /// <param name="inputData">The vector representation of the input data.</param>
        /// <returns>Returns the prediction at a given point.</returns>
        double Predict(Vector<double> inputData);

        // TODO: Remove this method.
        /// <summary>
        /// Evaluates the model on the given data.
        /// </summary>
        /// <param name="inputData">The matrix representation of the input data.</param>
        /// <param name="targetData">The array representation of the target data.</param>
        /// <returns>Returns with the loss value on the given data.</returns>
        double Evaluation(double[][] inputData, double[] targetData);

        /// <summary>
        /// Evaluates the model on the given data.
        /// </summary>
        /// <param name="inputData">The matrix representation of the input data.</param>
        /// <param name="targetData">The vector representation of the target data.</param>
        /// <returns>Returns with the loss value on the given data.</returns>
        double Evaluation(Matrix<double> inputData, Vector<double> targetData);

        // TODO: Remove this method.
        /// <summary>
        /// Trains the model based on the <paramref name="inputData"/> and <paramref name="targetData"/>.
        /// </summary>
        /// <param name="inputData">The matrix representation of the input data.</param>
        /// <param name="targetData">The vector representation of the target data.</param>
        /// <param name="epochs">The number of iteration passed through the <paramref name="inputData"/>.</param>
        /// <returns>Returns with the training history.</returns>
        List<History> Train(double[][] inputData, double[] targetData, int epochs = 1);

        /// <summary>
        /// Trains the model based on the <paramref name="inputData"/> and <paramref name="targetData"/>.
        /// </summary>
        /// <param name="inputData">The matrix representation of the input data.</param>
        /// <param name="targetData">The vector representation of the target data.</param>
        /// <param name="epochs">The number of iteration passed through the <paramref name="inputData"/>.</param>
        /// <returns>Returns with the training history.</returns>
        List<History> Train(Matrix<double> inputData, Vector<double> targetData, int epochs = 1);
    }
}
