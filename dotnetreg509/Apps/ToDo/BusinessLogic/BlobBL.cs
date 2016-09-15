using BlobStorage;
using System;
using System.IO;

namespace BusinessLogic
{
    public sealed class BlobBL : IBlobBL
    {
        /// <summary>
        /// This is to create an object for the Blob
        /// </summary>
        readonly IBlob _blob;

        /// <summary>
        /// Constructor which accepts the repository as a parameter which is a dependency.
        /// This dependency is configured in the UnityConfig file inside RegisterTypes function
        /// This is inside ServiceLocation folder
        /// </summary>
        /// <param name="repository"></param>
        public BlobBL(IBlob blob)
        {
            _blob = blob;
        }

        /// <summary>
        /// This is to delete a blob by it name 
        /// By default it will check in "mycontainer" if you don't pass container name
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="containerName"></param>
        public void DeleteBlob(string fileName, string containerName = "")
        {
            _blob.DeleteBlob(fileName, containerName);
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
            return _blob.GetBlob(fileName, containerName);
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
            _blob.SaveBlob(fileName, stream, containerName,access);
        }        
    }
}
