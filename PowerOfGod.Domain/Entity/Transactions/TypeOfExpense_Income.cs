using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PowerOfGod.Domain.Entity.Transactions
{
    public class TypeOfExpense_Income
    {
        [Key]
        public string Id { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string description { get; set; }
    }
}
