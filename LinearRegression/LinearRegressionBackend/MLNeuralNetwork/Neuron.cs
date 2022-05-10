using System;
using System.Diagnostics;

using MathNet.Numerics.LinearAlgebra;

using LinearRegressionBackend.DataProvider;

namespace LinearRegressionBackend.MLNeuralNetwork
{
    public class Neuron
    {
        // TODO: Change the Vector to Matrix
        public Vector<double> Weights { get; set; }

        public double Bias { get; set; }

        public IActivationFunction ActivationFunction { get; set; }

        public Neuron(Vector<double> weights, double bias, IActivationFunction activationFunction)
        {
            Weights = weights;
            Bias = bias;
            ActivationFunction = activationFunction;
        }

        public double ForwardPropagation(Vector<double> inputData)
        {
            double actionPotential = Activation(WeightedSum(inputData));

            return actionPotential;
        }

        public Vector<double> BackwardPropagation(Vector<double> actualValue, Vector<double> expectedValue)
        {
            throw new NotImplementedException();
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
