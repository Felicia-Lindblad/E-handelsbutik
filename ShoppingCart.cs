namespace E_handelsbutik
{
    public class ShoppingCart
    {
        private List<Item> itemsInCart;

        public ShoppingCart()
        {
            itemsInCart = new List<Item>();
        }

        public void AddItemToShoppningCart(Item item)
        {
            itemsInCart.Add(item);
            Console.WriteLine($"Produkten '{item.Name}' har lagts till i kundkorgen.");
        }

        public void RemoveItemFromShoppingcart(string itemName)
        {
            var itemToRemove = itemsInCart.FirstOrDefault(item => item.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));

            if (itemToRemove != null)
            {
                itemsInCart.Remove(itemToRemove);
                Console.WriteLine($"Produkten '{itemToRemove.Name}' har tagits bort från kundkorgen.");
            }
            else
            {
                Console.WriteLine("Produkten hittades inte i kundkorgen.");
            }
        }

        public void ShowItems()
        {
            if (itemsInCart.Count == 0)
            {
                Console.WriteLine("Kundkorgen är tom.");
            }
            else
            {
                Console.WriteLine("Varor i kundkorgen:");
                foreach (var item in itemsInCart)
                {
                    Console.WriteLine($"- {item.Name}: {item.Price} kr");
                }
            }
        }

        public int CalculateTotal()
        {
            return itemsInCart.Sum(item => item.Price);
        }
        public List<Item> GetItems()
        {
            return itemsInCart;
        }
    }
}
