using System;
using Entith.AspNet.Domain;
using System.Collections.Generic;
using System.Linq;

namespace Entith.AspNet.ModuledController
{
    public static class DomainMessagesModuleControllerExtensions
    {
        public static void AddResultsMessages(this ModuledController controller, SaveChangesResults results)
        {
            MessageManager manager = controller.GetMessageManager();

            AddMessages(manager.AddDanger, results.Results.Where(r => r.ResultType == SaveChangesResultType.Error));
            AddMessages(manager.AddWarning, results.Results.Where(r => r.ResultType == SaveChangesResultType.Warning));
            AddMessages(manager.AddInfo, results.Results.Where(r => r.ResultType == SaveChangesResultType.Info));
        }

        private static void AddMessages(Action<string> addMessage, IEnumerable<SaveChangesResult> results)
        {
            foreach(var result in results)
            {
                addMessage(result.Message);
            }
        }
    }
}

