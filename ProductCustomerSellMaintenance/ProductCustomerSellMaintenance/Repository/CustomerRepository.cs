using ProductCustomerSellMaintenance.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProductCustomerSellMaintenance.Repository
{
    public class CustomerRepository
    {
        private SqlConnection con;
        //To Handle connection related activities    
        private void connection()
        {
            con = new SqlConnection(@"Data Source=DESKTOP-75EGOEF\SQLEXPRESS;Initial Catalog=ProductCustomerSellMaintenance;Integrated Security=True");
        }
        //To Add Customer details    
        public bool AddCustomer(Customer obj)
        {

            connection();
            SqlCommand com = new SqlCommand("AddNewCustomerDetails", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@CustomerName", obj.CustomerName);
            com.Parameters.AddWithValue("@Address", obj.Address);
            com.Parameters.AddWithValue("@MobileNo", obj.MobileNo);

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