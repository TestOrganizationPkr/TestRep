using BusinessLogic;
using System.Web.Mvc;
namespace WebApp.Controllers
{
    public class DefaultController : Controller
    {
        readonly IDefaultBL _defaultBL;
        /// <summary>
        /// Constructor which accepts the service as a parameter which is a dependency.
        /// This dependency is configured in the UnityConfig file inside RegisterTypes function
        /// This is in Service layer project inside ServiceLocation folder
        /// This is a file called UnityMvcActivator which is responsible to Start and Shutdown the DI 
        /// Start will called when the application start
        /// Shutdown will called when the application stop
        /// </summary>
        /// <param name="businessLogic"></param>
        public DefaultController(IDefaultBL businessLogic)
        {
            this._defaultBL = businessLogic;
        }
        // GET: Default
        public ActionResult Index()
        {
            return View();
        }
    }
}