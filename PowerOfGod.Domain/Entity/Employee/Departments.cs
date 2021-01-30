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
    public class Departments
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int deptCode { get; set; }

        [Required(ErrorMessage = "Department required")]
        [DisplayName("Department Name")]
        public string deptName { get; set; }

        //public double hrlyrate { get; set; }
    }
}
