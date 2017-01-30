using BusinessLogic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;


namespace ToDoWebApp.Controllers
{
    public class CacheController : Controller
    {
        readonly IToDoBL _toDoService;
        readonly ILogger _logger;

        /// <summary>
        /// Constructor which accepts the businessService and loggerFactory as a parameter which is a dependency.
        /// </summary>
        /// <param name="businessService"></param>
        /// <param name="loggerFactory"></param>
        public CacheController(IToDoBL businessService, ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ToDoController>();
            _toDoService = businessService;
        }

        /// <summary>
        /// This is to get the cache item count
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetItemCount()
        {
            string result = string.Empty;
            try
            {
                string item = _toDoService.GetItemCount();
                string itemCount = (string.IsNullOrEmpty(item)) ? "0" : item;
                return Json(itemCount);
            }
            catch (Exception ex)
            {
                _logger.LogError("CacheController Unable to consume GetItemCount:" + ex.Message + ex.StackTrace);
                result = "error";
            }
            finally
            {
                Dispose();
            }
            return Json(result);
        }

        /// <summary>
        /// This is to deletecache from redis
        /// </summary>
        /// <param name="id"></param>
        /// <returns>This will return blank if the delete is success otherwise it will return error</returns> 
        [HttpPost]
        public IActionResult DeleteCache()
        {
            string result = string.Empty;
            try
            {
                _toDoService.RemoveItem();
            }
            catch (Exception ex)
            {
                _logger.LogError("CacheController Unable to consume Delete:" + ex.Message + ex.StackTrace);
                result = "error";
            }
            finally
            {
                Dispose();
            }
            return Json(result);
        }

        private new void Dispose()
        {
            _toDoService.Dispose();
        }
    }
}
