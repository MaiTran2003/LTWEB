using DoAn_LTWeb.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAn_LTWeb.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        // GET: Admin/Product
        CompanyDBContext db = new CompanyDBContext();
        public ActionResult Index(string search = "", string SortColumn = "Price", string IconClass = "fa-sort-asc", int page = 1)
        {
            // Search
            List<Product> products = db.Products.Where(row => row.Name.Contains(search)).ToList();
            ViewBag.search = search;
            // Sort: sắp xếp
            ViewBag.SortColumn = SortColumn;
            ViewBag.IconClass = IconClass;
             if (SortColumn == "Price")
            {
                if (IconClass == "fa-sort-asc")
                {
                    products = products.OrderBy(row => row.Price).ToList();

                }
                else
                {
                    products = products.OrderByDescending(row => row.Price).ToList();
                }
            }
            ViewBag.Sortcolumn = SortColumn;
            ViewBag.IconClass = IconClass;
            // Phân trang
            int NoOfRecordPerPage = 8;
            int NoOfPages = Convert.ToInt32(Math.Ceiling(
                Convert.ToDouble(products.Count) / Convert.ToDouble(NoOfRecordPerPage)));
            int NoOfRecordToSkip = (page - 1) * NoOfRecordPerPage;
            ViewBag.Page = page;
            ViewBag.NoOfPages = NoOfPages;
            products = products.Skip(NoOfRecordToSkip).Take(NoOfRecordPerPage).ToList();
            List<Brand> bra = db.Brands.ToList();
            ViewBag.bra = bra;
            return View(products);
        }
        // Thêm sản phẩm
        public ActionResult Add()
        {
            ViewBag.brands = db.Brands.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Add(Product p, HttpPostedFileBase imageFile)
        {
            ViewBag.brands = db.Brands.ToList();
            if(ModelState.IsValid)
            {
                CompanyDBContext db = new CompanyDBContext();
                if (imageFile != null && imageFile.ContentLength > 0)
                {
                    if(imageFile.ContentLength > 2000000)
                    {
                        ModelState.AddModelError("Image", "Kích thước file phải nhỏ hơn 2MB");
                        return View();
                    }
                    var allowEx = new[] { ".jpg", ".png" };
                    var fileEx = Path.GetExtension(imageFile.FileName).ToLower();
                    if(!allowEx.Contains(fileEx))
                    {
                        ModelState.AddModelError("Image", "Chỉ chấp nhận file ảnh jpg hoặc png.");
                        return View();
                    }
                    p.Image = "";
                    db.Products.Add(p);
                    db.SaveChanges();

                    Product p2 = db.Products.ToList().Last();

                    var fileName = p2.Id.ToString() + fileEx;
                    var path = Path.Combine(Server.MapPath("~/img"), fileName);
                    imageFile.SaveAs(path);

                    p2.Image = fileName;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    p.Image = "";
                    db.Products.Add(p);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return View();
            }
        }
        public ActionResult Edit(int id)
        {
            Product products = db.Products.Where(row => row.Id == id).FirstOrDefault();
            ViewBag.Brands = db.Brands.ToList();
            return View(products);
        }
        [HttpPost]
        public ActionResult Edit(Product pro)
        {
            Product products = db.Products.Where(row => row.Id == pro.Id).FirstOrDefault();
            // Update
            products.Name = pro.Name;
            products.Price = pro.Price;
            products.Description = pro.Description;
            db.SaveChanges();
            return RedirectToAction("Edit");
        }
        public ActionResult Delete(int id)
        {
            Product products = db.Products.Where(row => row.Id == id).FirstOrDefault();
            return View(products);
        }
        [HttpPost]
        public ActionResult Delete(int id, Product p)
        {
            Product products = db.Products.Where(row => row.Id == id).FirstOrDefault();
            db.Products.Remove(products);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        // Thông tin sản phẩm
        public ActionResult Detail(int id)
        {
            Product pro = db.Products.Where(x => x.Id == id).FirstOrDefault();
            return View(pro);
        }
    }
}