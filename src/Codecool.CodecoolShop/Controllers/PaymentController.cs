using Codecool.CodecoolShop.Daos.Implementations;
using Codecool.CodecoolShop.Data;
using Microsoft.AspNetCore.Mvc;
using static Codecool.CodecoolShop.Services.PaymentService;

namespace Codecool.CodecoolShop.Controllers
{
    [Route("create-payment-intent")]
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
    }
}
