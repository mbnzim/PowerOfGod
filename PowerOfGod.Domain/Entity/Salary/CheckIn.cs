using PowerOfGod.Domain.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerOfGod.Domain.Entity.Salary
{
    public class CheckIn
    {
        [Key]
        public int CheckInId { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [DisplayName("Start Time")]
        public TimeSpan startTime { get; set; }

        [DataType(DataType.Date)]
       // [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [RegularExpression(@"^\s*([01]?\d|2[0-3])(:([0-5]\d))(:([0-5]\d))?\s*$")]
        public DateTime Date { get; set; }

        public string email { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
