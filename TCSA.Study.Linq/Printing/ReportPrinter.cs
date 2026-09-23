using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using TCSA.Study.Linq.Models.Rows;
using TCSA.Study.Linq.Printing;

namespace TCSA.Study.Linq.Printing;

public static class ReportPrinter
{
    public static void PrintPortfolioSummary(List<Stock> stocks, List<Trade> trades)
    {
        AnsiConsole.Clear();

        Table table = new Table()
            .Border(TableBorder.Rounded)
            .Title("Portfolio Summary");

        table.AddColumn("Total stocks");
        table.AddColumn("Total trades");
        table.AddColumn("Total quantity traded");
        table.AddColumn("Total trade value");
        table.AddColumn("Average trade price");
        table.AddColumn("Lowest trade price");
        table.AddColumn("Highest trade price");
        table.AddColumn("Earliest trade date");
        table.AddColumn("Most recent trade date");

        table.AddRow(
            stocks.Count().ToString(),
            trades.Count().ToString(),
            trades.Sum(t => t.Quantity).ToString(),
            trades.Sum(t => t.Quantity * t.Price).ToString("F2"),
            trades.Average(t => t.Price).ToString("F2"),
            trades.Min(t => t.Price).ToString("C2", CultureInfo.GetCultureInfo("en-US")),
            trades.Max(t => t.Price).ToString("C2", CultureInfo.GetCultureInfo("en-US")),
            trades.Min(t => t.Date).ToString("yyyy-MM-dd"),
            trades.Max(t => t.Date).ToString("yyyy-MM-dd")
            );

        AnsiConsole.Write(table);
    }

    public static void PrintStocksBySector(List<Stock> stocks)
    {

        AnsiConsole.Clear();
        const string backToMenu = "Back to menu";

        string choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("Select a sector:")
            .AddChoices(backToMenu)
            .AddChoices(stocks.Select(s => s.Sector).Distinct().ToList())
            );

        TablePrinter.PrintStocks(stocks.Where(s => s.Sector == choice).OrderBy(s => s.CompanyName).ToList());
    }

    public static void PrintTradesBySymbol(List<Trade> trades)
    {
        AnsiConsole.Clear();
        string userSymbolChoice = AnsiConsole.Ask<string>("Enter the symbol of the company you want to find: ");

        var symbol = trades.FirstOrDefault(s => s.Symbol.Equals(userSymbolChoice, StringComparison.CurrentCultureIgnoreCase));
        if (symbol != null)
        {
            TablePrinter.PrintTrades(trades.Where(t => t.Symbol == symbol.Symbol).OrderByDescending(t => t.Date).ToList());
        }
        else
        {
            AnsiConsole.Clear();
            AnsiConsole.MarkupLine($"Company not found");
        }
    }

    public static void PrintHighVolumeTrades(List<Trade> trades)
    {
        AnsiConsole.Clear();

        int userQuantityChoice = AnsiConsole.Ask<int>("Enter the minimum quantity: ");

        TablePrinter.PrintTrades(trades.Where(t => t.Quantity >= userQuantityChoice).OrderBy(t => t.Symbol).ThenByDescending(t => t.Quantity).ToList());
    }

    public static void PrintStockActivityReport(List<Stock> stocks)
    {
        AnsiConsole.Clear();
        var reportRows = stocks
            .Select(stock => new StockActivityReportRow
            {
                Symbol = stock.Symbol,
                CompanyName = stock.CompanyName,
                Sector = stock.Sector,
                TradeCount = stock.Trades.Count(),
                TotalQuantity = stock.Trades.Sum(trade => trade.Quantity),
                TotalValue = stock.Trades.Sum(trade => trade.Price * trade.Quantity),
                HighestTradePrice = stock.Trades.Max(trade => trade.Price),
                LatestTradeDate = stock.Trades.Max(trade => trade.Date)
            })
            .OrderBy(row => row.Sector)
            .ThenBy(row => row.Symbol)
            .ToList();

        Table table = new Table()
           .Border(TableBorder.Rounded)
           .Title($"[bold yellow]Stock Activity Report[/]");

        table.AddColumn("Symbol");
        table.AddColumn("Company");
        table.AddColumn("Sector");
        table.AddColumn("Trade count");
        table.AddColumn("Total quantity");
        table.AddColumn("Total value");
        table.AddColumn("Highest trade price");
        table.AddColumn("Latest trade date");


        foreach (var rep in reportRows)
        {
            table.AddRow(
                $"[cyan]{Markup.Escape(rep.Symbol)}[/]",
                rep.CompanyName.ToString(),
                rep.Sector.ToString(),
                rep.TradeCount.ToString(),
                rep.TotalQuantity.ToString(),
                rep.TotalValue.ToString("C2", CultureInfo.GetCultureInfo("en-US")),
                rep.HighestTradePrice.ToString("C2", CultureInfo.GetCultureInfo("en-US")),
                rep.LatestTradeDate.ToString("yyyy-MM-dd"));
        }

        AnsiConsole.Write(table);


    }

    public static void PrintUniqueValuesReport(List<Stock> stocks)
    {
        AnsiConsole.Clear();

        var uniqueSectors = stocks.Select(s => s.Sector).Distinct().ToList();
        var uniqueStockSymbols = stocks.Select(s => s.Symbol).Distinct().ToList();
        var uniqueTradeSymbols = stocks.SelectMany(s => s.Trades).Select(t => t.Symbol).Distinct().ToList();
        var uniqueTradeTypes = stocks.SelectMany(s => s.Trades).Select(t => t.Type).Distinct().ToList();

        Table table = new Table()
           .Border(TableBorder.Rounded)
           .Title($"[bold yellow]Unique Values Report[/]");

        table.AddColumn("Unique sectors");
        table.AddColumn("Unique stock symbols");
        table.AddColumn("Unique trade symbols");
        table.AddColumn("Unique trade types");

        table.AddRow(
        string.Join(", ", uniqueSectors),
        string.Join(", ", uniqueStockSymbols),
        string.Join(", ", uniqueTradeSymbols),
        string.Join(", ", uniqueTradeTypes)
        );

        AnsiConsole.Write(table);


    }

    public static void PrintDataQualityReport(List<Stock> stocks, List<Trade> trades)
    {
        AnsiConsole.Clear();
        long userChoosenValue = AnsiConsole.Ask<long>("Enter a value that may be larger: ");
        string userChoosenSymbol = AnsiConsole.Ask<string>("Enter the symbol of the company you want to find: ");

        Table table = new Table()
           .Border(TableBorder.Rounded)
           .Title($"[bold yellow]Data quality report[/]");

        table.AddColumn("All stock have trades ");
        table.AddColumn("Any trades exceeds entered value ");
        table.AddColumn("Contains symbol ");

        bool allStocksHaveTrades = stocks.All(s => s.Trades.Any());
        bool anyTradeExceedsValue = trades.Any(t => t.Price * t.Quantity < userChoosenValue);
        bool containsSymbol = stocks.Select(s => s.Symbol).ToList().Contains(userChoosenSymbol.ToUpper());

        table.AddRow(
            allStocksHaveTrades.ToString(),
            anyTradeExceedsValue.ToString(),
            containsSymbol.ToString());

        AnsiConsole.Write(table);
    }
}
