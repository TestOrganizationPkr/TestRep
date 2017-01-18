using BusinessLogic;
using System;
using System.Web.Mvc;
namespace ToDoWebApp.Controllers
{

    public class CacheController : Controller
    {
        readonly IToDoBL _toDoService;
        
        /// <summary>
        /// Constructor which accepts the service as a parameter which is a dependency.
        /// This dependency is configured in the UnityConfig file inside RegisterTypes function
        /// This is in Service layer project inside ServiceLocation folder
        /// This is a file called UnityMvcActivator which is responsible to Start and Shutdown the DI 
        /// Start will called when the application start
        /// Shutdown will called when the application stop
        /// </summary>
        /// <param name="service"></param>
        public CacheController(IToDoBL service)
        {
            this._toDoService = service;
        }

        /// <summary>
        /// This is to get the cache item count
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetItemCount()
        {
            string result = string.Empty;
            try
            {
                string item = _toDoService.GetItemCount();
                string itemCount = (string.IsNullOrEmpty(item)) ? "0" : item;
                return Json(itemCount, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger.Error("CacheController Unable to consume GetItemCount:" + ex.Message + ex.StackTrace);
                result = "error";
            }
            finally
            {
                Dispose();
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// This is to deletecache from redis
        /// </summary>
        /// <param name="id"></param>
        /// <returns>This will return blank if the delete is success otherwise it will return error</returns> 
        [HttpPost]
        public JsonResult DeleteCache()
        {
            string result = string.Empty;
            try
            {
                _toDoService.RemoveItem();
            }
            catch (Exception ex)
            {
                Logger.Error("CacheController Unable to consume Delete:" + ex.Message + ex.StackTrace);
                result = "error";
            }
            finally
            {
                Dispose();
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        private new void Dispose()
        {
            _toDoService.Dispose();
        }
    }
}