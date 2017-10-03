using CSCBelgium.DAO;
using CSCBelgium.DAO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCBelgium.Service
{
    public class tblImagesService
    {
        private tblImagesDAO dao = new tblImagesDAO();
        public void AddImage(tblImages image)
        {
            dao.AddImage(image);
        }
        public void DeleteAllImagesOfCar(tblCars car)
        {
            dao.DeleteAllImagesOfCar(car);
        }
    }
}
