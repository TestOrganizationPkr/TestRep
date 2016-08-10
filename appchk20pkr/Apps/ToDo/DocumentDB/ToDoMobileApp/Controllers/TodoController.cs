using System;
using System.Net;
using System.Text;
using BusinessLogic;
using Domain.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Web.Http;
using ToDoMobileApp.Util;
using System.Threading.Tasks;
using Microsoft.Azure.Mobile.Server.Config;

namespace ToDoMobileApp.Controllers
{
    // Use the MobileAppController attribute for each ApiController you want to use  
    // from your mobile clients 
    [MobileAppController]
    public class ToDoController : ApiController
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
        /// <param name="businessLogic"></param>
        public ToDoController(IToDoBL businessLogic)
        {
            this._toDoBL = businessLogic;
        }


        /// <summary>
        /// This is to get all the items from the database
        /// Set the below parameter while posting the form
        /// Set (Content-Type :- application/json)
        /// (ZUMO-API-VERSION :- 2.0.0)
        /// </summary>
        /// <returns>HttpResponseMessage</returns>
        [HttpGet]
        public async Task<HttpResponseMessage> Get()    
        {
            try
            {
                // Below is how you can log the information
                Logger.Information("ToDoController Request Get: Enter into the method");
                var items = await _toDoBL.GetAll();
                string json = string.Empty;
                if (null != items)
                {
                    json = JsonConvert.SerializeObject(items);
                    var response = this.Request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StringContent(json, Encoding.UTF8, "application/json");
                    return response;

                }
                Logger.Information("ToDoController Response Get:  Exit from the method");
                return Request.CreateResponse(HttpStatusCode.NotFound);

            }
            catch (Exception ex)
            {
                var message = Request.CreateResponse(HttpStatusCode.InternalServerError);
                Logger.Error("ToDoController Unable to consume Get:" + ex.Message + ex.StackTrace);
                return message;
            }
            finally
            {
                //This is to dispose the object
                Dispose();
            }
        }

        /// <summary>
        /// This is to add new item from the database
        /// Set the below parameter while posting the form
        /// Set (Content-Type :- application/json)
        /// (ZUMO-API-VERSION :- 2.0.0)
        /// </summary>
        /// <param name="model"></param>
        /// <returns>HttpResponseMessage</returns>
        [HttpPost]
        public async Task<HttpResponseMessage> Post(ToDoItem model)
        {
            try
            {
                var item = await _toDoBL.Add(model);
                string json = string.Empty;
                if (null != item)
                {
                    json = JsonConvert.SerializeObject(item);
                    var response = this.Request.CreateResponse(HttpStatusCode.Created);
                    response.Content = new StringContent(json, Encoding.UTF8, "application/json");
                    Logger.Information("ToDoController Response Create:" + JsonConvert.SerializeObject(model));
                    return response;
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
                }
            }
            catch (Exception ex)
            {
                Logger.Error("ToDoController Unable to consume Create:" + ex.Message + ex.StackTrace);
                var message = Request.CreateResponse(HttpStatusCode.InternalServerError);
                return message;
            }
            finally
            {
                //This is to dispose the object
                Dispose();
            }

        }

        /// <summary>
        /// This is to update the item from the database
        /// Set the below parameter while posting/put the form
        /// Set (Content-Type :- application/json)
        /// (ZUMO-API-VERSION :- 2.0.0)
        /// </summary>
        /// <param name="model"></param>
        /// <returns>HttpResponseMessage</returns>
        [HttpPut]
        public async Task<HttpResponseMessage> Put(ToDoItem model)
        {
            try
            {
                if (ModelState.IsValid && !String.IsNullOrEmpty(model.Id))
                {
                    Logger.Information("ToDoController Request Update:" + JsonConvert.SerializeObject(model));
                    var status = await _toDoBL.Update(model);
                    if (status)
                    {
                        Logger.Information("ToDoController Response Update:" + JsonConvert.SerializeObject(model));
                        var message = Request.CreateResponse(HttpStatusCode.Accepted, "Record has been updated");
                        return message;
                    }
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                Logger.Error("ToDoController Unable to consume Update:" + ex.Message + ex.StackTrace);
                var message = Request.CreateResponse(HttpStatusCode.InternalServerError);
                return message;
            }
            finally
            {
                //This is to dispose the object
                Dispose();
            }
        }

        /// <summary>
        /// This is to delete the item from the database
        /// Set the below parameter will posting/put the form
        /// Set (Content-Type :- application/json)
        /// (ZUMO-API-VERSION :- 2.0.0)
        /// </summary>
        /// <param name="id"></param>
        /// <returns>HttpResponseMessage</returns>
        [HttpDelete]
        public async Task<HttpResponseMessage> Delete(string id)
        {
            try
            {
                Logger.Information("ToDoController Request Delete:" + id);
                var status = await _toDoBL.Delete(id);
                if (status)
                {
                    Logger.Information("ToDoController Response Delete:" + id);
                    var message = Request.CreateResponse(HttpStatusCode.Accepted, "Record has been deleted");
                    return message;
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
            }
            catch (Exception ex)
            {
                Logger.Error("ToDoController Unable to consume Delete:" + ex.Message + ex.StackTrace);
                var message = Request.CreateResponse(HttpStatusCode.InternalServerError);
                return message;
            }
            finally
            {
                Dispose();
            }
        }

        /// <summary>
        /// This is to dispose / release the object
        /// </summary>
        private new void Dispose()
        {
            //This is to remove the object
            _toDoBL.Dispose();
        }
    }
}
