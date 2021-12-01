using FluentValidation.Attributes;
using ShopBridge.ProductService;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopBridge.Models
{
    [Validator(typeof(ProductValidator))]
    public class Product
    {
        public int PRODUCT_ID { get; set; }
        public string PRODUCT_NAME { get; set; }
        public string PRODUCT_CATEGORY { get; set; }

        public int PRODUCT_QUANTITY { get; set; }

        public bool PRODUCT_IS_INSTOCK { get; set; }

        public double PRODUCT_PRICE { get; set; }
    }
}