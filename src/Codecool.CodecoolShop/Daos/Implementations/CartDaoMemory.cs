using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Codecool.CodecoolShop.Models;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    public class CartDaoMemory : ICartDao
    {
        private List<CartItem> data = new List<CartItem>();
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

        public void SaveCart(List<CartItem> cartList)
        {
            data = cartList;
        }
        public void EmptyCart()
        {
            data = new List<CartItem>();
        }

        public List<CartItem> GetProducts()
        {
            return data;
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
