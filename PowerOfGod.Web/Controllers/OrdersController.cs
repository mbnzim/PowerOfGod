using PowerOfGod.Business.ShoppingLogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;


namespace PowerOfGod.Web.Controllers
{
    public class OrdersController : Controller
    {
        private Order_Service order_Service;

        public OrdersController()
        {
            this.order_Service = new Order_Service();            
        }

        //Customer orders
        public ActionResult Customer_Orders(string id)
        {
            if (String.IsNullOrEmpty(id) || id == "all")
            {
                ViewBag.Status = "All";
                return View(order_Service.allOrders());
            }
            else
            {
                ViewBag.Status = id;
                return View(order_Service.findOrder_by_status(id));
            }
        }
        public ActionResult New_Orders(string id)
        {
            if (String.IsNullOrEmpty(id) || id == "all")
            {
                ViewBag.Status = "All";
                return View(order_Service.allOrders());
            }
            else
            {
                ViewBag.Status = id;
                return View(order_Service.findOrder_by_status(id));
            }
        }
        public ActionResult Order_Details(string id)
        {
            if (id == null)
                return RedirectToAction("Bad_Request", "Error");
            if (order_Service.findOrder_by_id(id) != null)
                return View(order_Service.GetOrderDetail(id));
            else
                return RedirectToAction("Not_Found", "Error");
        }
        public ActionResult Order_Tracking(string id)
        {
            if (id == null)
                return RedirectToAction("Bad_Request", "Error");
            if (order_Service.findOrder_by_id(id) != null)
            {
                ViewBag.Order = order_Service.findOrder_by_id(id);
                return View(order_Service.get_tracking_report(id));
            }
            else
                return RedirectToAction("Not_Found", "Error");
        }

        public ActionResult Mark_As_Packed(string id)
        {
            if (id == null)
                return RedirectToAction("Bad_Request", "Error");
            if (order_Service.findOrder_by_id(id) != null)
            {
                order_Service.mark_as_packed(id);
                return RedirectToAction("Order_Details", new { id = id });
            }
            else
                return RedirectToAction("Not_Found", "Error");
        }
      
        //account orders
        public ActionResult Order_History()
        {
            return View(order_Service.allOrders().Where(x => x.members.Email == User.Identity.Name));
        }
    }
}
