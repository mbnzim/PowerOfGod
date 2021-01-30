using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PowerOfGod.Domain.Entity.Transactions
{
    public class TransactionCode
    {
        [Key]
        [Required]
        [DisplayName("Transaction Code: ")]
        public string transCode { get; set; }

        [Required]
        [DisplayName("Transaction Title: ")]
        public string transTitle { get; set; }
    }
}
