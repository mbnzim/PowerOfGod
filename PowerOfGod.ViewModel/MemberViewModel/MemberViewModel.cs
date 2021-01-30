using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerOfGod.ViewModel.MemberViewModel
{
      public class MemberViewModel
    {
        [Key]
        public int MemberId { get; set; }
        public string Email { get; set; }
        public string fullname { get; set; }
        public string gender { get; set; }
        [Display(Name = "Position")]
        public string UserRole { get; set; }
        [DisplayName("Upload Image")]
        public byte[] Picture { get; set; }      
        public string PhoneNumber { get; set; }
        public string address { get; set; }
        public string IDNumber { get; set; }
        
    }
}
