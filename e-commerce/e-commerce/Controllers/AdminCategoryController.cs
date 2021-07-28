using e_commerce.DAL;
using e_commerce.Models;
using e_commerce.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace e_commerce.Controllers
{
    public class AdminCategoryController : Controller
    {
        SimplyShoppingEntities db = new SimplyShoppingEntities();
        // GET: AdminCategory
        public GenericUnitOfWork _uw = new GenericUnitOfWork();
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoryDetail categoryDetail)
        {
            try
            {
                var tbl_category = new Tbl_Category();
                tbl_category.CategoryName = categoryDetail.CategoryName;
                tbl_category.IsActive = true;
                tbl_category.IsDelete = false;
                db.Tbl_Category.Add(tbl_category);
                db.SaveChanges();
            }
            catch(Exception ex)
            {

            }
            //_uw.GetRepositoryInstance<Tbl_Category>().Add(tbl_category);
            return RedirectToAction("Categories", "Admin");
        }

        public ActionResult Edit(int id)
        {
            Tbl_Category  tbl_Category = db.Tbl_Category.Find(id);
            if (tbl_Category == null )
            {
                return HttpNotFound();
            }
            return View(tbl_Category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Tbl_Category tbl_Category)
        {
            Tbl_Category olddata = db.Tbl_Category.Find(tbl_Category.CategoryId);
            if(olddata == null)
            {
                return HttpNotFound();
            }
            olddata.CategoryName = tbl_Category.CategoryName;
            if(tbl_Category.IsActive == null)
            {
                olddata.IsActive = false;
            }
            else
            {
                olddata.IsActive = tbl_Category.IsActive;
            }
            olddata.IsDelete = false;
            db.Entry(olddata).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Categories", "Admin");
        }
        public ActionResult Delete(int id)
        {
            Tbl_Category data = db.Tbl_Category.Find(id);
            return View(data);
        }
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tbl_Category olddata = db.Tbl_Category.Find(id);
            olddata.IsDelete = true;
            olddata.IsActive = false;
            db.Entry(olddata).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction ("Categories","Admin");
        }
    }
}