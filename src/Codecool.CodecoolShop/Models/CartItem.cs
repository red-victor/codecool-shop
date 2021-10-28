using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace Codecool.CodecoolShop.Models
{
    public class CartItem
    {
        
        public int Id { get; set; }

        [ForeignKey("Product")]
        [JsonProperty("id")]
        public int ProductId { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }
    }
}
