using BusinessLogic;
using Microsoft.Azure.Mobile.Server.Config;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using ToDoMobileApp.Util;

namespace ToDoMobileApp.Controllers
{
    [MobileAppController]
    public class CacheController : ApiController
    {
        readonly IToDoBL _toDoBL;

        /// <summary>
        /// Constructor which accepts the service as a parameter which is a dependency.
        ///  This dependency is configured in the UnityConfigMobileApp file inside RegisterComponents function
        /// This is in Service layer project inside ServiceLocation folder
        /// This is a file called Startup.Mobile inside that  UnityConfigMobileApp.RegisterComponents(config); which is responsible to Start the DI 
        /// </summary>
        /// <param name="businessLogic"></param>
        public CacheController(IToDoBL businessLogic)
        {
            this._toDoBL = businessLogic;
        }

        /// <summary>
        /// This is to get all the itemcount from redis 
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
                var items = _toDoBL.GetItemCount();
                var response = Request.CreateResponse(HttpStatusCode.OK);
                if (null != items)
                {
                    response.Content = new StringContent(items, Encoding.UTF8, "application/json");
                }
                else
                {
                    response.Content = new StringContent("0", Encoding.UTF8, "application/json");
                }
                return response;
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
        /// This is to delete the itemcount from redis
        /// Set the below parameter will deleting the data
        /// Set (Content-Type :- application/json)
        /// (ZUMO-API-VERSION :- 2.0.0)
        /// </summary>
        /// <param name="id"></param>
        /// <returns>HttpResponseMessage</returns>
        [HttpDelete]
        public HttpResponseMessage Delete()
        {
            try
            {
                _toDoBL.RemoveItem();
                return Request.CreateResponse(HttpStatusCode.Accepted);
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
    }
}
