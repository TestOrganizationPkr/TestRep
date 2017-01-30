using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic;
using Microsoft.Extensions.Logging;
using ToDoWebApp.Models;
using Domain.Models;
using System.Threading.Tasks;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ToDoWebApp.Controllers
{
    public class ToDoController : Controller
    {
        // GET: /<controller>/
        readonly IToDoBL _toDoService;
        readonly ILogger _logger;

        /// <summary>
        /// Constructor which accepts the businessService and loggerFactory as a parameter which is a dependency.
        /// </summary>
        /// <param name="businessService"></param>
        /// <param name="loggerFactory"></param>
        public ToDoController(IToDoBL businessService, ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ToDoController>();
            _toDoService = businessService;
        }


        /// <summary>
        /// This is the default page which will run when the application start
        /// In this we are loading all the todo's items and returning that todolist to view
        /// </summary>
        /// <returns>If this is success it will rediect to index page otherwise error page</returns>
        /// <summary>
        /// This will take you to the home page 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// This is to get all the todo's item list
        /// </summary>
        /// <returns>This will return list of todo's object</returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            string result = string.Empty;
            try
            {
                return Json(new { data = _toDoService.GetAll() });
            }
            catch (Exception ex)
            {
                _logger.LogError("HomeController Unable to consume GetAll:" + ex.Message + ex.StackTrace);
                result = "error";
            }
            finally
            {
                Dispose();
            }
            return Json(result);
        }
        /// <summary>
        /// This is to create a new todo item 
        /// </summary>
        /// <param name="form"></param>
        /// <returns>this will return a todo object if the creation is success otherwise it will return error</returns>
        [HttpPost]
        public IActionResult Create([FromBody]ToDoItem item)
        {
            string result = string.Empty;
            ToDoItem record = null;
            try
            {
                if (ModelState.IsValid)
                {
                    record = _toDoService.Add(item);
                    if (null != record)
                    {
                        return Json(new { data = record });
                    }
                }
                else
                {
                    result = "error";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("HomeController Unable to consume Create:" + ex.Message + ex.StackTrace);
                result = "error";
            }
            finally
            {
                Dispose();
            }
            return Json(result);
        }
        /// <summary>
        /// This is to update todo item 
        /// </summary>
        /// <param name="form"></param>
        /// <returns>If this is success it will return todo item otherwise it will return error</returns>
        [HttpPost]
        public IActionResult Update([FromBody]ToDoItem item)
        {
            string result = string.Empty;
            bool status = false;
            try
            {
                if (ModelState.IsValid && !string.IsNullOrEmpty(item.Id))
                {
                    status = _toDoService.Update(item);
                    if (!status)
                    {
                        _logger.LogError("ToDoController Create: Invalid data" + JsonConvert.SerializeObject(item));
                        result = "error";
                    }
                    else
                    {
                        return Json(new { data = item });
                    }
                }
                else
                {
                    _logger.LogError("ToDoController Create: Invalid data" + JsonConvert.SerializeObject(item));
                    result = "error";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("ToDoController Unable to consume Create:" + ex.Message + ex.StackTrace);
                result = "error";
            }
            finally
            {
                //This is to dispose the object
                Dispose();
            }
            return Json(result);
        }
        /// <summary>
        /// This is to delete the Item from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>This will return blank if the delete is success otherwise it will return error</returns> 
        [HttpPost]
        public IActionResult Delete(string id)
        {
            string result = string.Empty;
            try
            {
                _logger.LogInformation("HomeController Request Delete:" + id);
                bool status = _toDoService.Delete(id);
                if (!status)
                {
                    _logger.LogError("HomeController : Passed id is not present in the database");
                    result = "error";
                }
                _logger.LogInformation("HomeController Response Delete:" + id);
            }
            catch (Exception ex)
            {
                _logger.LogError("HomeController Unable to consume Delete:" + ex.Message + ex.StackTrace);
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

