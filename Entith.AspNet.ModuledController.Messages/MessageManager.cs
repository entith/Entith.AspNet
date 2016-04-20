using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Entith.AspNet.ModuledController
{
    public class MessageManager
    {
        private ICollection<string> _success;
        private ICollection<string> _info;
        private ICollection<string> _warning;
        private ICollection<string> _danger;

        private bool _hasAddedFromTempData;

        public MessageManager()
        {
            Clear();

            _hasAddedFromTempData = false;
        }

        public void Clear()
        {
            _success = new List<string>();
            _info = new List<string>();
            _warning = new List<string>();
            _danger = new List<string>();
        }

        public void AddSuccess(string message)
        {
            _success.Add(message);
        }

        public void AddInfo(string message)
        {
            _info.Add(message);
        }

        public void AddWarning(string message)
        {
            _warning.Add(message);
        }

        public void AddDanger(string message)
        {
            _danger.Add(message);
        }

        public IEnumerable<string> GetSuccess()
        {
            return _success.AsEnumerable();
        }

        public IEnumerable<string> GetInfo()
        {
            return _info.AsEnumerable();
        }

        public IEnumerable<string> GetWarning()
        {
            return _warning.AsEnumerable();
        }

        public IEnumerable<string> GetDanger()
        {
            return _danger.AsEnumerable();
        }

        internal void AddFromTempData(TempDataDictionary tempData)
        {
            if (_hasAddedFromTempData || tempData["MessageManager"] == null)
                return;

            MessageManager oldMessageManager = tempData["MessageManager"] as MessageManager;

            foreach (string success in oldMessageManager.GetSuccess())
                AddSuccess(success);
            foreach (string info in oldMessageManager.GetInfo())
                AddInfo(info);
            foreach (string warning in oldMessageManager.GetWarning())
                AddWarning(warning);
            foreach (string danger in oldMessageManager.GetDanger())
                AddDanger(danger);
            _hasAddedFromTempData = true;
        }
    }
}
