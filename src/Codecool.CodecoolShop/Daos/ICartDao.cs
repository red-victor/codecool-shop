using Codecool.CodecoolShop.Models;
using System.Collections.Generic;

namespace Codecool.CodecoolShop.Daos
{
    public interface ICartDao
    {
        void SaveCart(List<CartItem> cart);
        void EmptyCart();

        List<CartItem> GetProducts();
    }
}
