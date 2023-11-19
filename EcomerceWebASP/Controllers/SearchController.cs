using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EcomerceWebASP.Models;

namespace EcomerceWebASP.Controllers
{
    public class SearchController : Controller
    {
        private MilkEntities db = new MilkEntities();

        // GET:  http://localhost:46418/search/indexq?=tin
        public ActionResult Index(string q)
        {
            // queryparamater = "glasses";
            ProductDTODetail productDTO = new ProductDTODetail();


            q = q.ToLower();

            var productList = (from s in db.Products
                               where s.nameProduct.ToLower().Contains(q)
                               select s);

            var query = productList.Include(p => p.ImageProduct);
            ViewBag.list = query.ToList();

            return View(query.ToList());


        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
