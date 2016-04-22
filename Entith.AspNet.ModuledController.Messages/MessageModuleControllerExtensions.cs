using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entith.AspNet.ModuledController
{
    public static class MessageModuleControllerExtensions
    {
        private static MessageControllerModule GetMessageModule(ModuledController controller)
        {
            MessageControllerModule module = controller.GetControllerModule<MessageControllerModule>();
            if (module == null)
                throw new Exception("MessageControllerModule missing from controller.");
            return module;
        }

        public static MessageManager GetMessageManager(this ModuledController controller)
        {
            return GetMessageModule(controller).GetManager();
        }

        public static void AddSuccess(this ModuledController controller, string message)
        {
            GetMessageManager(controller).AddSuccess(message);
        }

        public static void AddInfo(this ModuledController controller, string message)
        {
            GetMessageManager(controller).AddInfo(message);
        }

        public static void AddWarning(this ModuledController controller, string message)
        {
            GetMessageManager(controller).AddWarning(message);
        }

        public static void AddDanger(this ModuledController controller, string message)
        {
            GetMessageManager(controller).AddDanger(message);
        }

        public static void ClearMessages(this ModuledController controller)
        {
            GetMessageManager(controller).Clear();
        }
    }
}
