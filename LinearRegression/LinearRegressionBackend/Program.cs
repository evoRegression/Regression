using System;
using System.Collections.Generic;

using LinearRegressionBackend.NeuralNetworkPlayground;

namespace LinearRegressionBackend.OOPExercise
{
    class Program
    {
        static MLNeuralNetwork.Sigmoid Sigmoid;

        static Program()
        {
            Sigmoid = new();
        }

        static void Main(string[] args)
        {
            Console.WriteLine(GenerateNetworkTestingData(1));
        }

        static double GenerateNetworkTestingData(double input)
        {
            List<Layer> layers = new()
            {
                new Layer(weight: 0.1, bias: 0.2, activationFunction: Sigmoid),
                new Layer(weight: 0.3, bias: 0.4, activationFunction: Sigmoid),
            };

            Network network = new(layers);

            return network.Propagate(input).Output();
        }
    }
}
