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
        Random Random;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            ReLU = new();
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

    }
}
