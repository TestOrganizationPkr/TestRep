using BusinessLogic;
using Domain.Models;
using Microsoft.Azure.Mobile.Server.Config;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using ToDoMobileApp.Util;

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
        public HttpResponseMessage Get()    
        {
            try
            {
                // Below is how you can log the information
                Logger.Information("ToDoController Request Get: Enter into the method");
                var items = _toDoBL.GetAll();
                string json = string.Empty;
                if (null != items)
                {
                    json = JsonConvert.SerializeObject(items);
                    var response = Request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StringContent(json, Encoding.UTF8, "application/json");
                    return response;

                }
                return Request.CreateResponse(HttpStatusCode.OK);

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
        public HttpResponseMessage Post(ToDoItem model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var item = _toDoBL.Add(model);
                    string json = string.Empty;
                    if (null != item)
                    {
                        json = JsonConvert.SerializeObject(item);
                        var response = this.Request.CreateResponse(HttpStatusCode.Created);
                        response.Content = new StringContent(json, Encoding.UTF8, "application/json");
                        return response;
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.BadRequest);
                    }
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
        public HttpResponseMessage Put(ToDoItem model)
        {
            try
            {
                if (ModelState.IsValid && model.Id > 0)
                {
                    var status = _toDoBL.Update(model);
                    if (status)
                    {
                        var message = Request.CreateResponse(HttpStatusCode.OK, "Updated Successfully");
                        return message;
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.BadRequest);
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
                }                
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
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                var status = _toDoBL.Delete(id);
                if (status)
                {
                    var message = Request.CreateResponse(HttpStatusCode.OK, "Deleted Successfully");
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
