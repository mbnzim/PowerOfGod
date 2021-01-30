using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerOfGod.Domain.Entity.Shopping
{
    public class Order_Item
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Order_Item_id { get; set; }
        [ForeignKey("Order")]
        public string Order_id { get; set; }
        public virtual Order Order { get; set; }

        [ForeignKey("Item")]
        public int item_id { get; set; }
        public virtual Item Item { get; set; }

        public int quantity { get; set; }
        [DataType(DataType.Currency)]
        public double price { get; set; }
        [Display(Name = "Picture")]
        //[DataType(DataType.Upload)]
        public byte[] Picture { get; set; }

        public bool replied { get; set; }
        public Nullable<DateTime> date_replied { get; set; }

        public bool accepted { get; set; }
        public Nullable<DateTime> date_accepted { get; set; }

        public bool shipped { get; set; }
        public string status { get; set; }
        public Nullable<DateTime> date_shipped { get; set; }
    }
}
