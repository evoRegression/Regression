namespace DataProvider
{
    public interface IDataProvider
    {
        double Mean();
        double Median();
        double StandardDeviation();
        double Variance();
        double[][] Import(string filePath);
    }
}