namespace DataProvider
{
    public interface IDataProvider
    {
        double MeanXAxis();
        double MeanYAxis();
        double MedianXAxis();
        double MedianYAxis();
        double StandardDeviation();
        double Variance();
        double[][] Import(string filePath);
    }
}