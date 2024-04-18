using ProductMaster.Entities;
using ProductMaster.Entities.ExtraModel;
using ProductMaster.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductMaster.Business.Products.Contracts
{
    public interface IProductsBusinessService
    {
        Task<List<Product>> GetAllProducts();
        Task<Product?> GetProductById(long? productId);
        Task<Product> AddProduct(Product product);
        Task<Product> UpdateProduct(Product product);
        Task<string> UpdateProductsPrice(List<ProductPriceModel> products);
    }
}
