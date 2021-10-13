using System.Collections.Generic;
using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Models;

namespace Codecool.CodecoolShop.Services
{
    public class ProductService
    {
        private readonly IProductDao productDao;
        private readonly IProductCategoryDao productCategoryDao;
        private readonly ISupplierDao productSupplierDao;
        private readonly ICartDao cartDao;

        public ProductService(IProductDao productDao, IProductCategoryDao productCategoryDao, ISupplierDao productSupplierDao, ICartDao cartDao)
        {
            this.productDao = productDao;
            this.productCategoryDao = productCategoryDao;
            this.productSupplierDao = productSupplierDao;
            this.cartDao = cartDao;
        }

        public ProductCategory GetProductCategory(int categoryId)
        {
            return this.productCategoryDao.Get(categoryId);
        }

        public IEnumerable<Product> GetProductsForCategory(int categoryId)
        {
            ProductCategory categoryProducts = this.productCategoryDao.Get(categoryId);
            return this.productDao.GetBy(categoryProducts);
        }

        public IEnumerable<Product> GetProductsForSupplier(int supplierId)
        {
            var supplierProducts = this.productSupplierDao.Get(supplierId);
            return this.productDao.GetBy(supplierProducts);
        }

        public IEnumerable<Product> GetAllProducts()
        {
            var products = this.productDao.GetAll();
            return products;
        }

        public Product GetProduct(int id)
        {
            return this.productDao.Get(id);
        }

        public void AddToCart(Product product)
        {
            //cartDao.Add(product);
        }
    }
}
