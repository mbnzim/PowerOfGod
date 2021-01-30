using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerOfGod.ViewModel.EmployeeViewModel
{
    public class LeaveViewModel
    {
        [Key]
        public int LeaveID { get; set; }
        public string type { get; set; }
        public string reason { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> startDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> endDate { get; set; }
        public int numberOfDay { get; set; }
        //public DateTime? createDate { get; set; }
        public string status { get; set; }
        public string createdBy { get; set; }
        public string updateBy { get; set; }
        //public string updateBy { get; set; }
        public string email { get; set; }

    }
}
