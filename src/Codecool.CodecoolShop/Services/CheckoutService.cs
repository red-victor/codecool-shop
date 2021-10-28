using System.Collections.Generic;
using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Models;

namespace Codecool.CodecoolShop.Services
{
    public class CheckoutService
    {
        private readonly IProductDao productDao;
        private readonly ICartDao cartDao;

        public CheckoutService(IProductDao productDao, ICartDao cartDao)
        {
            this.productDao = productDao;
            this.cartDao = cartDao;
        }

        public IEnumerable<CartItem> GetCartProducts(string key)
        {
            return cartDao.GetProducts(key);
        }

        public void SaveCart(string userID, List<CartItem> cartList)
        {
            cartDao.SaveCart(userID, cartList);
        }

        public void EmptyCart(string userID)
        {
            cartDao.EmptyCart(userID);
        }

        public CheckoutViewModel GenerateCheckoutViewModel(string userId)
        {
            var cartData = GetCartProducts(userId);
            var checkoutViewModel = new CheckoutViewModel();

            foreach (var item in cartData)
            {
                var newCheckoutItem = new CheckoutItem();
                newCheckoutItem.Product = productDao.Get(item.ProductId);
                newCheckoutItem.Quantity = item.Quantity;
                checkoutViewModel.CheckoutItems.Add(newCheckoutItem);
            }

            return checkoutViewModel;
        }

        public void SaveToFile(string userID, string cartItems)
        {
            string orderLogDirectory = Util.GetOrderLogDirectory();
            string currentDateTime = Util.GetCurrentDateTime();
            string targetDirectory = $"{orderLogDirectory}{userID}---{currentDateTime}.json";
            System.IO.File.WriteAllText(targetDirectory, cartItems);
        }
    }
}
