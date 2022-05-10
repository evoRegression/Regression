using System;
using System.Collections.Generic;

using MathNet.Numerics.LinearAlgebra;

using LinearRegressionBackend.MLCommmons;

namespace LinearRegressionBackend.MLModel
{
    public class LinearRegressionModel : IMLModel
    {
        internal Coefficients _coefficient;
        internal IOptimizer _optimizer;
        internal IEstimator _estimator;
        internal ILossFunction _lossFunction;

        public LinearRegressionModel(double slope, double intercept, IOptimizer optimizer, ILossFunction lossFunction)
        {
            _coefficient = new Coefficients(slope, intercept);
            _optimizer = optimizer;
            _lossFunction = lossFunction;
        }

        public LinearRegressionModel(double slope, double intercept, IEstimator estimator, ILossFunction lossFunction)
        {
            _coefficient = new Coefficients(slope, intercept);
            _estimator = estimator;
            _lossFunction = lossFunction;
        }

        public double Evaluation(double[][] inputData, double[] targetData)
        {
            return _lossFunction.Loss(_coefficient.GetThetas(), inputData, targetData);
        }

        // TODO: Replace the old implementation with this.
        public double Evaluation(Matrix<double> inputData, Vector<double> targetData)
        {
            throw new NotImplementedException();
        }

        public List<History> Train(double[][] inputData, double[] targetData, int epochs = 1)
        {
            List<History> histories = new List<History>();

            if (_optimizer is IIterable == false)
            {
                epochs = 1;
                _coefficient.Slope = 0;
                _coefficient.Intercept = 0;
            }

            for (int i = 0; i < epochs; i++)
            {
                double[] newThetas = _optimizer.Minimize(_lossFunction, _coefficient.GetThetas(), inputData, targetData);
                _coefficient.Slope += newThetas[MLCommons.SLOPE_INDEX];
                _coefficient.Intercept += newThetas[MLCommons.INTERCEPT_INDEX];
                histories.Add(new History(_lossFunction.Loss(_coefficient.GetThetas(), inputData, targetData), _coefficient.GetThetas()));
            }

            return histories;
        }

        // TODO: Replace the old implementation with this.
        public List<History> Train(Matrix<double> inputData, Vector<double> targetData, int epochs = 1)
        {
            throw new NotImplementedException();
        }

        public double Predict(double[] inputData)
        {
            return _coefficient.Slope * inputData[0] + _coefficient.Intercept;
        }

        // TODO: Replace the old implementation with this.
        public double Predict(Vector<double> inputData)
        {
            throw new NotImplementedException();
        }
    }
}
