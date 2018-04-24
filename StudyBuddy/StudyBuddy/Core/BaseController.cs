using System.Web.Mvc;
using StudyBuddy.DAL.Common;
using NLog;

namespace StudyBuddy.Core
{
    public class BaseController : Controller
    {
        private static UnitOfWork _unitOfWork;

        public static UnitOfWork UnitOfWork { get { return _unitOfWork ?? (_unitOfWork = new UnitOfWork()); } }
        public static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public void Success(string message, bool dismissable = false)
        {
            AddAlert(AlertStyle.Success, message);
        }

        public void Information(string message, bool dismissable = false)
        {
            AddAlert(AlertStyle.Information, message);
        }

        public void Warning(string message, bool dismissable = false)
        {
            AddAlert(AlertStyle.Warning, message);
        }

        public void Danger(string message, bool dismissable = false)
        {
            AddAlert(AlertStyle.Danger, message);
        }

        private void AddAlert(AlertStyle alertStyle, string message)
        {
            var alert = TempData.ContainsKey(Alert.TempDataKey)
                ? (Alert)TempData[Alert.TempDataKey]
                : new Alert();

            alert.AlertStyle = alertStyle;
            alert.Message = message;

            TempData[Alert.TempDataKey] = alert;
        }
    }
}
