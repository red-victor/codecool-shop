using System;
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

namespace Codecool.CodecoolShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        public ProductService ProductService { get; set; }

        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
            ProductService = new ProductService(
                ProductDaoMemory.GetInstance(),
                ProductCategoryDaoMemory.GetInstance(),
                SupplierDaoMemory.GetInstance(),
                CartDaoMemory.GetInstance());
        }

        public IActionResult Index()
        {
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
            return View();
        }

        [HttpPost]
        [Route("api/checkout")]
        public JsonResult CheckoutJSON(string payload)
        {
            Console.WriteLine(payload);
            var cartList = JsonConvert.DeserializeObject<List<CartItem>>(payload);
            Console.WriteLine(cartList);
            return Json(new { success = true, responseText = "Data sent" });
        }
    }
}
