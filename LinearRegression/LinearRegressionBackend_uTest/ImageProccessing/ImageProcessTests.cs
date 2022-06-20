
using NUnit.Framework;
using LinearRegressionBackend;
using System.IO;
using MathNet.Numerics.LinearAlgebra;
using LinearRegressionBackend.DataProvider;
using System;

namespace LinearRegressionBackend_uTest.ImageProccessing
{
    class ImageProcessTests
    {

        [Test]
        public void ImageProcess_ReturnsSuccessfully()
        {

            //Arrange
            string FolderPath = "C:\\Users\\Ali\\Desktop\\Coding\\Evo\\Regression\\LinearRegression\\Resources";
            DirectoryInfo fl = new DirectoryInfo(FolderPath);
            IImageConverter converter = new ImageProcess();
            Matrix<double> matrixOfPixels = null;
            Matrix<double> matrixOfLabels = null;
            //Act
            (matrixOfPixels,matrixOfLabels) = DataConverter.ProcessInputImages(fl, converter);
            //Assert
            Assert.That(matrixOfPixels.RowCount, Is.EqualTo(9),
              "Something wrong with the matrixOfPixels");

        }
    }
}
