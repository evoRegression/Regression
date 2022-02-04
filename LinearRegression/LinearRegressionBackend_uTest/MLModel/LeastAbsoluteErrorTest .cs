using NUnit.Framework;
using System.Linq;
using MLModelProduct = LinearRegressionBackend.MLModel;

namespace LinearRegressionBackend_uTest.MLModel
{
    public class LeastAbsoluteErrorTests
    {
        [Test]
        public void LeastAbsoluteError_Loss_ReturnsSuccessfully()
        {
            //Arrange
            MLModelProduct.LeastAbsoluteError LSE = new MLModelProduct.LeastAbsoluteError();
            double[] thetas = { 2.0, 0.0 };
            double[][] inputData = { new double[]{ 1, 2 }, new double[]{ 2, 4 } };
            double[] targetData = inputData.Select(array => (double)array.GetValue(1)).ToArray();
            //Act
            double res = LSE.Loss(thetas, inputData, targetData);
            //Assert
            Assert.That(res, Is.EqualTo(0.0));
        }

        [Test]
        public void LeastAbsoluteError_Loss_ReturnsUnsuccessfully()
        {
            //Arrange
            MLModelProduct.LeastAbsoluteError LSE = new MLModelProduct.LeastAbsoluteError();
            double[] thetas = { 3.0, 0.0 };
            double[][] inputData = { new double[] { 1, 2 }, new double[] { 2, 4 } };
            double[] targetData = inputData.Select(array => (double)array.GetValue(1)).ToArray();
            //Act
            double res = LSE.Loss(thetas, inputData, targetData);
            //Assert
            Assert.That(res, Is.Not.EqualTo(0.0));
        }

        [Test]
        public void LeastAbsoluteError_LossDerivative_ReturnsSuccessfully()
        {
            //Arrange
            MLModelProduct.LeastAbsoluteError LSE = new MLModelProduct.LeastAbsoluteError();
            double[] thetas = { 0.0, 0.0 };
            double[][] inputData = { new double[] { 1, 2 }, new double[] { 2, 4 } };
            double[] targetData = inputData.Select(array => (double)array.GetValue(1)).ToArray();
            //Act
            double[] res = LSE.LossDerivates(thetas, inputData, targetData);
            //Assert
            Assert.That(res, Is.EqualTo(new double[] { -1.5, -1.0 }));
        }

        [Test]
        public void LeastAbsoluteError_LossDerivative_ReturnsUnsuccessfully()
        {
            //Arrange
            MLModelProduct.LeastAbsoluteError LSE = new MLModelProduct.LeastAbsoluteError();
            double[] thetas = { 0.0, 0.0 };
            double[][] inputData = { new double[] { 1.5, 2 }, new double[] { 2, 4 } };
            double[] targetData = inputData.Select(array => (double)array.GetValue(1)).ToArray();
            //Act
            double[] res = LSE.LossDerivates(thetas, inputData, targetData);
            //Assert
            Assert.That(res, Is.Not.EqualTo(new double[] { -1.5, -1.0 }));
        }
    }
}