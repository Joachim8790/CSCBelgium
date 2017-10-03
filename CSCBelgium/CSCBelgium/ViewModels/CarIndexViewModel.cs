using CSCBelgium.DAO.Model;
using CSCBelgium.Views.Configuratiepaneel.CustomCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSCBelgium.ViewModels
{
    public class CarIndexViewModel
    {
        public ICollection<tblCars> Cars { get; set; }
        public ICollection<tblImages> Images { get; set; }
        public int CarsInStock { get; set; }
        public JaNee JaNee { get; set;}
        public JaNee NeeJa { get; set; }
        public string parameter { get; set; }
    }
}