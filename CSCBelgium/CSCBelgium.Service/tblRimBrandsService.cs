using CSCBelgium.DAO;
using CSCBelgium.DAO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCBelgium.Service
{
    public class tblRimBrandsService
    {
        tblRimBrandsDAO dao = new tblRimBrandsDAO();
        public ICollection<tblRimBrands> getBrands()
        {
            return dao.getBrands();
        }
        public tblRimBrands getBrand(int brandID)
        {
            return dao.getBrand(brandID);
        }
        public void addBrand(tblRimBrands brand)
        {
            dao.addBrand(brand);
        }
        public void deleteBrand(int brandid)
        {
            dao.deleteBrand(brandid);
        }
    }
}
