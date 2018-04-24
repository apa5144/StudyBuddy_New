namespace StudyBuddy.Models
{
    public class Roster
    {
        public int SectionId { get; set; }

        public int StudentId { get; set; }

        public bool GroupAvailabiltiy { get; set; }

        public string Comment { get; set; }

        public Roster()
        {
            SectionId = default(int);
            StudentId = default(int);
            Comment = string.Empty;
            GroupAvailabiltiy = default(bool);
        }
    }
}