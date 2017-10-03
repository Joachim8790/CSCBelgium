using CSCBelgium.DAO;
using CSCBelgium.DAO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCBelgium.Service
{
   public class tblColorsService
    {
        private tblColorsDAO dao = new tblColorsDAO();
        public ICollection<tblColors> getColors()
        {
            return dao.getColors();
        }
        public tblColors getColor(int colorID)
        {
            return dao.getColor(colorID);
        }
        public void addColor(tblColors color)
        {
             dao.addColor(color);
        }
        public void deleteColor(int colorid)
        {
            dao.deleteColor(colorid);
        }
    }
}
