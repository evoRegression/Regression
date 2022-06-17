using System;
using System.Collections.Generic;

namespace LinearRegressionBackend.MLNeuralNetwork
{
    static class AvailableActivationFunctions
    {
        public static Dictionary<string, Func<IActivationFunction>> Builders;

        static AvailableActivationFunctions()
        {
            Builders = new()
            {
                { "Sigmoid", () => new Sigmoid() },
                { "ReLU", () => new ReLU() },
                { "Tanh", () => new Tanh() },
            };
        }
    }
}
