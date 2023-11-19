﻿using System;
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
    public class CollectionsController : Controller
    {
        private MilkEntities db = new MilkEntities();

        //API GET CATEGORY
        [HttpGet]
        public JsonResult GetCategory()
        {
            var category = from type in db.Types
                           select new Category()
                           {
                               nameType = type.nameType,
                               idType = type.idType,
                           };

            return Json(category.ToList(), JsonRequestBehavior.AllowGet);
        }

        // GET: Collections

        public ActionResult Index(string id)
        {
            if (id == null)
            {
                var query = db.Products.Include(p => p.ImageProduct);
                ViewBag.list = query.ToList();
                return View(query.ToList());
            }
            else
            {

                var productList = (from s in db.Products
                                   where s.idType == id
                                   select s);

                var query = productList.Include(p => p.ImageProduct);
                ViewBag.list = query.ToList();
                return View(query.ToList());
            }

        }

        // GET: Collections/Details/5

        public ActionResult Details(string id)
        {
            ProductDTODetail productDTO = new ProductDTODetail();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product = from el in db.Products
                          where el.idProduct == id
                          select el;

            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {

                var data = from p in product
                           select p;

                data.Include("ImageProducts").Include("Type");
                var datarelateto = (from p in db.Products
                                    join t in data on p.idType equals t.idType
                                    select p);
                datarelateto.Include("ImageProducts").Include("Type");
                var subData = (datarelateto.ToList()).Skip(3).Take(4);
                ViewBag.datarelateto = subData.ToList();
                ViewBag.List = data.ToList();
                return View(data.ToList());
            };
        }




    }
}
