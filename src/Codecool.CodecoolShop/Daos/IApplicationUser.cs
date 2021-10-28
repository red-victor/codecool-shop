using Codecool.CodecoolShop.Models;
using System.Collections.Generic;

namespace Codecool.CodecoolShop.Daos
{
    public interface IApplicationUser
    {
        void Add(ApplicationUser item);
        void Remove(string id);

        ApplicationUser Get(string id);
        IEnumerable<ApplicationUser> GetAll();
    }
}
