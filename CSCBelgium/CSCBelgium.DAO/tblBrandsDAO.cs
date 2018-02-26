using CSCBelgium.DAO.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCBelgium.DAO
{
   public class tblBrandsDAO
    {
        public ICollection<tblBrands> getBrands()
        {
            using (var db = new CSCbelgiumDatabaseEntities())
            {
                return db.tblBrands.Include(a => a.tblCars).ToList();
            }
        }
        public tblBrands getBrand(int brandID)
        {
            using (var db = new CSCbelgiumDatabaseEntities())
            {
                return db.tblBrands.Where(a => a.BrandID == brandID).Include(a => a.tblCars).SingleOrDefault();
            }
        }
        public void addBrand(tblBrands brand)
        {
            using (var db = new CSCbelgiumDatabaseEntities())
            {
                db.tblBrands.Add(brand);
                db.SaveChanges();
            }
        }
        public void deleteBrand(int brandid)
        {
            using (var db = new CSCbelgiumDatabaseEntities())
            {
                tblBrands brand = getBrand(brandid);
                db.Entry(brand).State = EntityState.Deleted;
                db.SaveChanges();

            }
        }
    }
}
