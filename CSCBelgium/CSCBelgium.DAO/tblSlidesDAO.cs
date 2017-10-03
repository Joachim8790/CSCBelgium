using CSCBelgium.DAO.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCBelgium.DAO
{
    public class tblSlidesDAO
    {
        public ICollection<tblSlides> getSlides()
        {
            using (var db = new CSCbelgiumDatabaseEntities())
            {
                return db.tblSlides.ToList();

            }
        }
        public tblSlides getSlide(int slideID)
        {
            using (var db = new CSCbelgiumDatabaseEntities())
            {
                return db.tblSlides.Where(a => a.SlideID == slideID).FirstOrDefault();

            }

        }
        public void deleteSlide(int slideID)
        {
            using (var db = new CSCbelgiumDatabaseEntities())
            {
                tblSlides slide = getSlide(slideID);
                db.Entry(slide).State = EntityState.Deleted;
                db.SaveChanges();

            }
        }
        public void addSlide(tblSlides slide)
        {
            using (var db = new CSCbelgiumDatabaseEntities())
            {
                db.tblSlides.Add(slide);
                db.SaveChanges();

            }
        }
        public void updateAlignment(tblSlides slide,int ddlID)
        {
            using (var db = new CSCbelgiumDatabaseEntities())
            {
                switch (ddlID)
                {
                    case 0:
                            slide.CaptionAlignment = "Links";
                        break;
                    case 1:
                            slide.CaptionAlignment = "Midden";
                        break;
                    case 2:
                            slide.CaptionAlignment = "Rechts";
                        break;

                }
                db.Entry(slide).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}
