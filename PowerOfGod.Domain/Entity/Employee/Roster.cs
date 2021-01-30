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
    public class Roster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int code { get; set; }

        [Required]
        //[DataType(DataType.Date)]
        [DisplayName("Date")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ?Date { get; set; }

        [Required]
        [DisplayName("Start Time")]
        public TimeSpan startTime { get; set; }
        [Required]
        [DisplayName("End Time")]
        public TimeSpan endTime { get; set; }

        //public string Shift { get; set; }

        //Foriegn Key
        [ForeignKey("departments")]
        public int deptCode { get; set; }
        public Departments departments { get; set; }

        //Foriegn Key
        [ForeignKey("employees")]
        public string EmpNum { get; set; }
        public Employees employees { get; set; }
    }
}
