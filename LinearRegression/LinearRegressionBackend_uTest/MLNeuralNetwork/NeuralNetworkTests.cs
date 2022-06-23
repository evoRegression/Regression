using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;

using MathNet.Numerics.LinearAlgebra;
using NUnit.Framework;

using LinearRegressionBackend.MLNeuralNetwork;

namespace LinearRegressionBackend_uTest.MLNeuralNetwork
{
    internal class NetworkTests
    {
        const double ACCURACY_DELTA = 0.00001d;

        ReLU ReLU;
        Sigmoid Sigmoid;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            ReLU = new();
            Sigmoid = new();
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

            NeuralNetwork network = new(layers);

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

            NeuralNetwork network = new(layers);

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

        [Test]
        public void Network_Backpropagation_MultipleExamples()
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

            NeuralNetwork network = new(layers);

            int epochs = 30000;
            double learningRate = 1.0;

            Matrix<double> input =
                Matrix<double>.Build.DenseOfRowArrays(
                    new[] { 0.2, 0.4 },
                    new[] { 0.6, 0.8 }
                );
            Matrix<double> expectedOutput =
                Matrix<double>.Build.DenseOfRowArrays(
                    new[] { 0.6610395753, 0.7069956986 },
                    new[] { 0.6660482481, 0.7130073216 }
                );

            // Act
            network.Train(
                input,
                expectedOutput,
                epochs,
                learningRate);

            Matrix<double> output = Matrix<double>.Build.DenseOfRows(
                input.EnumerateRows().Select(ex => network.Propagate(ex).Output()).ToArray());

            // Assert
            Assert.Multiple(() => {
                Assert.That(
                    output[0, 0],
                    Is.EqualTo(expectedOutput[0, 0]).Within(ACCURACY_DELTA),
                    "Incorrect output");
                Assert.That(
                    output[0, 1],
                    Is.EqualTo(expectedOutput[0, 1]).Within(ACCURACY_DELTA),
                    "Incorrect output");
                Assert.That(
                    output[1, 0],
                    Is.EqualTo(expectedOutput[1, 0]).Within(ACCURACY_DELTA),
                    "Incorrect output");
                Assert.That(
                    output[1, 1],
                    Is.EqualTo(expectedOutput[1, 1]).Within(ACCURACY_DELTA),
                    "Incorrect output");
            });
        }

        [Test]
        public async Task Network_ImportExport()
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

            NeuralNetwork network = new(layers);

            Vector<double> input = Vector<double>.Build.Dense(
                new[] { 1.0, 1.0 });

            string FILENAME = Environment.ExpandEnvironmentVariables(
                @"%USERPROFILE%\Desktop\network.json");

            // Act
            using (Stream outputStream = File.OpenWrite(FILENAME))
            {
                await network.Export(outputStream);
            }

            NeuralNetwork importedNetwork = null;

            using (Stream inputStream = File.OpenRead(FILENAME))
            {
                importedNetwork = await NeuralNetwork.Import(inputStream);
            }

            Vector<double> output = importedNetwork.Propagate(input).Output();

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
        public async Task Network_ImportExport_Compressed()
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

            NeuralNetwork network = new(layers);

            Vector<double> input = Vector<double>.Build.Dense(
                new[] { 1.0, 1.0 });

            string FILENAME = Environment.ExpandEnvironmentVariables(
                @"%USERPROFILE%\Desktop\network.json.gz");

            // Act
            using (Stream outputStream = File.OpenWrite(FILENAME))
            using (GZipStream compressor =
                new(outputStream, CompressionMode.Compress))
            {
                await network.Export(compressor);
            }

            NeuralNetwork importedNetwork = null;

            using (Stream inputStream = File.OpenRead(FILENAME))
            using (GZipStream decompressor =
                new(inputStream, CompressionMode.Decompress))
            {
                importedNetwork = await NeuralNetwork.Import(decompressor);
            }

            Vector<double> output = importedNetwork.Propagate(input).Output();

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

    }
}
