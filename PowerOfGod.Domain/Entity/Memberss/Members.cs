using PowerOfGod.Domain.Entity.Shopping;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerOfGod.Domain.Entity.Memberss
{
    public class Members
    {
        [Key]
        public int MemberId { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [RegularExpression(pattern: @"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Numbers and special characters are not allowed.")]
        [StringLength(maximumLength: 35, ErrorMessage = "Fist Name must be atleast 3 characters long", MinimumLength = 3)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [RegularExpression(pattern: @"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Numbers and special characters are not allowed.")]
        [StringLength(maximumLength: 35, ErrorMessage = "Fist Name must be atleast 3 characters long", MinimumLength = 3)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "ID Number required")]
        [DisplayName("ID Number ")]
        public string IDNumber { get; set; }

        [Required(ErrorMessage = "Please select the gender")]
        [DisplayName("Gender")]
        public string gender { get; set; }

        //[Required]
        [Display(Name = "Position")]
        public string UserRole { get; set; }

        [Required(ErrorMessage = "Email required")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Email Formatt is incorrect")]
        [Display(Name = "Email")]
        [DataType(dataType: DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        [DataType(dataType: DataType.PhoneNumber)]
        [RegularExpression(pattern: @"^\(?([0]{1})\)?[-. ]?([1-9]{1})[-. ]?([0-9]{8})$", ErrorMessage = "Entered phone format is not valid.")]
        [StringLength(maximumLength: 10, ErrorMessage = "SA Contact Number must be exactly 10 digits long", MinimumLength = 10)]
        public string PhoneNumber { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(150)]
        [Display(Name = "Physical Address")]
        public string address { get; set; }

        //[Required(ErrorMessage = "Please upload a Picture")]
        [DisplayName("Upload Image")]
        public byte[] Picture { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
        public virtual ICollection<Affiliate> Affiliates { get; set; }
    }  
}