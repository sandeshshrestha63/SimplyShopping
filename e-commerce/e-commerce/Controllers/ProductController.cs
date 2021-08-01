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
        public ActionResult AddToCart(Item item)
        {
            if (Session["cart"] == null)
            {
                List<Item> cart = new List<Item>();
                var product = db.Tbl_Product.Find(item.Product.ProductId);
                cart.Add(new Item()
                {
                    Product = product,
                    Quantity = item.Quantity
                });
                Session["cart"] = cart;
            }
            else
            {
                List<Item> cart = (List<Item>)Session["cart"];
                List<Item> newcart = new List<Item>();
                var product = db.Tbl_Product.Find(item.Product.ProductId);
                var i = 0;
                foreach (var data in cart)
                {
                    if (data.Product.ProductId == item.Product.ProductId)
                    {
                        int prevQty = data.Quantity;
                        //cart.Remove(item);
                        newcart.Add(new Item()
                        {
                            Product = product,
                            Quantity = prevQty + item.Quantity
                        });
                        i = i + 1;
                    }
                    else
                    {
                        newcart.Add(new Item()
                        {
                            Product = data.Product,
                            Quantity = data.Quantity
                        });
                    }
                }
                if (i == 0)
                {
                    newcart.Add(new Item()
                    {
                        Product = product,
                        Quantity = item.Quantity
                    });
                }
                Session["cart"] = newcart;
            }
            return RedirectToAction("Index","Home");
        }
    }
}