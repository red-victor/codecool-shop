using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Codecool.CodecoolShop.Models
{
    public class Product : BaseModel
    {
        [Required]
        public string Currency { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal DefaultPrice { get; set; }
        [Required]
        public ProductCategory ProductCategory { get; set; }
        [Required]
        public Supplier Supplier { get; set; }

        public void SetProductCategory(ProductCategory productCategory)
        {
            ProductCategory = productCategory;
            ProductCategory.Products.Add(this);
        }
    }
}
