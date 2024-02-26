using DoAn_LTWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAn_LTWeb.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Product
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
        // Thông tin sản phẩm
        public ActionResult Details(int id)
        {
            Product pro = db.Products.Where(x => x.Id == id).FirstOrDefault();
            int temp = 0;
            if (db.Carts != null)
            {
                foreach (var a in db.Carts)
                {
                    temp += a.Quantity;
                }
            }
            ViewBag.SLSP = temp;
            return View(pro);
        }
        public ActionResult sbar(int id, string search = "", string sortCol = "Price", string cheDo = "asc", int page = 1)
        {
            List<Product> pro = db.Products.Where(x => x.Brand_Id == id).ToList();
            List<Brand> bra = db.Brands.ToList();
            ViewBag.bra = bra;
            ViewBag.search = search;
            if (sortCol == "Price")
            {
                if (cheDo == "asc")
                {
                    pro = pro.OrderBy(x => x.Price).ToList();
                }
                else
                {
                    pro = pro.OrderByDescending(x => x.Price).ToList();
                }
            }
            ViewBag.sortCol = sortCol;
            ViewBag.cheDo = cheDo;
            int soDongMoiTrang = 4;
            int tongSoTrang = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(pro.Count) / Convert.ToDouble(soDongMoiTrang)));
            int skip = (page - 1) * soDongMoiTrang;
            pro = pro.Skip(skip).Take(soDongMoiTrang).ToList();
            ViewBag.page = page;
            ViewBag.tongST = tongSoTrang;
            ViewBag.id = id;
            return View(pro);
        }
    }
}