using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace ShopBridge.Utility
{
    public static class SqlCustomUtility
    {
        
        public static SqlConnection GetConnection(string connectionStringWebConfigKey)
        {
            //return new SqlConnection(WebConfigurationManager.ConnectionStrings[connectionStringWebConfigKey].ToString());
            return new SqlConnection(connectionStringWebConfigKey);
        }

    }
}