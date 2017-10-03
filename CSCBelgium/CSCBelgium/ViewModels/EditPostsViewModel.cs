using CSCBelgium.DAO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSCBelgium.ViewModels
{
    public class EditPostsViewModel
    {
        public ICollection<tblPosts> Posts { get; set; }
    }
}