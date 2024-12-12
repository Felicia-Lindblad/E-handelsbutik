using Spectre.Console;
using Figgle;
using System.Text.Json;

namespace E_handelsbutik
{
    public class Program
    {
        static void Main(string[] args)
        {
            string dataJSONFilePath = "Products.json";
            string allDataAsJSONType = File.ReadAllText(dataJSONFilePath);

            LittleDB littleDB = JsonSerializer.Deserialize<LittleDB>(allDataAsJSONType)!;

            // Skapa en rubrik för butiken
            var figgleText = FiggleFonts.Standard.Render("E-Handelsbutiken");
            AnsiConsole.MarkupLine($"[magenta]{figgleText}[/]");

            UserInterface userInterface = new UserInterface(littleDB);
            StoreManager storeManager = new StoreManager();

            bool isRunning = true;

            while (isRunning)
            {
                var mainMenu = new SelectionPrompt<string>()
                    .Title("[bold underline]Välkommen! Välj ett alternativ:[/]")
                    .AddChoices("Jag är kund", "Jag är butiksansvarig", "Avsluta programmet")
                    .UseConverter(choice => choice);

                var choice = AnsiConsole.Prompt(mainMenu);

                switch (choice)
                {
                    case "Jag är kund":
                        CustomerMenu(userInterface, littleDB, dataJSONFilePath);
                        break;
                    case "Jag är butiksansvarig":
                        StoreManagerMenu(storeManager, littleDB, dataJSONFilePath);
                        break;
                    case "Avsluta programmet":
                        isRunning = false;
                        AnsiConsole.MarkupLine("[bold red]Avslutar programmet...[/]");
                        break;
                }
            }
        }

        static void CustomerMenu(UserInterface userInterface, LittleDB littleDB, string dataJSONFilePath)
        {
            bool isCustomerRunning = true;

            while (isCustomerRunning)
            {
                var customerMenu = new SelectionPrompt<string>()
                    .Title("[bold underline]Kundmeny:[/]")
                    .AddChoices("Lägg till produkt i varukorgen", "Ta bort produkt från varukorgen", "Visa varukorgen", "Till kassan", "Tillbaka till huvudmenyn")
                    .UseConverter(choice => choice);

                var customerChoice = AnsiConsole.Prompt(customerMenu);

                switch (customerChoice)
                {
                    case "Lägg till produkt i varukorgen":
                        userInterface.AddItemToCart(littleDB.AllItemsFromListInJSON);
                        SaveNewDataToJSONFile(littleDB, dataJSONFilePath);
                        break;
                    case "Ta bort produkt från varukorgen":
                        userInterface.RemoveItemFromCart();
                        break;
                    case "Visa varukorgen":
                        userInterface.ShowAllItemsInCart();
                        break;
                    case "Till kassan":
                        userInterface.CheckOut();
                        SaveNewDataToJSONFile(littleDB, dataJSONFilePath);
                        break;
                    case "Tillbaka till huvudmenyn":
                        isCustomerRunning = false;
                        break;
                    default:
                        AnsiConsole.MarkupLine("[bold red]Ogiltigt val, försök igen.[/]");
                        break;
                }
            }
        }

        static void StoreManagerMenu(StoreManager storeManager, LittleDB littleDB, string dataJSONFilePath)
        {
            bool isStoreManagerRunning = true;

            while (isStoreManagerRunning)
            {
                var storeManagerMenu = new SelectionPrompt<string>()
                    .Title("[bold underline]Butiksansvarig meny:[/]")
                    .AddChoices("Lägg till produkt i butiken", "Ta bort produkt från butiken", "Ändra produktinformation", "Tillbaka till huvudmenyn")
                    .UseConverter(choice => choice);

                var managerChoice = AnsiConsole.Prompt(storeManagerMenu);

                switch (managerChoice)
                {
                    case "Lägg till produkt i butiken":
                        storeManager.AddItemToStore(littleDB.AllItemsFromListInJSON);
                        SaveNewDataToJSONFile(littleDB, dataJSONFilePath);
                        break;
                    case "Ta bort produkt från butiken":
                        storeManager.RemoveItemFromStore(littleDB.AllItemsFromListInJSON);
                        SaveNewDataToJSONFile(littleDB, dataJSONFilePath);
                        break;
                    case "Ändra produktinformation":
                        storeManager.ChangeItemInStore(littleDB.AllItemsFromListInJSON);
                        SaveNewDataToJSONFile(littleDB, dataJSONFilePath);
                        break;
                    case "Tillbaka till huvudmenyn":
                        isStoreManagerRunning = false;
                        break;
                    default:
                        AnsiConsole.MarkupLine("[bold red]Ogiltigt val, försök igen.[/]");
                        break;
                }
            }
        }

        static void SaveNewDataToJSONFile(LittleDB littleDB, string dataJSONFilePath)
        {
            string updatedLittleDB = JsonSerializer.Serialize(littleDB, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(dataJSONFilePath, updatedLittleDB);
        }
    }
}