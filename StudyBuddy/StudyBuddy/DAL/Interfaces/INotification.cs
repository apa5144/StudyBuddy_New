using System.Collections.Generic;
using StudyBuddy.Models;

namespace StudyBuddy.DAL.Interfaces
{
    public interface INotification
    {
        List<NotificationViewModel> GetLatestFourNotificationsByGuid(string guid);

        List<NotificationViewModel> GetAllNotificationsByGuid(string guid);

        NotificationViewModel GetNotificationByGuidAndNotificationId(string guid, long notificationId);

        bool DoesUnreadNotificationExistForGuid(string guid);

        bool IsNotificationReadByGuidAndNotificationId(string guid, long notificationId);

        void MarkReadByGuidAndNotificationId(string guid, long notificationId);

        void MarkAllReadByGuid(string guid);

        void AddNotificationByGuidAndTargetEmail(string body, string guid, string targetEmail);

        void AddSystemNotificationByTargetEmail(string body, string targetEmail);

        void AddSystemNotificationByTargetGuid(string body, string guid);
    }
}
