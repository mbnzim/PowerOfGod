using PowerOfGod.Domain.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerOfGod.Domain.Entity.UserContact
{
    public class ContactUs
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int contactId { get; set; }
        public string subject { get; set; }
        public string body { get; set; }
        public string userName { get; set; }
        [DefaultValue(false)]
        public bool read { get; set; }
        public DateTime datesent { get; set; }
        public string category { get; set; }
        public ICollection<Screenshot> screenshots { get; set; }
        public virtual ApplicationUser User { get; set; }

    }
}
