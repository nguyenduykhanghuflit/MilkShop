using EcomerceWebASP.Helpers;
using EcomerceWebASP.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EcomerceWebASP.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        private MilkEntities db = new MilkEntities();

        // GET: Admin/Dashboard
        public ActionResult Index(string dateFrom, string dateTo)
        {
            if (Session["SESSION_GROUP_ADMIN"] != null)
            {
                int countUser = db.Users.Count();
                int countBill = db.Bills.Count();
                int countProduct = db.Products.Count();
                ViewBag.countUser = countUser.ToString();
                ViewBag.countBill = countBill.ToString();
                ViewBag.product = countProduct.ToString();

                var bill = db.Bills;

                // Đặt ngày mặc định là ngày hôm nay nếu không có tham số truyền vào
                DateTime fromDate = string.IsNullOrEmpty(dateFrom) ? DateTime.Now.Date : DateTime.Parse(dateFrom);
                DateTime toDate = string.IsNullOrEmpty(dateTo) ? DateTime.Now.Date : DateTime.Parse(dateTo);
                ViewBag.fromDate = fromDate;
                ViewBag.toDate = toDate;
                // Lọc thông tin theo ngày
                ViewBag.BillTotal = bill.Where(w => DbFunctions.TruncateTime(w.createdAt) >= fromDate.Date && DbFunctions.TruncateTime(w.createdAt) <= toDate.Date).Count();
                ViewBag.BillWaiting = bill.Where(w => w.statusId == (int)BillStatusEnum.WAITING && DbFunctions.TruncateTime(w.createdAt) >= fromDate.Date && DbFunctions.TruncateTime(w.createdAt) <= toDate.Date).Count();
                ViewBag.BillAccept = bill.Where(w => w.statusId == (int)BillStatusEnum.ACCEPT && DbFunctions.TruncateTime(w.createdAt) >= fromDate.Date && DbFunctions.TruncateTime(w.createdAt) <= toDate.Date).Count();
                ViewBag.BillShipping = bill.Where(w => w.statusId == (int)BillStatusEnum.SHIPPING && DbFunctions.TruncateTime(w.createdAt) >= fromDate.Date && DbFunctions.TruncateTime(w.createdAt) <= toDate.Date).Count();
                ViewBag.BillSuccess = bill.Where(w => w.statusId == (int)BillStatusEnum.SUCCESS && DbFunctions.TruncateTime(w.createdAt) >= fromDate.Date && DbFunctions.TruncateTime(w.createdAt) <= toDate.Date).Count();
                ViewBag.BillCancel = bill.Where(w => w.statusId == (int)BillStatusEnum.CANCEL && DbFunctions.TruncateTime(w.createdAt) >= fromDate.Date && DbFunctions.TruncateTime(w.createdAt) <= toDate.Date).Count();
                var success = bill.Where(w => w.statusId == (int)BillStatusEnum.SUCCESS && DbFunctions.TruncateTime(w.createdAt) >= fromDate.Date && DbFunctions.TruncateTime(w.createdAt) <= toDate.Date);
                ViewBag.Revenue = success.Count() > 0 ? success.Sum(i => i.Total) : 0;

                DateTime currentDate = DateTime.Now.Date;
                DayOfWeek currentDayOfWeek = currentDate.DayOfWeek;
                DateTime startOfWeek = currentDate.AddDays(-(int)currentDayOfWeek + 1);

                // Lặp qua 7 ngày trong tuần và đếm số đơn hàng cho mỗi ngày
                string ordersCountByDayOfWeek = "";
                for (int i = 0; i < 7; i++)
                {
                    DateTime currentDay = startOfWeek.AddDays(i);
                    ordersCountByDayOfWeek += $"{bill.Count(w => DbFunctions.TruncateTime(w.createdAt) == DbFunctions.TruncateTime(currentDay))};";
                }
                ViewBag.Orders = ordersCountByDayOfWeek;

                return View();
            }

            return Redirect("~/login");
        }
    }
}