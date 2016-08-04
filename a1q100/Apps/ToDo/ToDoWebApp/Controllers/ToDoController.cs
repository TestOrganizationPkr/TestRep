using BusinessLogic;
using Domain.Models;
using Newtonsoft.Json;
using System;
using System.Web.Mvc;
using ToDoWebApp.AppAnalytics;

namespace ToDoWebApp.Controllers
{
    public class ToDoController : Controller
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
        public ToDoController(IToDoBL service)
        {
            this._toDoService = service;
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
        /// This is to get all the todo's item list
        /// </summary>
        /// <returns>This will return list of todo's object</returns>
        [HttpGet]
        public JsonResult GetAll()
        {
            string result = string.Empty;
            try
            {
                return Json(new { data = _toDoService.GetAll() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger.Error("HomeController Unable to consume GetAll:" + ex.Message + ex.StackTrace);
                result = "error";
            }
            finally
            {
                Dispose();
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// This is to create a new todo item 
        /// </summary>
        /// <param name="form"></param>
        /// <returns>this will return a todo object if the creation is success otherwise it will return error</returns>
        [HttpPost]
        public JsonResult Create(ToDoItem item)
        {
            Analytics.TrackEvent("ToDoController:Create");
            string result = string.Empty;
            ToDoItem record = null;
            try
            {
                if (ModelState.IsValid)
                {
                    record = _toDoService.Add(item);
                    if (null != record)
                    {
                        return Json(new { data = record }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    result = "error";
                }
            }
            catch (Exception ex)
            {
                Logger.Error("HomeController Unable to consume Create:" + ex.Message + ex.StackTrace);
                result = "error";
            }
            finally
            {
                Dispose();
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// This is to update todo item 
        /// </summary>
        /// <param name="form"></param>
        /// <returns>If this is success it will return todo item otherwise it will return error</returns>
        [HttpPost]
        public JsonResult Update(ToDoItem item)
        {
            Analytics.TrackEvent("ToDoController:Update");
            string result = string.Empty;
            bool status = false;
            try
            {
                if (ModelState.IsValid && item.Id > 0)
                {
                    status = _toDoService.Update(item);
                    if (!status)
                    {
                        Logger.Error("ToDoController Create: Invalid data" + JsonConvert.SerializeObject(item));
                        result = "error";
                    }
                    else
                    {
                        return Json(new { data = item }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    Logger.Error("ToDoController Create: Invalid data" + JsonConvert.SerializeObject(item));
                    result = "error";
                }
            }
            catch (Exception ex)
            {
                Logger.Error("ToDoController Unable to consume Create:" + ex.Message + ex.StackTrace);
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
        /// This is to delete the Item from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>This will return blank if the delete is success otherwise it will return error</returns> 
        [HttpPost]
        public JsonResult Delete(int id)
        {
            Analytics.TrackEvent("ToDoController:Delete");
            string result = string.Empty;
            try
            {
                Logger.Information("HomeController Request Delete:" + id);
                bool status = _toDoService.Delete(id);
                if (!status)
                {
                    Logger.Error("HomeController : Passed id is not present in the database");
                    result = "error";
                }
                Logger.Information("HomeController Response Delete:" + id);
            }
            catch (Exception ex)
            {
                Logger.Error("HomeController Unable to consume Delete:" + ex.Message + ex.StackTrace);
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
