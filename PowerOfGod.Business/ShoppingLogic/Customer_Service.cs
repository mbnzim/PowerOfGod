using PowerOfGod.Domain.Context;
using PowerOfGod.Domain.Entity.Memberss;
using PowerOfGod.Domain.Entity.Shopping;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerOfGod.Business.ShoppingLogic
{
    public class Customer_Service
    {

        private ApplicationDbContext db;

        public Customer_Service()
        {
            this.db = new ApplicationDbContext();
        }


        public List<Members> allCustomers()
        {
            return db.members.ToList();
        }
        public bool addCustomer(Members model, string affiliate_key)
        {
            try
            {
                db.members.Add(model);
                db.Affiliates.Add(new Affiliate() { Email = model.Email});
                db.SaveChanges(); 
                if(!String.IsNullOrEmpty(affiliate_key))
                {
                   var affiliate = db.Affiliates.Find(Guid.Parse(affiliate_key));
                    if(affiliate !=null)
                    {
                        db.Affiliate_Joiners.Add(new Affiliate_Joiner()
                        {
                            Affiliate_Key = affiliate.Affiliate_Key,
                            New_Customer_Email = model.Email,
                            Email = affiliate.members.Email,
                            used = false
                        });
                        db.SaveChanges();
                    }
                }
                return true;
            }
            catch (Exception ex)
            { return false; }
        }
        public bool editCustomer(Members model)
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
        public Members findCustomer_by_email(string email)
        {
            return db.members.FirstOrDefault(x=>x.Email ==email);
        }

        public string getGender(string id_num)
        {
            if (Convert.ToInt16(id_num.Substring(7, 1)) >= 5)
                return "Male";
            else
                return "Female";
        }
    }
}
