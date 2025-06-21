public class StockAverage
{
    public string Category { get; private set; }
    public double AverageQuantity { get; private set; }

    public StockAverage(string category, double averageQuantity)
    {
        Category = category;
        AverageQuantity = averageQuantity;
    }
}