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
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Order_ID { get; set; }

        public DateTime date_created { get; set; }
            

        public bool shipped { get; set; }
        public string status { get; set; }
        public bool packed { get; set; }
        //public string Email { get; set; }


        [ForeignKey("members")]
        public int MemberId { get; set; }
        public virtual Members members { get; set; }


        public virtual ICollection<Order_Item> Order_Items { get; set; }
        public virtual ICollection<Shipping_Address> Shipping_Addresses { get; set; }

      
        public virtual ICollection<Order_Tracking> Order_Tracking { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }


        //public virtual ICollection<Delivery_Schedule> Delivery_Schedules { get; set; }
        //public virtual ICollection<Exchange_n_Return> Exchange_n_Returns { get; set; }
    }
}
