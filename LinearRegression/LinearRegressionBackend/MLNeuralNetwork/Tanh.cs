using System;

namespace LinearRegressionBackend.MLNeuralNetwork
{
    public class Tanh : IActivationFunction
    {
        public string GetSerializedName()
        {
            return "Tanh";
        }

        public double Activation(double weightedSum)
        {
            return Math.Tanh(weightedSum);
        }

        public double Derivative(double weightedSum)
        {
            return 1 - Math.Pow(Math.Tanh(weightedSum), 2);
        }
    }
}
