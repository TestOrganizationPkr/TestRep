using BusinessLogic;
using Microsoft.Azure.Mobile.Server.Config;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using ToDoMobileApp.Util;

namespace ToDoMobileApp.Controllers
{
    [MobileAppController]
    public class BlobController : ApiController
    {
        private readonly IBlobBL _blobService;
        private readonly string _defaultFileName = "profilepic";

        /// <summary>
        /// Constructor which accepts the service as a parameter which is a dependency.
        ///  This dependency is configured in the UnityConfigMobileApp file inside RegisterComponents function
        /// This is in Business Logic layer project inside ServiceLocation folder
        /// This is a file called Startup.Mobile inside that  UnityConfigMobileApp.RegisterComponents(config); which is responsible to Start the DI 
        /// </summary>
        /// <param name="businessLogic"></param>
        public BlobController(IBlobBL businessLogic)
        {
            _blobService = businessLogic;
        }
        
        /// <summary>
        /// This is to save the profile pic
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        public async System.Threading.Tasks.Task<HttpResponseMessage> FileUpload(string containerName = "")
        {
            var response = Request.CreateResponse(HttpStatusCode.UnsupportedMediaType);
            try
            {
                //This is to check the request content any file or not
                if (!Request.Content.IsMimeMultipartContent())
                {
                    //If the file passed the wrong file except image then then we are retuning unsupported media type error
                    Logger.Error("This request not contain any file.");
                    return Request.CreateResponse(HttpStatusCode.UnsupportedMediaType);
                }

                //This is to read the file 
                var filesReadToProvider = await Request.Content.ReadAsMultipartAsync();

                foreach (var stream in filesReadToProvider.Contents)
                {
                    //Here we are reading content type and file name from the request
                    string contentType = Convert.ToString(stream.Headers.ContentType).ToLower();
                    string fileName = Convert.ToString(stream.Headers.ContentDisposition.FileName).ToLower();
                    fileName = fileName.Replace("\"", "");
                    //This is to validate the file content type and extension 
                    if (!string.IsNullOrEmpty(contentType) && !string.IsNullOrEmpty(fileName) && IsImage(contentType, fileName))
                    {
                        //Here we are reading the file and then passing to BL to save the file
                        Stream inputsteam = await stream.ReadAsStreamAsync();
                        _blobService.SaveBlob(_defaultFileName, inputsteam, containerName);
                        response = Request.CreateResponse(HttpStatusCode.OK);
                        return response;
                    }
                    else
                    {
                        //If the file passed the wrong file except image then then we are retuning unsupported media type error
                        Logger.Error("Invalid Format Content type : "+ contentType + " fileName :" + fileName);
                        response = Request.CreateResponse(HttpStatusCode.UnsupportedMediaType);
                        return response;
                    }

                }
            }
            catch (Exception ex)
            {
                Logger.Error("BlobController Unable to consume FileUpload:" + ex.Message + ex.StackTrace);
                response = Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            finally
            {
                //This is to dispose the object
                Dispose();
            }
            return response;
        }

        /// <summary>
        /// This is to check the file format 
        /// </summary>
        /// <param name="postedFile"></param>
        /// <returns></returns>
        private bool IsImage(string contentType, string fileName)
        {
            //-------------------------------------------
            //  Check the image mime types
            //-------------------------------------------
            if (contentType.ToLower() != "image/jpg" &&
                        contentType.ToLower() != "image/jpeg" &&
                        contentType.ToLower() != "image/pjpeg" &&
                        contentType.ToLower() != "image/gif" &&
                        contentType.ToLower() != "image/x-png" &&
                        contentType.ToLower() != "image/png")
            {
                return false;
            }

            //-------------------------------------------
            //  Check the image extension
            //-------------------------------------------
            if (Path.GetExtension(fileName).ToLower() != ".jpg"
                && Path.GetExtension(fileName).ToLower() != ".png"
                && Path.GetExtension(fileName).ToLower() != ".gif"
                && Path.GetExtension(fileName).ToLower() != ".jpeg")
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// This is to delete the file / profile pic
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        public HttpResponseMessage DeleteFile(string containerName = "")
        {
            var response = Request.CreateResponse(HttpStatusCode.OK);
            try
            {
                _blobService.DeleteBlob(_defaultFileName, containerName);
            }
            catch (Exception ex)
            {
                Logger.Error("BlobController Unable to consume DeleteFile:" + ex.Message + ex.StackTrace);
                response = Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            finally
            {
                //This is to dispose the object
                Dispose();
            }
            return response;
        }

        /// <summary>
        /// This is to read the file after that it will return base 64 string if the file exist
        /// otherwise it will return blank string
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetFileName()
        {
            string result = string.Empty;
            var response = Request.CreateResponse(HttpStatusCode.OK);
            try
            {
                result = _blobService.GetBlob(_defaultFileName);
                response = Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new StringContent(result, Encoding.UTF8, "text/plain");
            }
            catch (Exception ex)
            {
                Logger.Error("BlobController Unable to consume GetFileName:" + ex.Message + ex.StackTrace);
                response = Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            finally
            {
                //This is to dispose the object
                Dispose();
            }
            return response;
        }
    }
}
