using OxyPlot;

namespace LinearRegressionWPF.Models
{
    class RegressionPlot
    {
        private DataSet _dataSet;
        private RegressionLine _regressionLine;
        public PlotModel OxyModel { get; private set; }

        public RegressionPlot()
        {
            OxyModel = new PlotModel { Title = "Regression Plot" };
            _dataSet = new DataSet();
            OxyModel.Series.Add(_dataSet.ScatterSeries);
            _regressionLine = new RegressionLine(1, 0, 0, 10);
            OxyModel.Series.Add(_regressionLine.LineSeries);
        }

        public void updateDataSet(DataSet dataSet)
        {
            OxyModel.Series.Remove(_dataSet.ScatterSeries);
            OxyModel.Series.Add(dataSet.ScatterSeries);
            _dataSet = dataSet;
        }

        public void updateRegressionLine(RegressionLine regressionLine)
        {
            OxyModel.Series.Remove(_regressionLine.LineSeries);
            OxyModel.Series.Add(regressionLine.LineSeries);
            _regressionLine = regressionLine;
        }
    }
}
