using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Codecool.CodecoolShop.Models;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    public class CartDaoMemory : ICartDao
    {
        private Dictionary<string, List<CartItem>> data = new Dictionary<string, List<CartItem>>();
        private static CartDaoMemory instance = null;

        private CartDaoMemory()
        {
        }

        public static CartDaoMemory GetInstance()
        {
            if (instance == null)
            {
                instance = new CartDaoMemory();
            }

            return instance;
        }

        public void SaveCart(string userID, List<CartItem> cartList)
        {
            if (data.ContainsKey(userID))
            {
                data[userID] = cartList;
            }
            else
            {
                data.Add(userID, cartList);
            }
        }
        public void EmptyCart(string userID)
        {
            data.Remove(userID);
        }

        public List<CartItem> GetProducts(string userID)
        {
            return data[userID];
        }

        //public void Add(Product item)
        //{
        //    item.Id = data.Count + 1;
        //    //data.Add(item);
        //}

        //public void Remove(int id)
        //{
        //    //data.Remove(this.Get(id));
        //}

        //public CartItem Get(int id)
        //{
        //    return data.Find(match: x => x.Id == id);
        //}

        //public IEnumerable<Product> GetAll()
        //{
        //    return (IEnumerable<Product>)data;
        //}
    }
}
