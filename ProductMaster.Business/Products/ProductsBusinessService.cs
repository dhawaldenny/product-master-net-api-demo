using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProductMaster.Business.Products.Contracts;
using ProductMaster.Entities;
using ProductMaster.Entities.ExtraModel;
using ProductMaster.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ProductMaster.Business.Products
{
    public class ProductsBusinessService : IProductsBusinessService
    {
        private readonly ProductMasterContext _context;

        public ProductsBusinessService(ProductMasterContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<Product>> GetAllProducts()
        {
            try
            {
                return await _context.Products.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new APIException(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        public async Task<Product?> GetProductById(long? productId)
        {
            try
            {
                var existingProduct = await _context.Products.FirstOrDefaultAsync(x => x.ProductId == productId).ConfigureAwait(false);

                if (existingProduct != null)
                {                   
                    return existingProduct;
                }
                else
                {
                    throw new APIException($"Product with ID {productId} does not exist", HttpStatusCode.BadRequest);
                }
            }
            catch (Exception ex)
            {
                throw new APIException(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        public async Task<Product> AddProduct(Product p)
        {
            try
            {
                var product = await _context.Products.AddAsync(new Product()
                {
                    ProductName = p.ProductName,
                    ProductPrice = p.ProductPrice,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                }).ConfigureAwait(false);
                await _context.SaveChangesAsync().ConfigureAwait(false);

                return product.Entity;
            }
            catch (Exception ex)
            {
                throw new APIException(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            try
            {
                var existingProduct = await _context.Products.FirstOrDefaultAsync(x => x.ProductId == product.ProductId).ConfigureAwait(false);

                if (existingProduct != null)
                {
                    existingProduct.ProductName = product.ProductName;
                    existingProduct.ProductPrice = product.ProductPrice;
                    existingProduct.ModifiedDate = DateTime.Now;
                    existingProduct.CreatedDate = existingProduct.CreatedDate;

                    _context.Products.Update(existingProduct);
                    await _context.SaveChangesAsync().ConfigureAwait(false);

                    return existingProduct;
                }
                else
                {
                    throw new APIException("Product does not exist", HttpStatusCode.BadRequest);
                }
            }
            catch (Exception ex)
            {
                throw new APIException(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        public async Task<string> UpdateProductsPrice(List<ProductPriceModel> products)
        {
            try
            {
                bool anyUpdateFailed = false;

                foreach (var product in products)
                {
                    var existingProduct = await _context.Products.FirstOrDefaultAsync(x => x.ProductId == product.ProductId).ConfigureAwait(false);

                    if (existingProduct != null)
                    {
                        existingProduct.ProductPrice = product.ProductPrice;
                        existingProduct.ModifiedDate = DateTime.Now;
                        existingProduct.CreatedDate = existingProduct.CreatedDate;

                        _context.Products.Update(existingProduct);
                    }
                    else
                    {
                        throw new APIException($"Product with ID {product.ProductId} does not exist", HttpStatusCode.BadRequest);
                    }
                }

                await _context.SaveChangesAsync().ConfigureAwait(false);

                if (!anyUpdateFailed)
                {
                    return "All products updated successfully.";
                }
                else
                {
                    throw new APIException("Some products failed to update.", HttpStatusCode.BadRequest);
                }
            }
            catch (Exception ex)
            {
                throw new APIException(ex.Message, HttpStatusCode.BadRequest);
            }
        }

    }
}
