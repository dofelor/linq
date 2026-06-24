namespace TCSA.Study.Linq.Seeders;

public static class StockSeeder
{
    public static List<Stock> GetStocks()
    {
        return new List<Stock>
        {
            new Stock
            {
                Symbol = "NVDA",
                CompanyName = "Nvidia Corporation",
                Sector = "Technology"
            },
            new Stock
            {
                Symbol = "GOOGL",
                CompanyName = "Alphabet Inc.",
                Sector = "Communication Services"
            },
            new Stock
            {
                Symbol = "AAPL",
                CompanyName = "Apple Inc.",
                Sector = "Technology"
            },
            new Stock
            {
                Symbol = "MSFT",
                CompanyName = "Microsoft Corporation",
                Sector = "Technology"
            },
            new Stock
            {
                Symbol = "AMZN",
                CompanyName = "Amazon.com, Inc.",
                Sector = "Consumer Discretionary"
            },
            new Stock
            {
                Symbol = "AVGO",
                CompanyName = "Broadcom Inc.",
                Sector = "Technology"
            },
            new Stock
            {
                Symbol = "META",
                CompanyName = "Meta Platforms, Inc.",
                Sector = "Communication Services"
            },
            new Stock
            {
                Symbol = "TSLA",
                CompanyName = "Tesla, Inc.",
                Sector = "Consumer Discretionary"
            },
            new Stock
            {
                Symbol = "NASDAQ",
                CompanyName = "Nasdaq Composite",
                Sector = "Index"
            },
            new Stock
            {
                Symbol = "SP500",
                CompanyName = "S&P 500",
                Sector = "Index"
            }
        };
    }
}
