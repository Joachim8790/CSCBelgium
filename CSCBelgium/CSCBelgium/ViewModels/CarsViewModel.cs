using CSCBelgium.DAO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CSCBelgium.Views.Configuratiepaneel.CustomCode;

namespace CSCBelgium.ViewModels
{
    public class CarsViewModel
    {
        public ICollection<tblCars> cars { get; set; }
        public ICollection<tblImages> images { get; set; }
        public int selectedBrandId { get; set; }
        public IEnumerable<tblBrands> brandChoice { get; set; }
        public int selectedColorId { get; set; }
        public IEnumerable<tblColors> colorChoice { get; set; }
        public Fuel fuelChoice { get; set; }
        public Transmission transmissionChoice { get; set; }
        public int CarsInStock { get; set; }
        public int minKM { get; set; }
        public int maxKM { get; set; }
        public double minPrice { get; set; }
        public double maxPrice { get; set; }



    }
}