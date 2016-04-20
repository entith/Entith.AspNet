using System;
using System.IO;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;

namespace Entith.AspNet.ModuledController
{
    public interface IControllerModule
    {
        void SetManager(ControllerModuleManager manager);

		void BeginExecute(RequestContext requestContext, AsyncCallback callback, object state, ControllerContext context);

		void BeginExecuteCore(AsyncCallback callback, object state, ControllerContext context);

		void CreateActionInvoker(ControllerContext context);

		void CreateTempDataProvider(ControllerContext context);

		void Dispose(bool disposing, ControllerContext context);

		void EndExecute(IAsyncResult asyncResult, ControllerContext context);

		void EndExecuteCore(IAsyncResult asyncResult, ControllerContext context);

		void ExecuteCore(ControllerContext context);

		void HandleUnknownAction(string actionName, ControllerContext context);

		void Initialize(RequestContext requestContext, ControllerContext context);

		void OnActionExecuted(ActionExecutedContext filterContext, ControllerContext context);

		void OnActionExecuting(ActionExecutingContext filterContext, ControllerContext context);

		void OnAuthentication(AuthenticationContext filterContext, ControllerContext context);

		void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext, ControllerContext context);

		void OnAuthorization(AuthorizationContext filterContext, ControllerContext context);

		void OnException(ExceptionContext filterContext, ControllerContext context);

		void OnResultExecuted(ResultExecutedContext filterContext, ControllerContext context);

		void OnResultExecuting(ResultExecutingContext filterContext, ControllerContext context);

		void Content(string content, string contentType, Encoding contentEncoding, ControllerContext context);

		void File(byte[] fileContents, string contentType, string fileDownloadName, ControllerContext context);

		void File(Stream fileStream, string contentType, string fileDownloadName, ControllerContext context);

		void File(string fileName, string contentType, string fileDownloadName, ControllerContext context);

		void HttpNotFound(string statusDescription, ControllerContext context);

		void JavaScript(string script, ControllerContext context);

		void Json(object data, string contentType, Encoding contentEncoding, ControllerContext context);

		void Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior, ControllerContext context);

		void PartialView(string viewName, object model, ControllerContext context);

		void Redirect(string url, ControllerContext context);

		void RedirectPermanent(string url, ControllerContext context);

		void RedirectToAction(string actionName, string controllerName, RouteValueDictionary routeValues, ControllerContext context);

		void RedirectToActionPermanent(string actionName, string controllerName, RouteValueDictionary routeValues, ControllerContext context);

		void RedirectToRoute(string routeName, RouteValueDictionary routeValues, ControllerContext context);

		void RedirectToRoutePermanent(string routeName, RouteValueDictionary routeValues, ControllerContext context);

		void View(IView view, object model, ControllerContext context);

		void View(string viewName, string masterName, object model, ControllerContext context);
    }
}