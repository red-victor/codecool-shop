using System.Collections.Generic;
using Codecool.CodecoolShop.Data;
using Codecool.CodecoolShop.Models;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    public class SupplierDaoDatabase : ISupplierDao
    {
        private static SupplierDaoDatabase instance = null;

        private readonly ApplicationDbContext _db;
        private SupplierDaoDatabase(ApplicationDbContext db)
        {
            this._db = db;
        }

        public static SupplierDaoDatabase GetInstance(ApplicationDbContext _db)
        {
            if (instance == null)
            {
                instance = new SupplierDaoDatabase(_db);
            }

            return instance;
        }

        public void Add(Supplier item)
        {
            _db.Suppliers.Add(item);
        }

        public void Remove(int id)
        {
            _db.Suppliers.Remove(this.Get(id));
        }

        public Supplier Get(int id)
        {
            return _db.Suppliers.Find(id);
        }

        public IEnumerable<Supplier> GetAll()
        {
            return _db.Suppliers;
        }
    }
}
