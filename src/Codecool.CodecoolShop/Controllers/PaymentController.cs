﻿using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Daos.Implementations;
using Codecool.CodecoolShop.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Codecool.CodecoolShop.Controllers
{
    [Route("create-payment-intent")]
    [ApiController]
    public class PaymentController : Controller
    {
        public Services.ProductService ProductService { get; set; }
        public Services.CheckoutService CartService { get; set; }

        public PaymentController()
        {
            ProductService = new Services.ProductService(
                ProductDaoMemory.GetInstance(),
                ProductCategoryDaoMemory.GetInstance(),
                SupplierDaoMemory.GetInstance());

            CartService = new Services.CheckoutService(
                ProductDaoMemory.GetInstance(),
                CartDaoMemory.GetInstance());
        }

        [HttpPost]
        public ActionResult Create(PaymentIntentCreateRequest request)
        {
            var paymentIntents = new PaymentIntentService();
            var amount = CalculateOrderAmount(request.Items);
            // Add taxes
            var tax = amount * 5 / 100;
            amount += tax;
            var shipping = 500;
            amount += shipping;
            var paymentIntent = paymentIntents.Create(new PaymentIntentCreateOptions
            {
                Amount = amount,
                Currency = "usd",
            });
            return Json(new { clientSecret = paymentIntent.ClientSecret, stripeResponseStatusCode = paymentIntent.StripeResponse.StatusCode });
        }

        private int CalculateOrderAmount(CartItem[] items)
        {
            float sum = 0;

            foreach(var item in items)
               sum += item.Price * item.Quantity;
            
            return (int)((Math.Round(sum * 100f) / 100f) * 100);
        }
        
        public class PaymentIntentCreateRequest
        {
            [JsonProperty("items")]
            public CartItem[] Items { get; set; }
        }
    }
}
