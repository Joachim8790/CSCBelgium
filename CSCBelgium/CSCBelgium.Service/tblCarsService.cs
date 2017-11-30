using CSCBelgium.DAO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSCBelgium.DAO;

namespace CSCBelgium.Service
{
    public class tblCarsService
    {
        private tblCarsDAO dao = new tblCarsDAO();
        public ICollection<tblCars> getAllCars()
        {
            return dao.getAllCars();

        }
        public ICollection<tblImages> getAllImages()
        {
            return dao.getAllImages();

        }
        public tblCars getCar(int CarID)
        {

            return dao.getCar(CarID);
        }
        public void SellCar(tblCars car, bool sold)
        {
            dao.SellCar(car, sold);
        }
        public bool isSold(tblCars car)
        {
            return dao.isSold(car);
        }
        public void addCar(tblCars car)
        {
            dao.addCar(car);
        }
        public void deleteCar(int CarID)
        {
            dao.deleteCar(CarID);
        }
        public void editCar(tblCars car)
        {
            dao.editCar(car);
        }
        public ICollection<tblCars> FilterByBrand(ICollection<tblCars> cars, tblBrands brand)
        {
            return dao.FilterByBrand(cars, brand);

        }
        public ICollection<tblCars> FilterByColor(ICollection<tblCars> cars, tblColors color)
        {
            return dao.FilterByColor(cars, color);

        }
        public ICollection<tblCars> FilterByPrice(ICollection<tblCars> cars, int startprice, int endprice)
        {
            return dao.FilterByPrice(cars, startprice, endprice);

        }
        public ICollection<tblCars> FilterByKM(ICollection<tblCars> cars, int startKM, int endKM)
        {
            return dao.FilterByKM(cars, startKM, endKM);

        }
        public ICollection<tblImages> GetImagesByCar(tblCars car)
        {
            return dao.GetImagesByCar(car);
        }
        public ICollection<tblImages> GetFrontImages()
        {
            return dao.GetFrontImages();
        }
    }
}
