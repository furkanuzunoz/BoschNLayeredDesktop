using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess.Adonet.Helpers
{
    public static class DbCustomerHelper
    {
        public static DataSet GetData()
        {
            SqlConnection sqlConnection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDb;Initial Catalog=Northwind;");
            SqlDataAdapter dataAdapter = new SqlDataAdapter("Select * from Customers", sqlConnection);
            DataSet dataSet = new DataSet();
            sqlConnection.Open();
            dataAdapter.Fill(dataSet, "Customers");
            sqlConnection.Close();

            return dataSet;
        }
    }
}
