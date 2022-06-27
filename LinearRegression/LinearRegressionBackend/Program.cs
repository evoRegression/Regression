using System;
using System.Collections.Generic;
using System.CommandLine;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;

using MathNet.Numerics.LinearAlgebra;

using LinearRegressionBackend.MLNeuralNetwork;

namespace LinearRegressionBackend.OOPExercise
{
    class Program
    {

        const double DEFAULT_LEARNING_RATE = 0.1;
        const int DEFAULT_EPOCHS = 1000;
        const int DEFAULT_BATCH_SIZE = 100;

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
                new Option<string[]>(
                    name: "--activationfunc",
                    description:
                        "The activation functions used by each layer.")
                {
                    IsRequired = true,
                    Arity = ArgumentArity.OneOrMore,
                    AllowMultipleArgumentsPerToken = true,
                }
                    .FromAmong(
                        AvailableActivationFunctions.Builders.Keys.ToArray());

            trainCommand.AddGlobalOption(activationFuncOption);

            var learningRateOption =
                new Option<double>(
                    name: "--learningrate",
                    description:
                        "The learning rate used during training.",
                    getDefaultValue: () => DEFAULT_LEARNING_RATE);

            trainCommand.AddGlobalOption(learningRateOption);

            var epochsOption =
                new Option<int>(
                    name: "--epochs",
                    description:
                        "The number of epochs used for training.",
                    getDefaultValue: () => DEFAULT_EPOCHS);

            trainCommand.AddGlobalOption(epochsOption);

            var batchSizeOption =
                new Option<int>(
                    name: "--batchsize",
                    description:
                        "The batch size used during training.",
                    getDefaultValue: () => DEFAULT_BATCH_SIZE);

            trainCommand.AddGlobalOption(batchSizeOption);

            var trainDataInputArgument =
                new Argument<FileInfo>(
                    name: "input",
                    description: "A json or gzipped json file that contains the training dataset.");

            trainDataCommand.AddArgument(trainDataInputArgument);

            var trainImagesInputArgument =
                new Argument<DirectoryInfo>(
                    name: "input",
                    description: "A folder that contains the training dataset of images.");

            trainImagesCommand.AddArgument(trainImagesInputArgument);

            var outputArgument =
                new Argument<FileInfo>(
                    name: "output",
                    description: "The name of the exported gzipped json file.");

            trainDataCommand.AddArgument(outputArgument);
            trainImagesCommand.AddArgument(outputArgument);

            trainDataCommand.SetHandler(
                async (
                    layerCounts, 
                    activationFuncNames,
                    learningRate,
                    epochs,
                    batchSize,
                    input,
                    output
                ) =>
                {
                    NeuralNetwork network =
                        BuildNetwork(layerCounts, activationFuncNames);

                    TrainParams trainParams = new()
                    {
                        LearningRate = learningRate,
                        Epochs = epochs,
                        BatchSize = batchSize,
                    };

                    trainParams.Log();

                    using Stream inputStream =
                        File.OpenRead(input.FullName);
                    using GZipStream decompressor = 
                        new(inputStream, CompressionMode.Decompress);
                    using Stream outputStream =
                        File.OpenWrite(output.FullName);
                    using GZipStream compressor =
                        new(outputStream, CompressionMode.Compress);

                    DataSet dataSet = null;

                    if (input.Extension.Equals(".json"))
                    {
                        dataSet = await DataSet.Import(inputStream);
                    }
                    else if (input.Extension.Equals(".gz"))
                    {
                        dataSet = await DataSet.Import(decompressor);
                    }
                    else
                    {
                        Console.Error.WriteLine("Invalid file type.");
                        return;
                    }

                    TrainData(network, dataSet, trainParams);

                    await network.Export(compressor);
                },
                layersOption,
                activationFuncOption,
                learningRateOption,
                epochsOption,
                batchSizeOption,
                trainDataInputArgument,
                outputArgument);

            trainImagesCommand.SetHandler(
                (
                    layerCounts,
                    activationFuncNames,
                    learningRate,
                    epochs,
                    batchSize,
                    input,
                    output
                ) =>
                {
                    NeuralNetwork network =
                        BuildNetwork(layerCounts, activationFuncNames);

                    TrainParams trainParams = new()
                    {
                        LearningRate = learningRate,
                        Epochs = epochs,
                        BatchSize = batchSize,
                    };

                    trainParams.Log();

                    // TODO: Implement image training
                },
                layersOption,
                activationFuncOption,
                learningRateOption,
                epochsOption,
                batchSizeOption,
                trainImagesInputArgument,
                outputArgument);

            return await rootCommand.InvokeAsync(args);
        }

        static NeuralNetwork BuildNetwork(
            int[] layerCounts,
            string[] activationFuncNames)
        {
            Console.WriteLine(
                $"Network architecture: {string.Join(", ", layerCounts)}");
            Console.WriteLine(
                $"Activation functions: {string.Join(", ", activationFuncNames)}");

            IActivationFunction[] activationFunctions =
                activationFuncNames.Select(name =>
                    AvailableActivationFunctions.Builders[name]()).ToArray();

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
                    activationFunction: activationFunctions[i - 1]));
            }

            return new NeuralNetwork(layers);
        }

        class TrainParams
        {
            public int BatchSize;
            public int Epochs;
            public double LearningRate;

            public void Log()
            {
                Console.WriteLine($"Batch size: {BatchSize}");
                Console.WriteLine($"Epochs: {Epochs}");
                Console.WriteLine($"Learning rate: {LearningRate}");
            }
        }

        static void TrainData(
            NeuralNetwork network,
            DataSet dataSet,
            TrainParams trainParams)
        {
            network.BatchTrain(
                dataSet.TrainingInput,
                dataSet.TrainingOutput,
                trainParams.BatchSize,
                trainParams.Epochs,
                trainParams.LearningRate);
        }

    }
}
