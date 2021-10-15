using Codecool.CodecoolShop.Models;
using Newtonsoft.Json;
using Stripe;
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

        public PaymentIntent GeneratePaymentOptions(PaymentIntentCreateRequest request)
        {
            var paymentIntents = new PaymentIntentService();
            var amount = CalculateOrderAmount(request.Items);

            amount = AddTax(amount);

            return paymentIntents.Create(new PaymentIntentCreateOptions
            {
                Amount = amount,
                Currency = "usd",
            });
        }

        public int AddTax(int amount, int shippingPrice = 500)
        {
            var tax = amount * 5 / 100;
            amount += tax;
            amount += shippingPrice;
            return amount;
        }
        public class PaymentIntentCreateRequest
        {
            [JsonProperty("items")]
            public CartItem[] Items { get; set; }
        }
    }
}
