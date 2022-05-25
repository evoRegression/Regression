using System;
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

        public (double, double) ForwardPropagation(Vector<double> inputLayer)
        {
            double weightedSum = WeightedSum(inputLayer);
            double actionPotential = Activation(weightedSum);

            return (actionPotential, weightedSum);
        }

        public double Loss(double actualValue, double expectedValue)
        {
            // Mean Squared Error: 1/2 * (a - y)^2
            throw new NotImplementedException();
        }

        public void BackwardPropagation(double actualValue, double expectedValue, double weightedSum, Vector<double> inputs)
        {
            // dE/da = a - y; da/dz = g'(z); dz/dw = x; dz/db = 1
            // dE/dw = dE/da * da/dz * dz/dw
            // dE/db = dE/da * da/dz * dz/db

            // w = w - lr * dE/dw
            // b = b - lr * dE/db

            throw new NotImplementedException();
        }
    }
}
