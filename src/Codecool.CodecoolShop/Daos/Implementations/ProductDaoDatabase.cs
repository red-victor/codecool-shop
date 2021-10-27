using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Codecool.CodecoolShop.Data;
using Codecool.CodecoolShop.Models;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    public class ProductDaoDatabase : IProductDao
    {
        private static ProductDaoDatabase instance = null;

        private readonly ApplicationDbContext _db;

        private ProductDaoDatabase(ApplicationDbContext db)
        {
            this._db = db;
        }

        public static ProductDaoDatabase GetInstance(ApplicationDbContext db)
        {
            if (instance == null)
            {
                instance = new ProductDaoDatabase(db);
            }

            return instance;
        }

        public void Add(Product item)
        {
            _db.Products.Add(item);
        }

        public void Remove(int id)
        {
            _db.Products.Remove(this.Get(id));
        }

        public Product Get(int id)
        {
            return _db.Products.Find(id);
        }

        public IEnumerable<Product> GetAll()
        {
            return _db.Products.ToList();
        }

        public IEnumerable<Product> GetBy(Supplier supplier)
        {
            return _db.Products.Where(x => x.Supplier.Id == supplier.Id).ToList();
        }

        public IEnumerable<Product> GetBy(ProductCategory productCategory)
        {
            return _db.Products.Where(x => x.ProductCategory.Id == productCategory.Id).ToList();
        }
    }
}
