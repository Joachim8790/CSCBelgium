using CSCBelgium.DAO.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCBelgium.DAO
{
    public class tblRimsDAO
    {
        public ICollection<tblRims> getRims()
        {
            using (var db = new CSCbelgiumDatabaseEntities())
            {
                return db.tblRims.Include(a => a.tblRimBrands).ToList();
            }
        }
        public ICollection<tblRimImages> getRimImages()
        {
            using (var db = new CSCbelgiumDatabaseEntities())
            {
                return db.tblRimImages.ToList();
            }
        }
        public tblRims getRim(int rimID)
        {
            using (var db = new CSCbelgiumDatabaseEntities())
            {
                return db.tblRims.Where(a => a.RimID == rimID).FirstOrDefault();
            }
        }
        public ICollection<tblRimImages> getImagesOfRim(int rimID)
        {
            using (var db = new CSCbelgiumDatabaseEntities())
            {
                return db.tblRimImages.Where(a => a.RimID == rimID).ToList();
            }
        }
        private void deleteImagesOfRim(int rimID)
        {
            using (var db = new CSCbelgiumDatabaseEntities())
            {
                db.tblRimImages.RemoveRange(db.tblRimImages.Where(a => a.RimID == rimID));
                db.SaveChanges();
            }
        }

        public void UpdateImage(tblRimImages image)
        {
            using (var db = new CSCbelgiumDatabaseEntities())
            {
                db.Entry(image).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void deleteRim(int rimID)
        {
            using (var db = new CSCbelgiumDatabaseEntities())
            {
                deleteImagesOfRim(rimID);
                tblRims rim = getRim(rimID);
                db.Entry(rim).State = EntityState.Deleted;
                db.SaveChanges();

            }
        }
        public bool isSold(tblRims rim)
        {
            using (var db = new CSCbelgiumDatabaseEntities())
            {
                if (rim.Sold == 1)
                {
                    return true;

                }
                else
                {
                    return false;
                }

            }
        }
        public void SellRim(tblRims rim, bool sold)
        {
            using (var db = new CSCbelgiumDatabaseEntities())
            {
                if (sold)
                {
                    rim.Sold = (byte)1;
                    db.Entry(rim).State = EntityState.Modified;
                    db.SaveChanges();

                }
                else
                {
                    rim.Sold = (byte)0;
                    db.Entry(rim).State = EntityState.Modified;
                    db.SaveChanges();
                }

            }
        }
        public void addRim(tblRims rim)
        {
            using (var db = new CSCbelgiumDatabaseEntities())
            {
                db.tblRims.Add(rim);
                db.SaveChanges();
            }
        }
        public void addRimImage(tblRimImages rimImage)
        {
            using (var db = new CSCbelgiumDatabaseEntities())
            {
                db.tblRimImages.Add(rimImage);
                db.SaveChanges();
            }
        }
    }
}
