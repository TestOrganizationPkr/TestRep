using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.IO;

[assembly: CLSCompliant(true)]
namespace BlobStorage
{
    public class Blob : IBlob
    {
        // Create the blob client.
        private CloudBlobClient _blobClient = null;
        private CloudBlobContainer _container = null;
        private readonly string _defaultContainerName = "mycontainer";

        /// <summary>
        /// This is the Constructor which will read the connect string from web config file and create a connection
        /// Also if "mycontainer" is not present it will create a new container 
        /// </summary>
        public Blob()
        {
            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            // Create the blob client.
            _blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve a reference to a container.
            _container = _blobClient.GetContainerReference(_defaultContainerName);

            //_container.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Container });

            // Create the container if it doesn't already exist.
            _container.CreateIfNotExists();
        }

        /// <summary>
        /// This is to set the _container variable based on containerName passed
        /// </summary>
        /// <param name="containerName"></param>
        /// <param name="canCreate"></param>
        private void SetContainer(string containerName,bool canCreate = false, int access = 1)
        {
            // Retrieve a reference to a container.
            _container = _blobClient.GetContainerReference(containerName);
            
            if (canCreate)
            {
                BlobContainerPublicAccessType publicAccess = BlobContainerPublicAccessType.Container;
                switch (access)
                {
                    case 0: publicAccess = BlobContainerPublicAccessType.Off; break;
                    case 1: publicAccess = BlobContainerPublicAccessType.Container; break;
                    case 2: publicAccess = BlobContainerPublicAccessType.Blob; break;
                }
               // _container.SetPermissions(new BlobContainerPermissions { PublicAccess = publicAccess });

                // Create the container if it doesn't already exist.
                _container.CreateIfNotExists();
            }
        }

        /// <summary>
        /// This is to check file exist or not
        /// </summary>
        /// <param name="containerName"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private bool BlobExistsOnCloud(string containerName, string key)
        {
            var blob = _blobClient.GetContainerReference(containerName);
            try
            {
                //The Below line is to check container exist or not
                blob.FetchAttributes();
                return blob.GetBlockBlobReference(key).Exists();
            }
            catch 
            {
                return false;                
            }            
        }

        /// <summary>
        /// This is to delete a blob by it name 
        /// By default it will check in "mycontainer" if you don't pass container name
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="containerName"></param>
        public void DeleteBlob(string fileName, string containerName = "")
        {
            if (BlobExistsOnCloud((!string.IsNullOrEmpty(containerName) ? containerName : _defaultContainerName), fileName))
            {
                if (!string.IsNullOrEmpty(containerName))
                {
                    SetContainer(containerName);
                }
                // Retrieve reference to a blob named "filename".
                CloudBlockBlob blockBlob = _container.GetBlockBlobReference(fileName);

                // Delete the blob.
                blockBlob.Delete();
            }
        }

        /// <summary>
        /// This is to get the blob url by its name and containerName
        /// By default it will check in "mycontainer" if you don't pass container name
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="containerName"></param>
        /// <returns>It will return a string URL of the blog is present otherwise empty string</returns>
        public string GetBlob(string fileName, string containerName = "")
        {
            if (BlobExistsOnCloud((!string.IsNullOrEmpty(containerName) ? containerName : _defaultContainerName), fileName))
            {
                if (!string.IsNullOrEmpty(containerName))
                {
                    SetContainer(containerName);
                }
                // Retrieve reference to a blob named "filename".
                CloudBlockBlob blockBlob = _container.GetBlockBlobReference(fileName);
                
                string imageBase64URL = "";
                using (var memoryStream = new MemoryStream())
                {
                    blockBlob.DownloadToStream(memoryStream);
                    byte[] imageByteData = memoryStream.ToArray();
                    string imageBase64Data = Convert.ToBase64String(imageByteData);
                    imageBase64URL = string.Format("data:image/png;base64,{0}", imageBase64Data);
                }

                return imageBase64URL;
            }
            return string.Empty;
        }

        /// <summary>
        /// This is to save a blob
        /// By default it will save in "mycontainer" if you don't pass container name
        /// If you pass the container name then if the container is not present it will create a new container
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="stream"></param>
        /// <param name="containerName"></param>
        public void SaveBlob(string fileName, Stream stream, string containerName = "", int access = 1)
        {
            if (!string.IsNullOrEmpty(containerName))
            {
                SetContainer(containerName, true, access);
            }
            // Retrieve reference to a blob named "filename".
            CloudBlockBlob blockBlob = _container.GetBlockBlobReference(fileName);
            // Create or overwrite the "filename" blob with contents from a local file.
            blockBlob.UploadFromStream(stream);
        }

    }
}
