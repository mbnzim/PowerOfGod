using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerOfGod.Domain.Entity.EmployeLeave
{
    public class LeaveType
    {
        [Key]
        public int leaveTypeID { get; set; }

        [DisplayName("Leave Type")]
        public string type { get; set; }
    }
}
