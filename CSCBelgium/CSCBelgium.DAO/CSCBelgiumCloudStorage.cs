using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure; // Namespace for CloudConfigurationManager
using Microsoft.WindowsAzure.Storage; // Namespace for CloudStorageAccount
using Microsoft.WindowsAzure.Storage.Blob; // Namespace for Blob storage types
using Microsoft.Azure;
using System.Web;
using System.IO;
using CSCBelgium.DAO.Model;


namespace CSCBelgium.DAO
{
    public class CSCBelgiumCloudStorage
    {
        private CloudBlobClient blobClient;
        private tblCarsDAO dao;
        public  CSCBelgiumCloudStorage()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            blobClient = storageAccount.CreateCloudBlobClient();
            this.dao = new tblCarsDAO();
        }

        public void DeleteCarImagesFromStorage(int carID)
        {
            CloudBlobContainer container = blobClient.GetContainerReference("filesystem");
            foreach (tblImages image in dao.GetImagesByCar(dao.getCar(carID)))
            {
                CloudBlockBlob blob = container.GetBlockBlobReference(image.ImagePath.ToString());
                blob.Delete();
            }
        }

        public void UploadImageAsBlob(tblImages image, HttpPostedFileBase file)
        {
            file.InputStream.Position = 0;
            CloudBlobContainer container = blobClient.GetContainerReference("filesystem");
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(image.ImagePath);
            blockBlob.Properties.ContentType = "image/jpg";
            MemoryStream target = new MemoryStream();
            file.InputStream.CopyTo(target);
            byte[] Image = target.ToArray();          
            blockBlob.UploadFromByteArray(Image, 0, Image.Length);





        }
    }
}
