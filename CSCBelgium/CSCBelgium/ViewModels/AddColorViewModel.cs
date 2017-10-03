using CSCBelgium.DAO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSCBelgium.ViewModels
{
    public class AddColorViewModel
    {
        public ICollection<tblColors> colors { get; set; }
        public tblColors newColor { get; set; }
    }
}