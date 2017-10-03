using CSCBelgium.DAO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSCBelgium.ViewModels
{
    public class AddBrandViewModel
    {
        public ICollection<tblBrands> brands { get; set; }
       
        public tblBrands newBrand { get; set; }
    }
}