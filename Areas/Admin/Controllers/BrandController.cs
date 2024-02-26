using DoAn_LTWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAn_LTWeb.Areas.Admin.Controllers
{
    public class BrandController : Controller
    {
        // GET: Admin/Brand
        CompanyDBContext db = new CompanyDBContext();
        public ActionResult Index()
        {
            List<Brand> brands = db.Brands.ToList();
            return View(brands);
        }
        public ActionResult Create()
        {
            ViewBag.Brands = db.Brands.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Create(Brand b)
        {
            db.Brands.Add(b);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Update()
        {
            List<Brand> brands = db.Brands.ToList();
            return View(brands);
        }
        [HttpPost]
        public ActionResult Update(List<Brand> brands)
        {
            foreach(Brand brand in brands)
            {
                Brand oldBrand = db.Brands.Find(brand.Id);
                oldBrand.Name = brand.Name;
            }    
            db.SaveChanges();
            return RedirectToAction("Update");
        }
        public ActionResult Delete(int id)
        {
            Brand brands = db.Brands.Where(row => row.Id == id).FirstOrDefault();
            return View(brands);
        }
        [HttpPost]
        public ActionResult Delete(int id, Brand b)
        {
            Brand brands = db.Brands.Where(row => row.Id == id).FirstOrDefault();
            db.Brands.Remove(brands);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}