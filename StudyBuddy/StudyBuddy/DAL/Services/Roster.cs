using System.Collections.Generic;
using BLToolkit.DataAccess;
using StudyBuddy.Models;
using StudyBuddy.DAL.Interfaces;

namespace StudyBuddy.DAL.Services
{
    public abstract class RosterService : DataAccessor, IRoster
    {
        [SprocName("Roster_GetAll")]
        public abstract List<Roster> GetAll();

        [SprocName("Roster_GetOne")]
        public abstract Roster GetOne(int sectionId, string guid);

        [SprocName("Roster_Delete")]
        public abstract void Delete(int sectionId, string guid);

        [SprocName("Roster_Create")]
        public abstract Roster Create(int sectionId, string studentGuid, string sectionColor);

        [SprocName("Roster_Update")]
        public abstract Roster Update(int sectionId, bool groupAvailability, string comment);

        [SprocName("Roster_GetByGuid")]
        public abstract List<RosterViewModel> GetByGuid(string guid);

        [SprocName("Roster_GetTotalByGuid")]
        public abstract int GetTotalByGuid(string guid);

        [SprocName("Roster_GetLatestFiveBySectionId")]
        public abstract List<RosterLastestFiveViewModel> GetLatestFiveBySectionId(int sectionId, int studentId);

        [SprocName("Roster_UpdateSectionAvailabilityByGuid")]
        public abstract void UpdateSectionAvailabilityByGuid(int sectionId, string guid);

        [SprocName("Roster_GetSectionRoster")]
        public abstract List<ViewRosterViewModel> GetSectionRoster(int sectionId, string guid);

        [SprocName("Roster_GetBySectionId")]
        public abstract SectionViewModel GetBySectionId(int sectionId);

        [SprocName("Roster_GetSectionColorByGuid")]
        public abstract List<string> GetSectionColorByGuid(string guid);



    }
}
