using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerOfGod.Domain.Entity.Transactions
{
    public class Church_Income
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ExpensesId { get; set; }


        [Required]
        [Display(Name = "Income Amount")]
        public double Amount { get; set; }

        [Required]
        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        public DateTime date { get; set; }

        public virtual TypeOfIncome TypeOfIncomes { get; set; }
        public string Id { get; set; }
    }
}
