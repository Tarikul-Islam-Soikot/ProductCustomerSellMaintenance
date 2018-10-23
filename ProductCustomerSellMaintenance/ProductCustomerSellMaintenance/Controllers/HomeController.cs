using CrystalDecisions.CrystalReports.Engine;
using ProductCustomerSellMaintenance.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductCustomerSellMaintenance.Controllers
{
    public class HomeController : Controller
    {    
        [HttpGet]
        public ViewResult Index()
        {
            ProductCustomerSellMaintenanceEntities db = new ProductCustomerSellMaintenanceEntities();

           /* List<Customer> getCustomerList = db.Customers.Distinct().ToList();
            ViewBag.CustomerList = new SelectList(getCustomerList, "CustomerName", "CustomerName");*/

            List<Customer> getCustomerList = new List<Customer>();
            var getCustomer = db.Customers.GroupBy(m=> m.CustomerName).Select(m => m.FirstOrDefault());
            getCustomerList = getCustomer.ToList();
            ViewBag.CustomerList = new SelectList(getCustomerList, "CustomerName", "CustomerName");

            List<Product> getProductList = new List<Product>();
            var getProduct = db.Products.GroupBy(m => m.ProductName).Select(m => m.FirstOrDefault());
            getProductList = getProduct.ToList();
            ViewBag.ProductList = new SelectList(getProductList, "ProductName", "ProductName");

            /*List<Product> getProductList = db.Products.ToList();
            ViewBag.ProductList = new SelectList(getProductList, "ProductName", "ProductName");*/

           /* SalesEntities Sale = new SalesEntities();
            ViewBag.Sale = Sale.Sells.ToList();*/
           

            return View();
        }


        public ActionResult ShowTable()
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-75EGOEF\SQLEXPRESS;Initial Catalog=ProductCustomerSellMaintenance;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("Update_Price_For_Sale_Table", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            SalesEntities getSale = new SalesEntities();
            List<Sell> Sales = getSale.Sells.ToList();
            return View(Sales);
        }

        public ActionResult DownloadSalesList()
        {
            SalesEntities getSale = new SalesEntities();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Report/CrystalReport.rpt")));
            rd.SetDataSource(getSale.Sells.ToList());
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
           
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "Sales_List.pdf");
        }

        [HttpPost]
        public ActionResult UpdateSell(Sell sell)
        {
            using (SalesEntities entities = new SalesEntities())
            {
                Sell updatedSale = (from c in entities.Sells
                                            where c.CustomerId ==sell.CustomerId
                                            select c).FirstOrDefault();
                updatedSale.Customers = sell.Customers;
                updatedSale.Products = sell.Products;
                updatedSale.Quantity = sell.Quantity;
                updatedSale.Price = sell.Price;
                entities.SaveChanges();
            }

            return new EmptyResult();
        }

        [HttpPost]
        public ActionResult DeleteCustomer(int customerId)
        {
            using (SalesEntities entities = new SalesEntities())
            {
                Sell Sale = (from c in entities.Sells
                                    where c.CustomerId == customerId
                                    select c).FirstOrDefault();
                entities.Sells.Remove(Sale);           
                entities.SaveChanges();
            }

            return new EmptyResult();
        }

    }

}
