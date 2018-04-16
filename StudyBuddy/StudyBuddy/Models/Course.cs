namespace StudyBuddy.Models
{
    public class Course
    {
        public int Id { get; set; }

        public string Subject { get; set; }

        public string Number { get; set; }

        public string Title { get; set; }

        public Course()
        {
            Id = default(int);
            Subject = string.Empty;
            Number = string.Empty;
            Title = string.Empty;
        }
    }
}