using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductMaster.Business;
using ProductMaster.Business.Products.Contracts;
using ProductMaster.Entities.ExtraModel;
using ProductMaster.Entities.Models;

namespace ProductMaster.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsBusinessService _productsBusinessService;
        public ProductsController(IProductsBusinessService productsBusinessService)
        {
            _productsBusinessService = productsBusinessService ?? throw new ArgumentNullException(nameof(productsBusinessService));
        }

        [HttpGet]
        [Route("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            var apiResponse = new ApiResponse<List<string>>();

            try
            {
                var prodcutList = await _productsBusinessService.GetAllProducts();
                var resList = new ApiResponse<List<Product>>()
                {
                    Success = true,
                    Result = prodcutList
                };
                return Ok(resList);
            }
            catch (APIException ex)
            {
                apiResponse.Message = ex.Message;
                return StatusCode((int)ex._statusCode, apiResponse);
            }
            catch (Exception ex)
            {
                apiResponse.Message = ex.Message;
                return StatusCode(500, apiResponse);
            }
        }

        [HttpGet]
        [Route("productId/{productId}")]
        public async Task<IActionResult> GetProductById(long? productId)
        {
            var apiResponse = new ApiResponse<List<string>>();
            var messages = new List<string>();

            try
            {
                if (productId == null)
                {
                    messages.Add("Please pass productId value as parameter");
                }
                if (messages.Count > 0)
                {
                    apiResponse.Message = "Validation Error";
                    apiResponse.Result = messages;
                    return StatusCode(400, apiResponse);
                }

                var product = await _productsBusinessService.GetProductById(productId);
                var resList = new ApiResponse<Product>()
                {
                    Success = true,
                    Result = product
                };
                return Ok(resList);
            }
            catch (APIException ex)
            {
                apiResponse.Message = ex.Message;
                return StatusCode((int)ex._statusCode, apiResponse);
            }
            catch (Exception ex)
            {
                apiResponse.Message = ex.Message;
                return StatusCode(500, apiResponse);
            }
        }

        [HttpPost]
        [Route("AddProduct")]
        public async Task<IActionResult> AddProduct(Product product)
        {
            var apiResponse = new ApiResponse<List<string>>();
            var messages = new List<string>();

            try
            {
                if (string.IsNullOrWhiteSpace(product.ProductName))
                {
                    messages.Add("Please provide Product Name");
                }
                if (product.ProductPrice == null)
                {
                    messages.Add("Please provide Product Price");
                }
                if (messages.Count > 0)
                {
                    apiResponse.Message = "Validation Error";
                    apiResponse.Result = messages;
                    return StatusCode(400, apiResponse);
                }
                var addedProduct = await _productsBusinessService.AddProduct(product).ConfigureAwait(false);
                var res = new ApiResponse<Product>()
                {
                    Success = true,
                    Result = addedProduct
                };
                return Ok(res);

            }
            catch (APIException ex)
            {
                apiResponse.Message = ex.Message;
                return StatusCode((int)ex._statusCode, apiResponse);
            }
            catch (Exception ex)
            {
                apiResponse.Message = ex.Message;
                return StatusCode(500, apiResponse);
            }
        }

        [HttpPut]
        [Route("UpdateProduct")]
        public async Task<IActionResult> Update(Product product)
        {
            var apiResponse = new ApiResponse<List<string>>();
            var messages = new List<string>();

            try
            {
                if (string.IsNullOrWhiteSpace(product.ProductName))
                {
                    messages.Add("Please provide Product Name");
                }
                if (product.ProductPrice == null)
                {
                    messages.Add("Please provide Product Price");
                }
                if (messages.Count > 0)
                {
                    apiResponse.Message = "Validation Error";
                    apiResponse.Result = messages;
                    return StatusCode(400, apiResponse);
                }
                var updatedProduct = await _productsBusinessService.UpdateProduct(product).ConfigureAwait(false);
                var res = new ApiResponse<Product>()
                {
                    Success = true,
                    Result = updatedProduct
                };
                return Ok(res);
            }
            catch (APIException ex)
            {
                apiResponse.Message = ex.Message;
                return StatusCode((int)ex._statusCode, apiResponse);
            }
            catch (Exception ex)
            {
                apiResponse.Message = ex.Message;
                return StatusCode(500, apiResponse);
            }
        }

        [HttpPost("UpdateProductsPrice")]
        public async Task<IActionResult> UpdateProductsPrice(List<ProductPriceModel> products)
        {
            var apiResponse = new ApiResponse<List<string>>();
            var messages = new List<string>();

            try
            {
                foreach (var product in products)
                {                   
                    if (product.ProductPrice == null)
                    {
                        messages.Add("Please provide Product Price for ProductId :" + product.ProductId);
                    }
                    if (messages.Count > 0)
                    {
                        apiResponse.Message = "Validation Error";
                        apiResponse.Result = messages;
                        return StatusCode(400, apiResponse);
                    }
                }

                string resultMessage = await _productsBusinessService.UpdateProductsPrice(products);

                return Ok(resultMessage);
            }
            catch (APIException ex)
            {
                apiResponse.Message = ex.Message;
                return StatusCode((int)ex._statusCode, apiResponse);
            }
            catch (Exception ex)
            {
                apiResponse.Message = ex.Message;
                return StatusCode(500, apiResponse);
            }
        }

    }
}
