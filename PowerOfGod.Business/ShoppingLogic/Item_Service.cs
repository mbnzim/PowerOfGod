using PowerOfGod.Domain.Context;
using PowerOfGod.Domain.Entity.Shopping;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerOfGod.Business.ShoppingLogic
{
    public class Item_Service
    {
        private ApplicationDbContext db;

        public Item_Service()
        {
           this.db = new ApplicationDbContext();
        }

        public List<Item> allItems()
        {
            return db.Items.Include(i => i.Category).ToList();
        }
        public bool addItem(Item model)
        {
            try
            {
                db.Items.Add(model);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            { return false; }
        }
        public bool editItem(Item model)
        {
            try
            {
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            { return false; }
        }
        public bool deleteItem(Item model)
        {
            try
            {
                db.Items.Remove(model);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            { return false; }
        }
        public Item findItem_by_id(int? id)
        {
            return db.Items.Find(id);
        }
        //public List<StockCart_Item> get_cart_items(int id)
        //{
        //    //return db.StockCart_Items.
        //}


        public void updateStock_Received(int item_id, int quantity)
        {
            var item = db.Items.Find(item_id);
            item.QuantityInStock += quantity;
            db.SaveChanges();
        }
        public void updateOrder(int id, double price)
        {
            var item = db.Order_Items.Find(id);
            item.price = price;
            item.replied = true;
            item.date_replied = DateTime.Now;
            item.status = "Supplier Replied with Pricing Details";
            db.SaveChanges();
        }

    }
}
