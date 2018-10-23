using ProductCustomerSellMaintenance.Models;
using ProductCustomerSellMaintenance.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data;

namespace ProductCustomerSellMaintenance.Controllers
{
    public class SellsController : Controller
    {
        public ActionResult AddSell()
        {
            return View();
        }

        // POST: Sells/AddSell    
        [HttpPost]
        public ActionResult AddSell(SellModel Sell)
        {                      
           if (ModelState.IsValid)
            {
                SellRepository SellRepo = new SellRepository();

                if (SellRepo.AddSell(Sell))
                {
                    ViewBag.Message = "Sells details added successfully";
                }
            }

            return RedirectToAction("Index", "Home");
        }   

    }
}
