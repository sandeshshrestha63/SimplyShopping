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
                Tbl_Members data = db.Tbl_Members.Where(x => x.EmailId == tbl_Members.EmailId).FirstOrDefault() ;
                Session["UserName"] = data;
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
        public ActionResult NewMember()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveMember(Tbl_Members tbl_Members)
        {
            if(tbl_Members!= null)
            {
                Tbl_Members olddata = db.Tbl_Members.Where(x => x.EmailId == tbl_Members.EmailId.ToLower()).FirstOrDefault(); 
                if(olddata == null)
                {
                    if(tbl_Members.FristName ==null || tbl_Members.LastName == null || tbl_Members.EmailId ==null || tbl_Members.Password== null || tbl_Members.contact ==null)
                    {
                        ModelState.AddModelError("", "Please fill up all the details");
                        return RedirectToAction("NewMember", "Home");
                    }
                    tbl_Members.CreatedOn = DateTime.Now;
                    tbl_Members.IsActive = true;
                    tbl_Members.IsDelete = false;
                    db.Tbl_Members.Add(tbl_Members);
                    db.SaveChanges();
                    Tbl_Members dt = db.Tbl_Members.Where(x => x.EmailId == tbl_Members.EmailId).FirstOrDefault();
                    Session["UserName"] = null;
                    Session["UserName"] = dt;
                    return RedirectToAction("Checkout", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Email account is already registerd in our system");
                    return RedirectToAction("NewMember", "Account");
                }
               
            }
            return View();
        }
    }
}