using e_commerce.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using e_commerce.Report;

namespace e_commerce.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        SimplyShoppingEntities db = new SimplyShoppingEntities();
        public ActionResult Report(tbl_OrdKey tbl_OrdKey)
        {
            OrderDetailsReport orderDetailsReport = new OrderDetailsReport();
            List < tbl_OrdKey > list = db.tbl_OrdKey.ToList();
            byte[] abytes = orderDetailsReport.PrepareReport(list);
            return File(abytes, "application/pdf");
        }
    }
}