using OxyPlot;
using OxyPlot.Axes;

namespace LinearRegressionWPF.Models
{
    class RegressionPlot
    {
        private DataSet _dataSet;
        private RegressionLine _regressionLine;

        public PlotModel OxyModel { get; private set; }

        public RegressionPlot()
        {
            _dataSet = new DataSet();
            _regressionLine = RegressionLine.NullRegressionLine();

            OxyModel = new PlotModel { Title = "Regression Plot" };
            OxyModel.Series.Add(_dataSet.ScatterSeries);
            OxyModel.Series.Add(_regressionLine.LineSeries);
            OxyModel.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Bottom,
                MinimumPadding = 0.05,
                MaximumPadding = 0.05
            });
            OxyModel.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Left,
                MinimumPadding = 0.05,
                MaximumPadding = 0.05
            });
        }

        public void updateDataSet(DataSet dataSet)
        {
            OxyModel.Series.Remove(_dataSet.ScatterSeries);
            _dataSet = dataSet;
            OxyModel.Series.Add(_dataSet.ScatterSeries);
            OxyModel.InvalidatePlot(true);
        }

        public void updateDataSet(double[][] data)
        {
            DataSet dataSet = new DataSet();

            foreach (double[] dataPoint in data)
            {
                dataSet.addDataPoint(dataPoint[0], dataPoint[1]);
            }

            updateDataSet(dataSet);
        }

        public void addDataPoint(double[] dataPoint)
        {
            _dataSet.addDataPoint(dataPoint[0], dataPoint[1]);
            OxyModel.InvalidatePlot(true);
        }

        public void updateRegressionLine(RegressionLine regressionLine)
        {
            OxyModel.Series.Remove(_regressionLine.LineSeries);
            _regressionLine = regressionLine;
            OxyModel.Series.Add(regressionLine.LineSeries);

            // OxyPlot does not support z-index,
            // so the datapoints are refreshed to be on top
            OxyModel.Series.Remove(_dataSet.ScatterSeries);
            OxyModel.Series.Add(_dataSet.ScatterSeries);

            OxyModel.InvalidatePlot(true);
        }

        public void updateRegressionLine(double slope, double yIntercept)
        {
            double LOWER_BOUND = 0;
            double UPPER_BOUND = 10;
            updateRegressionLine(new RegressionLine(slope, yIntercept, LOWER_BOUND, UPPER_BOUND));
        }
    }
}
