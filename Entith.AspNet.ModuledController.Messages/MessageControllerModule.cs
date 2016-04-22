using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace Entith.AspNet.ModuledController
{
    public class MessageControllerModule : ControllerModule
    {
        private int _actionCounter;
        private MessageManager _messageManager;

        public MessageControllerModule(MessageManager messageManager)
        {
            _messageManager = messageManager;
            _actionCounter = 0;
        }

        internal MessageManager GetManager()
        {
            return _messageManager;
        }

        private void MessageManagerToTempData(ControllerContext context)
        {
            context.Controller.TempData["MessageManager"] = _messageManager;
        }

        public override void OnActionExecuting(System.Web.Mvc.ActionExecutingContext filterContext, ControllerContext context)
        {
            if(_actionCounter++ == 0)
                _messageManager.AddFromTempData(context.Controller.TempData);
        }

        public override void OnResultExecuting(System.Web.Mvc.ResultExecutingContext filterContext, ControllerContext context)
        {
            context.Controller.ViewBag.MessageManager = _messageManager;
        }

        public override void RedirectToAction(string actionName, string controllerName, RouteValueDictionary routeValues, ControllerContext context)
        {
            MessageManagerToTempData(context);
        }

        public override void RedirectToActionPermanent(string actionName, string controllerName, RouteValueDictionary routeValues, ControllerContext context)
        {
            MessageManagerToTempData(context);
        }

        public override void RedirectToRoute(string routeName, RouteValueDictionary routeValues, ControllerContext context)
        {
            MessageManagerToTempData(context);
        }

        public override void RedirectToRoutePermanent(string routeName, RouteValueDictionary routeValues, ControllerContext context)
        {
            MessageManagerToTempData(context);
        }
    }
}
