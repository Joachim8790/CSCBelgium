using CSCBelgium.DAO;
using CSCBelgium.DAO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCBelgium.Service
{
    public class tblRimsService
    {
        private tblRimsDAO dao = new tblRimsDAO();
        public ICollection<tblRims> getRims()
        {
            return dao.getRims();
        }
        public ICollection<tblRimImages> getRimImages()
        {
            return dao.getRimImages();
        }
        public tblRims getRim(int rimID)
        {
            return dao.getRim(rimID);
        }
        public void addRim(tblRims rim)
        {
            dao.addRim(rim);
        }
        public void addRimImage(tblRimImages rimImage)
        {
            dao.addRimImage(rimImage);
        }
        public ICollection<tblRimImages> getImagesOfRim(int rimID)
        {
            return dao.getImagesOfRim(rimID);
        }

        public void deleteRim(int rimID)
        {
            dao.deleteRim(rimID);
        }
        public bool isSold(tblRims rim)
        {
            return dao.isSold(rim);
        }
        public void SellRim(tblRims rim, bool sold)
        {
            dao.SellRim(rim, sold);
        }

        public void UpdateImage(tblRimImages image)
        {
            dao.UpdateImage(image);
        }
    }
}
