using ProductCustomerSellMaintenance.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProductCustomerSellMaintenance.Repository
{
    public class ProductRepository
    {
        private SqlConnection con;
        //To Handle connection related activities    
        private void connection()
        {
            con = new SqlConnection(@"Data Source=DESKTOP-75EGOEF\SQLEXPRESS;Initial Catalog=ProductCustomerSellMaintenance;Integrated Security=True");
        }
        //To Add Product details    
        public bool AddProduct(Product obj)
        {

            connection();
            SqlCommand com = new SqlCommand("AddNewProductDetails", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@ProductName", obj.ProductName);
            com.Parameters.AddWithValue("@UnitPrice", obj.UnitPrice);
            com.Parameters.AddWithValue("@ProductCode", obj.ProductCode);

            con.Open();
            int i = com.ExecuteNonQuery();
            con.Close();
            if (i >= 1)       
                return true;
            else          
                return false;       
        }

    }
}