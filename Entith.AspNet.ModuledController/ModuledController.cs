using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Security.Principal;
using System.Web.Mvc.Async;
using System.Web.Mvc.Filters;

namespace Entith.AspNet.ModuledController
{
    public abstract class ModuledController : Controller
    {
        protected ControllerModuleManager _moduleManager;

        public ModuledController(ControllerModuleManager moduleManager)
        {
            _moduleManager = moduleManager;
        }

        public T GetControllerModule<T>() where T: IControllerModule
        {
            return _moduleManager.GetModule<T>();
        }

        //
        // Summary:
        //     Begins execution of the specified request context
        //
        // Parameters:
        //   requestContext:
        //     The request context.
        //
        //   callback:
        //     The asynchronous callback.
        //
        //   state:
        //     The state.
        //
        // Returns:
        //     Returns an IAsyncController instance.
        protected override IAsyncResult BeginExecute(System.Web.Routing.RequestContext requestContext, AsyncCallback callback, object state)
        {
            _moduleManager.BeginExecute(requestContext, callback, state, ControllerContext);
            return base.BeginExecute(requestContext, callback, state);
        }
        //
        // Summary:
        //     Begins to invoke the action in the current controller context.
        //
        // Parameters:
        //   callback:
        //     The callback.
        //
        //   state:
        //     The state.
        //
        // Returns:
        //     Returns an IAsyncController instance.
        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            _moduleManager.BeginExecuteCore(callback, state, ControllerContext);
            return base.BeginExecuteCore(callback, state);
        }
        //
        // Summary:
        //     Creates an action invoker.
        //
        // Returns:
        //     An action invoker.
        protected override IActionInvoker CreateActionInvoker()
        {
            _moduleManager.CreateActionInvoker(ControllerContext);
            return base.CreateActionInvoker();
        }
        //
        // Summary:
        //     Creates a temporary data provider.
        //
        // Returns:
        //     A temporary data provider.
        protected override ITempDataProvider CreateTempDataProvider()
        {
            _moduleManager.CreateTempDataProvider(ControllerContext);
            return base.CreateTempDataProvider();
        }
        //
        // Summary:
        //     Releases unmanaged resources and optionally releases managed resources.
        //
        // Parameters:
        //   disposing:
        //     true to release both managed and unmanaged resources; false to release only unmanaged
        //     resources.
        protected override void Dispose(bool disposing)
        {
            _moduleManager.Dispose(disposing, ControllerContext);
            base.Dispose(disposing);
        }
        //
        // Summary:
        //     Ends the invocation of the action in the current controller context.
        //
        // Parameters:
        //   asyncResult:
        //     The asynchronous result.
        protected override void EndExecute(IAsyncResult asyncResult)
        {
            _moduleManager.EndExecute(asyncResult, ControllerContext);
            base.EndExecute(asyncResult);
        }
        //
        // Summary:
        //     Ends the execute core.
        //
        // Parameters:
        //   asyncResult:
        //     The asynchronous result.
        protected override void EndExecuteCore(IAsyncResult asyncResult)
        {
            _moduleManager.EndExecuteCore(asyncResult, ControllerContext);
            base.EndExecuteCore(asyncResult);
        }
        //
        // Summary:
        //     Invokes the action in the current controller context.
        protected override void ExecuteCore()
        {
            _moduleManager.ExecuteCore(ControllerContext);
            base.ExecuteCore();
        }
        //
        // Summary:
        //     Called when a request matches this controller, but no method with the specified
        //     action name is found in the controller.
        //
        // Parameters:
        //   actionName:
        //     The name of the attempted action.
        protected override void HandleUnknownAction(string actionName)
        {
            _moduleManager.HandleUnknownAction(actionName, ControllerContext);
            base.HandleUnknownAction(actionName);
        }
        //
        // Summary:
        //     Initializes data that might not be available when the constructor is called.
        //
        // Parameters:
        //   requestContext:
        //     The HTTP context and route data.
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            _moduleManager.Initialize(requestContext, ControllerContext);
            base.Initialize(requestContext);
        }
        //
        // Summary:
        //     Called after the action method is invoked.
        //
        // Parameters:
        //   filterContext:
        //     Information about the current request and action.
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            _moduleManager.OnActionExecuted(filterContext, ControllerContext);
            base.OnActionExecuted(filterContext);
        }
        //
        // Summary:
        //     Called before the action method is invoked.
        //
        // Parameters:
        //   filterContext:
        //     Information about the current request and action.
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _moduleManager.OnActionExecuting(filterContext, ControllerContext);
            base.OnActionExecuting(filterContext);
        }
        //
        // Summary:
        //     Called when authorization occurs.
        //
        // Parameters:
        //   filterContext:
        //     Information about the current request and action.
        protected override void OnAuthentication(AuthenticationContext filterContext)
        {
            _moduleManager.OnAuthentication(filterContext, ControllerContext);
            base.OnAuthentication(filterContext);
        }
        //
        // Summary:
        //     Called when authorization challenge occurs.
        //
        // Parameters:
        //   filterContext:
        //     Information about the current request and action.
        protected override void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            _moduleManager.OnAuthenticationChallenge(filterContext, ControllerContext);
            base.OnAuthenticationChallenge(filterContext);
        }
        //
        // Summary:
        //     Called when authorization occurs.
        //
        // Parameters:
        //   filterContext:
        //     Information about the current request and action.
        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            _moduleManager.OnAuthorization(filterContext, ControllerContext);
            base.OnAuthorization(filterContext);
        }
        //
        // Summary:
        //     Called when an unhandled exception occurs in the action.
        //
        // Parameters:
        //   filterContext:
        //     Information about the current request and action.
        protected override void OnException(ExceptionContext filterContext)
        {
            _moduleManager.OnException(filterContext, ControllerContext);
            base.OnException(filterContext);
        }
        //
        // Summary:
        //     Called after the action result that is returned by an action method is executed.
        //
        // Parameters:
        //   filterContext:
        //     Information about the current request and action result.
        protected override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            _moduleManager.OnResultExecuted(filterContext, ControllerContext);
            base.OnResultExecuted(filterContext);
        }
        //
        // Summary:
        //     Called before the action result that is returned by an action method is executed.
        //
        // Parameters:
        //   filterContext:
        //     Information about the current request and action result.
        protected override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            _moduleManager.OnResultExecuting(filterContext, ControllerContext);
            base.OnResultExecuting(filterContext);
        }
        //
        // Summary:
        //     Creates a content result object by using a string, the content type, and content
        //     encoding.
        //
        // Parameters:
        //   content:
        //     The content to write to the response.
        //
        //   contentType:
        //     The content type (MIME type).
        //
        //   contentEncoding:
        //     The content encoding.
        //
        // Returns:
        //     The content result instance.
        protected override ContentResult Content(string content, string contentType, Encoding contentEncoding)
        {
            _moduleManager.Content(content, contentType, contentEncoding, ControllerContext);
            return base.Content(content, contentType, contentEncoding);
        }
        //
        // Summary:
        //     Creates a FileContentResult object by using the file contents, content type,
        //     and the destination file name.
        //
        // Parameters:
        //   fileContents:
        //     The binary content to send to the response.
        //
        //   contentType:
        //     The content type (MIME type).
        //
        //   fileDownloadName:
        //     The file name to use in the file-download dialog box that is displayed in the
        //     browser.
        //
        // Returns:
        //     The file-content result object.
        protected override FileContentResult File(byte[] fileContents, string contentType, string fileDownloadName)
        {
            _moduleManager.File(fileContents, contentType, fileDownloadName, ControllerContext);
            return base.File(fileContents, contentType, fileDownloadName);
        }
        //
        // Summary:
        //     Creates a FileStreamResult object using the Stream object, the content type,
        //     and the target file name.
        //
        // Parameters:
        //   fileStream:
        //     The stream to send to the response.
        //
        //   contentType:
        //     The content type (MIME type)
        //
        //   fileDownloadName:
        //     The file name to use in the file-download dialog box that is displayed in the
        //     browser.
        //
        // Returns:
        //     The file-stream result object.
        protected override FileStreamResult File(Stream fileStream, string contentType, string fileDownloadName)
        {
            _moduleManager.File(fileStream, contentType, fileDownloadName, ControllerContext);
            return base.File(fileStream, contentType, fileDownloadName);
        }
        //
        // Summary:
        //     Creates a FilePathResult object by using the file name, the content type, and
        //     the file download name.
        //
        // Parameters:
        //   fileName:
        //     The path of the file to send to the response.
        //
        //   contentType:
        //     The content type (MIME type).
        //
        //   fileDownloadName:
        //     The file name to use in the file-download dialog box that is displayed in the
        //     browser.
        //
        // Returns:
        //     The file-stream result object.
        protected override FilePathResult File(string fileName, string contentType, string fileDownloadName)
        {
            _moduleManager.File(fileName, contentType, fileDownloadName, ControllerContext);
            return base.File(fileName, contentType, fileDownloadName);
        }
        //
        // Summary:
        //     Returns an instance of the System.Web.Mvc.HttpNotFoundResult class.
        //
        // Parameters:
        //   statusDescription:
        //     The status description.
        //
        // Returns:
        //     An instance of the System.Web.Mvc.HttpNotFoundResult class.
        protected override HttpNotFoundResult HttpNotFound(string statusDescription)
        {
            _moduleManager.HttpNotFound(statusDescription, ControllerContext);
            return base.HttpNotFound(statusDescription);
        }
        //
        // Summary:
        //     Creates a System.Web.Mvc.JavaScriptResult object.
        //
        // Parameters:
        //   script:
        //     The JavaScript code to run on the client
        //
        // Returns:
        //     The System.Web.Mvc.JavaScriptResult object that writes the script to the response.
        protected override JavaScriptResult JavaScript(string script)
        {
            _moduleManager.JavaScript(script, ControllerContext);
            return base.JavaScript(script);
        }
        //
        // Summary:
        //     Creates a System.Web.Mvc.JsonResult object that serializes the specified object
        //     to JavaScript Object Notation (JSON) format.
        //
        // Parameters:
        //   data:
        //     The JavaScript object graph to serialize.
        //
        //   contentType:
        //     The content type (MIME type).
        //
        //   contentEncoding:
        //     The content encoding.
        //
        // Returns:
        //     The JSON result object that serializes the specified object to JSON format.
        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding)
        {
            _moduleManager.Json(data, contentType, contentEncoding, ControllerContext);
            return base.Json(data, contentType, contentEncoding);
        }
        //
        // Summary:
        //     Creates a System.Web.Mvc.JsonResult object that serializes the specified object
        //     to JavaScript Object Notation (JSON) format using the content type, content encoding,
        //     and the JSON request behavior.
        //
        // Parameters:
        //   data:
        //     The JavaScript object graph to serialize.
        //
        //   contentType:
        //     The content type (MIME type).
        //
        //   contentEncoding:
        //     The content encoding.
        //
        //   behavior:
        //     The JSON request behavior
        //
        // Returns:
        //     The result object that serializes the specified object to JSON format.
        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            _moduleManager.Json(data, contentType, contentEncoding, behavior, ControllerContext);
            return base.Json(data, contentType, contentEncoding, behavior);
        }
        //
        // Summary:
        //     Creates a System.Web.Mvc.PartialViewResult object that renders a partial view,
        //     by using the specified view name and model.
        //
        // Parameters:
        //   viewName:
        //     The name of the view that is rendered to the response.
        //
        //   model:
        //     The model that is rendered by the partial view
        //
        // Returns:
        //     A partial-view result object.
        protected override PartialViewResult PartialView(string viewName, object model)
        {
            _moduleManager.PartialView(viewName, model, ControllerContext);
            return base.PartialView(viewName, model);
        }
        //
        // Summary:
        //     Creates a System.Web.Mvc.RedirectResult object that redirects to the specified
        //     URL.
        //
        // Parameters:
        //   url:
        //     The URL to redirect to.
        //
        // Returns:
        //     The redirect result object.
        protected override RedirectResult Redirect(string url)
        {
            _moduleManager.Redirect(url, ControllerContext);
            return base.Redirect(url);
        }
        //
        // Summary:
        //     Returns an instance of the System.Web.Mvc.RedirectResult class with the Permanent
        //     property set to true.
        //
        // Parameters:
        //   url:
        //     The URL to redirect to.
        //
        // Returns:
        //     An instance of the System.Web.Mvc.RedirectResult class with the Permanent property
        //     set to true.
        protected override RedirectResult RedirectPermanent(string url)
        {
            _moduleManager.RedirectPermanent(url, ControllerContext);
            return base.RedirectPermanent(url);
        }
        //
        // Summary:
        //     Redirects to the specified action using the action name, controller name, and
        //     route values.
        //
        // Parameters:
        //   actionName:
        //     The name of the action.
        //
        //   controllerName:
        //     The name of the controller.
        //
        //   routeValues:
        //     The parameters for a route.
        //
        // Returns:
        //     The redirect result object.
        protected override RedirectToRouteResult RedirectToAction(string actionName, string controllerName, System.Web.Routing.RouteValueDictionary routeValues)
        {
            _moduleManager.RedirectToAction(actionName, controllerName, routeValues, ControllerContext);
            return base.RedirectToAction(actionName, controllerName, routeValues);
        }
        //
        // Summary:
        //     Returns an instance of the System.Web.Mvc.RedirectResult class with the Permanent
        //     property set to true using the specified action name, controller name, and route
        //     values.
        //
        // Parameters:
        //   actionName:
        //     The action name.
        //
        //   controllerName:
        //     The controller name.
        //
        //   routeValues:
        //     The route values.
        //
        // Returns:
        //     An instance of the System.Web.Mvc.RedirectResult class with the Permanent property
        //     set to true using the specified action name, controller name, and route values.
        protected override RedirectToRouteResult RedirectToActionPermanent(string actionName, string controllerName, System.Web.Routing.RouteValueDictionary routeValues)
        {
            _moduleManager.RedirectToActionPermanent(actionName, controllerName, routeValues, ControllerContext);
            return base.RedirectToActionPermanent(actionName, controllerName, routeValues);
        }
        //
        // Summary:
        //     Redirects to the specified route using the route name and route dictionary.
        //
        // Parameters:
        //   routeName:
        //     The name of the route.
        //
        //   routeValues:
        //     The parameters for a route.
        //
        // Returns:
        //     The redirect-to-route result object.
        protected override RedirectToRouteResult RedirectToRoute(string routeName, System.Web.Routing.RouteValueDictionary routeValues)
        {
            _moduleManager.RedirectToRoute(routeName, routeValues, ControllerContext);
            return base.RedirectToRoute( routeName, routeValues);
        }
        //
        // Summary:
        //     Returns an instance of the RedirectResult class with the Permanent property set
        //     to true using the specified route name and route values.
        //
        // Parameters:
        //   routeName:
        //     The route name.
        //
        //   routeValues:
        //     The route values.
        //
        // Returns:
        //     An instance of the RedirectResult class with the Permanent property set to true.
        protected override RedirectToRouteResult RedirectToRoutePermanent(string routeName, System.Web.Routing.RouteValueDictionary routeValues)
        {
            _moduleManager.RedirectToRoutePermanent(routeName, routeValues, ControllerContext);
            return base.RedirectToRoutePermanent(routeName, routeValues);
        }
        //
        // Summary:
        //     Creates a System.Web.Mvc.ViewResult object that renders the specified System.Web.Mvc.IView
        //     object.
        //
        // Parameters:
        //   view:
        //     The view that is rendered to the response.
        //
        //   model:
        //     The model that is rendered by the view.
        //
        // Returns:
        //     The view result.
        protected override ViewResult View(IView view, object model)
        {
            _moduleManager.View(view, model, ControllerContext);
            return base.View(view, model);
        }
        //
        // Summary:
        //     Creates a System.Web.Mvc.ViewResult object using the view name, master-page name,
        //     and model that renders a view.
        //
        // Parameters:
        //   viewName:
        //     The name of the view that is rendered to the response.
        //
        //   masterName:
        //     The name of the master page or template to use when the view is rendered.
        //
        //   model:
        //     The model that is rendered by the view.
        //
        // Returns:
        //     The view result.
        protected override ViewResult View(string viewName, string masterName, object model)
        {
            _moduleManager.View(viewName, masterName, model, ControllerContext);
            return base.View(viewName, masterName, model);
        }
    }
}
