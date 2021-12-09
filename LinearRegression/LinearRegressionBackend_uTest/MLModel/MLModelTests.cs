using NUnit.Framework;

using MLModelProduct = LinearRegressionBackend.MLModel;

namespace LinearRegressionBackend_uTest.MLModel
{
    public class MLModelTests
    {
        [OneTimeSetUp]
        public void OneTimeSetUp() { }

        [SetUp]
        public void Setup() { }

        [TearDown]
        public void TearDown() { }

        [OneTimeTearDown]
        public void OneTimeTearDown() { }

        [Test]
        public void SimpleOrdinaryLeastSquare_IntegerNumbers_ReturnsSuccessfully()
        {
            // Arrange

            // Act

            // Assert
            Assert.Pass();
        }

        [Test]
        public void SimpleOrdinaryLeastSquare_HappyPath()
        {
            // Arrange
            MLModelProduct.MLModel model = new MLModelProduct.MLModel(0, 0);
            double[] xAxis = new double[] { 1, 2, 3, 4 };
            double[] yAxis = new double[] { 2, 4, 6, 8 };

            // Act
            MLModelProduct.Coefficients actualCoefficient = model.SimpleOrdinaryLeastSquare(xAxis, yAxis);

            // Assert
            Assert.AreEqual(2, actualCoefficient.Slope, "The slope is other than expected!");
            Assert.That(actualCoefficient.Intercept, Is.EqualTo(0), "The intercept is other than expected!");
        }

        [Test]
        public void SimpleOrdinaryLeastSquare_DoubleNumbers_ReturnsSuccessfully()
        {
            // Arrange

            // Act

            // Assert
            Assert.Pass();
        }
    }
}