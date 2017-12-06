using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSCBelgium.DAO.Model;
using System.Collections;
using System.Data.Entity;

namespace CSCBelgium.DAO
{
    public class tblPostsDAO
    {
        public ICollection<tblPosts> getAllPosts()
        {

            using (var db = new CSCbelgiumDatabaseEntities())
            {
               return  db.tblPosts.ToList();
            }
        }
        public void UpdatePost(tblPosts post)
        {
            using (var db = new CSCbelgiumDatabaseEntities())
            {
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
        public ICollection<tblPosts> get30LastPosts()
        {

            using (var db = new CSCbelgiumDatabaseEntities())
            {
                return db.tblPosts.OrderByDescending(a => a.PostDate).Take(30).ToList();
            }
        }
        public tblPosts getPost(int id)
        {
            using (var db = new CSCbelgiumDatabaseEntities())
            {
                return db.tblPosts.Where(a => a.PostID == id).FirstOrDefault();
            }
        }
        public void addNewPost(tblPosts newPost)
        {
            using (var db = new CSCbelgiumDatabaseEntities())
            {
                db.tblPosts.Add(newPost);
                db.SaveChanges();
            }
        }
        public void deletePost(int postID)
        {
            using (var db = new CSCbelgiumDatabaseEntities())
            {
                tblPosts post = getPost(postID);
                db.Entry(post).State = EntityState.Deleted;
                db.SaveChanges();
            }
        }
    }
}
