using System.Diagnostics;

using MathNet.Numerics.LinearAlgebra;

namespace LinearRegressionBackend.MLNeuralNetwork {
    public class Neuron {
        public Vector<double> Weights { get; set; }
        public double Bias { get; set; }
        public IActivationFunction ActivationFunction { get; set; }

        public Neuron(
            Vector<double> weights,
            double bias,
            IActivationFunction activationFunction
        ) {
            Weights = weights;
            Bias = bias;
            ActivationFunction = activationFunction;
        }

        public double WeightedSum(Vector<double> previousLayer) {
            return DataProvider.Numerical.DotProduct(previousLayer, Weights)
                + Bias;
        }

        public double Activation(Vector<double> previousLayer) {
            Debug.Assert(previousLayer.Count == Weights.Count);
            return ActivationFunction.Activation(WeightedSum(previousLayer));
        }
    }
}
