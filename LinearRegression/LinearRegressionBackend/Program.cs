using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Linq;
using System.Threading.Tasks;

using MathNet.Numerics.LinearAlgebra;

using LinearRegressionBackend.MLNeuralNetwork;

namespace LinearRegressionBackend.OOPExercise
{
    class Program
    {

        static async Task<int> Main(string[] args)
        {
            var rootCommand =
                new RootCommand("Machine Learning CLI Tool");

            var trainCommand = new Command(
                name: "train",
                description: "Train a neural network with the provided data");

            rootCommand.AddCommand(trainCommand);

            var trainDataCommand = new Command(
                name: "data",
                description: "Train the neural network with data from json");

            trainCommand.AddCommand(trainDataCommand);

            var trainImagesCommand = new Command(
                name: "images",
                description: "Train the neural network with data from images");

            trainCommand.AddCommand(trainImagesCommand);

            var layersOption =
                new Option<int[]>(
                    name: "--layers",
                    description: "The number of neurons in each layer.")
                {
                    IsRequired = true,
                    Arity = ArgumentArity.OneOrMore,
                    AllowMultipleArgumentsPerToken = true,
                };

            trainCommand.AddGlobalOption(layersOption);

            var activationFuncOption =
                new Option<string>(
                    name: "--activationfunc",
                    description:
                        "The activation function used by the neural network")
                {
                    IsRequired = true,
                }
                    .FromAmong(
                        AvailableActivationFunctions.Builders.Keys.ToArray());

            trainCommand.AddGlobalOption(activationFuncOption);

            trainDataCommand.SetHandler(
                (layerCounts, activationFuncName) =>
                {
                    NeuralNetwork network =
                        BuildNetwork(layerCounts, activationFuncName);
                },
                layersOption,
                activationFuncOption);

            trainImagesCommand.SetHandler(
                (layerCounts, activationFuncName) =>
                {
                    NeuralNetwork network =
                        BuildNetwork(layerCounts, activationFuncName);
                },
                layersOption,
                activationFuncOption);

            return await rootCommand.InvokeAsync(args);
        }

        static NeuralNetwork BuildNetwork(
            int[] layerCounts,
            string activationFuncName)
        {
            Console.WriteLine(
                $"Network architecture: {string.Join(", ", layerCounts)}");
            Console.WriteLine($"Activation function: {activationFuncName}");

            IActivationFunction activationFunction =
                AvailableActivationFunctions.Builders[activationFuncName]();

            List<Layer> layers = new();

            for (int i = 1; i < layerCounts.Length; i++)
            {
                int m = layerCounts[i - 1];
                int n = layerCounts[i];

                layers.Add(new Layer(
                    weight: Matrix<double>.Build.Random(
                        n, m, Environment.TickCount),
                    bias: Vector<double>.Build.Random(
                        n, Environment.TickCount),
                    activationFunction: activationFunction));
            }

            return new NeuralNetwork(layers);
        }

    }
}
