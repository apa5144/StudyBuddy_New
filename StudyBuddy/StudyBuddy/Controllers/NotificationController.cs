using StudyBuddy.Core;
using System;
using System.Security.Claims;
using System.Web.Mvc;

namespace StudyBuddy.Controllers
{
    [Authorization(Roles = "2,3")]
    public class NotificationController : BaseController
    {
        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                var guid = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value;

                var model = UnitOfWork.Notification.GetAllNotificationsByGuid(guid);

                return View(model);
            }
            catch (Exception ex)
            {
                Logger.Fatal(ex.Message);
                Danger("An error has occurred while getting notifications.");
                return RedirectToAction("Index", "Home");
            }
        }

        //[HttpGet]
        //public ActionResult Details(long notificationId)
        //{
        //    if(notificationId < 0)
        //        return RedirectToAction("Index", "Home");

        //    try
        //    {
        //        var guid = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value;

        //        UnitOfWork.Notification.MarkReadByGuidAndNotificationId(guid, notificationId);

        //        var model = UnitOfWork.Notification.GetAllNotificationsByGuid(guid);

        //        return RedirectToAction("Index", "Notification");
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Fatal(ex.Message);
        //        Danger("An error has occurred while getting notification information.");
        //        return RedirectToAction("Index", "Home");
        //    }            
        //}

        [HttpPost]
        public JsonResult MarkAsRead(long notificationId)
        {
            var guid = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value;

            UnitOfWork.Notification.MarkReadByGuidAndNotificationId(guid, notificationId);

            return Json(new { success = true });
        }

        [HttpPost]
        public JsonResult MarkAllRead()
        {
            var guid = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value;

            UnitOfWork.Notification.MarkAllReadByGuid(guid);

            return Json(new { success = true });
        }
    }
}