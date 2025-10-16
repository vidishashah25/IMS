using IMS.CoreBusiness.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.CoreBusiness
{
    public class Product
    {
        public int ProductId { get; set; }

        [Required]
        [StringLength(150)]
        public string ProductName { get; set; } = string.Empty;

        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be greator or equal to 0.")]
        public int Quantity { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Price must be greator or equal to 0.")]
        public double Price { get; set; }

        [Product_EnsurePriceGreaterThanInventoriesCost]
        public List<ProductInventory> ProductInventories { get; set; } = new List<ProductInventory>();

        public void AddInventory (Inventory inventoy)
        {
            if(!this.ProductInventories.Any(x => 
            x.Inventory is not null &&
            x.Inventory.InventoryName == inventoy.InventoryName))
            {
                this.ProductInventories.Add(new ProductInventory
                {
                    InventoryId = inventoy.InventoryId,
                    Inventory = inventoy,
                    InventoryQuantity = 1,
                    ProductId = this.ProductId,
                    Product = this
                });
            }
            
        }

        public void RemoveInventory(ProductInventory productInventory)
        {
            this.ProductInventories?.Remove(productInventory);
        }
    }
}
