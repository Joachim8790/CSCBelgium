using CSCBelgium.DAO.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CSCBelgium.DAO
{
    public class tblCarsDAO
    {
        public ICollection<tblCars> getAllCars()
        {
            using (var db = new CSCbelgiumDatabaseEntities())
            {
                return db.tblCars.Include(a => a.tblBrands).Include(b => b.tblColors).OrderBy(a => a.Createdate).ToList();
            }

        }
        public ICollection<tblImages> getAllImages()
        {
            using (var db = new CSCbelgiumDatabaseEntities())
            {
                return db.tblImages.ToList();
            }

        }
        public tblCars getCar(int CarID)
        {
            using (var db = new CSCbelgiumDatabaseEntities())
            {
                return db.tblCars.Where(a => a.CarID == CarID).FirstOrDefault();
            }

        }
        public bool isSold(tblCars car)
        {
            using (var db = new CSCbelgiumDatabaseEntities())
            {
                if (car.Sold== 1)
                {
                    return true;

                }
                else
                {
                    return false;
                }

            }
        }
        public void SellCar(tblCars car, bool sold)
        {
            using (var db = new CSCbelgiumDatabaseEntities())
            {
                if (sold)
                {
                    car.Sold = (byte)1;
                    db.Entry(car).State = EntityState.Modified;
                    db.SaveChanges();

                }
                else
                {
                    car.Sold = (byte)0;
                    db.Entry(car).State = EntityState.Modified;
                    db.SaveChanges();
                }
                
            }
        }
        public void addCar(tblCars car)
        {
            using (var db = new CSCbelgiumDatabaseEntities())
            {
                db.tblCars.Add(car);
                db.SaveChanges();
            }
        }
        public void deleteCar(int CarID)
        {
            using (var db = new CSCbelgiumDatabaseEntities())
            {
                tblImagesDAO imagesdao = new tblImagesDAO();
                imagesdao.DeleteAllImagesOfCar(getCar(CarID));
                tblCars car = getCar(CarID);
                db.Entry(car).State = EntityState.Deleted;
                db.SaveChanges();
            }
        }
        public void editCar(tblCars car)
        {
            using (var db = new CSCbelgiumDatabaseEntities())
            {
                db.Entry(car).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
        public ICollection<tblCars> FilterByBrand(ICollection<tblCars> cars,tblBrands brand)
        {
            using (var db = new CSCbelgiumDatabaseEntities())
            {
                return cars.Where(a => a.tblBrands.BrandName == brand.BrandName).ToList();
            }

        }
        public ICollection<tblCars> FilterByColor(ICollection<tblCars> cars, tblColors color)
        {
            using (var db = new CSCbelgiumDatabaseEntities())
            {
                return cars.Where(a => a.tblColors.ColorName == color.ColorName).ToList();
            }

        }
        public ICollection<tblCars> FilterByTransmission(ICollection<tblCars> cars)
        {
            using (var db = new CSCbelgiumDatabaseEntities())
            {
                return cars;
            }

        }
        public ICollection<tblCars> FilterByPrice(ICollection<tblCars> cars, int startprice, int endprice)
        {
            using (var db = new CSCbelgiumDatabaseEntities())
            {
                return cars.Where(d => (int)d.CarPrice >= startprice && (double)d.CarPrice <= endprice).ToList();
            }

        }
        public ICollection<tblCars> FilterByKM(ICollection<tblCars> cars, int startKM, int endKM)
        {
            using (var db = new CSCbelgiumDatabaseEntities())
            {
                return cars.Where(d => (int)d.CarKilometers >= startKM && (double)d.CarKilometers <= endKM).ToList();
            }

        }
        public ICollection<tblImages> GetImagesByCar(tblCars car)
        {
            using (var db = new CSCbelgiumDatabaseEntities())
            {
                return db.tblImages.Where(a => a.CarID == car.CarID).ToList();
            }
        }
        public ICollection<tblImages> GetFrontImages()
        {
            using (var db = new CSCbelgiumDatabaseEntities())
            {

                return db.tblImages.Where(a => a.ImageOrder == 0).ToList();
            }
        }

    }
}
