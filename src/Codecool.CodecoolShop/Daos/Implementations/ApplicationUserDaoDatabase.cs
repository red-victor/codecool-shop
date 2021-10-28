using System.Collections.Generic;
using Codecool.CodecoolShop.Data;
using Codecool.CodecoolShop.Models;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    public class ApplicationUserDaoDatabase : IApplicationUser
    {
        private readonly ApplicationDbContext _db;
        public ApplicationUserDaoDatabase(ApplicationDbContext db)
        {
            this._db = db;
        }

        public void Add(ApplicationUser user)
        {
            _db.ApplicationUsers.Add(user);
        }

        public void Remove(string id)
        {
            _db.ApplicationUsers.Remove(this.Get(id));
        }

        public ApplicationUser Get(string id)
        {
            return _db.ApplicationUsers.Find(id);
        }

        public IEnumerable<ApplicationUser> GetAll()
        {
            return _db.ApplicationUsers;
        }
    }
}
