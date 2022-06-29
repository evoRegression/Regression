using System;

namespace LinearRegressionBackend.MLNeuralNetwork
{
    public class ReLU : IActivationFunction
    {
        public string GetSerializedName()
        {
            return "ReLU";
        }

        public double Activation(double weightedSum)
        {
            return Math.Max(0, weightedSum);
        }

        public double Derivative(double weightedSum)
        {
            if (weightedSum <= 0)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }
    }
}
