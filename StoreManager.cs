
using Spectre.Console;

namespace E_handelsbutik
{
    public class StoreManager
    {
        public void AddItemToStore(List<Item> allItems)
        {
            // Fråga om produktens namn
            AnsiConsole.MarkupLine("[bold cyan]Ange produktens namn:[/]");
            string nameToAdd = Console.ReadLine()!;

            // Fråga om produktens pris
            AnsiConsole.MarkupLine("[bold cyan]Ange produktens pris:[/]");
            int priceToAdd = Convert.ToInt32(Console.ReadLine());

            // Skapa och lägg till produkten
            Item newItem = new Item(nameToAdd, priceToAdd);
            allItems.Add(newItem);

            // Bekräftelse
            AnsiConsole.MarkupLine($"[bold green]Produkten '{nameToAdd}' har lagts till i butiken.[/]");
        }
        public void RemoveItemFromStore(List<Item> allItems)
        {
            // Kontrollera om det finns några produkter
            if (allItems.Count == 0)
            {
                AnsiConsole.MarkupLine("[bold red]Det finns inga produkter att ta bort.[/]");
                return;
            }

            // Använd en Selection Prompt för att välja produkt med pilar
            var selectedProduct = AnsiConsole.Prompt(
                new SelectionPrompt<Item>()
                    .Title("[bold cyan]Välj en produkt att ta bort[/]")
                    .PageSize(10) // Maximalt antal produkter som visas på en sida
                    .MoreChoicesText("[grey](Tryck på pilarna för att välja)[/]")
                    .AddChoices(allItems) // Lägg till alla produkter som val
                    .UseConverter(item => $"{item.Name} - {item.Price} kr") // Konvertera produkten till en användbar sträng
            );

            // Bekräfta och ta bort den valda produkten
            allItems.Remove(selectedProduct);
            AnsiConsole.MarkupLine($"[bold green]Produkten '{selectedProduct.Name}' har tagits bort från butiken.[/]");
        }

        public void ChangeItemInStore(List<Item> allItems)
        {
            // Kontrollera om det finns några produkter
            if (allItems.Count == 0)
            {
                AnsiConsole.MarkupLine("[bold red]Det finns inga produkter att ändra.[/]");
                return;
            }

            // Visa alla produkter med hjälp av SelectionPrompt
            var productToChange = AnsiConsole.Prompt(
                new SelectionPrompt<Item>()
                    .Title("[bold cyan]Välj en produkt att ändra[/]")  // Rubrik
                    .PageSize(10)  // Maximalt antal produkter som visas på en sida
                    .MoreChoicesText("[grey](Tryck på pilarna för att välja)[/]")  // Text som förklarar navigeringen
                    .AddChoices(allItems)  // Lägg till alla produkter som alternativ
                    .UseConverter(item => $"{item.Name} - {item.Price} kr")  // Konvertera till användarvänligt format
            );

            // Visa alternativ för ändring
            var changeOption = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title($"[bold cyan]Vad vill du ändra på produkten '{productToChange.Name}'?[/]")  // Visa produktens namn
                    .AddChoices("Ändra namn", "Ändra pris", "Tillbaka")  // Alternativ för användaren
            );

            // Utför den valda ändringen
            switch (changeOption)
            {
                case "Ändra namn":
                    AnsiConsole.MarkupLine("[bold cyan]Ange nytt namn på produkten:[/]");
                    productToChange.Name = Console.ReadLine()!;
                    AnsiConsole.MarkupLine($"[bold green]Produktens namn har ändrats till '{productToChange.Name}'.[/]");
                    break;
                case "Ändra pris":
                    AnsiConsole.MarkupLine("[bold cyan]Ange nytt pris på produkten:[/]");
                    // Säkerställ att användaren skriver ett giltigt pris
                    if (int.TryParse(Console.ReadLine(), out int newPrice))
                    {
                        productToChange.Price = newPrice;
                        AnsiConsole.MarkupLine($"[bold green]Produktens pris har ändrats till {productToChange.Price} kr.[/]");
                    }
                    else
                    {
                        AnsiConsole.MarkupLine("[bold red]Ogiltigt pris, försök igen.[/]");
                    }
                    break;
                case "Tillbaka":
                    break;
                default:
                    AnsiConsole.MarkupLine("[bold red]Ogiltigt val.[/]");
                    break;
            }
        }
    }
}
