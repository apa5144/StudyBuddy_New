namespace StudyBuddy.Models
{
    public class Roster
    {
        public int Section_Id { get; set; }

        public int Student_Id { get; set; }

        public bool GroupAvailabiltiy { get; set; }

        public string Comment { get; set; }

        public Roster()
        {
            Section_Id = default(int);
            Student_Id = default(int);
            Comment = string.Empty;
            GroupAvailabiltiy = default(bool);
        }
    }
}