using System;
using System.IO;

using MathNet.Numerics.LinearAlgebra;
using NUnit.Framework;

using LinearRegressionBackend;
using LinearRegressionBackend.DataProvider;

namespace LinearRegressionBackend_uTest.DataProvider
{
    internal class DataConverterTests
    {
        [Test]
        public void ImageProcess_ReturnsSuccessfully()
        {
            //Arrange
            string folderPath = ".\\Images";
            DirectoryInfo imageDirectory = new DirectoryInfo(folderPath);
            IImageConverter converter = new ImageProcess();

            Matrix<double> inputs;
            Matrix<double> labels;

            //Act
            (inputs, labels) = DataConverter.ProcessInputImages(imageDirectory, converter);

            //Assert
            Assert.That(inputs.RowCount, Is.EqualTo(9), "Input matrix row is other than expected.");
            Assert.That(labels.RowCount, Is.EqualTo(9), "Label matrix row count is other than expected.");
            Assert.That(inputs.ColumnCount, Is.EqualTo(28*28), "Input matrix column is other than expected.");
            Assert.That(labels.ColumnCount, Is.EqualTo(3), "Label matrix column count is other than expected.");
        }
    }
}
