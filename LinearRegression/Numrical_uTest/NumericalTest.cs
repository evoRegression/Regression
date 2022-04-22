using LinearRegressionBackend.DataProvider;
using NUnit.Framework;

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
        public void Arrange_ReturnsSuccessfully()
        {
            // Arrange
            double start = -55.5, stop = 55.5, step = 9.1;

            // Act
            double[] test_array = Numerical.Arange(start, stop, step);

            // Assert
            for (int i = 1; i < test_array.Length; i++)
            {
                Assert.AreEqual(((decimal)test_array[i] - (decimal)test_array[i - 1]), (decimal)9.1, "The array wasn't genereated properly, at the {0} th element", i);
            }

        }

        [Test]
        public void Mean_DoubleTwoDimensionalMatrix_ReturnsSuccessfully()
        {
            // Arrange
            double[][] testMatrix = new double[5][];
            testMatrix[0] = new double[2];
            testMatrix[1] = new double[2];
            testMatrix[2] = new double[2];
            testMatrix[3] = new double[2];
            testMatrix[4] = new double[2];
            testMatrix[0][0] = -1; testMatrix[0][1] = 82;
            testMatrix[1][0] = 3; testMatrix[1][1] = 93;
            testMatrix[2][0] = 5; testMatrix[2][1] = 82;
            testMatrix[3][0] = 7; testMatrix[3][1] = 89;
            testMatrix[4][0] = 9.8; testMatrix[4][1] = 88;

            // Act
            double actualMeanIfAxisIs0 = Numerical.Mean(testMatrix, 0);
            double actualMeanIfAxisIs1 = Numerical.Mean(testMatrix, 1);

            // Assert
            Assert.AreEqual(4.76, actualMeanIfAxisIs0, "The mean is other than expected!");
            Assert.AreEqual(86.8, actualMeanIfAxisIs1, "The mean is other than expected!");
        }

        [Test]
        public void Median_DoubleTwoDimensionalMatrix_ReturnsSuccessfully()
        {
            // Arrange
            double[][] testMatrixOdd = new double[5][];
            testMatrixOdd[0] = new double[2];
            testMatrixOdd[1] = new double[2];
            testMatrixOdd[2] = new double[2];
            testMatrixOdd[3] = new double[2];
            testMatrixOdd[4] = new double[2];
            testMatrixOdd[0][0] = -1; testMatrixOdd[0][1] = 82;
            testMatrixOdd[1][0] = 3; testMatrixOdd[1][1] = 93;
            testMatrixOdd[2][0] = 5; testMatrixOdd[2][1] = 82;
            testMatrixOdd[3][0] = 7; testMatrixOdd[3][1] = 89;
            testMatrixOdd[4][0] = 9.8; testMatrixOdd[4][1] = 88;
            double[][] testMatrixEven = new double[6][];
            testMatrixEven[0] = new double[2];
            testMatrixEven[1] = new double[2];
            testMatrixEven[2] = new double[2];
            testMatrixEven[3] = new double[2];
            testMatrixEven[4] = new double[2];
            testMatrixEven[5] = new double[2];
            testMatrixEven[0][0] = -1; testMatrixEven[0][1] = 82;
            testMatrixEven[1][0] = 3; testMatrixEven[1][1] = 93;
            testMatrixEven[2][0] = 5; testMatrixEven[2][1] = 82;
            testMatrixEven[3][0] = 7; testMatrixEven[3][1] = 89;
            testMatrixEven[4][0] = 9.8; testMatrixEven[4][1] = 88;
            testMatrixEven[5][0] = 11.8; testMatrixEven[5][1] = 10000.101;

            // Act
            double actualMedianOddIfAxisIs0 = Numerical.Median(testMatrixOdd, 0);
            double actualMedianOddIfAxisIs1 = Numerical.Median(testMatrixOdd, 1);
            double actualMedianEvenIfAxisIs0 = Numerical.Median(testMatrixEven, 0);
            double actualMedianEvenIfAxisIs1 = Numerical.Median(testMatrixEven, 1);

            // Assert
            Assert.AreEqual(5, actualMedianOddIfAxisIs0, "The mean in case of the x axis of an odd matrix is other than expected!");
            Assert.AreEqual(88, actualMedianOddIfAxisIs1, "The mean in case of the y axis of an odd matrix is other than expected!");
            Assert.AreEqual(6, actualMedianEvenIfAxisIs0, "The mean in case of the x axis of an even matrix is other than expected!");
            Assert.AreEqual(88.5, actualMedianEvenIfAxisIs1, "The mean in case of the y axis of an even matrix is other than expected!");
        }

        [Test]
        public void StandartDeviation_DoubleTwoDimensionalMatrix_ReturnsSuccessfully()
        {
            // Arrange
            double[][] testMatrix = new double[5][];
            testMatrix[0] = new double[2];
            testMatrix[1] = new double[2];
            testMatrix[2] = new double[2];
            testMatrix[3] = new double[2];
            testMatrix[4] = new double[2];
            testMatrix[0][0] = -1; testMatrix[0][1] = 82;
            testMatrix[1][0] = 3; testMatrix[1][1] = 93;
            testMatrix[2][0] = 5; testMatrix[2][1] = 93;
            testMatrix[3][0] = 7; testMatrix[3][1] = 89;
            testMatrix[4][0] = 9.7; testMatrix[4][1] = 88.1;

            // Act
            double actualDeviationIfAxisIs0 = Numerical.StandardDeviation(testMatrix, 0);
            double actualDeviationIfAxisIs1 = Numerical.StandardDeviation(testMatrix, 1);
            bool xAxisIsCorrect = true;
            bool yAxisIsCorrect = true;
            if (actualDeviationIfAxisIs0 > 3.6264 || actualDeviationIfAxisIs0 < 3.6263)
            {
                xAxisIsCorrect = false;
            }

            if (actualDeviationIfAxisIs1 > 4.0450 || actualDeviationIfAxisIs1 < 4.0449)
            {
                yAxisIsCorrect = false;
            }

            // Assert
            Assert.AreEqual(true, xAxisIsCorrect, "The x-axis' deviation is other than expected!");
            Assert.AreEqual(true, yAxisIsCorrect, "The y-xis' deviation is other than expected!");
        }

        [Test]
        public void Variance_DoubleTwoDimensionalMatrix_ReturnsSuccessfully()
        {
            // Arrange
            double[][] testMatrix = new double[5][];
            testMatrix[0] = new double[2];
            testMatrix[1] = new double[2];
            testMatrix[2] = new double[2];
            testMatrix[3] = new double[2];
            testMatrix[4] = new double[2];
            testMatrix[0][0] = -1; testMatrix[0][1] = 82;
            testMatrix[1][0] = 3; testMatrix[1][1] = 93;
            testMatrix[2][0] = 5; testMatrix[2][1] = 93;
            testMatrix[3][0] = 7; testMatrix[3][1] = 89;
            testMatrix[4][0] = 9.7; testMatrix[4][1] = 88.1;

            // Act
            double actualVarianceIfAxisIs0 = Numerical.Variance(testMatrix, 0);
            double actualVarianceIfAxisIs1 = Numerical.Variance(testMatrix, 1);

            // Assert
            Assert.AreEqual(10.7, actualVarianceIfAxisIs0, "The Variance is other than expected!");
            Assert.AreEqual(11, actualVarianceIfAxisIs1, "The Variance is other than expected!");
        }

        [Test]
        public void Min_DoubleTwoDimensionalMatrix_ReturnsSuccessfully()
        {
            // Arrange
            double[][] testMatrix = new double[5][];
            testMatrix[0] = new double[2];
            testMatrix[1] = new double[2];
            testMatrix[2] = new double[2];
            testMatrix[3] = new double[2];
            testMatrix[4] = new double[2];
            testMatrix[0][0] = -1; testMatrix[0][1] = 82;
            testMatrix[1][0] = 3; testMatrix[1][1] = 93;
            testMatrix[2][0] = 5; testMatrix[2][1] = 82;
            testMatrix[3][0] = 7; testMatrix[3][1] = 89;
            testMatrix[4][0] = 9; testMatrix[4][1] = 88;

            // Act
            double actualMinIfAxisIs0 = Numerical.Min(testMatrix, 0);
            double actualMinIfAxisIs1 = Numerical.Min(testMatrix, 1);

            // Assert
            Assert.AreEqual(-1, actualMinIfAxisIs0, "The minimum is other than expected!");
            Assert.AreEqual(82, actualMinIfAxisIs1, "The minimum is other than expected!");
        }

        [Test]
        public void Max_DoubleTwoDimensionalMatrix_ReturnsSuccessfully()
        {
            // Arrange
            double[][] testMatrix = new double[5][];
            testMatrix[0] = new double[2];
            testMatrix[1] = new double[2];
            testMatrix[2] = new double[2];
            testMatrix[3] = new double[2];
            testMatrix[4] = new double[2];
            testMatrix[0][0] = -1; testMatrix[0][1] = 82;
            testMatrix[1][0] = 3; testMatrix[1][1] = 93;
            testMatrix[2][0] = 5; testMatrix[2][1] = 93;
            testMatrix[3][0] = 7; testMatrix[3][1] = 89;
            testMatrix[4][0] = 9; testMatrix[4][1] = 88;

            // Act
            double actualMaxIfAxisIs0 = Numerical.Max(testMatrix, 0);
            double actualMaxIfAxisIs1 = Numerical.Max(testMatrix, 1);

            // Assert
            Assert.AreEqual(9, actualMaxIfAxisIs0, "The minimum is other than expected!");
            Assert.AreEqual(93, actualMaxIfAxisIs1, "The minimum is other than expected!");
        }
    }
}