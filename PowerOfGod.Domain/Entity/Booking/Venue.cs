using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using PowerOfGod.Domain.Context;

namespace PowerOfGod.Domain.Entity.Booking
{
    public class Venue
    {
        [Key]
        public int VenueID { get; set; }
        [DisplayName("Venue Name ")]
        public string Venue_Name { get; set; }
        //[DisplayName("Venue Photograph ")]
        //public string Image_Url { get; set; }
        [DisplayName("Venue Location ")]
        public string Location { get; set; }
        [DisplayName("Venue Booking Price ")]
        public double Price { get; set; }
        [DisplayName("Venue Capacity")]
        public int Venue_Capacity { get; set; }


        public int VenueCodeID { get; set; }
        public virtual TypeOFVenue TypeOFVenue { get; set; }



    }
    public class TypeOFVenue
    {
        [Key]
        public int VenueCodeID { get; set; }  
        public string VenueType { get; set; }
    }

    public class VenueBookingDescription
    {
        [Key]
        public int DescriptionID { get; set; }
        [DisplayName("Booking Description")]
        public string booking_Description { get; set; }
    }
    public class VenueBookingVM
    {
        [Key]
        public int BookingId { get; set; }
        [DisplayName("Venue Name")]
        [StringLength(50)]
        public string VenueName { get; set; }
        [DisplayName("Booking Description")]
        public string Book_Descr { get; set; }
        [DisplayName("Venue Location")]
        public string Location { get; set; }
        [DataType(DataType.Currency)]
        [DisplayName("Venue Price")]
        public double Price { get; set; }
        [DisplayName("Date")]
        public DateTime Start_date { get; set; }
        [DataType(DataType.Time)]
        [DisplayName("Start Time")]
        public TimeSpan StartTime { get; set; }
        [DisplayName("End Time")]
        [DataType(DataType.Time)]
        public TimeSpan EndTime { get; set; }
        [DisplayName("User Email")]
        public string Email { get; set; }
        [DisplayName("Venue approval")]
        public string V_Status { get; set; }


    }
}

