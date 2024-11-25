
namespace E_handelsbutik
{
    public class StoreManager
    {
        public void AddItemToStore(List<Item> allItems)
        {
            Console.Write("Ange produktens namn: ");
            string nameToAdd = Console.ReadLine()!;

            Console.Write("Ange produktens pris: ");
            int priceToAdd = Convert.ToInt32(Console.ReadLine());

            Item newItem = new Item(nameToAdd, priceToAdd);

            allItems.Add(newItem);
            Console.WriteLine($"Produkten '{nameToAdd}' har lagts till i butiken.");
        }
        public void RemoveItemFromStore(List<Item> allItems)
        {
            allItems.ForEach(item => Console.WriteLine($"\nProdukt: {item.Name} | Pris: {item.Price}"));

            Console.Write("Ange namnet på produkt du vill ta bort:");
            string productToRemove = Console.ReadLine()!;

            var productToRemoveByManager = allItems.FirstOrDefault(product => product.Name.Equals(productToRemove, StringComparison.OrdinalIgnoreCase));

            if (productToRemoveByManager == null)
            {
                Console.WriteLine("Artikeln hittades inte");
            }
            else
            {
                allItems.Remove(productToRemoveByManager);
                Console.WriteLine($"Produkten {productToRemoveByManager.Name} har tagits bort");
            }
        }
        public void ChangeItemInStore(List<Item> allItems)
        {
            allItems.ForEach(item => Console.WriteLine($"\nProdukt: {item.Name} | Pris: {item.Price}"));

            Console.Write("Ange namnet på produkt du vill ändra:");
            string productToChange = Console.ReadLine()!;

            var productToChangeByManager = allItems.FirstOrDefault(product => product.Name.Equals(productToChange, StringComparison.OrdinalIgnoreCase));

            if (productToChangeByManager == null)
            {
                Console.WriteLine("Produkten hittades inte");
            }
            else
            {
                Console.WriteLine("Välj nedan vad du vill uppdatera");
                Console.WriteLine("1. Namn");
                Console.WriteLine("2. Pris");
                string pruductUpdateChoice = Console.ReadLine()!;

                switch (pruductUpdateChoice)
                {
                    case "1":
                        Console.Write("Ange nytt namn på produkten:");
                        productToChangeByManager.Name = Console.ReadLine()!;
                        break;
                    case "2":
                        Console.Write("Ange nytt pris på produkten:");
                        productToChangeByManager.Price = Convert.ToInt32(Console.ReadLine()!);
                        break;
                    default:
                        Console.WriteLine("Ogiltigt val.");
                        return;
                }
            }
        }
    }
}
