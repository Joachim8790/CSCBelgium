using CSCBelgium.DAO.Model;
using CSCBelgium.Views.Configuratiepaneel.CustomCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSCBelgium.ViewModels
{
    public class EditCarViewModel
    {
        public tblCars car { get; set; }
        public int selectedBrandId { get; set; }
        public IEnumerable<tblBrands> brandChoice { get; set; }
        public int selectedColorId { get; set; }
        public IEnumerable<tblColors> colorChoice { get; set; }
        public Fuel fuelChoice { get; set; }
        public Transmission transmissionChoice { get; set; }
    }
}