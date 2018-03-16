using CSCBelgium.DAO.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CSCBelgium.ViewModels
{
    public class HomeViewModel
    {
        public ICollection<tblCars> first8Cars { get; set; }
        public ICollection<tblImages> firstImages { get; set; }
        [Display(Name ="Naam en voornaam")]
        public string name { get; set; }
        [Display(Name = "Emailadres")]
        public string emailaddress { get; set; }
        [Display(Name = "Onderwerp")]
        public string subject { get; set; }
        [Display(Name = "Bericht")]
        [Required]
        public string message { get; set; }
        public ICollection<tblSlides> slides { get; set; }
        public string scrollToElement { get; set; }

    }
}