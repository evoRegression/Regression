using System;

namespace LinearRegressionBackend.MLNeuralNetwork
{
    public class Tanh : IActivationFunction
    {
        public double Activation(double weightedSum)
        {
            return Math.Tanh(weightedSum);
        }
    }
}
