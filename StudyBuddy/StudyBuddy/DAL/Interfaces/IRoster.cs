using System.Collections.Generic;
using StudyBuddy.Models;

namespace StudyBuddy.DAL.Interfaces
{
    public interface IRoster
    {
        List<Roster> GetAll();

        Roster GetOne(int sectionId, string guid);

        Roster Create(int sectionId, string studentGuid, string sectionColor);

        void Delete(int sectionId, string guid);

        Roster Update(int sectionId, bool groupAvailability, string comment);

        List<RosterViewModel> GetByGuid(string guid);

        int GetTotalByGuid(string guid);

        List<RosterLastestFiveViewModel> GetLatestFiveBySectionId(int sectionId, int studentId);

        void UpdateSectionAvailabilityByGuid(int sectionId, string guid);

        List<ViewRosterViewModel> GetSectionRoster(int sectionId, string guid);

        SectionViewModel GetBySectionId(int sectionId);

        List<string> GetSectionColorByGuid(string guid);
    }
}
