using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerOfGod.ViewModel.EmployeeViewModel
{
    public class CalanderViewModel
    {
        public string id { get; set; }
        public string text { get; set; }
        public string email { get; set; }
        public string EmpNum { get; set; }
        public string fullname { get; set; }
        //public DateTime start_date { get; set; }
        //public DateTime end_date { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> Date { get; set; }
        public TimeSpan startTime { get; set; }
        public TimeSpan endTime { get; set; }
    }
}
