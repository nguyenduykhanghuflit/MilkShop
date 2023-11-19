using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using EcomerceWebASP.Models;
using Newtonsoft.Json;

namespace EcomerceWebASP.Areas.Admin.Controllers
{
    public class BillsController : Controller
    {
        private MilkEntities db = new MilkEntities();

        public ActionResult Index()
        {
            if (Session["SESSION_GROUP_ADMIN"] != null)
            {
                IOrderedQueryable<Bills> bills = db.Bills.Include(b => b.Users).OrderByDescending(a => a.createdAt);

                ViewBag.BillStatus = db.BillStatus.ToList();

                return View(bills.ToList());
            }
            return Redirect("~/login");
        }


        // GET: Admin/Bills/Details/5
        public ActionResult Details(string id)
        {
            if (Session["SESSION_GROUP_ADMIN"] != null)
            {
                try
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    Bills Bills = db.Bills.Find(id);
                    if (Bills == null)
                    {
                        return HttpNotFound();
                    }
                    else
                    {
                        var billList = (from s in db.Bills
                                        where s.idBill == id
                                        select s);

                        var query = billList.Include(p => p.BillStatus).Include(p => p.DetailBIll);
                        ViewBag.data = query.FirstOrDefault();
                        return View(query.ToList());
                    }
                }
                catch (Exception ex)
                {
                    return HttpNotFound();
                }

            }
            return Redirect("~/login");
        }


        // GET: Admin/Bills/Edit/5
        public ActionResult Edit(string id)
        {
            if (Session["SESSION_GROUP_ADMIN"] != null)
            {
                try
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    Bills Bills = db.Bills.Find(id);
                    ViewBag.BillStatus = db.BillStatus.ToList();
                    if (Bills == null)
                    {
                        return HttpNotFound();
                    }
                    else
                    {
                        ViewBag.data = Bills;
                        return View();
                    }
                }
                catch (Exception ex)
                {
                    return HttpNotFound();
                }

            }
            return Redirect("~/login");
        }



        [HttpGet]
        public JsonResult GetAllBill()
        {
            if (Session["SESSION_GROUP_ADMIN"] != null)
            {
                ProductDTODetail productDTO = new ProductDTODetail();

                var bills = db.Bills.Include(b => b.Users);
                var listProduct = from p in bills

                                  select new BillData()
                                  {
                                      idBill = p.idBill,
                                      idUser = p.idUser,
                                      Ship = p.Shipping,
                                      Total = p.Total,
                                      PTTT = p.PTTT,
                                      status = p.status,
                                      createdAt = p.createdAt,
                                      Qty = p.totalQty,
                                      nameUser = p.nameBook,
                                      email = p.email,
                                      phone = p.phone,

                                  };
                return Json(listProduct.OrderBy(i => i.createdAt).ToList(), JsonRequestBehavior.AllowGet);
            }
            return Json("không đủ quyền", JsonRequestBehavior.AllowGet);

        }




        [HttpGet]
        public JsonResult UpdateStatus(string idBill, int statusId)
        {

            try
            {
                var bill = db.Bills.Where(w => w.idBill == idBill).FirstOrDefault();
                if (bill != null)
                {
                    bill.statusId = statusId;
                    bill.status = db.BillStatus.Single(s => s.id == statusId).statusName;
                    db.Entry(bill).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json("1", JsonRequestBehavior.AllowGet);
                }
                return Json("2", JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(ex.ToString(), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult UpdateBill(string idBill, string nameBook, string phone, string address)
        {

            if (Session["SESSION_GROUP_ADMIN"] != null)
            {
                if (idBill == null)
                {
                    return Json("ERR");
                }

                Bills Bills = db.Bills.Find(idBill);

                if (Bills != null)
                {
                    Bills.nameBook = nameBook;
                    Bills.phone = phone;
                    Bills.address = address;

                    db.SaveChanges();
                    return Json("success");
                }

            }
            return Json("không đủ quyền");



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





