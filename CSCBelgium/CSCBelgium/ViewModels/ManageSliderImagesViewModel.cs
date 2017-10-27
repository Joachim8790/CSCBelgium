using CSCBelgium.DAO.Model;
using CSCBelgium.Views.Configuratiepaneel.CustomCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CSCBelgium.ViewModels
{
    public class ManageSliderImagesViewModel
    {
        public ICollection<tblSlides> slides { get; set; }
        public Uitlijning LinksEerst { get; set; }
        public Uitlijning MiddenEerst { get; set; }
        public Uitlijning RechtsEerst { get; set; }
        public ICollection<int> order { get; set; }
        public ICollection<List<SelectListItem>> orderlist { get; set; }
        public int selectedInt { get; set; }
    }
}