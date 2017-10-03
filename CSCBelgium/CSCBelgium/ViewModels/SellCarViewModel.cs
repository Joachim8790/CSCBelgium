using CSCBelgium.Views.Configuratiepaneel.CustomCode;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CSCBelgium.ViewModels
{
    public class SellCarViewModel
    {
        [Display(Name="Voornaam")]
        public string SurName { get; set; }
        [Display(Name = "Naam")]
        public string Name { get; set; }
        [Display(Name = "Emailadres")]
        public string Emailaddress { get; set; }
        [Display(Name = "Telefoonnummer")]
        public string Phone { get; set; }
        [Display(Name = "Merk")]
        public string CarBrand { get; set; }
        [Display(Name = "Opmerking")]
        public string Comment { get; set; }
        [Display(Name = "Model")]
        public string CarModel { get; set; }
        [Display(Name = "Kilometerstand")]
        public int CarKilometers { get; set; }
        [Display(Name = "Schade")]
        public string CarDamage { get; set; }
        [Display(Name = "Beschrijving")]
        public string CarDescription { get; set; }
        [Display(Name = "Eerste inschrijving")]
        public DateTime firstRegistration { get; set; }
        [Display(Name = "Bouwjaar")]
        public int YearOfConstruction { get; set; }
        [Display(Name = "Kleur")]
        public string Color { get; set; }
        [Display(Name = "Brandstof")]
        public Fuel fuelChoice { get; set; }
        [Display(Name = "Transmissie")]
        public Transmission transmissionChoice { get; set; }
        public IEnumerable<HttpPostedFileBase> files { get; set; }
    }
}