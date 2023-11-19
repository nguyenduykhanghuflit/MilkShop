using EcomerceWebASP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EcomerceWebASP.Controllers
{
    // ngoc phu
    public class CartController : Controller
    {
        private MilkEntities db = new MilkEntities();


        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Checkout()
        {
            return View();
        }
    }
}
