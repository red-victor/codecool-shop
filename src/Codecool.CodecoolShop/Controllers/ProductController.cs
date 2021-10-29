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
using Codecool.CodecoolShop.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Codecool.CodecoolShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;

        private readonly IUnitOfWork _unitOfWork;

        public Services.ProductService ProductService { get; set; }

        public Services.CheckoutService CheckoutService { get; set; }
        public Services.CookieService CookieService { get; set; }

        public ProductController(ILogger<ProductController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;

            ProductService = new Services.ProductService(
                _unitOfWork.Products,
                _unitOfWork.Categories,
                _unitOfWork.Suppliers);

            CheckoutService = new Services.CheckoutService(
                _unitOfWork.Products,
                _unitOfWork.Carts);

            CookieService = new CookieService();
        }

        

        public IActionResult Index()
        {
            var cookieUserId = Request.Cookies["userId"];

            if (cookieUserId == null)
            {
                var options = CookieService.GenerateCookieOptions();
                var userUUId = Util.GenerateID();
                Response.Cookies.Append("userId", userUUId, options);
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

        [Authorize]
        public IActionResult Checkout()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var checkoutViewModel = CheckoutService.GenerateCheckoutViewModel(userId);
            checkoutViewModel.User = _unitOfWork.Users.Get(userId);
            return View(checkoutViewModel);
        }

        [Authorize]
        public IActionResult Cart()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var checkoutViewModel = CheckoutService.GenerateCheckoutViewModel(userId);

            return View(checkoutViewModel);
        }

        [Authorize]
        [HttpPost]
        [Route("api/saveCart")]
        public JsonResult CartJSON(string payload)
        {
            var cartList = Util.DeserializeJSON(payload);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            CheckoutService.SaveCart(userId, cartList);

            // ICartDao cartDataStore = CartDaoMemory.GetInstance();
            // cartDataStore.SaveCart(Request.Cookies["userId"], cartList);
            // cartDataStore.SaveCart(userId, cartList);

            return Json(new { success = true, responseText = "Data sent" });
        }

        [Authorize]
        public IActionResult OrderDetails()
        {
            // var userID = Request.Cookies["userId"];
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var checkoutViewModel = CheckoutService.GenerateCheckoutViewModel(userId);
            CheckoutService.EmptyCart(userId);

            return View(checkoutViewModel);
        }

        [Authorize]
        [HttpPost]
        [Route("api/saveOrder")]
        public JsonResult OrderDetailsJSON(string payload)
        {
            var cartList = Util.DeserializeJSON(payload);

            //string userID = Request.Cookies["userId"];

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            CheckoutService.SaveCart(userId, cartList);

            CheckoutService.SaveToFile(userId, payload);

            return Json(new { success = true, responseText = "Data saved" });
        }
    }
}

/*if (_db.Products.Count() == 0 && _db.ProductCategories.Count() == 0 && _db.Suppliers.Count() == 0)
              SetupDatabases();*/

/*private void SetupDatabases()
        {
            IProductDao productDataStore = ProductDaoDatabase.GetInstance(_db);
            IProductCategoryDao productCategoryDataStore = ProductCategoryDaoDatabase.GetInstance(_db);
            ISupplierDao supplierDataStore = SupplierDaoDatabase.GetInstance(_db);

            Supplier amazon = new Supplier { Name = "Amazon", Description = "Digital content and services" };
            supplierDataStore.Add(amazon);
            Supplier lenovo = new Supplier { Name = "Lenovo", Description = "Computers" };
            supplierDataStore.Add(lenovo);
            ProductCategory tablet = new ProductCategory { Name = "Tablet", Department = "Hardware", Description = "A tablet computer, commonly shortened to tablet, is a thin, flat mobile computer with a touchscreen display." };
            productCategoryDataStore.Add(tablet);
            ProductCategory laptop = new ProductCategory { Name = "Laptop", Department = "Hardware", Description = "Laptop" };
            productCategoryDataStore.Add(laptop);
            productDataStore.Add(new Models.Product { Name = "Amazon Fire", DefaultPrice = 49.9m, Currency = "USD", Description = "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.", ProductCategory = tablet, Supplier = amazon });
            productDataStore.Add(new Models.Product { Name = "Lenovo IdeaPad Miix 700", DefaultPrice = 479.0m, Currency = "USD", Description = "Keyboard cover is included. Fanless Core m5 processor. Full-size USB ports. Adjustable kickstand.", ProductCategory = tablet, Supplier = lenovo });
            productDataStore.Add(new Models.Product { Name = "Amazon Fire HD 8", DefaultPrice = 89.0m, Currency = "USD", Description = "Amazon's latest Fire HD 8 tablet is a great value for media consumption.", ProductCategory = tablet, Supplier = amazon });
            productDataStore.Add(new Models.Product { Name = "Lenovo ThinkPad", DefaultPrice = 230.3m, Currency = "USD", Description = "Cel mai smecher smecher smecher leptop", ProductCategory = laptop, Supplier = lenovo });
            _db.SaveChanges();
        }*/