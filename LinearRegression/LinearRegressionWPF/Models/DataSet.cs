using OxyPlot.Series;

namespace LinearRegressionWPF.Models
{
    class DataSet
    {
        public ScatterSeries ScatterSeries { get; private set; }

        public DataSet()
        {
            ScatterSeries = new ScatterSeries { MarkerType = OxyPlot.MarkerType.Circle };
        }

        public void addDataPoint(double dataX, double dataY)
        {
            ScatterSeries.Points.Add(new ScatterPoint(dataX, dataY));
        }
    }
}
