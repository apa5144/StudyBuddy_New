using BLToolkit.DataAccess;
using StudyBuddy.Models;
using StudyBuddy.DAL.Interfaces;
using System.Collections.Generic;

namespace StudyBuddy.DAL.Services
{
    public abstract class NotificationService : DataAccessor, INotification
    {
        [SprocName("Notification_GetLatestFourNotificationsByGuid")]
        public abstract List<NotificationViewModel> GetLatestFourNotificationsByGuid(string guid);

        [SprocName("Notification_GetAllNotificationsByGuid")]
        public abstract List<NotificationViewModel> GetAllNotificationsByGuid(string guid);

        [SprocName("Notification_GetNotificationByGuidAndNotificationId")]
        public abstract NotificationViewModel GetNotificationByGuidAndNotificationId(string guid, long notificationId);

        [SprocName("Notification_DoesUnreadNotificationExistForGuid")]
        public abstract bool DoesUnreadNotificationExistForGuid(string guid);

        [SprocName("Notification_IsNotificationReadByGuidAndNotificationId")]
        public abstract bool IsNotificationReadByGuidAndNotificationId(string guid, long notificationId);

        [SprocName("Notification_MarkReadByGuidAndNotificationId")]
        public abstract void MarkReadByGuidAndNotificationId(string guid, long notificationId);

        [SprocName("Notification_MarkAllReadByGuid")]
        public abstract void MarkAllReadByGuid(string guid);

        [SprocName("Notification_AddNotificationByGuidAndTargetEmail")]
        public abstract void AddNotificationByGuidAndTargetEmail(string body, string guid, string targetEmail);

        [SprocName("Notification_AddSystemNotificationByTargetEmail")]
        public abstract void AddSystemNotificationByTargetEmail(string body, string targetEmail);

        [SprocName("Notification_AddSystemNotificationByTargetGuid")]
        public abstract void AddSystemNotificationByTargetGuid(string body, string guid);
    }
}
