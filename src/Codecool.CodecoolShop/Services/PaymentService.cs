using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Models;
using Newtonsoft.Json;
using Stripe;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Codecool.CodecoolShop.Services
{
    public class PaymentService
    {
        private readonly IProductDao productDao;

        public PaymentService(IProductDao productDao)
        {
            this.productDao = productDao;
        }

        public SessionCreateOptions GenerateSessionOptions(CheckoutViewModel checkoutViewModel)
        {
            return new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = GenerateLineItems(checkoutViewModel.CheckoutItems),
                Mode = "payment",
                SuccessUrl = "https://localhost:44368/payments/success?session_id={CHECKOUT_SESSION_ID}",
                CancelUrl = "https://localhost:44368/payments/fail",
                ShippingAddressCollection = new SessionShippingAddressCollectionOptions
                {
                    AllowedCountries = new List<string> { "RO" },
                },
                Discounts = new List<SessionDiscountOptions> { new SessionDiscountOptions { Coupon = "4hTw5q56" } },
            };
        }

        private List<SessionLineItemOptions> GenerateLineItems(List<CheckoutItem> checkoutItems)
        {
            var lineItems = new List<SessionLineItemOptions>();

            foreach (var item in checkoutItems)
            {
                lineItems.Add(new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = Convert.ToInt64(item.Product.DefaultPrice * 100),
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Product.Name,
                        },
                    },
                    Quantity = item.Quantity,
                    TaxRates = new List<string> { "txr_1JpfljEgDlOXw5zT0OCRh96R" },
                });
            }

            return lineItems;
        }

        /*public int CalculateOrderAmount(CartItem[] items)
        {
            decimal sum = 0;

            foreach (var item in items)
                sum += productDao.GetPrice(item.Id) * item.Quantity;

            return (int)Math.Round(sum * 100);
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
        }*/
    }
}
