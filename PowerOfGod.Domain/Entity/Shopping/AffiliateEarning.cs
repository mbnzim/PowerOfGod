using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerOfGod.Domain.Entity.Shopping
{
    public class CartTransaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Ref { get; set; }
        public string Affiliate_Key { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public decimal Remaining_Balance { get; set; }
        public DateTime Transaction_Date { get; set; }
    }

    public class Deposit : CartTransaction
    {
        public string Joiner_Email { get; set; }
    }

    public class Withdraw : CartTransaction
    {

    }
    public class Transfer : CartTransaction
    {
        public string To_Affiliate { get; set; }
    }

    public class WithdrawLevel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Level_ID { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
    }
}
