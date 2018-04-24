using System.Collections.Generic;
using StudyBuddy.Models;

namespace StudyBuddy.DAL.Interfaces
{
    public interface IRoster
    {
        List<Roster> GetAll();

        Roster GetOne(int section_Id);

        Roster Create( bool groupAvailabiltiy, string comment);

        void Delete(int section_Id);

        Roster Update(int section_Id, bool groupAvailability, string comment);
    }
}
