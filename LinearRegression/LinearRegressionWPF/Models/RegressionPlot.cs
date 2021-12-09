using OxyPlot;
using OxyPlot.Axes;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LinearRegressionWPF.Models
{
    class RegressionPlot : INotifyPropertyChanged
    {
        private DataSet _dataSet;
        private RegressionLine _regressionLine;

        public PlotModel OxyModel { get; private set; }

        public RegressionPlot()
        {
            _dataSet = new DataSet();
            _regressionLine = RegressionLine.NullRegressionLine();
            OxyModel = createOxyModel(_dataSet, _regressionLine);
        }

        private PlotModel createOxyModel(DataSet dataSet, RegressionLine regressionLine)
        {
            PlotModel model = new PlotModel { Title = "Regression Plot" };
            model.Series.Add(dataSet.ScatterSeries);
            model.Series.Add(regressionLine.LineSeries);
            model.Axes.Add(new LinearAxis {
                Position = AxisPosition.Bottom,
                MinimumPadding = 0.05,
                MaximumPadding = 0.05
            });
            model.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Left,
                MinimumPadding = 0.05,
                MaximumPadding = 0.05
            });
            return model;
        }

        private void updateOxyModel(DataSet dataSet, RegressionLine regressionLine)
        {
            OxyModel.Series.Remove(_dataSet.ScatterSeries);
            OxyModel.Series.Remove(_regressionLine.LineSeries);
            OxyModel = createOxyModel(dataSet, regressionLine);
        }

        public void updateDataSet(DataSet dataSet)
        {
            updateOxyModel(dataSet, _regressionLine);
            _dataSet = dataSet;
            NotifyPropertyChanged(nameof(OxyModel));
        }

        public void updateRegressionLine(RegressionLine regressionLine)
        {
            updateOxyModel(_dataSet, regressionLine);
            _regressionLine = regressionLine;
            NotifyPropertyChanged(nameof(OxyModel));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
