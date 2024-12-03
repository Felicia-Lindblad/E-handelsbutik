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

            UserInterface userInterface = new UserInterface(littleDB);
            StoreManager storeManager = new StoreManager();

            bool isRunning = true;

            while (isRunning)
            {
                Console.WriteLine("Välkommen! Välj ett alternativ:");
                Console.WriteLine("1. Jag är kund");
                Console.WriteLine("2. Jag är butiksansvarig");
                Console.WriteLine("3. Avsluta programmet");
                Console.Write("Ditt val: ");
                string choice = Console.ReadLine()!;

                switch (choice)
                {
                    case "1":
                        CustomerMenu(userInterface, littleDB, dataJSONFilePath);
                        break;
                    case "2":
                        StoreManagerMenu(storeManager, littleDB, dataJSONFilePath);
                        break;
                    case "3":
                        isRunning = false;
                        Console.WriteLine("Avslutar programmet...");
                        break;
                    default:
                        Console.WriteLine("Ogiltigt val, försök igen.");
                        break;
                }
            }
        }
        static void CustomerMenu(UserInterface userInterface, LittleDB littleDB, string dataJSONFilePath)
        {
            bool isCustomerRunning = true;

            while (isCustomerRunning)
            {
                Console.WriteLine("\nKundmeny:");
                Console.WriteLine("1. Lägg till produkt i varukorgen");
                Console.WriteLine("2. Ta bort produkt från varukorgen");
                Console.WriteLine("3. Visa varukorgen");
                Console.WriteLine("4. Till kassan");
                Console.WriteLine("5. Tillbaka till huvudmenyn");
                Console.Write("Ditt val: ");
                string customerChoice = Console.ReadLine()!;

                switch (customerChoice)
                {
                    case "1":
                        userInterface.AddItemToCart(littleDB.AllItemsFromListInJSON);
                        SaveNewDataToJSONFile(littleDB, dataJSONFilePath);
                        break;
                    case "2":
                        userInterface.RemoveItemFromCart();
                        break;
                    case "3":
                        userInterface.ShowAllItemsInCart();
                        break;
                    case "4":
                        userInterface.CheckOut();
                        SaveNewDataToJSONFile(littleDB, dataJSONFilePath);
                        break;
                    case "5":
                        isCustomerRunning = false;
                        break;
                    default:
                        Console.WriteLine("Ogiltigt val, försök igen.");
                        break;
                }
            }
        }
        static void StoreManagerMenu(StoreManager storeManager, LittleDB littleDB, string dataJSONFilePath)
        {
            bool isStoreManagerRunning = true;

            while (isStoreManagerRunning)
            {
                Console.WriteLine("\nButiksansvarig meny:");
                Console.WriteLine("1. Lägg till produkt i butiken");
                Console.WriteLine("2. Ta bort produkt från butiken");
                Console.WriteLine("3. Ändra produktinformation");
                Console.WriteLine("4. Tillbaka till huvudmenyn");
                Console.Write("Ditt val: ");
                string managerChoice = Console.ReadLine()!;

                switch (managerChoice)
                {
                    case "1":
                        storeManager.AddItemToStore(littleDB.AllItemsFromListInJSON);
                        SaveNewDataToJSONFile(littleDB, dataJSONFilePath);
                        break;
                    case "2":
                        storeManager.RemoveItemFromStore(littleDB.AllItemsFromListInJSON);
                        SaveNewDataToJSONFile(littleDB, dataJSONFilePath);
                        break;
                    case "3":
                        storeManager.ChangeItemInStore(littleDB.AllItemsFromListInJSON);
                        SaveNewDataToJSONFile(littleDB, dataJSONFilePath);
                        break;
                    case "4":
                        isStoreManagerRunning = false;
                        break;
                    default:
                        Console.WriteLine("Ogiltigt val, försök igen.");
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
