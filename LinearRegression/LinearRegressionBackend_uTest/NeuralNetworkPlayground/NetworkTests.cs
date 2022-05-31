using System;
using System.Collections.Generic;

using NUnit.Framework;

using LinearRegressionBackend.NeuralNetworkPlayground;

namespace LinearRegressionBackend_uTest.NeuralNetworkPlayground
{
    internal class NetworkTests
    {
        const double ACCURACY_DELTA = 0.00001d;

        LinearRegressionBackend.MLNeuralNetwork.ReLU ReLU;
        LinearRegressionBackend.MLNeuralNetwork.Sigmoid Sigmoid;
        Random Random;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            ReLU = new();
            Sigmoid = new();
            Random = new();
        }

        [SetUp]
        public void Setup() { }

        [TearDown]
        public void TearDown() { }

        [OneTimeTearDown]
        public void OneTimeTearDown() { }

        [Test]
        public void Network_Propagation()
        {
            // Arrange
            List<Layer> layers = new()
            {
                new Layer(weight: 1, bias: 1, activationFunction: ReLU),
                new Layer(weight: 1, bias: 1, activationFunction: ReLU),
            };

            Network network = new(layers);

            double input = 1;

            // Act
            double output = network.Propagate(input).Output();

            // Assert
            Assert.That(output, Is.EqualTo(3).Within(ACCURACY_DELTA),
                "Incorrect output");
        }

        [Test]
        public void Network_Backpropagation()
        {
            // Arrange
            List<Layer> layers = new()
            {
                new Layer(
                    weight: Random.NextDouble(),
                    bias: Random.NextDouble(),
                    activationFunction: Sigmoid),
                new Layer(
                    weight: Random.NextDouble(),
                    bias: Random.NextDouble(),
                    activationFunction: Sigmoid),
            };

            Network network = new(layers);

            int epochs = 1000;
            double learningRate = 0.2;

            double input = 1;
            double expectedOutput = 0.6393012717748393;

            // Act
            for (int i = 0; i < epochs; i++)
            {
                Propagation prop = network.Propagate(input);
                network.Backpropagate(prop, expectedOutput, learningRate);
            }

            double output = network.Propagate(input).Output();

            // Assert
            Assert.That(
                output,
                Is.EqualTo(expectedOutput).Within(ACCURACY_DELTA),
                "Incorrect output");
        }

    }
}
