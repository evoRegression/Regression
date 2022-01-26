using OxyPlot.Series;

namespace LinearRegressionWPF.Models
{
    class DataSet
    {
        public ScatterSeries ScatterSeries { get; private set; }

        public DataSet() : this(OxyPlot.OxyColor.Parse("#c88d00")) { }

        public DataSet(OxyPlot.OxyColor markerFill) {
            ScatterSeries = new ScatterSeries { MarkerType = OxyPlot.MarkerType.Circle, MarkerFill = markerFill };
        }

        public void addDataPoint(double dataX, double dataY)
        {
            ScatterSeries.Points.Add(new ScatterPoint(dataX, dataY));
        }
    }
}
