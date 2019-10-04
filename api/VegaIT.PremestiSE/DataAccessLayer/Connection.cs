using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DataAccessLayer
{
    public class Connection
    {
        public static SqlConnection CreateConnection()
        {
            SqlConnectionStringBuilder con = new SqlConnectionStringBuilder();
            con.DataSource = @"DVARMEDJA";
            con.InitialCatalog = "premestise";
            con.IntegratedSecurity = true;

            string connection = con.ToString();

            return new SqlConnection(connection);
        }
    }
}
