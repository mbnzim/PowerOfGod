using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PowerOfGod.Domain.Context;

namespace PowerOfGod.Domain.Entity.Transactions
{
    public class Church_Expenses
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ExpensesId { get; set; }

  
      

        [Required]
        [Display(Name = "Expense Amount")]
        public double Amount { get; set; }

        [Required]
        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        public DateTime date { get; set; }

        //public string transCode { get; set; }
        //public virtual TransactionCode TransactionCode { get; set; }
        public string Id { get; set; }
        public virtual TypeOfExpense_Income TypeOfExpense_Incomes { get; set; }

        public double total { get; set; }

        public Church_Expenses()
        {
            this.date = DateTime.Now;
        }

        ApplicationDbContext db = new ApplicationDbContext();
        public double SumOfExpenses()
        {
            return total = (double)(db.Church_Expenses).Sum(x => x.Amount);
        }
        public double CalcTotal()
        {

            return db.Transactions.Sum(d => d.amount);
        }

        //public string transCode { get; set; }
        //public virtual TransactionCode TransactionCode { get; set; }
    }
}
    

