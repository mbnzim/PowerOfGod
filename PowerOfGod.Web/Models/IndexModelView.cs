using Microsoft.Ajax.Utilities;
using PowerOfGod.Domain.Entity.Memberss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PowerOfGod.Web.Models
{
    public class IndexModelView
    {
        public LoginViewModel loginmodel { get; set; }
        public Members registermodel { get; set; }
    }
}