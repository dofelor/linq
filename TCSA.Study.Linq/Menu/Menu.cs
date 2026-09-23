using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;
using TCSA.Study.Linq.Printing;
using TCSA.Study.Linq.Seeders;

namespace TCSA.Study.Linq.Menu
{
    public static class Menu
    {
        public static void MainMenu()
        {
            List<Stock> stocks = StockSeeder.GetStocks();
            List<Trade> trades = stocks
                .SelectMany(stock => stock.Trades)
                .ToList();

            // SANDBOX - Write your code here
            //

            const string stockOption = "Print stocks";
            const string tradeOption = "Print all trades";
            const string reportMenuOption = "Print stock report";
            const string exitOption = "Exit";

            while (true)
            {
                string selectedOption = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[bold yellow]What would you like to view?[/]")
                        .AddChoices(stockOption, tradeOption, reportMenuOption, exitOption));

                AnsiConsole.Clear();

                if (selectedOption == exitOption)
                {
                    break;
                }

                if (selectedOption == stockOption)
                {
                    TablePrinter.PrintStocks(stocks);
                }
                else if (selectedOption == tradeOption)
                {
                    TablePrinter.PrintTrades(trades);
                }
                else if (selectedOption == reportMenuOption)
                {
                    ReportMenu(stocks, trades);
                }


                AnsiConsole.WriteLine();
            }
        }

        public static void ReportMenu(List<Stock> stocks, List<Trade> trades)
        {
            const string backToMenu = "Back to menu";
            const string portfolioSummary = "Portfolio Summary";
            const string stocksBySector = "Stocks By Sector";
            const string tradesBySymbol = "Trades By Symbol";
            const string highVolumeTrades = "High-Volume Trades";
            const string stockActivityReport = "Stock Activity Report";
            const string uniqueValuesReport = "Unique Values Report";
            const string dataQualityReport = "Data Quality Report";


            bool closeMenuReport = false;

            while (!closeMenuReport)
            {
                string selectedOption = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Select the report you want to view.")
                    .AddChoices(backToMenu, portfolioSummary, stocksBySector, tradesBySymbol, highVolumeTrades,
                    stockActivityReport, uniqueValuesReport, dataQualityReport));

                switch (selectedOption)
                {
                    case backToMenu:
                        closeMenuReport = true;
                        AnsiConsole.Clear();
                        break;
                    case portfolioSummary:
                        ReportPrinter.PrintPortfolioSummary(stocks, trades);
                        break;
                    case stocksBySector:
                        ReportPrinter.PrintStocksBySector(stocks);
                        break;
                    case tradesBySymbol:
                        ReportPrinter.PrintTradesBySymbol(trades);
                        break;
                    case highVolumeTrades:
                        ReportPrinter.PrintHighVolumeTrades(trades);
                        break;
                    case stockActivityReport:
                        ReportPrinter.PrintStockActivityReport(stocks);
                        break;
                    case uniqueValuesReport:
                        ReportPrinter.PrintUniqueValuesReport(stocks);
                        break;
                    case dataQualityReport:
                        ReportPrinter.PrintDataQualityReport(stocks, trades);
                        break;
                }
            }

        }
    }
}
