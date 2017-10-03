using CSCBelgium.DAO.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CSCBelgium.ViewModels
{
    public class RimDetailsViewModel
    {
        public tblRims rim { get; set; }
        public ICollection<tblRimImages> images {get;set;}
        [Display(Name = "Naam")]
        public string name { get; set; }
        [Display(Name = "Emailadres")]
        public string emailadres { get; set; }
        [Display(Name = "Bericht")]
        public string message { get; set; }
        public string rimbrand { get; set; }
        public string rimmodel { get; set; }
    }
}