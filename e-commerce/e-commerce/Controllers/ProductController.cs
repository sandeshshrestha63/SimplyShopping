using e_commerce.DAL;
using e_commerce.Models.Home;
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
        public ActionResult Product(int id)
        {
            Tbl_Product tbl_Product = db.Tbl_Product.Find(id);
            Tbl_Category dt = db.Tbl_Category.Find(tbl_Product.CategoryId);
            ViewBag.category = dt.CategoryName;
            Item item = new Item();
            item.Product = tbl_Product;
            return View(item);
        }
    }
}