using Domain.Models;
using Newtonsoft.Json;
using BusinessLogic;
using System;
using System.Web.Mvc;
using ToDoWebApp.Models;
using System.Threading.Tasks;
using ToDoWebApp.AppAnalytics;

namespace ToDoWebApp.Controllers
{
    public class ToDoController : Controller
    {
        readonly IToDoBL _toDoBL;

        /// <summary>
        /// Constructor which accepts the service as a parameter which is a dependency.
        /// This dependency is configured in the UnityConfig file inside RegisterTypes function
        /// This is in Service layer project inside ServiceLocation folder
        /// This is a file called UnityMvcActivator which is responsible to Start and Shutdown the DI 
        /// Start will called when the application start
        /// Shutdown will called when the application stop
        /// </summary>
        /// <param name="service"></param>
        public ToDoController(IToDoBL service)
        {
            _toDoBL = service;
        }

        /// <summary>
        /// This will take you to the home page 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            Analytics.TrackEvent("ToDoController:Index");
            return View();
        }

        /// <summary>
        /// This is the default page which will run when the application start
        /// In this we are loading all the todo's items and returning that todolist to view
        /// </summary>
        /// <returns>If this is success it will rediect to index page otherwise error page</returns>
        [HttpGet]
        public async Task<JsonResult> GetAll()
        {
            //Below is how you can log the information
            Logger.Information("ToDoController Index Enter into method");
            string result = string.Empty;
            try
            {
                return Json(new { data = await _toDoBL.GetAll() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger.Error("ToDoController Unable to consume Index:" + ex.Message + ex.StackTrace);
                result = "Error";
            }
            finally
            {
                //This is to dispose the object
                Dispose();
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// This is to create a new todo item 
        /// </summary>
        /// <param name="form"></param>
        /// <returns>If this is success it will rediect to index page otherwise error page</returns>
        [HttpPost]
        public async Task<JsonResult> Create(ToDoItem item)
        {
            string result = string.Empty;
            ToDoItem record = null;
            try
            {
                Analytics.TrackEvent("ToDoController:Create");
                if (ModelState.IsValid)
                {
                    record = await _toDoBL.Add(item);
                    if (null != record)
                    {
                        return Json(new { data = record }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    Logger.Error("ToDoController Create: Invalid data" + JsonConvert.SerializeObject(item));
                    result = "Error";
                }
            }
            catch (Exception ex)
            {
                Logger.Error("ToDoController Unable to consume Create:" + ex.Message + ex.StackTrace);
                result = "Error";
            }
            finally
            {
                //This is to dispose the object
                Dispose();
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// This is to update todo item 
        /// </summary>
        /// <param name="form"></param>
        /// <returns>If this is success it will rediect to index page otherwise error page</returns>
        [HttpPost]
        public async Task<JsonResult> Update(ToDoItem item)
        {
            string result = string.Empty;
            bool status = false;
            try
            {
                Analytics.TrackEvent("ToDoController:Update");
                if (ModelState.IsValid & !string.IsNullOrEmpty(item.Id))
                {
                    status = await _toDoBL.Update(item);
                    if (status)
                        return Json(new { data = item }, JsonRequestBehavior.AllowGet);
                    else result = "Error";
                }
                else
                {
                    Logger.Error("ToDoController Create: Invalid data" + JsonConvert.SerializeObject(item));
                    result = "Error";
                }
            }
            catch (Exception ex)
            {
                Logger.Error("ToDoController Unable to consume Create:" + ex.Message + ex.StackTrace);
                result = "Error";
            }
            finally
            {
                //This is to dispose the object
                Dispose();
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// This is to delete the todo item from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>If this is success it will rediect to index page otherwise error page</returns>
        [HttpPost]
        public async Task<JsonResult> Delete(string id)
        {
            string result = "";
            try
            {
                Analytics.TrackEvent("ToDoController:Delete");
                bool status = await _toDoBL.Delete(id);
                if (!status)
                {
                    Logger.Error("ToDoController : Delete : "+ id +" id is not present in the database");
                    result = "Error";
                }
            }
            catch (Exception ex)
            {
                Logger.Error("ToDoController Unable to consume Delete:" + ex.Message + ex.StackTrace);
                result = "Error";
            }
            finally
            {
                //This is to dispose the object
                Dispose();
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// This is to dispose / release the object
        /// </summary>
        public new void Dispose()
        {
            GC.SuppressFinalize(this);
            _toDoBL.Dispose();
        }
    }
}
