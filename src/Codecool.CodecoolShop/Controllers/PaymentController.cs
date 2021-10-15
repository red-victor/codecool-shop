using Codecool.CodecoolShop.Daos;
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
        public Services.PaymentService PaymentService { get; set; }

        public PaymentController()
        {
            PaymentService = new Services.PaymentService();
        }

        [HttpPost]
        public ActionResult Create(PaymentIntentCreateRequest request)
        {
            var paymentIntents = new PaymentIntentService();
            var amount = PaymentService.CalculateOrderAmount(request.Items);
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
            return Json(new { clientSecret = paymentIntent.ClientSecret});
        }

        
        
        public class PaymentIntentCreateRequest
        {
            [JsonProperty("items")]
            public CartItem[] Items { get; set; }
        }
    }
}
