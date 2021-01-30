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
    public class Delivery
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Delivery_ID { get; set; }
        [ForeignKey("Members")]
        public string To { get; set; }
        [ForeignKey("Shipping_Address")]
        public int Address_ID { get; set; }
        [ForeignKey("Order")]
        public int Order_ID { get; set; }
        [ForeignKey("Invoice")]
        public int Invoice_ID { get; set; }

        public DateTime Scheduled_Date { get; set; }
        public bool Schedule_Confirmed { get; set; }
        public DateTime Date_DElivereed { get; set; }

        public Members members { get; set; }
        public Shipping_Address Shipping_Address { get; set; }
        public Order Order { get; set; }
        public Invoice Invoice { get; set; }
    }
    public class Order_Delivery : Delivery
    {
        public string Contact_Person { get; set; }
        public string Telephone { get; set; }
        public bool Recieved_In_GoodOrder { get; set; }
        public string recipient { get; set; }
        // public string signature { get; set; }
        public DateTime date { get; set; }
    }
}
