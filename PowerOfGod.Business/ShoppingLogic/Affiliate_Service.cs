using PowerOfGod.Domain.Context;
using PowerOfGod.Domain.Entity.Shopping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerOfGod.Business.ShoppingLogic
{
    public class Affiliate_Service
    {
        private ApplicationDbContext db;
        public Affiliate_Service()
        {
            this.db = new ApplicationDbContext();
        }

        public List<Affiliate> getAffiliates()
        {
            return db.Affiliates.ToList();
        }
        public List<Affiliate_Joiner> getAffiliate_Joiners()
        {
            return db.Affiliate_Joiners.ToList();
        }

        public bool hasAffiliate(string joiner_email)
        {
            return db.Affiliate_Joiners.Count(x => x.New_Customer_Email == joiner_email) > 0;
        }
        public Affiliate GetJoinerAffiliate(string joiner_email)
        {
            return db.Affiliate_Joiners.FirstOrDefault(x => x.New_Customer_Email == joiner_email) !=null? db.Affiliate_Joiners.FirstOrDefault(x => x.New_Customer_Email == joiner_email).Affiliate : null;
        }
        public void payAffiliate_Network(string buyer_email, decimal amount_paid)
        {
            decimal percentage = (decimal)0.0025;
            Affiliate affiliate;
            int count = 1;
            while(count <=4)
            {
                if(hasAffiliate(buyer_email))
                {
                    affiliate = GetJoinerAffiliate(buyer_email);
                    try
                    {
                        var balance = getAccountBalance(affiliate.Affiliate_Key.ToString());
                        var benefit = balance + calc_affiliate_Benefit(amount_paid, percentage);
                        deposit_Transaction(new Deposit()
                        {
                            Affiliate_Key = affiliate.Affiliate_Key.ToString(),
                            Joiner_Email = buyer_email,
                            Description = "Joiner purchase earnings",
                            Amount = calc_affiliate_Benefit(amount_paid, percentage),
                            Remaining_Balance = benefit,
                            Transaction_Date = DateTime.Now
                        });
                    }
                    catch (Exception ex) { }
                    buyer_email = affiliate.members.Email;
                    percentage /= 2;
                }               
                count++;
            }
        }
        public List<CartTransaction> getAllTransactions()
        {
            return db.CartTransactions.ToList();
        }
        public decimal calc_affiliate_Benefit(decimal amount, decimal percent)
        {
            return amount * percent;
        }
        public void deposit_Transaction(Deposit deposit)
        {
            try
            {
                db.Deposits.Add(deposit);
                db.SaveChanges();
            }
            catch (Exception ex) { }
        }
        public bool can_Withdraw(Withdraw withdraw)
        {
            return withdraw.Amount >= db.WithdrawLevels.FirstOrDefault().Amount;
        }
        public void withdraw_Transaction(Withdraw withdraw)
        {
            try
            {
                if (can_Withdraw(withdraw))
                {
                    db.Withdrawals.Add(withdraw);
                    db.SaveChanges();
                }
            }
            catch (Exception ex) { }
        }
        public decimal getAccountBalance(string affiliate_key)
        {
            decimal amount = 0;
            foreach (var item in db.CartTransactions.ToList().Where(x => x.Affiliate_Key == affiliate_key))
            {
                amount += item.Amount;
            }
            return amount;
        }
    }
}
