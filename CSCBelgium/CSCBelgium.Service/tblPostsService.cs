using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSCBelgium.DAO.Model;
using CSCBelgium.DAO;

namespace CSCBelgium.Service
{
    public class tblPostsService
    {
        public ICollection<tblPosts> getAllPosts()
        {
            tblPostsDAO dao = new tblPostsDAO();
            return dao.getAllPosts();
           
        }
        public tblPosts getPost(int id)
        {

            tblPostsDAO dao = new tblPostsDAO();
            return dao.getPost(id);
        }
        public void addNewPost(tblPosts newPost)
        {

            tblPostsDAO dao = new tblPostsDAO();
            dao.addNewPost(newPost);
        }
        public void deletePost(int postID)
        {

            tblPostsDAO dao = new tblPostsDAO();
            dao.deletePost(postID);
        }
        public ICollection<tblPosts> get30LastPosts()
        {

            tblPostsDAO dao = new tblPostsDAO();
            return  dao.get30LastPosts();
        }
        public void UpdatePost(tblPosts post)
        {
            tblPostsDAO dao = new tblPostsDAO();
            dao.UpdatePost(post);
        }
    }
}
