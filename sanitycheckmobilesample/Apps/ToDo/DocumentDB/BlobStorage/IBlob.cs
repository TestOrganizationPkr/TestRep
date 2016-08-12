using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;

namespace BlobStorage
{
    public interface IBlob
    {
        /// <summary>
        /// This is to delete a blob by it name 
        /// By default it will check in "mycontainer" if you don't pass container name
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="containerName"></param>
        void DeleteBlob(string fileName, string containerName = "");

        /// <summary>
        /// This is to get the blob url by its name and containerName
        /// By default it will check in "mycontainer" if you don't pass container name
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="containerName"></param>
        /// <returns>It will return a string URL of the blog is present otherwise empty string</returns>
        string GetBlob(string fileName, string containerName = "");

        /// <summary>
        /// This is to save a blob
        /// By default it will save in "mycontainer" if you don't pass container name
        /// If you pass the container name then if the container is not present it will create a new container
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="stream"></param>
        /// <param name="containerName"></param>
        void SaveBlob(string fileName, Stream stream, string containerName = "", int access = 1);
    }
}
