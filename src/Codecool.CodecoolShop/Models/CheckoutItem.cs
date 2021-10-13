using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Codecool.CodecoolShop.Models
{
    public class CheckoutItem
    {
        public int Quantity { get; set; }

        public Product Product { get; set; }

        public decimal Sum => Product.DefaultPrice * Quantity; 
    }
}
