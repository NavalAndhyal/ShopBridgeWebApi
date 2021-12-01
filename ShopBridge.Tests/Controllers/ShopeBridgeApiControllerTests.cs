using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShopBridge.Controllers;
using ShopBridge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridge.Controllers.Tests
{
    public class SkipInitializeAttribute : Attribute
    {
    }

    [TestClass()]
    public class ShopeBridgeApiControllerTests
    {
        ShopeBridgeApiController apiController = new ShopeBridgeApiController();
        Product product = null;
        public TestContext TestContext { get; set; }

        private bool IsInitializationDone { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            product = new Product
            {
                PRODUCT_ID = 1000,
                PRODUCT_NAME = "NAVAL123",
                PRODUCT_CATEGORY = "HOME",
                PRODUCT_QUANTITY = 12,
            };
            apiController = new ShopeBridgeApiController();
        }


        [TestMethod()]
        public async Task GetTestAsync()
        {
            var Prod_cnt = 14;
            //var shopController = new ShopeBridgeApiController();
            var result = await apiController.Get();// as List<Product>;
            Assert.AreEqual(Prod_cnt, result.Count);
        }

        [TestMethod()]
        public async Task GetByIdTestAsync()
        {
            var ProductId = 1000;
            //Product product = new Product
            //{
            //    PRODUCT_ID = 1000,
            //    PRODUCT_NAME = "NAVAL123",
            //    PRODUCT_CATEGORY = "HOME",
            //    PRODUCT_QUANTITY = 12,
            //};
            //var shopController = new ShopeBridgeApiController();
            var result = await apiController.Get(ProductId);// as List<Product>;
            Assert.AreEqual(product.PRODUCT_ID, result.PRODUCT_ID);
        }

        [TestMethod]
        public async Task PutTestAsync()
        {
            var ProductId = 1031;
            var result = await apiController.Put(product.PRODUCT_ID,product);

            Assert.AreEqual(ProductId, result.PRODUCT_ID);

        }

        [TestMethod]
        public async Task DeleteTestAsync()
        {

            //Modify this for each test case, because it will delete the entry from db
            var ProductId = 1031;

            var shopController = new ShopeBridgeApiController();
            var result = await shopController.Delete(ProductId);

            Assert.AreEqual(ProductId, result.PRODUCT_ID);

        }
    }
}