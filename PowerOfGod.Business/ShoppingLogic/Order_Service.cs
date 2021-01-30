
//using Anele.Shopify.Data;
//using Anele.Shopify.Models;
using PowerOfGod.Domain.Context;
using PowerOfGod.Domain.Entity.Memberss;
using PowerOfGod.Domain.Entity.Shopping;
using PowerOfGod.ViewModel.ShoppingViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PowerOfGod.Business.ShoppingLogic
{
    public class Order_Service
    {
        private ApplicationDbContext db;
        private Address_Service address_Service;
        private Payment_Service payment_Service;

        public Order_Service()
        {
            this.db = new ApplicationDbContext();
            this.address_Service = new Address_Service();
            this.payment_Service = new Payment_Service();
        }
        public List<Order> allOrders()
        {
            return db.Orders.ToList();
        }
        public List<Order> findOrder_by_status(string status)
        {
            return db.Orders.Where(p => p.status.ToLower() == status.ToLower()).ToList();
        }
        public Order findOrder_by_id(string id)
        {
            return db.Orders.Find(id);
        }
        public List<Order_Item> Order_items(string id)
        {
            return findOrder_by_id(id).Order_Items.ToList();
        }
        public OrderDetailModel GetOrderDetail(string id)
        {
            try
            {
                string shipping_method = "Collect at Power of God Assembles", payment_method = "Awaiting Payment";
                if (address_Service.GetShipping_Address(id) != null)
                    shipping_method = "Standard delivery";
                if (payment_Service.GetOrderPayment(id) != null)
                    payment_method = payment_Service.GetOrderPayment(id).PaymentMethod;

                return new OrderDetailModel()
                {
                    members = findOrder_by_id(id).members,
                    order = findOrder_by_id(id),
                    shipping_method = shipping_method,
                    delivery = null,
                    address = address_Service.GetShipping_Address(id),
                    payment_Method = payment_method,
                    payment = payment_Service.GetOrderPayment(id),
                    order_items = Order_items(id),
                    order_total = (decimal)get_order_total(id)
                };                
            }
            catch(Exception ex) {
                return new OrderDetailModel();
            }
        }
        public List<Order_Tracking> get_tracking_report(string id)
        {
            return db.Order_Trackings.Where(x => x.order_ID == id).ToList();
        }
        public void mark_as_packed(string id)
        {
            var order = findOrder_by_id(id);
            order.packed = true;
            if (db.Shipping_Addresses.Where(p => p.Order_ID == id) != null)
            {
                order.status = "With courier";
                //order tracking
                db.Order_Trackings.Add(new Order_Tracking()
                {
                    order_ID = order.Order_ID,
                    date = DateTime.Now,
                    status = "Order Packed, Now with our courier",
                    Recipient = ""
                });
            }

            db.SaveChanges();
        }
        public void schedule_OrderDelivery(string order_Id, DateTime date)
        {
            var order = findOrder_by_id(order_Id);
            order.status = "Scheduled for delivery";
            //order tracking
            db.Order_Trackings.Add(new Order_Tracking()
            {
                order_ID = order.Order_ID,
                date = DateTime.Now,
                status = "Scheduled for delivery on " + date.ToLongDateString(),
                Recipient = ""
            });
            db.SaveChanges();
        }
       
        public List<Order_Item> allOrderItems(string order_id)
        {
            return db.Order_Items.Where(x=>x.Order_id ==order_id).ToList();
        }
        public void addOrder(Members member)
        {
            try
            {
                db.Orders.Add(new Order()
                {
                    Order_ID = GenerateOrderNumber(10),
                    MemberId = member.MemberId,
                    date_created = DateTime.Now,
                    shipped = false,
                    status = "Awaiting Payment"
                });
                db.SaveChanges();
            }
            catch(Exception ex) { }           
        }
        public void addOrderItems(Order order, List<Cart_Item> items)
        {
            foreach (var item in items)
            {
                var x = new Order_Item()
                {
                    Order_id = order.Order_ID,
                    item_id = item.item_id,
                    quantity = item.quantity,
                    Picture=item.Picture,
                    price = item.price

                };
                db.Order_Items.Add(x);
                db.SaveChanges();
            }
        }
       
        public void addOrderTrackingReport(Order_Tracking tracking)
        {
            try
            {
                db.Order_Trackings.Add(tracking);
                db.SaveChanges();
            }
            catch (Exception ex) { }
        }

        public void expectedDeliveryDateReport(Order order)
        {
            try
            {
                var expected_Date = DateTime.Now.AddDays(3);
                do
                {
                    expected_Date = expected_Date.AddDays(1);
                } while (expected_Date.DayOfWeek.ToString().ToLower() == "sunday" ||
                    expected_Date.DayOfWeek.ToString().ToLower() == "saturday");

                if (IsDeliveryRequested(order.Order_ID))
                {                   
                    addOrderTrackingReport(new Order_Tracking()
                    {
                        order_ID = order.Order_ID,
                        date = DateTime.Now,
                        status = "Expected delivery on " + expected_Date.ToLongDateString() + " before 5pm",
                        Recipient = ""
                    });
                    Order_Tracking ot = new Order_Tracking();
                    if (ot.date.Date >= expected_Date.Date)
                    {
                        ot.status = "Delivered!";
                    }

                }
                else
                {
                    expected_Date = DateTime.Now.AddHours(1);

                    addOrderTrackingReport(new Order_Tracking()
                    {
                        order_ID = order.Order_ID,
                        date = DateTime.Now,
                        status = "Can be collected during business hours as from " + expected_Date.ToLongDateString() + " "+ expected_Date.ToLongTimeString(),
                        Recipient = ""
                    });
                }

                
            }
            catch (Exception ex) { }
        }
        public bool IsDeliveryRequested(string order_id)
        {
            return db.Shipping_Addresses.FirstOrDefault(p => p.Order_ID == order_id) != null;
        }
        public void add_payment(string order_id)
        {
            //Payment p = new Payment();
            var order = db.Orders.Find(order_id);
            //var email = order.members.Email;
            var memberId = order.members.MemberId;
            //var emailp = db.Payments.me
            try
            {
                //if (IsPaymentComplete(order_id))
                {
                    db.Payments.Add(new Payment()
                    {
                        Date = DateTime.Now,
                        MemberId = memberId,
                        AmountPaid = get_order_total(order.Order_ID),
                        PaymentFor = "Order " + order_id + " Payment",
                        PaymentMethod = "EFT via PayFast Online",
                        Order_ID = order_id
                    });                    
                    db.SaveChanges();
                    updateOrderReport(order_id);
                }
            }
            catch (Exception) { }
        }
        public void updateOrderReport(string order_id)
        {
            var order = db.Orders.Find(order_id);
            try
            {
                if (IsPaymentComplete(order_id))
                {
                    order.status = "At The Church";
                    db.SaveChanges();
                    //order tracking
                    db.Order_Trackings.Add(new Order_Tracking()
                    {
                        order_ID = order.Order_ID,
                        date = DateTime.Now,
                        status = "Payment Recieved | Order still The Church",
                        Recipient = ""
                    });
                    db.SaveChanges();
                }
            }
            catch (Exception) { }
        }
        public bool IsPaymentComplete(string order_id)
        {
            return db.Payments.ToList()
                      .FindAll(x => x.Order_ID == order_id)
                      .Sum(x => x.AmountPaid) >= get_order_total(order_id);
        }
        public double get_order_total(string id)
        {
            double amount = 0;
            foreach (var item in db.Order_Items.ToList().FindAll(match: x => x.Order_id == id))
            {
                amount += (item.price * item.quantity);
            }
            return amount;
        }
        public void update_Stock(string id)
        {
            var order = db.Orders.Find(id);
            List<Order_Item> items = db.Order_Items.ToList().FindAll(x => x.Order_id == id);
            foreach (var item in items)
            {
                var product = db.Items.Find(item.item_id);
                if (product != null)
                {
                    if ((product.QuantityInStock - item.quantity) >= 0)
                    {
                        product.QuantityInStock -= item.quantity;
                    }
                    else
                    {
                        item.quantity = product.QuantityInStock;
                        product.QuantityInStock = 0;
                    }
                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception ex) { }
                }
            }
        }

        public string GenerateOrderNumber(int length)
        {
            var random = new Random();
            string number = string.Empty;
            for (int i = 0; i < length; i++)
                number = String.Concat(number, random.Next(10).ToString());
            while (findOrder_by_id(number) != null)
               number = GenerateOrderNumber(length);
            return number;
        }

    }
}
