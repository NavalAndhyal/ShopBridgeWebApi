using FluentValidation;
using ShopBridge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ShopBridge.ProductService
{
    public class ProductValidator : AbstractValidator<Product>
    {
        IProductService productService = new ProductRepository();
        public ProductValidator()
        {
            RuleFor(x => x.PRODUCT_ID).GreaterThan(0).WithMessage("The Product ID must be at greather than 0.");


            RuleFor(x => x.PRODUCT_NAME)
                .NotEmpty()
                .WithMessage("The Product Name cannot be blank.")
                .Length(0, 50)
                .WithMessage("The Product Name cannot be more than 100 characters.");

            RuleFor(x => x.PRODUCT_CATEGORY)
                .NotEmpty()
                .WithMessage("The Product Category Cannot be blank")
                .Length(0,50)
                .WithMessage("The Product Description must be at least 50 characters long.");

            RuleFor(x => x.PRODUCT_PRICE).GreaterThan(0).WithMessage("The Product Price must be at greather than 0.");

            RuleFor(x => x.PRODUCT_QUANTITY).GreaterThan(0).WithMessage("The Product Quantity must be at greather than 0.");
        }

        public async Task<bool> ValidateProductId(int ProductId)
        {
            List<Product> products = await productService.GetProducts();

            bool res = (from product in products
                       where product.PRODUCT_ID == ProductId
                       select product).ToList().Count > 0;

            return res;
        }
    }
}