using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAn_LTWeb.Models;
namespace DoAn_LTWeb.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index(string search = "")
        {
            CompanyDBContext db = new CompanyDBContext();
            // Search
            List<Product> products = db.Products.Where(row => row.Name.Contains(search)).ToList();
            ViewBag.search = search;
            int temp = 0;
            if (db.Carts != null)
            {
                foreach (var a in db.Carts)
                {
                    temp += a.Quantity;
                }
            }
            ViewBag.SLSP = temp;
            return View(products);
        }
    }
}