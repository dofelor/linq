using Spectre.Console;
using TCSA.Study.Linq.Printing;
using TCSA.Study.Linq;
using TCSA.Study.Linq.Seeders;

List<Stock> stocks = StockSeeder.GetStocks();
List<Trade> trades = TradeSeeder.GetTrades();

const string stockOption = "Print stocks";
const string tradeOption = "Print all trades";
const string exitOption = "Exit";

while (true)
{
    string selectedOption = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
            .Title("[bold yellow]What would you like to view?[/]")
            .AddChoices(stockOption, tradeOption, exitOption));

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

    AnsiConsole.WriteLine();
}
