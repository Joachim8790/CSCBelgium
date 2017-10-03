using CSCBelgium.DAO.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CSCBelgium.ViewModels
{
    public class CarDetailsViewModel
    {
        public tblCars car { get; set; }
        public ICollection<tblImages> images { get; set; }
        [Display(Name = "Naam")]
        public string name { get; set; }
        [Display(Name = "Emailadres")]
        public string emailadres { get; set; }
        [Display(Name = "Bericht")]
        [Required]
        public string message { get; set; }
        public string carbrand { get; set; }
        public string carmodel { get; set; }
    }
}