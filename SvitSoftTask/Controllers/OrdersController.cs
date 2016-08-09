using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SvitSoftTask.DAL;
using SvitSoftTask.Helpers;
using SvitSoftTask.Hubs;
using SvitSoftTask.Models;

namespace SvitSoftTask.Controllers
{
    public class OrdersController : Controller
    {
        private MyDbContext db = new MyDbContext();

        public ActionResult Index()
        {

            return View();
        }

        public JsonResult GetOrders()
        {

            var ordersResult = (
                from order in db.Orders
                join orc in db.Orders on order.Email equals orc.Email
                where order.ID >= orc.ID
                group order by new {order.OrderDate, order.ClientName}
                into orderGroup
                select new
                {
                    orderGroup.Key.OrderDate,
                    orderGroup.Key.ClientName,
                    OrderNumber = orderGroup.Count()
                }
                ).ToList();



            return Json(ordersResult, JsonRequestBehavior.AllowGet );
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Order order)
        {

            if (!ModelState.IsValid)
                return View(order);

            string body = ViewRenderer.RenderPartialView(@"~\Views\Orders\EmailMessage.cshtml", order, ControllerContext);
            try
            {
                MailHelper.SendMail(order.Email, "Заказ покемона", body);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Ошибка при отправке почты: " + e.Message);

            }

            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();

                int orderCount = db.Orders.Count(x => x.Email == order.Email);

                var hubContext = GlobalHost.ConnectionManager.GetHubContext<SvitSoftHub>();
                hubContext.Clients.All.AddOrder(new { order.OrderDate, order.ClientName, OrderNumber = orderCount });

                return RedirectToAction("Index");
            }

            return View(order);
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
