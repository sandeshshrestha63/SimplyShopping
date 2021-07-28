using e_commerce.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace e_commerce.Controllers
{
    public class AdminProductController : Controller
    {
        // GET: AdminProduct
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Tbl_Product tbl_Product)
        {
            //string[] AllowedExt = Properties.Settings.Default.OnlineFormExt.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            //string path = Properties.Settings.Default.ProductImages;
            //var res = UploadHelper.UploadFile(dis01Discount_Main_Details.ImgFile, path, AllowedExt);
            //if (res.Status)
            //{
            //    tbl_Product.ProductImage= path + res.FileName;
            //}
            return RedirectToAction("Index");
        }
    }
}