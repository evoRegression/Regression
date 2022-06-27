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
            return 0.5 * Math.Pow(actualValue - expectedValue, 2);
        }

        public void BackwardPropagation(double actualValue, double expectedValue, double weightedSum, Vector<double> inputs)
        {
            double dg = ActivationFunction.Derivative(weightedSum);
            double delta = (actualValue - expectedValue) * dg;

            Weights = Weights.MapIndexed((i, w) => w - 0.2 * delta * inputs[i]);
            Bias = Bias - 0.2 * delta;
        }
    }
}
