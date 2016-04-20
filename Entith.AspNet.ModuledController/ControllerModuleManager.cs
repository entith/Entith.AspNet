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
    public class ControllerModuleManager
    {
        private IEnumerable<IControllerModule> _modules;

        public ControllerModuleManager(IEnumerable<IControllerModule> modules)
        {
            _modules = modules;

            foreach (var module in modules)
            {
                module.SetManager(this);
            }
        }

		public T GetModule<T>() where T : IControllerModule
		{
			return _modules.OfType<T>().FirstOrDefault();
		}

		internal void BeginExecute(System.Web.Routing.RequestContext requestContext, AsyncCallback callback, object state, ControllerContext context)
		{
			foreach (var module in _modules)
				module.BeginExecute(requestContext, callback, state, context);
		}
		internal void BeginExecuteCore(AsyncCallback callback, object state, ControllerContext context)
		{
			foreach (var module in _modules)
				module.BeginExecuteCore(callback, state, context);
		}
		internal void CreateActionInvoker(ControllerContext context)
		{
			foreach (var module in _modules)
				module.CreateActionInvoker(context);
		}
		internal void CreateTempDataProvider(ControllerContext context)
		{
			foreach (var module in _modules)
				module.CreateTempDataProvider(context);
		}
		internal void Dispose(bool disposing, ControllerContext context)
		{
			foreach (var module in _modules)
				module.Dispose(disposing, context);
		}
		internal void EndExecute(IAsyncResult asyncResult, ControllerContext context)
		{
			foreach (var module in _modules)
				module.EndExecute(asyncResult, context);
		}
		internal void EndExecuteCore(IAsyncResult asyncResult, ControllerContext context)
		{
			foreach (var module in _modules)
				module.EndExecuteCore(asyncResult, context);
		}
		internal void ExecuteCore(ControllerContext context)
		{
			foreach (var module in _modules)
				module.ExecuteCore(context);
		}
		internal void HandleUnknownAction(string actionName, ControllerContext context)
		{
			foreach (var module in _modules)
				module.HandleUnknownAction(actionName, context);
		}
		internal void Initialize(System.Web.Routing.RequestContext requestContext, ControllerContext context)
		{
			foreach (var module in _modules)
				module.Initialize(requestContext, context);
		}
		internal void OnActionExecuted(ActionExecutedContext filterContext, ControllerContext context)
		{
			foreach (var module in _modules)
				module.OnActionExecuted(filterContext, context);
		}
		internal void OnActionExecuting(ActionExecutingContext filterContext, ControllerContext context)
		{
			foreach (var module in _modules)
				module.OnActionExecuting(filterContext, context);
		}
		internal void OnAuthentication(AuthenticationContext filterContext, ControllerContext context)
		{
			foreach (var module in _modules)
				module.OnAuthentication(filterContext, context);
		}
		internal void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext, ControllerContext context)
		{
			foreach (var module in _modules)
				module.OnAuthenticationChallenge(filterContext, context);
		}
		internal void OnAuthorization(AuthorizationContext filterContext, ControllerContext context)
		{
			foreach (var module in _modules)
				module.OnAuthorization(filterContext, context);
		}
		internal void OnException(ExceptionContext filterContext, ControllerContext context)
		{
			foreach (var module in _modules)
				module.OnException(filterContext, context);
		}
		internal void OnResultExecuted(ResultExecutedContext filterContext, ControllerContext context)
		{
			foreach (var module in _modules)
				module.OnResultExecuted(filterContext, context);
		}
		internal void OnResultExecuting(ResultExecutingContext filterContext, ControllerContext context)
		{
			foreach (var module in _modules)
				module.OnResultExecuting(filterContext, context);
		}
		internal void Content(string content, string contentType, Encoding contentEncoding, ControllerContext context)
		{
			foreach (var module in _modules)
				module.Content(content, contentType, contentEncoding, context);
		}
		internal void File(byte[] fileContents, string contentType, string fileDownloadName, ControllerContext context)
		{
			foreach (var module in _modules)
				module.File(fileContents, contentType, fileDownloadName, context);
		}
		internal void File(Stream fileStream, string contentType, string fileDownloadName, ControllerContext context)
		{
			foreach (var module in _modules)
				module.File(fileStream, contentType, fileDownloadName, context);
		}
		internal void File(string fileName, string contentType, string fileDownloadName, ControllerContext context)
		{
			foreach (var module in _modules)
				module.File(fileName, contentType, fileDownloadName, context);
		}
		internal void HttpNotFound(string statusDescription, ControllerContext context)
		{
			foreach (var module in _modules)
				module.HttpNotFound(statusDescription, context);
		}
		internal void JavaScript(string script, ControllerContext context)
		{
			foreach (var module in _modules)
				module.JavaScript(script, context);
		}
		internal void Json(object data, string contentType, Encoding contentEncoding, ControllerContext context)
		{
			foreach (var module in _modules)
				module.Json(data, contentType, contentEncoding, context);
		}
		internal void Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior, ControllerContext context)
		{
			foreach (var module in _modules)
				module.Json(data, contentType, contentEncoding, behavior, context);
		}
		internal void PartialView(string viewName, object model, ControllerContext context)
		{
			foreach (var module in _modules)
				module.PartialView(viewName, model, context);
		}
		internal void Redirect(string url, ControllerContext context)
		{
			foreach (var module in _modules)
				module.Redirect(url, context);
		}
		internal void RedirectPermanent(string url, ControllerContext context)
		{
			foreach (var module in _modules)
				module.RedirectPermanent(url, context);
		}
		internal void RedirectToAction(string actionName, string controllerName, System.Web.Routing.RouteValueDictionary routeValues, ControllerContext context)
		{
			foreach (var module in _modules)
				module.RedirectToAction(actionName, controllerName, routeValues, context);
		}
		internal void RedirectToActionPermanent(string actionName, string controllerName, System.Web.Routing.RouteValueDictionary routeValues, ControllerContext context)
		{
			foreach (var module in _modules)
				module.RedirectToActionPermanent(actionName, controllerName, routeValues, context);
		}
		internal void RedirectToRoute(string routeName, System.Web.Routing.RouteValueDictionary routeValues, ControllerContext context)
		{
			foreach (var module in _modules)
				module.RedirectToRoute(routeName, routeValues, context);
		}
		internal void RedirectToRoutePermanent(string routeName, System.Web.Routing.RouteValueDictionary routeValues, ControllerContext context)
		{
			foreach (var module in _modules)
				module.RedirectToRoutePermanent(routeName, routeValues, context);
		}
		internal void View(IView view, object model, ControllerContext context)
		{
			foreach (var module in _modules)
				module.View(view, model, context);
		}
		internal void View(string viewName, string masterName, object model, ControllerContext context)
		{
			foreach (var module in _modules)
				module.View(viewName, masterName, model, context);
		}
    }
}
