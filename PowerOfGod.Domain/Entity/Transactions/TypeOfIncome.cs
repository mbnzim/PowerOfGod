using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerOfGod.Domain.Entity.Transactions
{
    public class TypeOfIncome
    {
        [Key]
        public string Id { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string description { get; set; }
    }
}
