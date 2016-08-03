using BusinessLogic;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace ToDoWebApp.Controllers
{
    public class BlobController : Controller
    {
        private readonly IBlobBL _blobService;
        private readonly string _defaultFileName = "profilepic";

        /// <summary>
        /// Constructor which accepts the business logic as a parameter which is a dependency.
        /// This dependency is configured in the UnityConfig file inside RegisterTypes function
        /// This is in Business logic layer project inside ServiceLocation folder
        /// This is a file called UnityMvcActivator which is responsible to Start and Shutdown the DI 
        /// Start will called when the application start
        /// Shutdown will called when the application stop
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
        public JsonResult FileUpload(HttpPostedFileBase file, string containerName = "")
        {
            string result = string.Empty;
            try
            {
                if (null != file)
                {
                    if (IsImage(file))
                    {
                        _blobService.SaveBlob(_defaultFileName, file.InputStream, containerName);
                    }
                    else
                    {
                        //If the file passed the wrong file except image then then we are return error as text
                        Logger.Error("Invalid Format Content type : " + file.ContentType + " fileName :" + file.FileName);
                        result = "error";
                    }
                }
                else
                {
                    Logger.Error("This request not contain any file.");
                }
            }
            catch (Exception ex)
            {
                Logger.Error("BlobController Unable to consume FileUpload:" + ex.Message + ex.StackTrace);
                result = "error";
            }
            finally
            {
                //This is to dispose the object
                Dispose();
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// This is to check the file format 
        /// </summary>
        /// <param name="postedFile"></param>
        /// <returns></returns>
        private bool IsImage(HttpPostedFileBase postedFile)
        {
            //-------------------------------------------
            //  Check the image mime types
            //-------------------------------------------
            if (postedFile.ContentType.ToLower() != "image/jpg" &&
                        postedFile.ContentType.ToLower() != "image/jpeg" &&
                        postedFile.ContentType.ToLower() != "image/pjpeg" &&
                        postedFile.ContentType.ToLower() != "image/gif" &&
                        postedFile.ContentType.ToLower() != "image/x-png" &&
                        postedFile.ContentType.ToLower() != "image/png")
            {
                return false;
            }

            //-------------------------------------------
            //  Check the image extension
            //-------------------------------------------
            if (Path.GetExtension(postedFile.FileName).ToLower() != ".jpg"
                && Path.GetExtension(postedFile.FileName).ToLower() != ".png"
                && Path.GetExtension(postedFile.FileName).ToLower() != ".gif"
                && Path.GetExtension(postedFile.FileName).ToLower() != ".jpeg")
            {
                return false;
            }

            //-------------------------------------------
            //  Attempt to read the file and check the first bytes
            //-------------------------------------------
            try
            {
                if (!postedFile.InputStream.CanRead)
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// This is to delete the file / profile pic
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult DeleteFile(string containerName = "")
        {
            string result = string.Empty;
            try
            {
                _blobService.DeleteBlob(_defaultFileName, containerName);
            }
            catch (Exception ex)
            {
                Logger.Error("BlobController Unable to consume DeleteFile:" + ex.Message + ex.StackTrace);
                result = "error";
            }
            finally
            {
                //This is to dispose the object
                Dispose();
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// This is to read the file after that it will return base 64 string if the file exist
        /// otherwise it will return blank string
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetFileName()
        {
            string result = string.Empty;
            try
            {
                result = _blobService.GetBlob(_defaultFileName);
            }
            catch (Exception ex)
            {
                Logger.Error("BlobController Unable to consume GetFileName:" + ex.Message + ex.StackTrace);
                result = "error";
            }
            finally
            {
                //This is to dispose the object
                Dispose();
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        
    }
}