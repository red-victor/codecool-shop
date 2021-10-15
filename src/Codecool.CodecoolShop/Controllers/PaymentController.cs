using Microsoft.AspNetCore.Mvc;
using static Codecool.CodecoolShop.Services.PaymentService;

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
            var paymentIntent = PaymentService.GeneratePaymentOptions(request);

            return Json(new { clientSecret = paymentIntent.ClientSecret});
        }
    }
}
