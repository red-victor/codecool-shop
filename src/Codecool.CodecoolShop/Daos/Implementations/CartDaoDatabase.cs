using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Codecool.CodecoolShop.Data;
using Codecool.CodecoolShop.Models;
using Microsoft.EntityFrameworkCore;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    public class CartDaoDatabase : ICartDao
    {
        private readonly ApplicationDbContext _db;

        public CartDaoDatabase(ApplicationDbContext db)
        {
            this._db = db;
        }

        public void EmptyCart(string userID)
        {
            var result = _db.Carts.SingleOrDefault(c => c.User.Id == userID);
            if (result != null)
            {
                result.Items = new List<CartItem>();
                _db.SaveChanges();
            }
        }

        public IEnumerable<CartItem> GetProducts(string userId)
        {
            return _db.Carts.Where(c => c.User.Id == userId).Include(c => c.Items).FirstOrDefault().Items;
        }

        public void SaveCart(string userId, List<CartItem> items)
        {
            var result = _db.Carts.SingleOrDefault(c => c.User.Id == userId);
            if (result != null)
            {
                result.Items = items;
                _db.SaveChanges();
            }
            else
            {
                var cart = new Cart() { Items = items, User = _db.Users.Find(userId) };
                _db.Carts.Add(cart);
                _db.SaveChanges();
            }
        }

        /*public void SaveCart(string userID, List<CartItem> cartList)
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
        }*/

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
