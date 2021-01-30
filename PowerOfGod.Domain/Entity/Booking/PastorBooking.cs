using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;


using PowerOfGod.Domain.Context;

namespace PowerOfGod.Domain.Entity.Booking
{
    public class PastorBooking
    {
        [Key]
        public int PastorBookingID { get; set; }

        [DisplayName("Event Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        //[DataType(DataType.Date)] 
        public DateTime Start_date { get; set; }
        [DisplayName("Event Start Time")]
        [DataType(DataType.Time)]
        public TimeSpan TimeStart { get; set; }
        [DisplayName("Event End Time")]
        [DataType(DataType.Time)]
        public TimeSpan TimeEnd { get; set; }
        [DisplayName("BookedBy")]
        public string BookedBy { get; set; }
        public double BookingAmount { get; set; }
        public string Status { get; set; }

        //[Displa yName("Reserve Pastor")]
        //public bool Reserved { get; set; }

        private int _NumberInUse;
        private int _NumberReserved;


        
        //public int UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public int PastorID { get; set; }
        public virtual Pastor Pastor { get; set; }


        public int ReasonID { get; set; }
        public virtual PastorBookingReason PastorBookingReason { get; set; }


        public int NumberOfInUsePastors()
        {
            return new ApplicationDbContext()
                .pastorsBooking.Where(m => m.PastorBookingID == this.PastorBookingID).Count();
        }


        public int NumberOfReservedPastors()
        {
            return new ApplicationDbContext()
                .pastors.Where(m => m.PastorID == this.PastorBookingID).Count();
        }
        public int NumberInUse
        {
            get { return _NumberInUse; }
            set { this._NumberInUse = this.NumberOfInUsePastors(); }
        }
        public bool CheckDate()
        {
            if (Start_date < DateTime.Now.Date)
            {
                return false;
            }
            if (Start_date == DateTime.Now.Date)
            {
                return true;
            }
            return true;
        }
    }
    
        public class VenueBooking
        {
            [Key]
            public int VenueBookingID { get; set; }
            //  [DataType(DataType.Date)]
            [DisplayName("Event Date")]
            public DateTime Vstart_date { get; set; }
            [DisplayName("Event Start Time")]
            [DataType(DataType.Time)]
            public TimeSpan StartTime { get; set; }
            [DisplayName("Event End Date")]
            [DataType(DataType.Time)]
            public TimeSpan EndTime { get; set; }
           public double V_Price { get; set; }
           public string Venue_Status { get; set; }


            [DisplayName("BookedBy")]
            public string BookedBy { get; set; }
     
            //public int UserId { get; set; }
            public virtual ApplicationUser User { get; set; }


            public int DescriptionID { get; set; }
            public virtual VenueBookingDescription VenueBookingDescription { get; set; }


            public int VenueID { get; set; }
            public virtual Venue Venue { get; set; }


            private int _NumberOfVenuesInUse { get; set; }

            public int NumberOfInUsePastors()
            {
                return new ApplicationDbContext()
                    .venueBooking.Where(m => m.VenueBookingID == this.VenueBookingID).Count();
            }


            public int NumberOfReservedPastors()
            {
                return new ApplicationDbContext()
                    .venues.Where(m => m.VenueID == this.VenueBookingID).Count();
            }
            public int NumberInUse
            {
                get { return _NumberOfVenuesInUse; }
                set { this._NumberOfVenuesInUse = this.NumberOfInUsePastors(); }
            }
            public bool CheckedDate()
            {
                if (this.Vstart_date < DateTime.Now.Date)
                {
                    return false;
                }
                if (Vstart_date == DateTime.Now.Date)
                {
                    return true;
                }
                return true;
            }

        }
    }

