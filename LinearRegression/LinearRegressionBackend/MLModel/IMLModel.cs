using System;
using System.Collections.Generic;

namespace LinearRegressionBackend.MLModel
{
    /// <summary>
    /// Provides an algorithm that can be uses to reason over and learn from a set of data.
    /// </summary>
    public interface IMLModel
    {
        /// <summary>
        /// Trains the model for a fixed number of epochs.
        /// </summary>
        /// <param name="inputData">The matrix representation of the input data.</param>
        /// <param name="targetData">The array representation of the target data.</param>
        /// <param name="epochs">The number of iterations.</param>
        /// <returns>Returns with the training history.</returns>
        List<History> Fit(double[][] inputData, double[] targetData, int epochs = 1);

        /// <summary>
        /// Generates output predictions for the input data.
        /// </summary>
        /// <param name="inputData">The matrix representation of the input data.</param>
        /// <returns></returns>
        double Predict(double[] inputData);

        /// <summary>
        /// Evaluates the model on the given data.
        /// </summary>
        /// <param name="inputData">The matrix representation of the input data.</param>
        /// <param name="targetData">The array representation of the target data.</param>
        /// <returns>Returns with the loss value on the given data.</returns>
        double Evaluation(double[][] inputData, double[] targetData);

        void Save(string path);

        IMLModel Load(string path);
    }
}
