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
    public class Department_Service
    {
        private ApplicationDbContext db;
        public Department_Service()
        {
            this.db = new ApplicationDbContext();
        }

        public List<CartDepartment> allDepartments()
        {            
            return db.Cartdepartments.ToList();
        }
        public bool addDepartment(CartDepartment model)
        {
            try
            {
                db.Cartdepartments.Add(model);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            { return false; }
        }
        public bool editDepartment(CartDepartment model)
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
        public bool deleteDepartment(CartDepartment model)
        {
            try
            {
                db.Cartdepartments.Remove(model);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            { return false; }
        }
        public CartDepartment findDepartment_by_id(int? id)
        {
            return db.Cartdepartments.Find(id);
        }
        //public List<Item> Department_items(int? id)
        //{
        //    return find_by_id(id)Items.ToList();
        //}
    }
}
