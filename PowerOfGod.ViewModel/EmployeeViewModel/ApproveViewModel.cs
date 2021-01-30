using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerOfGod.ViewModel.EmployeeViewModel
{
    public class ApproveViewModel
    {
        [Key]
        public int LeaveID { get; set; }
        public string status { get; set; }
        public string updateBy { get; set; }
    }
}
