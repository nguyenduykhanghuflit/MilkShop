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
    public class RegisterController : Controller
    {
        private MilkEntities db = new MilkEntities();
        // GET: Register
        public ActionResult Index()
        {
            ViewBag.Message = TempData["Message"];
            return View();
        }
        //POST:Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "fullName,username,password,address,email,phone")] Users user)
        {

            try
            {
                var data = db.Users.SingleOrDefault(a => a.username == user.username);
                if (data != null)
                {
                    TempData["Message"] = "Username đã tồn tại";
                    return Redirect("~/register");
                }

                int count = db.Users.Count() + 1;
                user.idPermission = "R02";
                var id = 'U' + count.ToString();
                user.idUser = id;

                db.Users.Add(user);
                db.SaveChanges();
                return Redirect("~/login");
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return Redirect("~/register");
            }

        }
    }
}