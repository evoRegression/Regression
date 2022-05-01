using NUnit.Framework;
using System.Linq;
using MLModelProduct = LinearRegressionBackend.MLModel;

namespace LinearRegressionBackend_uTest.MLModel
{
    public class LeastSquareErrorTests
    {
        [Test]
        public void LeastSquareError_Loss_ReturnsSuccessfully()
        {
            //Arrange
            MLModelProduct.LeastSquareError LSE = new MLModelProduct.LeastSquareError();
            double[] thetas = { 2.0, 0.0 };
            double[][] inputData = { new double[]{ 1, 2 }, new double[]{ 2, 4 } };
            double[] targetData = inputData.Select(array => (double)array.GetValue(1)).ToArray();
            //Act
            double res = LSE.Loss(thetas, inputData, targetData);
            //Assert
            Assert.That(res, Is.EqualTo(0.0));
        }

        [Test]
        public void LeastSquareError_Loss_ReturnsUnsuccessfully()
        {
            //Arrange
            MLModelProduct.LeastSquareError LSE = new MLModelProduct.LeastSquareError();
            double[] thetas = { 3.0, 0.0 };
            double[][] inputData = { new double[] { 1, 2 }, new double[] { 2, 4 } };
            double[] targetData = inputData.Select(array => (double)array.GetValue(1)).ToArray();
            //Act
            double res = LSE.Loss(thetas, inputData, targetData);
            //Assert
            Assert.That(res, Is.Not.EqualTo(0.0));
        }

        [Test]
        public void LeastSquareError_LossDerivates_ReturnsSuccessfully()
        {
            //Arrange
            MLModelProduct.LeastSquareError LSE = new MLModelProduct.LeastSquareError();
            double[] thetas = { 2.0, 0.0 };
            double[][] inputData = { new double[] { 1, 2 }, new double[] { 2, 4 } };
            double[] targetData = inputData.Select(array => (double)array.GetValue(1)).ToArray();
            //Act
            double[] res = LSE.LossDerivates(thetas, inputData, targetData);
            System.Console.WriteLine(res[0]+" " + res[1]);
            //Assert
            Assert.That(res, Is.EqualTo(new double[] { 0.0,0.0}));
        }
    }
}