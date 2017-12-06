using CSCBelgium.DAO.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCBelgium.DAO
{
    public class tblImagesDAO
    {
        public void AddImage(tblImages image)
        {
            using (var db = new CSCbelgiumDatabaseEntities())
            {
                db.tblImages.Add(image);
                db.SaveChanges();
            }
        }
        public void UpdateImage(tblImages image)
        {
            using (var db = new CSCbelgiumDatabaseEntities())
            {
                db.Entry(image).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void DeleteAllImagesOfCar(tblCars car)
        {
            using (var db = new CSCbelgiumDatabaseEntities())
            {
                db.tblImages.RemoveRange(db.tblImages.Where(a => a.CarID == car.CarID));
                db.SaveChanges();
                
            }
        }
    }
}
