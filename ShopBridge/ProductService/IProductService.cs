using ShopBridge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridge.ProductService
{
    interface IProductService
    {
        Task<Product> AddProduct(Product employee);

        Task<List<Product>> GetProducts();

        Task<Product> UpdateProduct(Product product);

        Task<Product> DeleteProduct(int Id);

        Task<Product> GetProduct(int Id);
    }
}
