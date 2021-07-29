using e_commerce.DAL;
using e_commerce.Models.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace e_commerce.Controllers
{
    public class HomeController : Controller
    {
        SimplyShoppingEntities db = new SimplyShoppingEntities();
        public ActionResult Index(string Search,int? page)
        {
            HomeIndexViewModel model = new HomeIndexViewModel();
            return View(model.CreateModel(Search,page));
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult AddToCart(int productId)
        {
            if (Session["cart"] == null)
            {
                List<Item> cart = new List<Item>();
                var product = db.Tbl_Product.Find(productId);
                cart.Add(new Item()
                {
                    Product = product,
                    Quantity = 1
                });
                Session["cart"] = cart;
            }
            else
            {
                List<Item> cart = (List<Item>)Session["cart"];
                List<Item> newcart = new List<Item>();
                var product = db.Tbl_Product.Find(productId);
                var i = 0;
                foreach (var item in cart)
                {
                    if (item.Product.ProductId == productId)
                    {
                        int prevQty = item.Quantity;
                        //cart.Remove(item);
                        newcart.Add(new Item()
                        {
                            Product = product,
                            Quantity = prevQty + 1
                        });
                        i = i + 1;
                    }
                    else
                    {
                        newcart.Add(new Item()
                        {
                            Product = item.Product,
                            Quantity = item.Quantity
                        });
                    }
                }
                if (i==0)
                {
                    newcart.Add(new Item()
                    {
                        Product = product,
                        Quantity = 1
                    });
                }
                Session["cart"] = newcart;
            }
            return Redirect("Index");
        }
        public ActionResult RemoveFromCart(int productId)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            foreach (var item in cart)
            {
                if (item.Product.ProductId == productId)
                {
                    cart.Remove(item);
                    break;
                }
            }
            Session["cart"] = cart;
            return Redirect("Index");
        }

        public ActionResult Checkout()
        {
            return View();
        }

        public ActionResult CheckoutDetails()
        {
            return View();
        }

        public ActionResult Updatecart(int productId,int mthd)
        {
            if (Session["cart"] == null)
            {
                List<Item> cart = new List<Item>();
                var product = db.Tbl_Product.Find(productId);
                cart.Add(new Item()
                {
                    Product = product,
                    Quantity = 1
                });
                Session["cart"] = cart;
            }
            else
            {
                List<Item> cart = (List<Item>)Session["cart"];
                List<Item> newcart = new List<Item>();
                var product = db.Tbl_Product.Find(productId);
                var i = 0;
                foreach (var item in cart)
                {
                    if (item.Product.ProductId == productId)
                    {
                        int prevQty = item.Quantity;
                        //cart.Remove(item);
                        if(mthd ==1)
                        {
                            newcart.Add(new Item()
                            {
                                Product = product,
                                Quantity = prevQty + 1
                            });
                        }
                        else
                        {
                            if(prevQty >1)
                            {
                                newcart.Add(new Item()
                                {
                                    Product = product,
                                    Quantity = prevQty -1
                                });
                            }
                        }
                        
                    }
                    else
                    {
                        newcart.Add(new Item()
                        {
                            Product = item.Product,
                            Quantity = item.Quantity
                        });
                    }
                }
               
                Session["cart"] = newcart;
            }
            
            return RedirectToAction("Checkout", "Home");
        }
    }
}