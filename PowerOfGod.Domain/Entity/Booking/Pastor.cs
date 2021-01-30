using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;


namespace PowerOfGod.Domain.Entity.Booking
{
    public class Pastor
    {
        [Key]
        public int PastorID { get; set; }
        [Required(ErrorMessage = "First Name required")]
        [DisplayName("Pastor Name")]
        [StringLength(50)]
        public string PfirstName { get; set; }
        [Required(ErrorMessage = "Last Name required")]
        [DisplayName("Pastor Last Name")]
        [StringLength(50)]
        public string PlastName { get; set; }
        //[DisplayName("Pastor Photo")]
        //public byte[] Picture { get; set; }
        [DisplayName("Booking Price ")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}", ConvertEmptyStringToNull = true)]
        [DataType(DataType.Currency)]
        public double BookAmount { get; set; }

        [DisplayName("Cellphone number")]
        [DataType(DataType.PhoneNumber)]
        public string Cellnumber { get; set; }

        [DisplayName("Email ")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        

        public int TypeOfPastorID { get; set; }
        public virtual TypeOfPastor typeOfPastor { get; set; }
    }

    public class PastorBookingReason
    {
        [Key]
        public int ReasonID { get; set; }
        [DisplayName("Reason for booking ")]
        public string Reason { get; set; }
    }
    public class TypeOfPastor
    {
        [Key]
        public int TypeOfPastorID { get; set; }
        [DisplayName("Type of Pastor")]
        public string Pastortype { get; set; }

    }
    public class PastorBookingViewModel
    {
        [Key]
        public int PastorBookingVMId { get; set; }
        [DisplayName("Pastor Name")]
        public string PFirstName { get; set; }
        [DisplayName("Pastor Lastname")]
        public string PLastName { get; set; }
        [DisplayName("Booking Description")]
        public string Description { get; set; }
        [DisplayName("Start Date/Time")]
        public DateTime Start_date { get; set; }
        public TimeSpan TimeStart { get; set; }
        public TimeSpan TimeEnd { get; set; }
        public string Cellnumber { get; set; }

        //public byte[] Picture { get; set; }
        [DisplayName("Email ")]
        public string Email { get; set; }
        [DisplayName("Pastor Booking Amount")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}", ConvertEmptyStringToNull = true)]
        [DataType(DataType.Currency)]
        public double BookAmount { get; set; }
        public string Status { get; set; }



    }
}
