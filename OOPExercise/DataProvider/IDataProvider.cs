namespace DataProvider
{
    public interface IDataProvider
    {
        int[] Read(string filePath);
        double Mean();
        double Median();
        double StandardDeviation();
        double Variance();
    }
}