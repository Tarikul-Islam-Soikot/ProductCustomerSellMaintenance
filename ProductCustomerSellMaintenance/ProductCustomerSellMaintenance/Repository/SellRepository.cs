using ProductCustomerSellMaintenance.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProductCustomerSellMaintenance.Repository
{
    public class SellRepository
    {
        private SqlConnection con;
        //To Handle connection related activities    
        private void connection()
        {
            con = new SqlConnection(@"Data Source=DESKTOP-75EGOEF\SQLEXPRESS;Initial Catalog=ProductCustomerSellMaintenance;Integrated Security=True");
        }
        //To Add Sells details    
        public bool AddSell(SellModel obj)
        {

            connection();
            SqlCommand com = new SqlCommand("AddNewSellDetails", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Customers", obj.Customers);
            com.Parameters.AddWithValue("@Products", obj.Products);
            com.Parameters.AddWithValue("@Quantity", obj.Quantity);

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