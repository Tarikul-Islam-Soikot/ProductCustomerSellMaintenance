using ProductCustomerSellMaintenance.Models;
using ProductCustomerSellMaintenance.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductCustomerSellMaintenance.Controllers
{
    public class ProductController : Controller
    {
        public ActionResult create()
        {
            ProductCustomerSellMaintenanceEntities db = new ProductCustomerSellMaintenanceEntities();

            List<Product> getProductList = db.Products.ToList();
            return View(getProductList);
        }
        public ActionResult AddProduct()
        {
            return View();
        }

        // POST: Product/AddProduct    
        [HttpPost]
        public ActionResult AddProduct(Product Pro)
        {

            if (ModelState.IsValid)
            {
                ProductRepository ProductRepo = new ProductRepository();

                if (ProductRepo.AddProduct(Pro))
                {
                    ViewBag.Message = "Product details added successfully";
                }
            }

            return View();
        }   

    }
}
