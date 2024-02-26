using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DoAn_LTWeb.Models;

namespace DoAn_LTWeb.ApiControllers
{
    public class ProductsController : ApiController
    {
        private CompanyDBContext db = new CompanyDBContext();

        public IHttpActionResult Get(string search = "", string SortColumn = "Price", string IconClass = "fa-sort-asc", int page = 1)
        {
            // Search
            List<Product> products = db.Products.Where(row => row.Name.Contains(search)).ToList();

            // Sort: sắp xếp
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

            // Phân trang
            int NoOfRecordPerPage = 8;
            int NoOfPages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(products.Count) / Convert.ToDouble(NoOfRecordPerPage)));
            int NoOfRecordToSkip = (page - 1) * NoOfRecordPerPage;
            products = products.Skip(NoOfRecordToSkip).Take(NoOfRecordPerPage).ToList();

            // Trả về dữ liệu dưới dạng HttpResponseMessage
            return Ok(products);
        }
    }
}
