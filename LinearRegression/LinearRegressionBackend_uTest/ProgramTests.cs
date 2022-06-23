using System;
using System.IO;
using System.Threading.Tasks;

using MathNet.Numerics.LinearAlgebra;
using NUnit.Framework;

using LinearRegressionBackend.MLNeuralNetwork;
using System.IO.Compression;

namespace LinearRegressionBackend_uTest.MLNeuralNetwork
{
    internal class ProgramTests
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
        public async Task DataSet_ImportExport()
        {
            // Arrange
            Vector<double> input = Vector<double>.Build.Dense(
                new[] { 1.0, 1.0 });
            Vector<double> expectedOutput = Vector<double>.Build.Dense(
                new[] { 0.6695031929, 0.7171364979 });

            DataSet dataSet = new()
            {
                TrainingInput = input.ToRowMatrix(),
                TrainingOutput = expectedOutput.ToRowMatrix(),
            };

            string FILENAME = Environment.ExpandEnvironmentVariables(
                @"%TESTTMP%\dataset.json");

            // Act
            using (Stream outputStream = File.OpenWrite(FILENAME))
            {
                await dataSet.Export(outputStream);
            }

            DataSet importedDataSet = null;

            using (Stream inputStream = File.OpenRead(FILENAME))
            {
                importedDataSet = await DataSet.Import(inputStream);
            }

            // Assert
            Assert.Multiple(() => {
                Assert.That(
                    importedDataSet.TrainingInput[0, 0],
                    Is.EqualTo(input[0]).Within(ACCURACY_DELTA),
                    "Incorrect output");
                Assert.That(
                    importedDataSet.TrainingInput[0, 1],
                    Is.EqualTo(input[1]).Within(ACCURACY_DELTA),
                    "Incorrect output");
                Assert.That(
                    importedDataSet.TrainingOutput[0, 0],
                    Is.EqualTo(expectedOutput[0]).Within(ACCURACY_DELTA),
                    "Incorrect output");
                Assert.That(
                    importedDataSet.TrainingOutput[0, 1],
                    Is.EqualTo(expectedOutput[1]).Within(ACCURACY_DELTA),
                    "Incorrect output");
            });
        }

        [Test]
        public async Task DataSet_ImportExport_Compressed()
        {
            // Arrange
            Vector<double> input = Vector<double>.Build.Dense(
                new[] { 1.0, 1.0 });
            Vector<double> expectedOutput = Vector<double>.Build.Dense(
                new[] { 0.6695031929, 0.7171364979 });

            DataSet dataSet = new()
            {
                TrainingInput = input.ToRowMatrix(),
                TrainingOutput = expectedOutput.ToRowMatrix(),
            };

            string FILENAME = Environment.ExpandEnvironmentVariables(
                @"%TESTTMP%\dataset.json.gz");

            // Act
            using (Stream outputStream = File.OpenWrite(FILENAME))
            using (GZipStream compressor =
                new(outputStream, CompressionMode.Compress))
            {
                await dataSet.Export(compressor);
            }

            DataSet importedDataSet = null;

            using (Stream inputStream = File.OpenRead(FILENAME))
            using (GZipStream decompressor =
                new(inputStream, CompressionMode.Decompress))
            {
                importedDataSet = await DataSet.Import(decompressor);
            }

            // Assert
            Assert.Multiple(() => {
                Assert.That(
                    importedDataSet.TrainingInput[0, 0],
                    Is.EqualTo(input[0]).Within(ACCURACY_DELTA),
                    "Incorrect output");
                Assert.That(
                    importedDataSet.TrainingInput[0, 1],
                    Is.EqualTo(input[1]).Within(ACCURACY_DELTA),
                    "Incorrect output");
                Assert.That(
                    importedDataSet.TrainingOutput[0, 0],
                    Is.EqualTo(expectedOutput[0]).Within(ACCURACY_DELTA),
                    "Incorrect output");
                Assert.That(
                    importedDataSet.TrainingOutput[0, 1],
                    Is.EqualTo(expectedOutput[1]).Within(ACCURACY_DELTA),
                    "Incorrect output");
            });
        }

    }
}
