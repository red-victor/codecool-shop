using Codecool.CodecoolShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Codecool.CodecoolShop.Services
{
    public class PaymentService
    {
        public int CalculateOrderAmount(CartItem[] items)
        {
            float sum = 0;

            foreach (var item in items)
                sum += item.Price * item.Quantity;

            return (int)((Math.Round(sum * 100f) / 100f) * 100);
        }
    }
}
