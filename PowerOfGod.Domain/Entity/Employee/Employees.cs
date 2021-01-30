using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerOfGod.Domain.Entity.Employee
{
    public class Employees
    {
        [Key]
        [Required(ErrorMessage = "Employee Number required")]
        [DisplayName("Employee Number")]
        public string EmpNum { get; set; }
        [Required(ErrorMessage = "First Name required")]
        [DisplayName("First Name")]
        public string firstName { get; set; }
        [Required(ErrorMessage = "Last Name required")]
        [DisplayName("Last Name")]
        public string lastName { get; set; }
        [Required(ErrorMessage = "ID Number required")]
        [DisplayName("ID Number ")]
        public string IDNumber { get; set; }
        [Required(ErrorMessage = "Please select the gender")]
        [DisplayName("Gender")]
        public string gender { get; set; }
        [Required]
        [Display(Name = "Position")]
        public string UserRole { get; set; }
        [DataType(DataType.Date)]
        public DateTime hireDate { get; set; }
        [Required(ErrorMessage = "Mobile number required")]
        [DisplayName("Mobile")]
        public string mobile { get; set; }
        [Required(ErrorMessage = "Email required")]
        [DisplayName("Email")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Email Formatt is incorrect")]
        public string email { get; set; }
        [DisplayName("Status")]
        public string status { get; set; }

        [DisplayName("Shift")]
        public string shift { get; set; }

        public int queue { get; set; }
        [DisplayName("Upload Image")]
        public byte[] Picture { get; set; }


        //Foriegn Key
        [ForeignKey("departments")]
        public int deptCode { get; set; }
        virtual public Departments departments { get; set; }

        //Foriegn Key
        public string typeCode { get; set; }
        virtual public TypeOfContracts contract { get; set; }

    }
}
