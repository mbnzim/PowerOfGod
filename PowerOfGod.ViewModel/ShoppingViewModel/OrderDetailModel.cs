using PowerOfGod.Domain.Entity.Memberss;
using PowerOfGod.Domain.Entity.Shopping;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerOfGod.ViewModel.ShoppingViewModel
{
    public class OrderDetailModel
    {
        public Members members { get; set; }
        public Order order { get; set; }
        public string shipping_method { get; set; }
        public Order_Delivery delivery { get; set; }
        public Shipping_Address address { get; set; }
        [Display(Name ="Payment Method")]
        public string payment_Method { get; set; }
        public Payment payment { get; set; }
        public List<Order_Item> order_items { get; set; }
        [Display(Name = "Order Total")]
        [DataType(DataType.Currency)]
        public decimal order_total { get; set; }
    }
}
