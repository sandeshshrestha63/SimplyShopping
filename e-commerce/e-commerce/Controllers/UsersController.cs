using e_commerce.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace e_commerce.Controllers
{
    public class UsersController : Controller
    {
        SimplyShoppingEntities db = new SimplyShoppingEntities();
        // GET: Users
        public ActionResult Index()
        {
            List<Tbl_Users> list = db.Tbl_Users.ToList();
            return View(list);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Tbl_Users tbl_Users)
        {
            tbl_Users.created_on = DateTime.Now;
            tbl_Users.status = true;
            db.Tbl_Users.Add(tbl_Users);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            Tbl_Users data = db.Tbl_Users.Find(id);
            return View(data);
        }
    }
}