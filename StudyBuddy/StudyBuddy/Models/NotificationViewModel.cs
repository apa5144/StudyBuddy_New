using System;

namespace StudyBuddy.Models
{
    public class NotificationViewModel
    {
        public long Id { get; set; }
        public string Body { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsRead { get; set; }
        public string Picture { get; set; }

        public NotificationViewModel()
        {
            Id = default(long);
            Body = string.Empty;
            DateCreated = Convert.ToDateTime("1/1/1753 00:00:00");
            IsRead = default(bool);
            Picture = string.Empty;
        }
    }
}