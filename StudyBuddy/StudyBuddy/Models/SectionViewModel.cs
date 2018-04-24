namespace StudyBuddy.Models
{
    public class SectionViewModel
    {
        public string Title { get; set; }
        public string Subject { get; set; }
        public string Section { get; set; }

        public SectionViewModel()
        {
            Title = string.Empty;
            Subject = string.Empty;
            Section = string.Empty;
        }
    }
}