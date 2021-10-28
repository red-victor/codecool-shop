using System.Collections.Generic;
using Codecool.CodecoolShop.Data;
using Codecool.CodecoolShop.Models;
using Microsoft.EntityFrameworkCore;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    class ProductCategoryDaoDatabase : IProductCategoryDao
    {
        private readonly ApplicationDbContext _db;

        public ProductCategoryDaoDatabase(ApplicationDbContext db)
        {
            this._db = db;
        }

        public void Add(ProductCategory item)
        {
            _db.ProductCategories.Add(item);
        }

        public void Remove(int id)
        {
            _db.ProductCategories.Remove(this.Get(id));
        }

        public ProductCategory Get(int id)
        {
            return _db.ProductCategories.Find(id);
        }

        public IEnumerable<ProductCategory> GetAll()
        {
            return _db.ProductCategories;
        }
    }
}
