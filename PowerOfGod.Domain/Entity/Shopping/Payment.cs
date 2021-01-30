using PowerOfGod.Domain.Entity.Memberss;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerOfGod.Domain.Entity.Shopping
{
    public class Payment
    {
        [Key]
        [Display(Name = "ID")]
        public int payment_ID { get; set; }
       //Display(Name = "Customer")]
        //[ForeignKey("Customer")]
       

        [Required]
        [Display(Name = "Date")]
        public DateTime Date { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Amount")]
        public double AmountPaid { get; set; }
        [Required]
        [Display(Name = "Description")]
        public string PaymentFor { get; set; }
        [Required]
        [Display(Name = "Payment Method")]
        public string PaymentMethod { get; set; }

        [ForeignKey("Order")]
        public string Order_ID { get; set; }

       //public string Email { get; set; }

        public int MemberId { get; set; }

        public virtual Members members{ get; set; } 

        public virtual Order Order { get; set; }
    }
}
