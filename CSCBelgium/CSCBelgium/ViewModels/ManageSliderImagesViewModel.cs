using CSCBelgium.DAO.Model;
using CSCBelgium.Views.Configuratiepaneel.CustomCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSCBelgium.ViewModels
{
    public class ManageSliderImagesViewModel
    {
        public ICollection<tblSlides> slides { get; set; }
        public Uitlijning LinksEerst { get; set; }
        public Uitlijning MiddenEerst { get; set; }
        public Uitlijning RechtsEerst { get; set; }
    }
}