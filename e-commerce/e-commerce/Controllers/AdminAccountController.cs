using e_commerce.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace e_commerce.Controllers
{
    public class AdminAccountController : Controller
    {
        SimplyShoppingEntities db = new SimplyShoppingEntities();
        // GET: AdminAccount
        public ActionResult login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Tbl_Users user)
        {
            bool isValid = db.Tbl_Users.Any(x => x.email == user.email && x.password == user.password);
            if (isValid)
            {
                FormsAuthentication.SetAuthCookie(user.email, false);
                return RedirectToAction("Dashboard", "Admin");
            }
            else
            {
                ModelState.AddModelError("", "Invalid Username and Password");
                return View();
            }   
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}