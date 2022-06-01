using System;
using System.Collections.Generic;

using MathNet.Numerics.LinearAlgebra;
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
            Matrix<double> weight1 = Matrix<double>.Build.DenseOfRowArrays(
                new[] { 1.0, 1.0 },
                new[] { 1.0, 1.0 }
            );

            Vector<double> bias1 = Vector<double>.Build.Dense(new[] {
                1.0, 
                1.0,
            });

            Matrix<double> weight2 = Matrix<double>.Build.DenseOfRowArrays(
                new[] { 1.0, 1.0 },
                new[] { 1.0, 1.0 }
            );

            Vector<double> bias2 = Vector<double>.Build.Dense(new[] {
                1.0,
                1.0,
            });

            List<Layer> layers = new()
            {
                new Layer(
                    weight: weight1,
                    bias: bias1,
                    activationFunction: ReLU),
                new Layer(
                    weight: weight2,
                    bias: bias2,
                    activationFunction: ReLU),
            };

            Network network = new(layers);

            Vector<double> input = Vector<double>.Build.Dense(
                new[] { 1.0, 1.0 });

            // Act
            Vector<double> output = network.Propagate(input).Output();

            // Assert
            Assert.Multiple(() => {
                Assert.That(
                    output[0],
                    Is.EqualTo(7).Within(ACCURACY_DELTA),
                    "Incorrect output");
                Assert.That(
                    output[1],
                    Is.EqualTo(7).Within(ACCURACY_DELTA),
                    "Incorrect output");
            });
        }

        [Test]
        public void Network_Backpropagation()
        {
            // Arrange
            List<Layer> layers = new()
            {
                new Layer(
                    weight: Matrix<double>.Build.Random(2, 2, 1),
                    bias: Vector<double>.Build.Random(2, 2),
                    activationFunction: Sigmoid),
                new Layer(
                    weight: Matrix<double>.Build.Random(2, 2, 3),
                    bias: Vector<double>.Build.Random(2, 4),
                    activationFunction: Sigmoid),
            };

            Network network = new(layers);

            int epochs = 1000;
            double learningRate = 0.2;

            Vector<double> input = Vector<double>.Build.Dense(
                new[] { 1.0, 1.0 });
            Vector<double> expectedOutput = Vector<double>.Build.Dense(
                new[] { 0.6695031929, 0.7171364979 });

            // Act
            network.Train(
                input.ToRowMatrix(),
                expectedOutput.ToRowMatrix(),
                epochs,
                learningRate);

            Vector<double> output = network.Propagate(input).Output();

            // Assert
            Assert.Multiple(() => {
                Assert.That(
                    output[0],
                    Is.EqualTo(expectedOutput[0]).Within(ACCURACY_DELTA),
                    "Incorrect output");
                Assert.That(
                    output[1],
                    Is.EqualTo(expectedOutput[1]).Within(ACCURACY_DELTA),
                    "Incorrect output");
            });
        }

    }
}
