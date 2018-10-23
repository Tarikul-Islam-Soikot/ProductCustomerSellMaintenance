using ProductCustomerSellMaintenance.Models;
using ProductCustomerSellMaintenance.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductCustomerSellMaintenance.Controllers
{
    public class CustomerController : Controller
    {
        public ActionResult create()
        {
            ProductCustomerSellMaintenanceEntities db = new ProductCustomerSellMaintenanceEntities();

            List<Customer> getCustomerList = db.Customers.ToList();                        
            return View(getCustomerList);
        }

        public ActionResult AddCustomer()
        {
            return View();
        }

        // POST: Customer/AddCustomer    
        [HttpPost]
        public ActionResult AddCustomer(Customer Pro)
        {

            if (ModelState.IsValid)
            {
                CustomerRepository CustomerRepo = new CustomerRepository();

                if (CustomerRepo.AddCustomer(Pro))
                {
                    ViewBag.Message = "Customer details added successfully";
                }
            }

            return View();
        }   

    }
}
