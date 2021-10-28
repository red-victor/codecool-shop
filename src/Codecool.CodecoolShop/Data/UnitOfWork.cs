using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Daos.Implementations;

namespace Codecool.CodecoolShop.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public ISupplierDao Suppliers { get; private set; }
        public IProductCategoryDao Categories { get; private set; }
        public IProductDao Products { get; private set; }
        public ICartDao Carts { get; private set; }
        public IApplicationUser Users { get; private set; }


        public UnitOfWork(ApplicationDbContext db)
        {
            this._db = db;
            this.Users = new ApplicationUserDaoDatabase(_db);
            this.Suppliers = new SupplierDaoDatabase(_db);
            this.Categories = new ProductCategoryDaoDatabase(_db);
            this.Products = new ProductDaoDatabase(_db);
            this.Carts = new CartDaoDatabase(_db);
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
