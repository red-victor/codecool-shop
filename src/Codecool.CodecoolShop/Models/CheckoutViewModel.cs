using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Codecool.CodecoolShop.Models
{
    public class CheckoutViewModel
    {
        public List<CheckoutItem> CheckoutItems = new List<CheckoutItem>();

        public decimal Total => CheckoutItems.Sum(item => item.Sum);
        public decimal Tax => Total * 5 / 100;
        public decimal Shipping => 5;
        public decimal FinalPrice => Math.Round(Total + Shipping + Tax, 2);
    }
}
