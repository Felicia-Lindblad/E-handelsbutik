
using Spectre.Console;
using System.Xml.Serialization;

namespace E_handelsbutik
{
    public class UserInterface
    {
        private ShoppingCart shoppingCart;
        private LittleDB littleDB;

        public UserInterface(LittleDB littleDB)
        {
            shoppingCart = new ShoppingCart();
            this.littleDB = littleDB;
        }
        public void AddItemToCart(List<Item> allItems)
        {
            // Visa tillgängliga produkter i en interaktiv lista
            var productMenu = new SelectionPrompt<Item>()
                .Title("[bold underline]Välj en produkt att lägga till i varukorgen:[/]")
                .PageSize(10) // Begränsa hur många alternativ som visas på en gång
                .AddChoices(allItems)
                .UseConverter(item => $"{item.Name} - {item.Price} kr");

            // Låt användaren välja en produkt
            var selectedProduct = AnsiConsole.Prompt(productMenu);

            // Lägg till den valda produkten i varukorgen
            shoppingCart.AddItemToShoppningCart(selectedProduct);
        }
        public void RemoveItemFromCart()
        {
            if (shoppingCart.CalculateTotal() == 0)
            {
                // Om varukorgen är tom, visa ett meddelande
                AnsiConsole.MarkupLine("[bold red]Kundkorgen är tom. Det finns inga produkter att ta bort.[/]");
                return;
            }

            // Visa varor i kundkorgen
            var cartItemsMenu = new SelectionPrompt<Item>()
                .Title("[bold underline]Välj en produkt att ta bort från varukorgen:[/]")
                .PageSize(10)
                .AddChoices(shoppingCart.GetItems())  // Hämtar alla objekt i varukorgen
                .UseConverter(item => $"{item.Name} - {item.Price} kr");

            // Låt användaren välja en produkt att ta bort
            var selectedProductToRemove = AnsiConsole.Prompt(cartItemsMenu);

            // Bekräfta borttagning med en ja/nej-fråga
            bool confirmRemoval = AnsiConsole.Prompt(
                new TextPrompt<string>("[bold]Vill du ta bort[/] [yellow]" + selectedProductToRemove.Name + "[/] [bold]från varukorgen? (ja/nej)[/]")
                    .Validate(value => value.Equals("ja", StringComparison.OrdinalIgnoreCase) || value.Equals("nej", StringComparison.OrdinalIgnoreCase) ? ValidationResult.Success() : ValidationResult.Error("[red]Vänligen skriv 'ja' eller 'nej'[/]"))
            ).Equals("ja", StringComparison.OrdinalIgnoreCase);

            if (confirmRemoval)
            {
                shoppingCart.RemoveItemFromShoppingcart(selectedProductToRemove.Name);
                AnsiConsole.MarkupLine($"[green]Produkten '{selectedProductToRemove.Name}' har tagits bort från varukorgen.[/]");
            }
            else
            {
                AnsiConsole.MarkupLine("[yellow]Borttagning avbröts.[/]");
            }
        }
        public void ShowAllItemsInCart()
        {
            // Om varukorgen är tom
            if (shoppingCart.CalculateTotal() == 0)
            {
                AnsiConsole.MarkupLine("[bold red]Kundkorgen är tom.[/]");
                return;
            }

            // Visa varor i varukorgen
            AnsiConsole.MarkupLine("[bold underline]Varor i din varukorg:[/]");
            shoppingCart.ShowItems();

            // Visa totalbelopp i en stilig form
            AnsiConsole.MarkupLine($"[bold green]Totalbelopp:[/] [yellow]{shoppingCart.CalculateTotal()} kr[/]");
        }
        public void CheckOut()
        {
            if (shoppingCart.CalculateTotal() == 0)
            {
                AnsiConsole.MarkupLine("[bold red]Din varukorg är tom![/]");
                return;
            }

            // Visa varor i varukorgen innan kassan
            AnsiConsole.MarkupLine("[bold underline]Din varukorg innehåller följande produkter:[/]");
            shoppingCart.ShowItems();

            // Visa totalbeloppet att betala
            AnsiConsole.MarkupLine($"[bold green]Totalbelopp att betala:[/] [yellow]{shoppingCart.CalculateTotal()} kr[/]");

            // Tackmeddelande
            AnsiConsole.MarkupLine("[bold blue]Tack för ditt köp![/]");
        }
    }
}
