
using System.Text.Json.Serialization;

namespace E_handelsbutik
{
    public class LittleDB
    {
        [JsonPropertyName("Products")]
        public List<Item> AllItemsFromListInJSON { get; set; }
    }
}
