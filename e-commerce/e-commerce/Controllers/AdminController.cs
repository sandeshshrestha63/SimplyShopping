using e_commerce.DAL;
using e_commerce.Helpers;
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
        SimplyShoppingEntities db = new SimplyShoppingEntities();
        public ActionResult Dashboard()
        {
            DateTime prevMonth = DateTime.Now.AddMonths(-1);
            DateTime prevYear = DateTime.Now.AddYears(-1);
            var totalMonthSales = db.tbl_OrdKey.Where(x => x.PaymentType != (byte)EnumPaymentType.NotPaid).Where(x => x.OrdDate.Value.Year == DateTime.Now.Year && x.OrdDate.Value.Month == DateTime.Now.Month).Sum(x => x.OrdTot);
            var totalPreMonthSales = db.tbl_OrdKey.Where(x => x.PaymentType != (byte)EnumPaymentType.NotPaid).Where(x => x.OrdDate.Value.Year == prevYear.Year && x.OrdDate.Value.Month == prevMonth.Month).Sum(x => x.OrdTot);
            var totalYearlySales = db.tbl_OrdKey.Where(x => x.PaymentType != (byte)EnumPaymentType.NotPaid).Where(x => x.OrdDate.Value.Year == DateTime.Now.Year).Sum(x => x.OrdTot);
            var totalPrevYearlySales= db.tbl_OrdKey.Where(x => x.PaymentType != (byte)EnumPaymentType.NotPaid).Where(x => x.OrdDate.Value.Year == prevYear.Year).Sum(x => x.OrdTot);
            ViewBag.CurrentYearlyTotalSales = totalYearlySales;
            ViewBag.PreviousYearlyTotalSales = totalPrevYearlySales;
            ViewBag.CurrentMonthTotalSale = totalMonthSales;
            ViewBag.PreviousMonthTotalSale = totalPreMonthSales;
            return View();
        }
        public ActionResult Categories()
        {
            List<Tbl_Category> allCategory = _uw.GetRepositoryInstance<Tbl_Category>().GetAllRecordsIQueryable().Where(x =>x.IsDelete == false).ToList();
            return View(allCategory);
        }
    }
}