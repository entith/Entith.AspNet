using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;

namespace Entith.AspNet.ModuledController
{
    public abstract class ControllerModule : IControllerModule
    {
        protected ControllerModuleManager _manager;

        public void SetManager(ControllerModuleManager manager)
        {
            _manager = manager;
        }

		public virtual void BeginExecute(RequestContext requestContext, AsyncCallback callback, object state, ControllerContext context)
		{
		}

		public virtual void BeginExecuteCore(AsyncCallback callback, object state, ControllerContext context)
		{
		}

		public virtual void Content(string content, string contentType, Encoding contentEncoding, ControllerContext context)
		{
		}

		public virtual void CreateActionInvoker(ControllerContext context)
		{
		}

		public virtual void CreateTempDataProvider(ControllerContext context)
		{
		}

		public virtual void Dispose(bool disposing, ControllerContext context)
		{
		}

		public virtual void EndExecute(IAsyncResult asyncResult, ControllerContext context)
		{
		}

		public virtual void EndExecuteCore(IAsyncResult asyncResult, ControllerContext context)
		{
		}

		public virtual void ExecuteCore(ControllerContext context)
		{
		}

		public virtual void File(Stream fileStream, string contentType, string fileDownloadName, ControllerContext context)
		{
		}

		public virtual void File(string fileName, string contentType, string fileDownloadName, ControllerContext context)
		{
		}

		public virtual void File(byte[] fileContents, string contentType, string fileDownloadName, ControllerContext context)
		{
		}

		public virtual void HandleUnknownAction(string actionName, ControllerContext context)
		{
		}

		public virtual void HttpNotFound(string statusDescription, ControllerContext context)
		{
		}

		public virtual void Initialize(RequestContext requestContext, ControllerContext context)
		{
		}

		public virtual void JavaScript(string script, ControllerContext context)
		{
		}

		public virtual void Json(object data, string contentType, Encoding contentEncoding, ControllerContext context)
		{
		}

		public virtual void Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior, ControllerContext context)
		{
		}

		public virtual void OnActionExecuted(ActionExecutedContext filterContext, ControllerContext context)
		{
		}

		public virtual void OnActionExecuting(ActionExecutingContext filterContext, ControllerContext context)
		{
		}

		public virtual void OnAuthentication(AuthenticationContext filterContext, ControllerContext context)
		{
		}

		public virtual void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext, ControllerContext context)
		{
		}

		public virtual void OnAuthorization(AuthorizationContext filterContext, ControllerContext context)
		{
		}

		public virtual void OnException(ExceptionContext filterContext, ControllerContext context)
		{
		}

		public virtual void OnResultExecuted(ResultExecutedContext filterContext, ControllerContext context)
		{
		}

		public virtual void OnResultExecuting(ResultExecutingContext filterContext, ControllerContext context)
		{
		}

		public virtual void PartialView(string viewName, object model, ControllerContext context)
		{
		}

		public virtual void Redirect(string url, ControllerContext context)
		{
		}

		public virtual void RedirectPermanent(string url, ControllerContext context)
		{
		}

		public virtual void RedirectToAction(string actionName, string controllerName, RouteValueDictionary routeValues, ControllerContext context)
		{
		}

		public virtual void RedirectToActionPermanent(string actionName, string controllerName, RouteValueDictionary routeValues, ControllerContext context)
		{
		}

		public virtual void RedirectToRoute(string routeName, RouteValueDictionary routeValues, ControllerContext context)
		{
		}

		public virtual void RedirectToRoutePermanent(string routeName, RouteValueDictionary routeValues, ControllerContext context)
		{
		}

		public virtual void View(IView view, object model, ControllerContext context)
		{
		}

		public virtual void View(string viewName, string masterName, object model, ControllerContext context)
		{
		}
    }
}
