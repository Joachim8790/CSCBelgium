using CSCBelgium.DAO;
using CSCBelgium.DAO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCBelgium.Service
{
    public class tblBrandsService
    {
        tblBrandsDAO dao = new tblBrandsDAO();
        public ICollection<tblBrands> getBrands()
        {
            return dao.getBrands();
        }
        public tblBrands getBrand(int brandID)
        {
            return dao.getBrand(brandID);
        }
        public void addBrand(tblBrands brand)
        {
            dao.addBrand(brand);
        }
        public void deleteBrand(int brandid)
        {
            dao.deleteBrand(brandid);
        }
    }
}
