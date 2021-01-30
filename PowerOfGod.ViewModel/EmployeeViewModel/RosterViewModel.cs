using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerOfGod.ViewModel.EmployeeViewModel
{
    public class RosterViewModel
    {
        [Key]
        public string EmpNum { get; set; }
        public int queue { get; set; }
    }
}
