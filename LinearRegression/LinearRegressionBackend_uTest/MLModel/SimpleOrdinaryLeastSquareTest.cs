using NUnit.Framework;
using System.Linq;
using MLModelProduct = LinearRegressionBackend.MLModel;

namespace LinearRegressionBackend_uTest.MLModel
{
    public class SimpleOrdinaryLeastSquareTests
    {
        [Test]
        public void SimpleOrdinaryLeastSquare_ReturnsSuccessfully()
        {
            //Arrange
            MLModelProduct.SimpleOrdinaryLeastSquare SOLS = new MLModelProduct.SimpleOrdinaryLeastSquare();
            double[] thetas = { 0.0, 0.0 };
            double[][] inputData = { new double[] { 1.0, 2.0 }, new double[] { 2.0, 4.0 } };
            double[] targetData = inputData.Select(array => (double)array.GetValue(1)).ToArray();
            //Act
            double[] res = SOLS.Minimize(null, thetas, inputData, targetData);

            //Assert
            Assert.That(res, Is.EqualTo(new double[] { 2.0, 0.0 }));
        }
    }
}