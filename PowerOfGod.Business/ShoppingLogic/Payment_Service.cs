//using Anele.Shopify.Data;
using PowerOfGod.Domain.Context;
using PowerOfGod.Domain.Entity.Shopping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerOfGod.Business.ShoppingLogic
{
    public class Payment_Service
    {
        private ApplicationDbContext db;

        public Payment_Service()
        {
            this.db = new ApplicationDbContext();
        }

        public List<Payment> GetAllPayments()
        {
            return db.Payments.ToList();
        }
        public Payment GetOrderPayment(string order_Id)
        {
            return GetAllPayments().FirstOrDefault(x => x.Order_ID == order_Id);
        }
    }
}
