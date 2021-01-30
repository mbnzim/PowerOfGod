using PowerOfGod.Domain.Entity.Employee;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerOfGod.Domain.Entity.Salary
{
    public class JobTitle
    {
        [Key]
        public int JobTitleId { get; set; }
        public double rate { get; set; }

        public int SalaryId { get; set; }
        public Salary salaries { get; set; }
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]


        public int deptCode { get; set; }
        virtual public Departments departments { get; set; }
    }
}
