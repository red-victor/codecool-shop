using Codecool.CodecoolShop.Daos.Implementations;
using Codecool.CodecoolShop.Data;
using Microsoft.AspNetCore.Mvc;
using static Codecool.CodecoolShop.Services.PaymentService;

using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;
using System.Security.Claims;

namespace Codecool.CodecoolShop.Controllers
{
    /*[Route("create-payment-intent")]
    [ApiController]
    public class PaymentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public Services.PaymentService PaymentService { get; set; }

        public PaymentController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            PaymentService = new Services.PaymentService(_unitOfWork.Products);
        }

        [HttpPost]
        public ActionResult Create(PaymentIntentCreateRequest request)
        {
            var paymentIntent = PaymentService.GeneratePaymentOptions(request);

            return Json(new { clientSecret = paymentIntent.ClientSecret});
        }
    }*/


    public class PaymentsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Services.PaymentService _paymentService;
        private readonly Services.CheckoutService _checkoutService;

        public PaymentsController(IUnitOfWork unitOfWork)
        {
            StripeConfiguration.ApiKey = "sk_test_51JpfkREgDlOXw5zT0bH9M21N2Sm7VYXApWvRkv9yXnfrYqga2WkOXXDzHZes83s9l3zrKSoJAuM8wCZOwoNSy33N00wfiUwqNL";
            this._unitOfWork = unitOfWork;
            this._paymentService = new Services.PaymentService(_unitOfWork.Products);
            this._checkoutService = new Services.CheckoutService(_unitOfWork.Products, _unitOfWork.Carts);
        }

        [HttpPost("create-checkout-session")]
        public ActionResult CreateCheckoutSession()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var checkoutOptions = _checkoutService.GenerateCheckoutViewModel(userId);
            var sessionOptions = _paymentService.GenerateSessionOptions(checkoutOptions);
            sessionOptions.AddExtraParam("shipping_rates[]", "shr_1JpfnYEgDlOXw5zTVbHM5obQ");
            var service = new SessionService();
            Session session = service.Create(sessionOptions);

            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }

        public IActionResult Success([FromQuery] string session_id)
        {
            var sessionService = new SessionService();
            Session session = sessionService.Get(session_id);

            var customerService = new CustomerService();
            Customer customer = customerService.Get(session.CustomerId);

            return View();
        }
    }
}
