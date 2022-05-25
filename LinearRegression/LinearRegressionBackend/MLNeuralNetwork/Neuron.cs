using System.Diagnostics;

using MathNet.Numerics.LinearAlgebra;

using LinearRegressionBackend.DataProvider;

namespace LinearRegressionBackend.MLNeuralNetwork
{
    public class Neuron
    {
        public Vector<double> Weights { get; set; }

        public double Bias { get; set; }

        public IActivationFunction ActivationFunction { get; set; }

        public Neuron(Vector<double> weights, double bias, IActivationFunction activationFunction)
        {
            Weights = weights;
            Bias = bias;
            ActivationFunction = activationFunction;
        }

        public double WeightedSum(Vector<double> previousLayer)
        {
            Debug.Assert(previousLayer.Count == Weights.Count);
            return Numerical.DotProduct(previousLayer, Weights) + Bias;
        }

        public double Activation(double weightedSum)
        {
            return ActivationFunction.Activation(weightedSum);
        }
    }
}
