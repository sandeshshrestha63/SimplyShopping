using e_commerce.DAL;
using e_commerce.Models;
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

        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePassword changePassword)
        {
            try
            {
                if(changePassword!= null)
                {
                    var email = User.Identity.Name;
                    Tbl_Users tbl_Users = db.Tbl_Users.Where(x => x.email == email).FirstOrDefault();
                    if(changePassword.NewPassword == null || changePassword.OldPassword == null || changePassword.ConfirmPassword== null)
                    {
                        ModelState.AddModelError("", "Please add all details");
                        return View();
                    }
                    else if(changePassword.OldPassword != tbl_Users.password)
                    {
                        ModelState.AddModelError("", "Old password Doesn't match with user email");
                        return View();
                    }
                    else if(changePassword.NewPassword != changePassword.ConfirmPassword)
                    {
                        ModelState.AddModelError("", "New Password and Confirm password Doesnt match !");
                        return View();
                    }
                    else
                    {
                        tbl_Users.password = changePassword.ConfirmPassword;
                        db.Entry(tbl_Users).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        ViewData["Message"] = "Success";
                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Some error occured! Please try again.");
                    return View();
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.ToString());
                return View();
            }
        }
    }
}