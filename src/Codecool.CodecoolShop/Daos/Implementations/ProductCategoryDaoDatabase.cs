using System.Collections.Generic;
using Codecool.CodecoolShop.Data;
using Codecool.CodecoolShop.Models;
using Microsoft.EntityFrameworkCore;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    class ProductCategoryDaoDatabase : IProductCategoryDao
    {
        private static ProductCategoryDaoDatabase instance = null;

        private readonly ApplicationDbContext _db;

        private ProductCategoryDaoDatabase(ApplicationDbContext db)
        {
            this._db = db;
        }

        public static ProductCategoryDaoDatabase GetInstance(ApplicationDbContext db)
        {
            if (instance == null)
            {
                instance = new ProductCategoryDaoDatabase(db);
            }

            return instance;
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
