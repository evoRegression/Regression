using System;

using MathNet.Numerics.LinearAlgebra;
using Moq;
using NUnit.Framework;

using LinearRegressionBackend;
using LinearRegressionBackend.MLNeuralNetwork;

namespace LinearRegressionBackend_uTest.MLNeuralNetwork
{
    internal class NeuronTests
    {
        const double ACCURACY_DELTA = 0.00001d;

        [OneTimeSetUp]
        public void OneTimeSetUp() { }

        [SetUp]
        public void Setup() { }

        [TearDown]
        public void TearDown() { }

        [OneTimeTearDown]
        public void OneTimeTearDown() { }

        [Test]
        public void Neuron_ZeroWeights_CorrectWeightedSum()
        {
            // Arrange
            Vector<double> previousLayer = Vector<double>.Build.Dense(
                new double[] { 0.1, 0.1, 0.1, 0.1, 0.1 }
            );

            Vector<double> weights = Vector<double>.Build.Dense(
                new double[] { 0, 0, 0, 0, 0 }
            );
            double bias = 0;
            Neuron neuron = new(weights, bias, new Sigmoid());

            // Act
            double weightedSum = neuron.WeightedSum(previousLayer);

            // Assert
            Assert.That(weightedSum, Is.EqualTo(0).Within(ACCURACY_DELTA),
                "Incorrect weighted sum");
        }

        [Test]
        public void Neuron_PositiveWeights_CorrectWeightedSum()
        {
            // Arrange
            Vector<double> previousLayer = Vector<double>.Build.Dense(
                new double[] { 0.1, 0.1, 0.1, 0.1, 0.1 }
            );

            Vector<double> weights = Vector<double>.Build.Dense(
                new double[] { 1, 1, 1, 1, 1 }
            );
            double bias = 0;
            Neuron neuron = new(weights, bias, new Sigmoid());

            // Act
            double weightedSum = neuron.WeightedSum(previousLayer);

            // Assert
            Assert.That(weightedSum, Is.EqualTo(0.5).Within(ACCURACY_DELTA),
                "Incorrect weighted sum");
        }

        [Test]
        public void Neuron_NegativeWeights_CorrectWeightedSum()
        {
            // Arrange
            Vector<double> previousLayer = Vector<double>.Build.Dense(
                new double[] { 0.1, 0.1, 0.1, 0.1, 0.1 }
            );

            Vector<double> weights = Vector<double>.Build.Dense(
                new double[] { -1, -1, -1, -1, -1 }
            );
            double bias = 0;
            Neuron neuron = new(weights, bias, new Sigmoid());

            // Act
            double weightedSum = neuron.WeightedSum(previousLayer);

            // Assert
            Assert.That(weightedSum, Is.EqualTo(-0.5).Within(ACCURACY_DELTA),
                "Incorrect weighted sum");
        }

        [Test]
        public void Sigmoid_ZeroSum_CorrectActivation()
        {
            // Arrange
            double weightedSum = 0;
            Sigmoid sigmoid = new();

            // Act
            double activation = sigmoid.Activation(weightedSum);

            // Assert
            Assert.That(activation, Is.EqualTo(0.5).Within(ACCURACY_DELTA),
                "Incorrect activation");
        }

        [Test]
        public void Sigmoid_PositiveSum_CorrectActivation()
        {
            // Arrange
            double weightedSum = 1;
            Sigmoid sigmoid = new();

            // Act
            double activation = sigmoid.Activation(weightedSum);

            // Assert
            Assert.That(activation,
                Is.EqualTo(0.73105857863).Within(ACCURACY_DELTA),
                "Incorrect activation");
        }

        [Test]
        public void Sigmoid_NegativeSum_CorrectActivation()
        {
            // Arrange
            double weightedSum = -1;
            Sigmoid sigmoid = new();

            // Act
            double activation = sigmoid.Activation(weightedSum);

            // Assert
            Assert.That(activation,
                Is.EqualTo(0.26894142137).Within(ACCURACY_DELTA),
                "Incorrect activation");
        }

        [Test]
        public void Tanh_ZeroSum_CorrectActivation()
        {
            // Arrange
            double weightedSum = 0;
            Tanh tanh = new();

            // Act
            double activation = tanh.Activation(weightedSum);

            // Assert
            Assert.That(activation, Is.EqualTo(0).Within(ACCURACY_DELTA),
                "Incorrect activation");
        }

        [Test]
        public void Tanh_PositiveSum_CorrectActivation()
        {
            // Arrange
            double weightedSum = 1;
            Tanh tanh = new();

            // Act
            double activation = tanh.Activation(weightedSum);

            // Assert
            Assert.That(activation,
                Is.EqualTo(0.761594155956).Within(ACCURACY_DELTA),
                "Incorrect activation");
        }

        [Test]
        public void Tanh_NegativeSum_CorrectActivation()
        {
            // Arrange
            double weightedSum = -1;
            Tanh tanh = new();

            // Act
            double activation = tanh.Activation(weightedSum);

            // Assert
            Assert.That(activation,
                Is.EqualTo(-0.761594155956).Within(ACCURACY_DELTA),
                "Incorrect activation");
        }

        [Test]
        public void ReLU_ZeroSum_CorrectActivation()
        {
            // Arrange
            double weightedSum = 0;
            ReLU relu = new();

            // Act
            double activation = relu.Activation(weightedSum);

            // Assert
            Assert.That(activation, Is.EqualTo(0).Within(ACCURACY_DELTA),
                "Incorrect activation");
        }

        [Test]
        public void ReLU_PositiveSum_CorrectActivation()
        {
            // Arrange
            double weightedSum = 1;
            ReLU relu = new();

            // Act
            double activation = relu.Activation(weightedSum);

            // Assert
            Assert.That(activation, Is.EqualTo(1).Within(ACCURACY_DELTA),
                "Incorrect activation");
        }

        [Test]
        public void ReLU_NegativeSum_CorrectActivation()
        {
            // Arrange
            double weightedSum = -1;
            ReLU relu = new();

            // Act
            double activation = relu.Activation(weightedSum);

            // Assert
            Assert.That(activation, Is.EqualTo(0).Within(ACCURACY_DELTA),
                "Incorrect activation");
        }

        [Test]
        public void Neuron_PositiveWeights_CorrectActivation()
        {
            // Arrange
            Vector<double> previousLayer = Vector<double>.Build.Dense(
                new double[] { 0.1, 0.1, 0.1, 0.1, 0.1 }
            );

            Vector<double> weights = Vector<double>.Build.Dense(
                new double[] { 1, 1, 1, 1, 1 }
            );
            double bias = 0;
            Neuron neuron = new(weights, bias, new ReLU());
            double weightedSum = neuron.WeightedSum(previousLayer);

            // Act
            double activation = neuron.Activation(weightedSum);

            // Assert
            Assert.That(activation, Is.EqualTo(0.5).Within(ACCURACY_DELTA),
                "Incorrect activation");
        }

        /// <summary>
        /// Linear function f = w_1*x_1 + w_2*x_2 + b
        /// Approximate the f = 2x_1 + 3x_2 + 1, where w_1=2, w_2=3, b=1
        /// </summary>
        [Test]
        public void BackwardPropagation_LinearFunction_CorrectBackwardPass()
        {
            // Arrange
            Matrix<double> inputs = Matrix<double>.Build.DenseOfArray(
                new double[,] { { 1, 1 }, { 0, 1 }, { -1, 0 }, { -1, -1 }, { 0, 0 } });
            Vector<double> expectedOutputs = Vector<double>.Build.Dense(
                new double[] { 6, 3, -2, -4, 1 });
            Vector<double> weights = Vector<double>.Build.Random(2);

            Mock<IActivationFunction> linearActivationMock = new();
            linearActivationMock.Setup(a => a.Activation(It.IsAny<double>())).Returns<double>(weightedSum => weightedSum);
            linearActivationMock.Setup(a => a.Derivative(It.IsAny<double>())).Returns<double>(1);

            int epochs = 1000;
            Neuron neuron = new(weights, 0, linearActivationMock.Object);
            
            // Act
            for (int i = 0; i < epochs; i++)
            {
                // It is called Stochastic Gradient Descent. SGD algorithm updates the weights one input at a time
                for (int j = 0; j < inputs.RowCount; j++)
                {
                    (double actionPotential, double weightedSum) = neuron.ForwardPropagation(inputs.Row(j));
                    neuron.BackwardPropagation(actionPotential, expectedOutputs[j], weightedSum, inputs.Row(j));
                }
            }

            // Assert
            Assert.That(neuron.Weights[0], Is.EqualTo(2).Within(ACCURACY_DELTA),
                "Incorrect first weight");
            Assert.That(neuron.Weights[1], Is.EqualTo(3).Within(ACCURACY_DELTA),
                "Incorrect second weight");
            Assert.That(neuron.Bias, Is.EqualTo(1).Within(ACCURACY_DELTA),
                "Incorrect bias");
        }
    }
}
