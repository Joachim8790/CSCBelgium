using CSCBelgium.DAO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSCBelgium.ViewModels
{
    public class AddRimBrandViewModel
    {
        public ICollection<tblRimBrands> brands { get; set; }
        public tblRimBrands newBrand { get; set; }
    }
}