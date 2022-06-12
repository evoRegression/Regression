using System;
using System.Collections.Generic;
using System.Linq;

using MathNet.Numerics.LinearAlgebra;

using LinearRegressionBackend.MLNeuralNetwork;

namespace LinearRegressionBackend_uTest.TestCaseGenerator
{
    class Program
    {
        static readonly Sigmoid Sigmoid;

        static Program()
        {
            Sigmoid = new();
        }

        static void Main()
        {
            {
                PrintTestCaseTitle("Network_Backpropagation");
                Vector<double> input = Vector<double>.Build.Dense(
                    new[] { 1.0, 1.0 });
                Console.WriteLine(
                    GenerateData_Network_Backpropagation(input).ToString("G10"));
            }
        }

        static void PrintTestCaseTitle(string title)
        {
            Console.WriteLine(title);
            Console.WriteLine(new String('=', title.Length));
        }

        static Matrix<double> GenerateData_Network_Backpropagation(
            Vector<double> input)
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

            NeuralNetwork network = new(layers);

            return network.Propagate(input).Output().ToRowMatrix();
        }
    }
}
