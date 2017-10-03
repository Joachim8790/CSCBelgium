using CSCBelgium.DAO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSCBelgium.ViewModels
{
    public class AddRimViewModel
    {
        public tblRims rim { get; set; }
        public IEnumerable<tblBrands> brandChoice { get; set; }
        public int selectedBrandId { get; set; }
        public IEnumerable<HttpPostedFileBase> files { get; set; }
    }
}