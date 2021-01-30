using PowerOfGod.Domain.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerOfGod.Domain.Entity.EmployeLeave
{
    public class EmployeeLeavel
    {
        [Key]
        public int LeaveID { get; set; }
        [DisplayName("Reason")]
        public string reason { get; set; }

        [DisplayName("Start Date")]
        [Required(ErrorMessage = "Satrt date required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime startDate { get; set; }

        [DisplayName("End Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "End date required")]
        public DateTime endDate { get; set; }

        [DisplayName("Number of Date")]
        public int numberOfDay { get; set; }

        [DisplayName("Created Date")]

        public Nullable<DateTime> createDate { get; set; }

        [DisplayName("Status")]
        public string status { get; set; }


        [DisplayName("Created By")]
        public string createdBy { get; set; }

        [DisplayName("Updated By")]
        public string updateBy { get; set; }


        [DisplayName("Upload File")]
        public byte[] Picture { get; set; }

        public int leaveTypeID { get; set; }
        public virtual LeaveType leavetype { get; set; }

        //public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }


        public double LeaveDays()
        {
            TimeSpan difference = endDate - startDate;
            var days = difference.TotalDays;
            return days;
        }
    }
}
