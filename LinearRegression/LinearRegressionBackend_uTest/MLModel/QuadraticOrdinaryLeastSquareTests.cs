using System.Linq;

using NUnit.Framework;

using LinearRegressionBackend.MLModel;

namespace LinearRegressionBackend_uTest.MLModel
{
    public class QuadraticOrdinaryLeastSquareTests
    {
        [Test]
        public void QuadraticOrdinaryLeastSquare_ReturnsSuccessfully()
        {
            //Arrange
            QuadraticOrdinaryLeastSquare quadraticLeastSquare = new QuadraticOrdinaryLeastSquare();
            double[] thetas = { 0.0, 0.0 };
            double[][] inputData = { new double[]{ 1.0, 2.0 }, new double[]{ 2.0, 4.0 } };
            double[] targetData = inputData.Select(array => (double)array.GetValue(1)).ToArray();

            //Act
            double[] res = quadraticLeastSquare.Minimize(inputData, targetData);

            //Assert
            Assert.That(res, Is.EqualTo(new double[] { 2.0, 0.0 }));
        }
    }
}