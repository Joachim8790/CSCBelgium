using CSCBelgium.DAO.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCBelgium.DAO
{
    public class tblColorsDAO
    {

        public ICollection<tblColors> getColors()
        {
            using (var db = new CSCbelgiumDatabaseEntities())
            {
                return db.tblColors.ToList();
            }
        }
        public tblColors getColor(int colorID)
        {
            using (var db = new CSCbelgiumDatabaseEntities())
            {
                return db.tblColors.Where(a => a.ColorID == colorID).FirstOrDefault();
            }
        }
        public void addColor(tblColors color)
        {
            using (var db = new CSCbelgiumDatabaseEntities())
            {
                db.tblColors.Add(color);
                db.SaveChanges();
            }
        }
        public void deleteColor(int colorid)
        {
            using (var db = new CSCbelgiumDatabaseEntities())
            {
                tblColors color = getColor(colorid);
                db.Entry(color).State = EntityState.Deleted;
                db.SaveChanges();

            }
        }
    }
}
