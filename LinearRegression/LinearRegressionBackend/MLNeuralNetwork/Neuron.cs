using System.Diagnostics;

using MathNet.Numerics.LinearAlgebra;

namespace LinearRegressionBackend.MLNeuralNetwork {
    public class Neuron {
        private Vector<double> _Weights;
        private double _Bias;
        private IActivationFunction _ActivationFunction;

        public Neuron(Vector<double> weights, IActivationFunction activationFunction) {
            _Weights = weights;
            _ActivationFunction = activationFunction;
        }

        public double Activation(Vector<double> previousLayer) {
            Debug.Assert(previousLayer.Count == _Weights.Count);

            double weightedSum = DataProvider.Numerical.DotProduct(
                previousLayer, _Weights
            ) + _Bias;

            return _ActivationFunction.Activation(weightedSum);
        }
    }
}
