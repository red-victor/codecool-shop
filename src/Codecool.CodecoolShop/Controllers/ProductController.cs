using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Daos.Implementations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Codecool.CodecoolShop.Models;
using Codecool.CodecoolShop.Services;
using Newtonsoft.Json;
using Stripe;
using Microsoft.AspNetCore.Http;

namespace Codecool.CodecoolShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        public Services.ProductService ProductService { get; set; }

        public Services.CheckoutService CheckoutService { get; set; }

        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;

            ProductService = new Services.ProductService(
                ProductDaoMemory.GetInstance(),
                ProductCategoryDaoMemory.GetInstance(),
                SupplierDaoMemory.GetInstance());

            CheckoutService = new Services.CheckoutService(
                ProductDaoMemory.GetInstance(),
                CartDaoMemory.GetInstance());
        }

        public IActionResult Index()
        {
            var cookieUserId = Request.Cookies["userId"];
            if (cookieUserId == null)
            {
                CookieOptions option = new CookieOptions();
                option.Expires = DateTime.Now.AddDays(1);
                var userUUId = Util.GenerateID();
                Response.Cookies.Append("userId", userUUId, option);
            }

            var products = ProductService.GetAllProducts();
            return View(products.ToList());
        }

        public IActionResult GetByCategory(string id)
        {
            var products = ProductService.GetProductsForCategory(int.Parse(id));
            return View("Index", products.ToList());
        }

        public IActionResult GetBySupplier(string id)
        {
            var products = ProductService.GetProductsForSupplier(int.Parse(id));
            return View("Index", products.ToList());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Checkout()
        {
            return View();
        }

        public IActionResult Cart()
        {
            var userID = Request.Cookies["userId"];
            var checkoutViewModel = CheckoutService.GenerateCheckoutViewModel(userID);

            return View(checkoutViewModel);
        }

        [HttpPost]
        [Route("api/cart")]
        public JsonResult CartJSON(string payload)
        {
            var cartList = Util.DeserializeJSON(payload);
            ICartDao cartDataStore = CartDaoMemory.GetInstance();
            cartDataStore.SaveCart(Request.Cookies["userId"], cartList);

            return Json(new { success = true, responseText = "Data sent" });
        }

        public IActionResult OrderDetails()
        {
            var userID = Request.Cookies["userId"];
            var checkoutViewModel = CheckoutService.GenerateCheckoutViewModel(userID);
            CheckoutService.EmptyCart(userID);

            return View(checkoutViewModel);
        }

        [HttpPost]
        [Route("api/saveOrder")]
        public JsonResult OrderDetailsJSON(string payload)
        {
            var cartList = Util.DeserializeJSON(payload);
            string userID = Request.Cookies["userId"];
            CheckoutService.SaveCart(userID, cartList);

            CheckoutService.SaveToFile(userID, payload);

            return Json(new { success = true, responseText = "Data saved" });
        }
    }
}
