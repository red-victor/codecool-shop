using Codecool.CodecoolShop.Models;
using System.Collections.Generic;

namespace Codecool.CodecoolShop.Daos
{
    public interface ICartDao
    {
        void SaveCart(string userID, List<CartItem> cart);
        void EmptyCart(string userID);

        IEnumerable<CartItem> GetProducts(string userID);
    }
}
