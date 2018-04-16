namespace StudyBuddy.Models
{
    public class Section
    {
        public int Id { get; set; }

        public string Sect { get; set; }

        public string Instructor { get; set; }

        public int Course_Id { get; set; }

        public Section()
        {
            Id = default(int);
            Sect = string.Empty;
            Instructor = string.Empty;
            Course_Id = default(int);
        }
    }
}   