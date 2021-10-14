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

        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
            ProductService = new Services.ProductService(
                ProductDaoMemory.GetInstance(),
                ProductCategoryDaoMemory.GetInstance(),
                SupplierDaoMemory.GetInstance(),
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

        public IActionResult AddToCart(string id)
        {
            var product = ProductService.GetProduct(int.Parse(id));
            ProductService.AddToCart(product);

            var products = ProductService.GetAllProducts();
            return View(products.ToList());
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
            ICartDao cartDataStore = CartDaoMemory.GetInstance();
            var cartData = cartDataStore.GetProducts(Request.Cookies["userId"]);

            IProductDao productDataStore = ProductDaoMemory.GetInstance();

            var checkoutViewModel = new CheckoutViewModel();

            foreach( var product in cartData)
            {
                var newCheckoutItem = new CheckoutItem();
                newCheckoutItem.Product = productDataStore.Get(product.Id);
                newCheckoutItem.Quantity = product.Quantity;
                checkoutViewModel.CheckoutItems.Add(newCheckoutItem);
            }

            return View(checkoutViewModel);
        }

        [HttpPost]
        [Route("api/checkout")]
        public JsonResult CheckoutJSON(string payload)
        {
            var cartList = JsonConvert.DeserializeObject<List<CartItem>>(payload);
            ICartDao cartDataStore = CartDaoMemory.GetInstance();
            cartDataStore.SaveCart(Request.Cookies["userId"], cartList);

            return Json(new { success = true, responseText = "Data sent" });
        }

        public IActionResult OrderDetails()
        {
            ICartDao cartDataStore = CartDaoMemory.GetInstance();
            var cartData = cartDataStore.GetProducts(Request.Cookies["userId"]);
            cartDataStore.EmptyCart(Request.Cookies["userId"]);

            IProductDao productDataStore = ProductDaoMemory.GetInstance();

            var checkoutViewModel = new CheckoutViewModel();

            foreach (var product in cartData)
            {
                var newCheckoutItem = new CheckoutItem();
                newCheckoutItem.Product = productDataStore.Get(product.Id);
                newCheckoutItem.Quantity = product.Quantity;
                checkoutViewModel.CheckoutItems.Add(newCheckoutItem);
            }

            return View(checkoutViewModel);
        }

        [HttpPost]
        [Route("api/saveOrder")]
        public JsonResult OrderDetailsJSON(string payload)
        {
            string workingDirectory = Environment.CurrentDirectory;
            string userID = Request.Cookies["userId"][0..4];
            string dateNow = DateTime.Now.ToString("yyyy-MM-dd--HH-mm-ss");
            string targetDirectory = $"{workingDirectory}\\OrderLogs\\{userID}---{dateNow}.json";
            System.IO.File.WriteAllText(targetDirectory, payload);

            return Json(new { success = true, responseText = "Data saved" });
        }
    }
}
