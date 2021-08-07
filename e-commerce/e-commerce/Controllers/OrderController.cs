using e_commerce.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace e_commerce.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        SimplyShoppingEntities db = new SimplyShoppingEntities();
        // GET: Order
        public ActionResult Index()
        {
            var orderList = db.tbl_OrdKey.OrderByDescending(x=> x.OrdID).ToList();
            return View(orderList);
        }
    }
}