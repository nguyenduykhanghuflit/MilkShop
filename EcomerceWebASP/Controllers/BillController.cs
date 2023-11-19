using EcomerceWebASP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Net;
using EcomerceWebASP.Helpers;
using System.Data.Entity.Validation;

namespace EcomerceWebASP.Controllers
{
    public class BillController : Controller
    {
        private MilkEntities db = new MilkEntities();
        // GET: Bill
        public ActionResult Index()

        {

            return View();
        }


        [HttpPost]
        public ActionResult PostBill(string idBill, int Shipping, int Total, int totalQty, string nameBook, string email, int phone,
                                    string address, string PTTT, DetailBIll[] detailBill)
        {
            try
            {
                var idUserReal = "";
                if (Request.Cookies["user"] != null)
                {
                    idUserReal = Request.Cookies["user"].Value;

                }
                var bill = new Bills()
                {
                    idBill = idBill,
                    idUser = idUserReal != "" ? idUserReal : null,
                    createdAt = DateTime.Now,
                    Shipping = Shipping,
                    Total = Total,
                    totalQty = totalQty,
                    nameBook = nameBook,
                    email = email,
                    phone = Convert.ToString(phone),
                    address = address,
                    PTTT = PTTT,
                    DetailBIll = detailBill,
                    statusId = 1,
                    status = db.BillStatus.Single(s => s.id == 1).statusName

                };
                db.Bills.Add(bill);


                db.SaveChanges();
                return Json("Đặt hàng thành công");
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }

        }





    }
}