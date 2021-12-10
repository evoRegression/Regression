using OxyPlot.Series;

namespace LinearRegressionWPF.Models
{
    class RegressionLine
    {
        public const double STEP = 0.01;
        public FunctionSeries LineSeries { get; private set; }

        public RegressionLine(double slope, double yIntercept, double lowerBound, double upperBound)
        {
            LineSeries = new FunctionSeries((x) => (slope * x) + yIntercept, lowerBound, upperBound, STEP, "Regression Line");
        }

        public static RegressionLine NullRegressionLine()
        {
            return new RegressionLine(0, 0, 0, 0);
        }
    }
}
