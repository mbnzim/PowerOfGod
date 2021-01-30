using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerOfGod.ViewModel.EmployeeViewModel
{
    public class ContactUsViewModel
    {
        public int id { get; set; }
        [Required]
        public string subject { get; set; }
        [Required]
        public string body { get; set; }
        public string username { get; set; }
        public bool read { get; set; }
        public DateTime datesent { get; set; }
        // public string category { get; set; }
    }
    public class ScreenshotsViewModel
    {
        public int id { get; set; }
        public byte[] imagedata { get; set; }
        public string imagemimeType { get; set; }
    }
}
