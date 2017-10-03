using CSCBelgium.DAO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSCBelgium.ViewModels
{
    public class RimsViewModel
    {
        public ICollection<tblRims> rims { get; set; }
        public ICollection<tblRimImages> images { get; set; }
        public int CarsInStock { get; set; }
    }
}