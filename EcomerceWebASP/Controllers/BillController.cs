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

        public JsonResult Checkvoucher(string voucherCode)
        {
            if (string.IsNullOrEmpty(voucherCode))
            {
                return Json(new { err = true, msg = "Voucher không hợp lệ", data = "" }, JsonRequestBehavior.AllowGet);
            }

            Voucher voucher = db.Voucher.SingleOrDefault(s => s.voucherCode.ToLower() == voucherCode.ToLower());

            if (voucher == null)
                return Json(new { err = true, msg = "Voucher không tồn tại", data = "" }, JsonRequestBehavior.AllowGet);

            if (voucher.amount == 0)
                return Json(new { err = true, msg = "Voucher không còn sử dụng được nữa", data = "" }, JsonRequestBehavior.AllowGet);

            /* if (DateTime.Compare(voucher.dateEnd, new DateTime()) > 0)
                 return Json(new { err = true, msg = "Voucher hết hạn", data = "" }, JsonRequestBehavior.AllowGet);*/

            return Json(new { err = false, msg = "Voucher hợp lệ", data = voucher }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult PostBill(string idBill, int Shipping, int Total, int totalQty, string nameBook, string email, int phone,
                                    string address, string PTTT, DetailBIll[] detailBill, string voucherCode)
        {
            try
            {
                var idUserReal = "";
                if (Request.Cookies["user"] != null)
                {
                    idUserReal = Request.Cookies["user"].Value;

                }

                decimal total = Total;
                decimal discount = 0;
                Voucher voucher = db.Voucher.SingleOrDefault(s => s.voucherCode.ToLower() == voucherCode.ToLower());
                if (voucher != null)
                {
                    if (voucher.amount == 0)
                    {
                        discount = 0;
                        voucherCode = "";
                    }
                    else
                    {
                        discount = voucher.discount;
                        /*  total = total - ((total * discount) / 100);*/

                    }
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
                    status = db.BillStatus.Single(s => s.id == 1).statusName,
                    voucherCode = voucherCode,
                    discount = discount

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