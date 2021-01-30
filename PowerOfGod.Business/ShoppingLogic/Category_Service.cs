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
    public class Category_Service
    {
        private ApplicationDbContext db;

        public Category_Service()
        {
            this.db = new ApplicationDbContext();
        }

        public List<Category> allCategories()
        {
            return db.Categories.ToList();
        }
        public bool addCategory(Category model)
        {
            try
            {
                db.Categories.Add(model);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            { return false; }
        }
        public bool editCategory(Category model)
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
        public bool deleteCategory(Category model)
        {
            try
            {
                db.Categories.Remove(model);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            { return false; }
        }
        public Category findCategory_by_id(int? id)
        {
            return db.Categories.Find(id);
        }
    }
}
