using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAn_LTWeb.Models;

namespace DoAn_LTWeb.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Mypf(int? id)
        {
            CompanyDBContext db = new CompanyDBContext();
            User user = db.Users.FirstOrDefault(row => row.UserId == id);

            if (user == null)
            {
                // Handle the case where the user with the specified id is not found
                return HttpNotFound();
            }

            return View(user);
        }
    }
}