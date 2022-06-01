using System;
using System.Collections.Generic;

using MathNet.Numerics.LinearAlgebra;

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
            Vector<double> input = Vector<double>.Build.Dense(
                new[] { 1.0, 1.0 });
            Console.WriteLine(
                GenerateNetworkTestingData(input).ToString("G10"));
        }

        static Vector<double> GenerateNetworkTestingData(Vector<double> input)
        {
            Matrix<double> weight1 = Matrix<double>.Build.DenseOfRowArrays(
                new[] { 0.1, 0.15 },
                new[] { 0.2, 0.25 }
            );

            Vector<double> bias1 = Vector<double>.Build.Dense(new[] {
                0.1,
                0.2,
            });

            Matrix<double> weight2 = Matrix<double>.Build.DenseOfRowArrays(
                new[] { 0.3, 0.35 },
                new[] { 0.4, 0.45 }
            );

            Vector<double> bias2 = Vector<double>.Build.Dense(new[] {
                0.3,
                0.4,
            });

            List<Layer> layers = new()
            {
                new Layer(
                    weight: weight1,
                    bias: bias1,
                    activationFunction: Sigmoid),
                new Layer(
                    weight: weight2,
                    bias: bias2,
                    activationFunction: Sigmoid),
            };

            Network network = new(layers);

            network.Propagate(input);
            return network.Layers[network.Layers.Count - 1].Activation;
        }
    }
}
