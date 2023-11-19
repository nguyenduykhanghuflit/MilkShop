﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EcomerceWebASP.Models;


namespace EcomerceWebASP.Areas.Admin.Controllers
{
    public class TypesController : Controller
    {
        private MilkEntities db = new MilkEntities();

        // GET: Admin/Types
        public ActionResult Index()
        {

            if (Session["SESSION_GROUP_ADMIN"] != null)
            {

                return View(db.Types.ToList());
            }
            return Redirect("~/login");

        }

        // GET: Admin/Types/Details/5
        public ActionResult Details(string id)
        {




            if (Session["SESSION_GROUP_ADMIN"] != null)
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Types type = db.Types.Find(id);
                if (type == null)
                {
                    return HttpNotFound();
                }
                return View(type);

            }
            return Redirect("~/login");

        }

        // GET: Admin/Types/Create
        public ActionResult Create()
        {
            if (Session["SESSION_GROUP_ADMIN"] != null)
            {

                return View();

            }
            return Redirect("~/login");


        }

        // POST: Admin/Types/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idType,nameType")] Types type)
        {
            if (Session["SESSION_GROUP_ADMIN"] != null)
            {

                if (ModelState.IsValid)
                {

                    db.Types.Add(type);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

            }
            return Redirect("~/login");
        }

        // GET: Admin/Types/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Types type = db.Types.Find(id);
            if (type == null)
            {
                return HttpNotFound();
            }
            return View(type);
        }

        // POST: Admin/Types/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idType,nameType")] Type type)
        {
            if (ModelState.IsValid)
            {
                db.Entry(type).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(type);
        }

        // GET: Admin/Types/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Types type = db.Types.Find(id);
            if (type == null)
            {
                return HttpNotFound();
            }
            return View(type);
        }

        // POST: Admin/Types/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Types type = db.Types.Find(id);
            db.Types.Remove(type);
            db.SaveChanges();
            return RedirectToAction("Index");
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
