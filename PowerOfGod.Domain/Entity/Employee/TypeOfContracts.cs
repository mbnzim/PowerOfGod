using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerOfGod.Domain.Entity.Employee
{
    public class TypeOfContracts
    {
        [Key]
        [Required(ErrorMessage = "Code required")]
        [DisplayName("Type Code")]
        public string typeCode { get; set; }
        [Required(ErrorMessage = "Type Name required")]
        [DisplayName("Type Name")]
        public string typeName { get; set; }
        [Required]
        [DisplayName("Payment")]
        public string Payment { get; set; }

        public string PaymentMethod()
        {
            if (Payment == "Yes")
            {
                return "Yes";
            }
            else
                return "No";
        }
    }

}
