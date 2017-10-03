using CSCBelgium.DAO.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCBelgium.DAO
{
    public class tblRimBrandsDAO
    {
        public ICollection<tblRimBrands> getBrands()
        {
            using (var db = new CSCbelgiumDatabaseEntities())
            {
                return db.tblRimBrands.ToList();
            }
        }
        public tblRimBrands getBrand(int brandID)
        {
            using (var db = new CSCbelgiumDatabaseEntities())
            {
                return db.tblRimBrands.Where(a => a.RimBrandID == brandID).FirstOrDefault();
            }
        }
        public void addBrand(tblRimBrands brand)
        {
            using (var db = new CSCbelgiumDatabaseEntities())
            {
                db.tblRimBrands.Add(brand);
                db.SaveChanges();
            }
        }
        public void deleteBrand(int brandid)
        {
            using (var db = new CSCbelgiumDatabaseEntities())
            {
                tblRimBrands brand = getBrand(brandid);
                db.Entry(brand).State = EntityState.Deleted;
                db.SaveChanges();

            }
        }
    }
}
