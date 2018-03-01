﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSCBelgium.DAO;
using CSCBelgium.DAO.Model;
using System.Web;

namespace CSCBelgium.Service
{
    public class CSCBelgiumCloudService
    {
        private CSCBelgiumCloudStorage storage;
        public CSCBelgiumCloudService()
        {
            this.storage = new CSCBelgiumCloudStorage();
        }
        public void UploadImageAsBlob(tblImages image, HttpPostedFileBase file)
        {
            storage.UploadImageAsBlob(image, file);

        }
        public void DeleteCarImagesFromStorage(int carID)
        {
            storage.DeleteCarImagesFromStorage(carID);
        }
    }
}
