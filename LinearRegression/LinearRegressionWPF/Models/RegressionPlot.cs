using System.Collections.Generic;
using System.Linq;
using OxyPlot;
using OxyPlot.Axes;

using LinearRegressionBackend.DataProvider;

namespace LinearRegressionWPF.Models
{
    class RegressionPlot
    {
        private DataSet _dataSet;
        private DataSet _predictedDataSet;
        private RegressionLine _regressionLine;
        private LinearAxis _xAxis;
        private const double X_MIN_DEFAULT = 0;
        private const double X_MAX_DEFAULT = 1;
        private LinearAxis _yAxis;
        private const double Y_MIN_DEFAULT = 0;
        private const double Y_MAX_DEFAULT = 1;
        private const double AXIS_PADDING = 0.05;

        public double XMin
        {
            get => _xAxis.ClipMinimum;
        }

        public double XMax
        {
            get => _xAxis.ClipMaximum;
        }

        public double XRange
        {
            get => XMax - XMin;
        }

        public double YMin
        {
            get => _yAxis.ClipMinimum;
        }

        public double YMax
        {
            get => _yAxis.ClipMaximum;
        }

        public double YRange
        {
            get => YMax - YMin;
        }

        public PlotModel OxyModel { get; private set; }

        public RegressionPlot()
        {
            _dataSet = new DataSet();
            _predictedDataSet = new DataSet(OxyPlot.OxyColor.Parse("#cc0000"));
            _regressionLine = RegressionLine.NullRegressionLine();

            OxyModel = new PlotModel { Title = "Regression Plot" };

            OxyModel.Series.Add(_dataSet.ScatterSeries);
            OxyModel.Series.Add(_predictedDataSet.ScatterSeries);
            OxyModel.Series.Add(_regressionLine.LineSeries);

            _xAxis = new LinearAxis {
                Position = AxisPosition.Bottom,
                Minimum = X_MIN_DEFAULT,
                Maximum = X_MAX_DEFAULT
            };
            _yAxis = new LinearAxis {
                Position = AxisPosition.Left,
                Minimum = Y_MIN_DEFAULT,
                Maximum = Y_MAX_DEFAULT
            };
            OxyModel.Axes.Add(_xAxis);
            OxyModel.Axes.Add(_yAxis);
        }

        public void updateDataSet(DataSet dataSet)
        {
            OxyModel.Series.Remove(_dataSet.ScatterSeries);
            _dataSet = dataSet;
            OxyModel.Series.Add(dataSet.ScatterSeries);
            clearPredictions();
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

        public void addPredictedPoint(double[] dataPoint)
        {
            _predictedDataSet.addDataPoint(dataPoint[0], dataPoint[1]);
            fitAxisBoundsToData();
            OxyModel.InvalidatePlot(true);
        }

        public void clearPredictions()
        {
            OxyModel.Series.Remove(_predictedDataSet.ScatterSeries);
            _predictedDataSet = new DataSet(OxyPlot.OxyColor.Parse("#cc0000"));
            OxyModel.Series.Add(_predictedDataSet.ScatterSeries);
            fitAxisBoundsToData();
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
            OxyModel.Series.Remove(_predictedDataSet.ScatterSeries);
            OxyModel.Series.Add(_predictedDataSet.ScatterSeries);

            OxyModel.InvalidatePlot(true);
        }

        public void updateRegressionLine(double slope, double yIntercept)
        {
            updateRegressionLine(new RegressionLine(slope, yIntercept, _xAxis.ClipMinimum, _xAxis.ClipMaximum));
        }

        private void updateAxisBounds(double xMin, double xMax, double yMin, double yMax)
        {
            _xAxis.Minimum = xMin;
            _xAxis.Maximum = xMax;
            _yAxis.Minimum = yMin;
            _yAxis.Maximum = yMax;
        }

        private void fitAxisBoundsToData() {
            List<double[]> dataPoints = _dataSet.ScatterSeries.Points.ConvertAll(point => new double[] { point.X, point.Y });
            List<double[]> predictedPoints = _predictedDataSet.ScatterSeries.Points.ConvertAll(point => new double[] { point.X, point.Y });

            double[][] allPoints = dataPoints.Concat(predictedPoints).ToArray();

            double xMin = Numerical.Min(allPoints, 0);
            double xMax = Numerical.Max(allPoints, 0);
            double yMin = Numerical.Min(allPoints, 1);
            double yMax = Numerical.Max(allPoints, 1);

            double xRange = xMax - xMin;
            double yRange = yMax - yMin;

            xMin -= xRange * AXIS_PADDING;
            xMax += xRange * AXIS_PADDING;
            yMin -= yRange * AXIS_PADDING;
            yMax += yRange * AXIS_PADDING;

            updateAxisBounds(xMin, xMax, yMin, yMax);
        }
    }
}
