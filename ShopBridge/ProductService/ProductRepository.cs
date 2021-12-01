using ShopBridge.Models;
using ShopBridge.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;

namespace ShopBridge.ProductService
{
    public class ProductRepository : IProductService
    {
        private SqlConnection sqlConnection = null;
        SqlCommand sqlCmd;

        public ProductRepository()
        {
            sqlConnection = SqlCustomUtility.GetConnection(@"Data Source=EPINHYDW05C1\MSSQLSERVER1;Initial Catalog=SalesDB;Integrated Security=True");
        }
        public async Task<Product> AddProduct(Product product)
        {
            return await SaveUpdateProductSpProcCall(product);
        }

        public async Task<Product> DeleteProduct(int Id)
        {
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            Product product = await GetProduct(Id);

            sqlCmd = new SqlCommand("spProduct", sqlConnection);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@ActionType", "DeleteData");
            sqlCmd.Parameters.AddWithValue("@ProductId", Id.ToString());

            return await sqlCmd.ExecuteNonQueryAsync() > 0 ? product : null;

        }



        public async Task<Product> GetProduct(int Id)
        {

            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            DataTable dtData = new DataTable();
            sqlCmd = new SqlCommand("spProduct", sqlConnection);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@ActionType", "FetchRecord");
            sqlCmd.Parameters.AddWithValue("@ProductId", Id);
            SqlDataAdapter sqlSda = new SqlDataAdapter(sqlCmd);
            sqlSda.Fill(dtData);
            List<Product> product = await GetListByDataTable(dtData);
            return (dtData.Rows.Count > 0) ? product[0] : null;
        }

        public async Task<List<Product>> GetProducts()
        {
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            DataTable dtData = new DataTable();
            sqlCmd = new SqlCommand("spProduct", sqlConnection);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@ActionType", "FetchData");
            SqlDataAdapter sqlSda = new SqlDataAdapter(sqlCmd);
            sqlSda.Fill(dtData);

            return await GetListByDataTable(dtData);
        }

        private async Task<List<Product>> GetListByDataTable(DataTable dt)
        {

            var result =(from rw in dt.AsEnumerable()
                         select new Product()
                         {
                             PRODUCT_ID = Convert.ToInt32(rw["PRODUCT_ID"]),
                             PRODUCT_NAME = Convert.ToString(rw["PRODUCT_NAME"]),
                             PRODUCT_CATEGORY = Convert.ToString(rw["PRODUCT_CATEGORY"]),
                             PRODUCT_QUANTITY = Convert.ToInt32(rw["PRODUCT_QUANTITY"]),
                             PRODUCT_IS_INSTOCK = Convert.ToBoolean(rw["PRODUCT_IS_INSTOCK"]),
                             PRODUCT_PRICE = Convert.ToDouble(rw["PRODUCT_PRICE"])
                         }).ToList();

            return await Task.FromResult(result);
        }

        private async Task<Product> SaveUpdateProductSpProcCall(Product product)
        {
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            DataTable dtData = new DataTable();
            sqlCmd = new SqlCommand("spProduct", sqlConnection);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@ActionType", "SaveProduct");
            sqlCmd.Parameters.AddWithValue("@ProductId", product.PRODUCT_ID);
            sqlCmd.Parameters.AddWithValue("@ProductName", product.PRODUCT_NAME);
            sqlCmd.Parameters.AddWithValue("@ProductCategory", product.PRODUCT_CATEGORY);
            sqlCmd.Parameters.AddWithValue("@ProductPrice", product.PRODUCT_PRICE);
            sqlCmd.Parameters.AddWithValue("@ProductQuantity", product.PRODUCT_QUANTITY);
            sqlCmd.Parameters.AddWithValue("@ProductIsInStock", product.PRODUCT_IS_INSTOCK);

            return await sqlCmd.ExecuteNonQueryAsync() > 0 ? product : null;
        }

        public async Task<Product> UpdateProduct(Product product)
        {

            return await SaveUpdateProductSpProcCall(product);

        }
    }
}