using CSCBelgium.DAO;
using CSCBelgium.DAO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCBelgium.Service
{
    public class tblSlidesService
    {
        private tblSlidesDAO dao = new tblSlidesDAO();
        public ICollection<tblSlides> getSlides()
        {
            return dao.getSlides();
        }
        public tblSlides getSlide(int slideID)
        {
            return dao.getSlide(slideID);

        }
        public void deleteSlide(int slideID)
        {
            dao.deleteSlide(slideID);
        }
        public void addSlide(tblSlides slide)
        {
            dao.addSlide(slide);
        }
        public void updateAlignment(tblSlides slide, int ddlID)
        {

            dao.updateAlignment(slide, ddlID);

        }
    }
    

}
