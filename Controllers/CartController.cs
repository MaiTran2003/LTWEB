using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAn_LTWeb.Migrations;
using DoAn_LTWeb.Models;

namespace DoAn_LTWeb.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            CompanyDBContext db = new CompanyDBContext();
            List<Cart> cart = db.Carts.ToList();
            int temp = 0;
            if (db.Carts != null)
            {
                foreach (var a in db.Carts)
                {
                    temp += a.Quantity;
                }
            }
            ViewBag.SLSP = temp;
            return View(cart);
        }


        public ActionResult Add(int? id)
        {
            if (id.HasValue)
            {
                CompanyDBContext db = new CompanyDBContext();
                Cart cartItem = db.Carts.Where(row => row.Id == id).FirstOrDefault();
                if (cartItem != null)
                {
                    cartItem.Quantity += 1;
                }
                else
                {
                    Cart car = new Cart();
                    car.Id = (int)id;
                    car.Quantity = 1;
                    db.Carts.Add(car);
                }
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Products");
        }

        public ActionResult UpdateQuantity(int quan, int proid)
        {
            CompanyDBContext db = new CompanyDBContext();
            if(quan > 0)
            {
                Cart cartItem = db.Carts.Where(row => row.Id == proid).FirstOrDefault();
                if (cartItem != null)
                {
                    cartItem.Quantity = quan;
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult DeleteQuantity(int proid)
        {
            CompanyDBContext db = new CompanyDBContext();
            Cart cartItem = db.Carts.Where(row => row.Id == proid).FirstOrDefault();
            if (cartItem != null)
            {
                db.Carts.Remove(cartItem);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}