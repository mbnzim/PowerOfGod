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
   public class Donation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        //[Required]
        [Display(Name = "Donator Fullnames")]
        public string FullName { get; set; }

        [Required]
        [Display(Name = "Donating Towards")]
        public string description { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Select Date")]
        [Display(Name = "Donation Date")]
        [DataType(DataType.Date)]
        public DateTime date { get; set; }

        [Required]
        [Display(Name = "Donation Amount")]
        public double amount { get; set; }

       // [Required]
        [Display(Name = "Cardholder's Name")]
        public string Cardholder { get; set; }

        [Required(ErrorMessage = "Credit card Number must be correct")]
        [StringLength(16)]
        [Display(Name = "Credit card Number")]
        public string CardNumber { get; set; }

        [Required(ErrorMessage = "CCV Code must be correct")]
        [StringLength(4)]
        [Display(Name = "CCV Code")]
        public string CCV { get; set; }

        [Required(ErrorMessage = "Expiry Date must be correct")]
        [Display(Name = "Expiry Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ExpiryDate { get; set; }

        public virtual ApplicationUser User { get; set; }

        //public string transCode { get; set; }
        //public virtual TransactionCode TransactionCode { get; set; }

    }
}
