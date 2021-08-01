using e_commerce.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace e_commerce.Controllers
{
    public class AccountController : Controller
    {
        SimplyShoppingEntities db = new SimplyShoppingEntities();
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Tbl_Members tbl_Members)
        {
            bool isValid = db.Tbl_Members.Any(x => x.EmailId == tbl_Members.EmailId&& x.Password  == tbl_Members.Password  && x.IsActive== true && x.IsDelete == false);
            if(isValid)
            {
                Session["UserName"] = tbl_Members.EmailId;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View("Login");
            }
        }
        public ActionResult Logout()
        {
            Session["UserName"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}