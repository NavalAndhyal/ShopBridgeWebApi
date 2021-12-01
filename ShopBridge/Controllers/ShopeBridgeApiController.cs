using ShopBridge.Models;
using ShopBridge.ProductService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ShopBridge.Controllers
{
    public class ShopeBridgeApiController : ApiController
    {
        IProductService productRepository = new ProductRepository();
        // GET api/<controller>
        public async Task<List<Product>> Get()
        {
            return await productRepository.GetProducts();
        }


        // GET api/<controller>/5
        public async Task<Product> Get(int id)
        {
                Product product = await productRepository.GetProduct(id);

                if (product == null)
                {
                    var res = new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent("No Product found with ID = " + id),
                        ReasonPhrase = "Product Not Found"
                    };
                    throw new HttpResponseException(res);
                }
                else
                    return product;        
        }

        // POST api/<controller>
        public async Task<Product> Post([FromBody] Product product)
        {
            ProductValidator productValidator = new ProductValidator();
            if (await productValidator.ValidateProductId(product.PRODUCT_ID))
            {
                var res = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent("Product is already found with ID = " + product.PRODUCT_ID),
                    ReasonPhrase = "Duplicate Product ID"
                };
                throw new HttpResponseException(res);
            }
            return await productRepository.AddProduct(product);
        }

        // PUT api/<controller>/5
        public async Task<Product> Put(int id, [FromBody] Product product)
        {
            return await productRepository.UpdateProduct(product);
        }

        // DELETE api/<controller>/5
        public async Task<Product> Delete(int id)
        {
            Product product = await productRepository.GetProduct(id);

            if (product == null)
            {
                var res = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent("No Product found with ID = " + id),
                    ReasonPhrase = "Product Not Found"
                };
                throw new HttpResponseException(res);
            }
            else
                return await productRepository.DeleteProduct(id);
        }
    }
}