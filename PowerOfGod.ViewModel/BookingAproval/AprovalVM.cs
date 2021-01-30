 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace PowerOfGod.ViewModel.BookingAproval
{
    public class ApprovalVM
    {
        [Key]
        public int PastorBookingID { get; set; }
        public string Status { get; set; }
    }
    public class ApprovalVenueVM
    {
        [Key]
        public int VenueBookingID { get; set; }
        public string Venue_Status { get; set; }
    }
}

