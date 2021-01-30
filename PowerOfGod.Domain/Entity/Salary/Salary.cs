using PowerOfGod.Domain.Entity.Employee;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace PowerOfGod.Domain.Entity.Salary
{
    public class Salary
    {
        [Key]
        public int SalaryId { get; set; }

        [DataType(DataType.Time)]
        public TimeSpan startTime { get; set; }

        [DataType(DataType.Time)]
        public TimeSpan endTime { get; set; }


        [DataType(DataType.Date)]
       [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        
        public Nullable<DateTime> Date { get; set; }

        public int hoursWorked { get; set; }
        public double Rate { get; set; }
        public double wage { get; set; }
        //public int JobTitleId { get; set; }
        //public virtual JobTitle jobTitle { get; set; }

        public string email { get; set; }
        //public Employees employees { get; set; }
        //public int deptCode { get; set; }
        //public virtual Departments departments { get; set; }


        public string EmpNum { get; set; }
        public virtual Employees employees { get; set; }

        [DisplayName("Full Name")]
        public string firstName { get; set; }

        public int CalcHourWorked()
        {
            return (endTime.Hours - startTime.Hours);
        }

        public double CalcDaySalaryWorked()
        {
            return (CalcHourWorked() * Rate);
        }

    }

}
