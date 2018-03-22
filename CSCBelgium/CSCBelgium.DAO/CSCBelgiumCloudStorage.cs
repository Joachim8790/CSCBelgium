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
            foreach (IListBlobItem blob in container.GetDirectoryReference("Cars/Car"+carID).ListBlobs(true))
            {
                if (blob.GetType() == typeof(CloudBlob) || blob.GetType().BaseType == typeof(CloudBlob))
                {
                    ((CloudBlob)blob).DeleteIfExists();
                }
            }
        }
        public void DeleteSlideImagesFromStorage(int slideID)
        {
            CloudBlobContainer container = blobClient.GetContainerReference("filesystem");
            foreach (IListBlobItem blob in container.GetDirectoryReference("Slides/Slide" + slideID).ListBlobs(true))
            {
                if (blob.GetType() == typeof(CloudBlob) || blob.GetType().BaseType == typeof(CloudBlob))
                {
                    ((CloudBlob)blob).DeleteIfExists();
                }
            }
        }
        public void DeleteRimImagesFromStorage(int rimID)
        {
            CloudBlobContainer container = blobClient.GetContainerReference("filesystem");
            foreach (IListBlobItem blob in container.GetDirectoryReference("Rims/Rim" + rimID).ListBlobs(true))
            {
                if (blob.GetType() == typeof(CloudBlob) || blob.GetType().BaseType == typeof(CloudBlob))
                {
                    ((CloudBlob)blob).DeleteIfExists();
                }
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
        public void UploadImageAsBlob(tblSlides image, HttpPostedFileBase file)
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
        public void UploadImageAsBlob(tblRimImages image, HttpPostedFileBase file)
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
