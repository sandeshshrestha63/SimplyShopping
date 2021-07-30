using e_commerce.DAL;
using e_commerce.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace e_commerce.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        // GET: Admin
        public GenericUnitOfWork _uw = new GenericUnitOfWork();
        public ActionResult Dashboard()
        {
            return View();
        }
        public ActionResult Categories()
        {
            List<Tbl_Category> allCategory = _uw.GetRepositoryInstance<Tbl_Category>().GetAllRecordsIQueryable().Where(x =>x.IsDelete == false).ToList();
            return View(allCategory);
        }
    }
}