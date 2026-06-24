using TCSA.Study.Linq.Models.Enums;

namespace TCSA.Study.Linq;

public class Trade
{
    public int Id { get; set; }
    public string Symbol { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public TradeType Type { get; set; }
}
