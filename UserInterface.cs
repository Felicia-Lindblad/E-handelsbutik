
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
            bool continueAdding = true;

            while (continueAdding)
            {
                Console.WriteLine("Tillgängliga produkter:");
                allItems.ForEach(item =>
                    Console.WriteLine($"{item.Name} - {item.Price} kr"));

                Console.Write("Ange namnet på produkten du vill lägga till i kundkorgen: ");
                string productName = Console.ReadLine()!;

                var productToAdd = allItems.FirstOrDefault(item => item.Name.Equals(productName, StringComparison.OrdinalIgnoreCase));

                if (productToAdd != null)
                {
                    shoppingCart.AddItemToShoppningCart(productToAdd);
                }
                else
                {
                    Console.WriteLine("Produkten hittades inte.");
                }

                Console.Write("Vill du lägga till en annan produkt? (ja/nej): ");
                string response = Console.ReadLine()!;
                continueAdding = response.Equals("ja", StringComparison.OrdinalIgnoreCase);
            }
        }
        public void RemoveItemFromCart()
        {
            if (shoppingCart.CalculateTotal() == 0)
            {
                Console.WriteLine("Kundkorgen är tom. Det finns inga produkter att ta bort.");
                return;
            }

            Console.WriteLine("Produkter i din kundkorg:");
            shoppingCart.ShowItems();

            Console.Write("Ange namnet på produkten du vill ta bort från kundkorgen: ");
            string productName = Console.ReadLine()!;

            // Anropa RemoveItemFromShoppingcart från ShoppingCart-klassen
            shoppingCart.RemoveItemFromShoppingcart(productName);
        }
        public void ShowAllItemsInCart()
        {
            shoppingCart.ShowItems();
            Console.WriteLine($"Totalbelopp: {shoppingCart.CalculateTotal()} kr");
        }
        public void CheckOut()
        {
            shoppingCart.ShowItems();
            Console.WriteLine($"Totalbelopp att betala: {shoppingCart.CalculateTotal()} kr");
            Console.WriteLine("Tack för ditt köp!");
        }
    }
}
