using Spectre.Console;

namespace TCSA.Study.Linq.Printing;

public static class TablePrinter
{
    public static void PrintStocks(List<Stock> stocks)
    {
        Table table = new Table()
            .Border(TableBorder.Rounded)
            .Title("[bold yellow]Wall Street Watchlist[/]");

        table.AddColumn("[bold]Symbol[/]");
        table.AddColumn("[bold]Company[/]");
        table.AddColumn("[bold]Sector[/]");

        foreach (Stock stock in stocks)
        {
            table.AddRow(
                $"[cyan]{Markup.Escape(stock.Symbol)}[/]",
                Markup.Escape(stock.CompanyName),
                Markup.Escape(stock.Sector));
        }

        AnsiConsole.Write(table);
    }

    public static void PrintTrades(List<Trade> trades)
    {
        Table table = new Table()
            .Border(TableBorder.Rounded)
            .Title("[bold yellow]Trades[/]");

        table.AddColumn("[bold]Id[/]");
        table.AddColumn("[bold]Symbol[/]");
        table.AddColumn("[bold]Date[/]");
        table.AddColumn("[bold]Price[/]");
        table.AddColumn("[bold]Quantity[/]");
        table.AddColumn("[bold]Type[/]");

        foreach (Trade trade in trades)
        {
            table.AddRow(
                trade.Id.ToString(),
                $"[cyan]{Markup.Escape(trade.Symbol)}[/]",
                trade.Date.ToString("yyyy-MM-dd"),
                trade.Price.ToString("C"),
                trade.Quantity.ToString(),
                trade.Type.ToString());
        }

        AnsiConsole.Write(table);
    }
}
