using e_commerce.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace e_commerce.Controllers
{
    public class ProductController : Controller
    {
        SimplyShoppingEntities db = new SimplyShoppingEntities();
        // GET: Product
        public ActionResult ProductList(int? CategoryId)
        {
            var productlist = db.Tbl_Product.Where(x => x.CategoryId == CategoryId).ToList();
            return View(productlist);
        }
    }
}