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

        public List<CartItem> GetCartProducts(string key)
        {
            return cartDao.GetProducts(key);
        }

        public void EmptyCart(string userID)
        {
            cartDao.EmptyCart(userID);
        }

        public CheckoutViewModel GenerateCheckoutViewModel(string userID)
        {
            var cartData = GetCartProducts(userID);
            var checkoutViewModel = new CheckoutViewModel();

            foreach (var product in cartData)
            {
                var newCheckoutItem = new CheckoutItem();
                newCheckoutItem.Product = productDao.Get(product.Id);
                newCheckoutItem.Quantity = product.Quantity;
                checkoutViewModel.CheckoutItems.Add(newCheckoutItem);
            }

            return checkoutViewModel;
        }
    }
}
