using Codecool.CodecoolShop.Daos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Codecool.CodecoolShop.Data
{
    public interface IUnitOfWork : IDisposable
    {
        ISupplierDao Suppliers { get; }
        IProductCategoryDao Categories { get; }
        IProductDao Products { get; }
        ICartDao Carts { get; }
    }
}
