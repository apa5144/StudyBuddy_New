using StudyBuddy.DAL.Common;
using StudyBuddy.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

namespace StudyBuddy.Core
{
    public static class Helpers
    {
        private static readonly UnitOfWork UnitOfWork = new UnitOfWork();

        public static HeaderViewModel GetHeader(this IPrincipal user)
        {
            var guid = ((ClaimsIdentity)user.Identity).FindFirst(ClaimTypes.NameIdentifier).Value;
            var header = UnitOfWork.Header.GetHeaderByGuid(guid);

            return header;
        }

        public static List<NotificationViewModel> GetNotifications(this IPrincipal user)
        {
            var guid = ((ClaimsIdentity)user.Identity).FindFirst(ClaimTypes.NameIdentifier).Value;
            var notifications = UnitOfWork.Notification.GetLatestFourNotificationsByGuid(guid);

            return notifications;
        }

        public static bool DoesAnyUnreadNotificationExist(this IPrincipal user)
        {
            var guid = ((ClaimsIdentity)user.Identity).FindFirst(ClaimTypes.NameIdentifier).Value;

            return UnitOfWork.Notification.DoesUnreadNotificationExistForGuid(guid);
        }

        public static bool IsSpecificNotificationRead(this IPrincipal user, long notificationId)
        {
            var guid = ((ClaimsIdentity)user.Identity).FindFirst(ClaimTypes.NameIdentifier).Value;

            return UnitOfWork.Notification.IsNotificationReadByGuidAndNotificationId(guid, notificationId);
        }

        public static HtmlString DisplayForPhone(this HtmlHelper helper, string phone)
        {
            if (phone == null)
            {
                return new HtmlString(string.Empty);
            }
            string formatted = phone;
            if (phone.Length == 10)
            {
                formatted = $"({phone.Substring(0, 3)}) {phone.Substring(3, 3)}-{phone.Substring(6, 4)}";
            }
            else if (phone.Length == 7)
            {
                formatted = $"{phone.Substring(0, 3)}-{phone.Substring(3, 4)}";
            }
            string s = $"<a href='tel:{phone}'>{formatted}</a>";
            return new HtmlString(s);
        }

        public static List<RosterLastestFiveViewModel> GetLatestFive(int sectionId, int studentId)
        {
            var latest = UnitOfWork.Roster.GetLatestFiveBySectionId(sectionId, studentId);

            return latest;
        }
    }
}