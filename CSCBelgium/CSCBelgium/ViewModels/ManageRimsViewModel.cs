using CSCBelgium.DAO.Model;
using CSCBelgium.Views.Configuratiepaneel.CustomCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSCBelgium.ViewModels
{
    public class ManageRimsViewModel
    {
        public ICollection<tblRims> rims { get; set; }
        public JaNee janee { get; set; }
        public JaNee neeja { get; set; }
        public int RimsInStock { get; set; }
        public ICollection<tblRimImages> images { get; set; }
    }
}