using TCSA.Study.Linq.Models.Enums;

namespace TCSA.Study.Linq.Seeders;

public static class TradeSeeder
{
    public static List<Trade> GetTrades()
    {
        List<string> symbols = StockSeeder.GetStocks()
            .Select(stock => stock.Symbol)
            .ToList();

        Random random = new Random(42);
        DateTime startDate = DateTime.Today.AddYears(-1);
        List<Trade> trades = new List<Trade>();

        for (int id = 1; id <= 500; id++)
        {
            trades.Add(new Trade
            {
                Id = id,
                Symbol = symbols[random.Next(symbols.Count)],
                Date = startDate.AddDays(random.Next(365)),
                Price = Math.Round((decimal)(random.NextDouble() * 950 + 50), 2),
                Quantity = random.Next(1, 1_001),
                Type = random.Next(2) == 0 ? TradeType.Buy : TradeType.Sell
            });
        }

        return trades;
    }
}
