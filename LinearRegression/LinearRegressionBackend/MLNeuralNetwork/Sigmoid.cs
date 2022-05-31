using System;

namespace LinearRegressionBackend.MLNeuralNetwork
{
    public class Sigmoid : IActivationFunction
    {
        public double Activation(double weightedSum)
        {
            return 1.0 / (1.0 + Math.Pow(Math.E, -weightedSum));
        }

        public double Derivative(double weightedSum)
        {
            return Math.Pow(Math.E, -weightedSum)
                / Math.Pow(1.0 + Math.Pow(Math.E, -weightedSum), 2);
        }
    }
}
