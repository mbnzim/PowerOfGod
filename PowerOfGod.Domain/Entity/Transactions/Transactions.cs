using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using PowerOfGod.Domain.Context;
using PowerOfGod.Domain.Entity.Booking;

namespace PowerOfGod.Domain.Entity.Transactions
{
    public class Transactions
    {
        [Key]
        public int transId { get; set; }

        [Required]
        [DisplayName("Description: ")]
        [DataType(DataType.Text)]
        public string description { get; set; }

        [Required]
        [DisplayName("Amount: ")]
        [DataType(DataType.Currency)]
        public double amount { get; set; }

        [Required]
        [DisplayName("Date: ")]
        [DataType(DataType.Date)]
        public DateTime date { get; set; }

        //public int id { get; set; }
        //public virtual Donation Donation { get; set; }

        //public int ExpensesId { get; set; }
        //public virtual Church_Expenses Church_Expenses { get; set; }

        [DisplayName("Transaction Code: ")]
        public string transCode { get; set; }
        public virtual TransactionCode TransactionCode { get; set; }

        ApplicationDbContext db = new ApplicationDbContext();

        [DisplayName("Total: ")]
        public double total { get; set; }

        public double CalcTotal()
        {

            return db.Transactions.Sum(d => d.amount);
        }

        Church_Expenses exp = new Church_Expenses();
        public PastorBooking booking;

        public double Income()
        {
            return CalcTotal() - exp.SumOfExpenses();
        }

    }
}
