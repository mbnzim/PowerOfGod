using Microsoft.Ajax.Utilities;
using PowerOfGod.Business.ShoppingLogic;
using PowerOfGod.Domain.Context;
using PowerOfGod.Domain.Entity.Memberss;
using PowerOfGod.Domain.Entity.Shopping;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace PowerOfGod.Web.Controllers
{
    public class ShoppingController : Controller
    {
        private Cart_Service cart_Service;
        private Item_Service item_Service;
        private Customer_Service customer_Service;
        private Order_Service order_Service;
        private Affiliate_Service affiliate_Service;
        private Department_Service department_Service;
        private Address_Service address_Service;

        private ApplicationDbContext db = new ApplicationDbContext();
        public string shoppingCartID { get; set; }
        public const string CartSessionKey = "CartId";

        public ShoppingController()
        {
            this.cart_Service = new Cart_Service();
            this.item_Service = new Item_Service();
            this.customer_Service = new Customer_Service();
            this.order_Service = new Order_Service();
            this.affiliate_Service = new Affiliate_Service();
            this.department_Service = new Department_Service();
            this.address_Service = new Address_Service();
        }
        public ActionResult Index(int? id)
        {
            var items_results = new List<Item>();
            try
            {
                if(id!=null)
                {
                    if(id==0)
                    {
                        items_results = item_Service.allItems();
                        ViewBag.Department = "All Departments";
                    }
                    else
                    {
                        items_results = item_Service.allItems().Where(x => x.Category.Department_ID == (int)id).ToList();
                        ViewBag.Department = department_Service.findDepartment_by_id(id).Department_Name;
                    }                    
                }
                else
                {
                    items_results = item_Service.allItems();
                    ViewBag.Department = "All Departments";
                }
            }
            catch(Exception ex) { }
            return View(items_results);
        }
        public ActionResult add_to_cart(int id)
        {
            var item = item_Service.findItem_by_id(id);
            if (item != null)
            {
                cart_Service.add_item_to_cart(id);
                return RedirectToAction("Index");
            }
            else
                return RedirectToAction("Not_Found", "Error");
        }
        public ActionResult remove_from_cart(string id)
        {
            var item = cart_Service.get_Cart_Items().FirstOrDefault(x => x.cart_item_id == id);
            if (item != null)
            {
                cart_Service.remove_item_from_cart(id: id);
                return RedirectToAction("ShoppingCart");
            }
            else
                return RedirectToAction("Not_Found", "Error");
        }
        public ActionResult ShoppingCart()
        {
            ViewBag.Total = cart_Service.get_cart_total(cart_Service.GetCartID());
            ViewBag.TotalQTY = cart_Service.get_Cart_Items().FindAll(x => x.cart_id == cart_Service.GetCartID()).Sum(q => q.quantity);
            //TempData["Show"] = "There is not enough stock for your request, avaliable stock " + ;
            return View(cart_Service.get_Cart_Items().FindAll(x => x.cart_id == cart_Service.GetCartID()));
        }
        [HttpPost]
        public ActionResult ShoppingCart(List<Cart_Item> items)
        {
            foreach (var i in items)
            {
                cart_Service.updateCart(i.cart_item_id, i.quantity);
                
            }          
            return RedirectToAction("ShoppingCart");
            
        }
        public ActionResult countCartItems()
        {
            int qty = cart_Service.get_Cart_Items().Sum(x => x.quantity);
            return Content(qty.ToString());
        }
        //public ActionResult TextBox(int? Quantity, int? id)
        //{
        //    Cart_Item c = db.Cart_Items.Find(id);
        //    Item p = db.Items.Find(c.item_id);
        //    if (Quantity > c.quantity)
        //    {
        //        c.quantity = c.quantity + (Convert.ToInt16(Quantity) - c.quantity);
        //        if (c.quantity > p.QuantityInStock)
        //        {
        //            TempData["Show"] = "There is not enough stock for your request, avaliable stock " + p.QuantityInStock;
        //            return RedirectToAction("Index");
        //        }
        //        p.QuantityInStock = p.QuantityInStock - c.quantity;
        //        c.price = p.Price * c.quantity;
        //        db.Entry(c).State = EntityState.Modified;
        //        db.Entry(p).State = EntityState.Modified;
        //        db.SaveChanges();
        //    }
        //    else if (Quantity < c.quantity)
        //    {
        //        c.quantity = c.quantity - (c.quantity - Convert.ToInt16(Quantity));
        //        p.QuantityInStock = p.QuantityInStock + c.quantity;
        //        c.price = p.Price * c.quantity;
        //        db.Entry(c).State = EntityState.Modified;
        //        db.Entry(p).State = EntityState.Modified;
        //        db.SaveChanges();
        //    }
        //    return RedirectToAction("ShoppingCart");
        //}
        public ActionResult Checkout(int? Quantity, int? id)
        {
            var I= new Item();
            Item p = db.Items.Find(I.ItemCode);
            var C = new Cart_Item();
            if(C.quantity>I.QuantityInStock)
            {
                TempData["Message"] = "<script>alert('There is not enough stock for your request, avaliable stock ');</script>" + I.QuantityInStock;
                return RedirectToAction("Index");
            }
            if(C.quantity>I.QuantityInStock)
            {
                TempData["Message"] = "<script>alert('There is not enough stock for your request, avaliable stock ');</script>" + I.QuantityInStock;
                return RedirectToAction("Index");
            }


          else  if (cart_Service.get_Cart_Items().Count == 0)
            {
                ViewBag.Err = "Opps... you should have atleat one cart item, please shop a few items";
                return RedirectToAction("Index");
            }
            else
                return RedirectToAction("HowToGetMyOrder");
        }
        [Authorize]
        public ActionResult HowToGetMyOrder()
        {
            return View();
        }
        [HttpPost]
        public ActionResult HowToGetMyOrder(string street_number, string street_name, string City, string State, string ZipCode, string Country)
        {
            Session["street_number"] = street_number;
            Session["street_name"] = street_name;
            Session["City"] = City;
            Session["State"] = State;
            Session["ZipCode"] = ZipCode;
            Session["Country"] = Country;
            return RedirectToAction("PlaceOrder", new { id = "deliver" });
        }
        [Authorize]
        public ActionResult PlaceOrder(string id)
        {
            //Members m = new Members();
            /* Find the details of the customer placing the order*/
            var customer = customer_Service.findCustomer_by_email(HttpContext.User.Identity.Name);
            /* Place the order */
            order_Service.addOrder(customer);
            /* Get the last placed order by the customer */
            var order = order_Service.allOrders()
                .FindAll(x => x.MemberId == customer.MemberId)
                .OrderByDescending(x=>x.date_created)
                .FirstOrDefault();
            /* If the customer requests delivery, save order address */
            if (id == "deliver")
            {
                address_Service.addOrderAddress(new Shipping_Address()
                {
                    Order_ID = order.Order_ID,
                    street_number = Convert.ToInt16(Session["street_number"].ToString()),
                    street_name = Session["street_name"].ToString(),
                    City = Session["City"].ToString(),
                    State = Session["State"].ToString(),
                    ZipCode = Session["ZipCode"].ToString(),
                    Country = Session["Country"].ToString(),

                    Building_Name = "",
                    Floor ="",
                    Contact_Number ="",
                    Comments = "",
                    Address_Type = ""
                });
            }
            /* Migrate cart items to map as order items */
            order_Service.addOrderItems(order,cart_Service.get_Cart_Items());
            /* Empty the cart items */     
            cart_Service.empty_Cart();
            /* Update Order Tracking Report */
            order_Service.addOrderTrackingReport(new Order_Tracking()
            {
                order_ID = order.Order_ID,
                date = DateTime.Now,
                status = "Awaiting Payment",
                Recipient = ""
            });

            //Redirect to payment
            return RedirectToAction("Payment", new { id = order.Order_ID });
        }
        public ActionResult Payment(string id)
        {

            var order = db.Orders.Find(id);

            ViewBag.Order = order;
            ViewBag.Account = customer_Service.findCustomer_by_email(order.members.Email);
            ViewBag.Address = address_Service.allOrderAddreses().Find(x => x.Order_ID == order.Order_ID);
            ViewBag.Items = order_Service.allOrderItems(order.Order_ID);
            ViewBag.Total = order_Service.get_order_total(order.Order_ID);


            try
            {

                
                string url = "<a href=" + "http://shopify-here.azurewebsites.net/Shopping/Payment/" + id + " >  here" + "</a>";
                string table = "<br/>" +
                               "Items in this order<br/>" +
                               "<table>";
                table += "<tr>" +
                         "<th>Item</th>"
                         +
                         "<th>Quantity</th>"
                         +
                         "<th>Price</th>" +
                         "</tr>";
                foreach (var item in (List<Order_Item>)ViewBag.Items)
                {
                    string itemsinoder = "<tr> " +
                                         "<td>" + item.Item.Name + " </td>" +
                                         "<td>" + item.quantity + " </td>" +
                                         "<td>" + item.Picture + " </td>" +
                                        "<td>R" + item.price + " </td>" +
                                         "<tr/>";
                    table += itemsinoder;
                }

                table += "<tr>" +
                         "<th></th>"
                         +
                         "<th></th>" 
                         +
                         "<th></th>"
                         +
                         "<th>" + order_Service.get_order_total(order.Order_ID).ToString("R0.00") + "</th>" +
                         "</tr>";
                table += "</table>";

                var client = new SendGridClient("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");
                var from = new EmailAddress("no-reply@PowerofGodAssembless.com", "Power of God Online Store");
                var subject = "Order " + id + " | Awaiting Payment";
                var to = new EmailAddress(((Members)ViewBag.Account).Email, ((Members)ViewBag.Account).FirstName + " " + ((Members)ViewBag.Account).LastName);
                var htmlContent = "Hi " + order.members.FirstName + ", Your order was placed, you can securely pay your order from " + url + table;
                var msg = MailHelper.CreateSingleEmail(from, to, subject, null, htmlContent);
                var response = client.SendEmailAsync(msg);
            }
            catch (Exception ex)
            {

            }
            //return View();




            return View(order_Service.GetOrderDetail(id));

        }
        public ActionResult Secure_Payment(string id)
        {
            var order = order_Service.findOrder_by_id(id);
            return Redirect(PaymentLink(order_Service.get_order_total(order.Order_ID).ToString(), "Order Payment | Order No: " + order.Order_ID, order.Order_ID));
        }
        public ActionResult Return_Url(string id)
        {
            var order = order_Service.findOrder_by_id(id);

            ViewBag.Order = order;
            ViewBag.Account = customer_Service.findCustomer_by_email(order.members.Email);
            ViewBag.Address = address_Service.allOrderAddreses().Find(x => x.Order_ID == order.Order_ID);
            ViewBag.Items = order_Service.allOrderItems(order.Order_ID);
            ViewBag.Total = order_Service.get_order_total(order.Order_ID);           
            return View();
        }
        public ActionResult Payment_Successfull(string id)
        {
            try
            {
                var orders = order_Service.findOrder_by_id(id);
                order_Service.update_Stock(id);
                order_Service.add_payment(orders.Order_ID);
                order_Service.expectedDeliveryDateReport(orders);


                if (affiliate_Service.getAffiliate_Joiners().FirstOrDefault(x => x.New_Customer_Email == orders.members.Email) != null)
                { /* deposit benefits */
                    affiliate_Service.payAffiliate_Network(orders.members.Email, (decimal)order_Service.get_order_total(orders.Order_ID));
                }
            }
            catch (Exception ex) { }
            var order = db.Orders.Find(id);

            ViewBag.Order = order;
            ViewBag.Account = customer_Service.findCustomer_by_email(order.members.Email);
            ViewBag.Address = address_Service.allOrderAddreses().Find(x => x.Order_ID == order.Order_ID);
            ViewBag.Items = order_Service.allOrderItems(order.Order_ID);
            ViewBag.Total = order_Service.get_order_total(order.Order_ID);


            try
            {


                string url = "<a href=" + "http://shopify-here.azurewebsites.net/Shopping/Payment/" + id + " >  here" + "</a>";
                string table = "<br/>" +
                               "Items in this order<br/>" +
                               "<table>";
                table += "<tr>" +
                         "<th>Item</th>"
                         +
                         "<th>Quantity</th>"
                         +
                         "<th>Price</th>" +
                         "</tr>";
                foreach (var item in (List<Order_Item>)ViewBag.Items)
                {
                    string itemsinoder = "<tr> " +
                                         "<td>" + item.Item.Name + " </td>" +
                                         "<td>" + item.quantity + " </td>" +
                                         "<td>" + item.Picture + " </td>" +
                                        "<td>R" + item.price + " </td>" +
                                         "<tr/>";
                    table += itemsinoder;
                }

                table += "<tr>" +
                         "<th></th>"
                         +
                         "<th></th>"
                         +
                         "<th></th>"
                         +
                         "<th>" + order_Service.get_order_total(order.Order_ID).ToString("R0.00") + "</th>" +
                         "</tr>";
                table += "</table>";

                var client = new SendGridClient("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");
                var from = new EmailAddress("no-reply@PowerofGodAssembless.com", "Power of God Online Store");
                var subject = "Order " + id + " | Secure_Payment";
                var to = new EmailAddress(((Members)ViewBag.Account).Email, ((Members)ViewBag.Account).FirstName + " " + ((Members)ViewBag.Account).LastName);
                var htmlContent = "Hi " + order.members.FirstName + ", Your have Succefully Paid For Your Order , You can pick up your order Anytime during working days And on Sundays " + url + table;
                var msg = MailHelper.CreateSingleEmail(from, to, subject, null, htmlContent);
                var response = client.SendEmailAsync(msg);
            }
            catch (Exception ex)
            {

            }
            //return View();


            return View();
        }

        public string PaymentLink(string totalCost, string paymentSubjetc, string order_id)
        {

            string paymentMode = ConfigurationManager.AppSettings["PaymentMode"], site, merchantId, merchantKey, returnUrl, cancelUrl, PF_NotifyURL;

            if (paymentMode == "test")
            {
                site = "https://sandbox.payfast.co.za/eng/process?";
                merchantId = "10005631";
                merchantKey = "znbndc034nb6b";
            }
            else if (paymentMode == "live")
            {
                site = "https://www.payfast.co.za/eng/process?";
                merchantId = ConfigurationManager.AppSettings["PF_MerchantID"];
                merchantKey = ConfigurationManager.AppSettings["PF_MerchantKey"];
            }
            else
            {
                throw new InvalidOperationException("Payment method unknown.");
            }
            var stringBuilder = new StringBuilder();
            PF_NotifyURL = Url.Action("Payment_Successfull", "Shopping",
                new System.Web.Routing.RouteValueDictionary(new { id = order_id }),
                "http", Request.Url.Host);
            returnUrl = Url.Action("Order_Details", "Orders",
                new System.Web.Routing.RouteValueDictionary(new { id = order_id }),
                "http", Request.Url.Host);
            cancelUrl = Url.Action("Payment", "Shopping",
                new System.Web.Routing.RouteValueDictionary(new { id = order_id }),
                "http", Request.Url.Host);

            /* mechant details */
            stringBuilder.Append("&merchant_id=" + HttpUtility.HtmlEncode(merchantId));
            stringBuilder.Append("&merchant_key=" + HttpUtility.HtmlEncode(merchantKey));
            stringBuilder.Append("&return_url=" + HttpUtility.HtmlEncode(returnUrl));
            stringBuilder.Append("&cancel_url=" + HttpUtility.HtmlEncode(cancelUrl));
            stringBuilder.Append("&notify_url=" + HttpUtility.HtmlEncode(PF_NotifyURL));
            /* buyer details */
            var members = order_Service.allOrders().FirstOrDefault(x => x.Order_ID == order_id).members;
            if (members != null)
            {
                stringBuilder.Append("&name_first=" + HttpUtility.HtmlEncode(members.FirstName));
                stringBuilder.Append("&name_last=" + HttpUtility.HtmlEncode(members.LastName));
                stringBuilder.Append("&email_address=" + HttpUtility.HtmlEncode(members.Email));
                stringBuilder.Append("&cell_number=" + HttpUtility.HtmlEncode(members.PhoneNumber));
            }
            /* Transaction details */
            var order = order_Service.findOrder_by_id(order_id);
            if (order != null)
            {
                stringBuilder.Append("&m_payment_id=" + HttpUtility.HtmlEncode(order.Order_ID));
                stringBuilder.Append("&amount=" + HttpUtility.HtmlEncode((decimal)order_Service.get_order_total(order.Order_ID)));
                stringBuilder.Append("&item_name=" + HttpUtility.HtmlEncode(paymentSubjetc));
                stringBuilder.Append("&item_description=" + HttpUtility.HtmlEncode(paymentSubjetc));

                stringBuilder.Append("&email_confirmation=" + HttpUtility.HtmlEncode("1"));
                stringBuilder.Append("&confirmation_address=" + HttpUtility.HtmlEncode(ConfigurationManager.AppSettings["PF_ConfirmationAddress"]));
            }

            return (site + stringBuilder);
        }
    }
}