using PowerOfGod.Domain.Entity.Memberss;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerOfGod.Domain.Entity.Shopping
{
    public class Affiliate
    {
        [Key, Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Affiliate_Key { get; set; }

        //[ForeignKey("Members")]
        public string Email { get; set; }
        public virtual Members members { get; set; } 

        public virtual ICollection<Affiliate_Joiner> Affiliate_Joiners { get; set; }
    }

    public class Affiliate_Joiner
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [ForeignKey("Affiliate")]
        public Guid Affiliate_Key { get; set; }
        public string Email { get; set; }
        //[ForeignKey("Customer")]
        public string New_Customer_Email { get; set; }

        public bool used { get; set; }
        public virtual Affiliate Affiliate { get; set; }
        //public virtual Customer Customer { get; set; }
    }
}
