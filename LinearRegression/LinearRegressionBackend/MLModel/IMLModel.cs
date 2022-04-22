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
        /// Generates output predictions for the input data.
        /// </summary>
        /// <param name="inputData">The matrix representation of the input data.</param>
        /// <returns>Returns the prediction at a given point.</returns>
        double Predict(double[] inputData);

        /// <summary>
        /// Evaluates the model on the given data.
        /// </summary>
        /// <param name="inputData">The matrix representation of the input data.</param>
        /// <param name="targetData">The array representation of the target data.</param>
        /// <returns>Returns with the loss value on the given data.</returns>
        double Evaluation(double[][] inputData, double[] targetData);

        /// <summary>
        /// Trains the model on the given data.
        /// </summary>
        /// <param name="data">The matrix representation of the input data.</param>
        public List<History> Train(double[][] data, double[] targetData, int epochs = 1);
    }
}
