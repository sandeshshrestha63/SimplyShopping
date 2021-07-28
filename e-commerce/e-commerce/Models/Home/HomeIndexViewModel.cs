using e_commerce.DAL;
using e_commerce.Repository;
using PagedList;
using PagedList.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace e_commerce.Models.Home
{
    public class HomeIndexViewModel
    {
        public IPagedList<Tbl_Product> listofProducts { get; set; }
        public GenericUnitOfWork _uw = new GenericUnitOfWork();
        SimplyShoppingEntities db = new SimplyShoppingEntities();

        public HomeIndexViewModel CreateModel(string Search,int? page)
        {
            List<Tbl_Product> data = new List<Tbl_Product>();
            PagedList<Tbl_Product> list; 
            var pageSize = 2;
            int pageindex = 1;
            pageindex = page.HasValue ? Convert.ToInt32(page) : 1;
            if (Search != null)
            {
                var categorySearch = db.Tbl_Category.Where(x => x.CategoryName.Contains(Search)).ToList();
                if (categorySearch != null)
                {
                    foreach (var item in categorySearch)
                    {
                        foreach (var dt in item.Tbl_Product)
                        {
                            data.Add(dt);
                        }
                    }
                }
                var productSearch = db.Tbl_Product.Where(x => x.ProductName.Contains(Search)).ToList();
                foreach (var item in productSearch)
                {
                    data.Add(item);
                }
                data = data.Distinct().ToList();
            }
            else
            {
                data = db.Tbl_Product.ToList();
            }

            return new HomeIndexViewModel()
            {
                listofProducts = data.ToPagedList(pageindex, pageSize)
                };
        }
    }
}