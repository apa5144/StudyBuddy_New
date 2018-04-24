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
        public abstract Roster GetOne(int section_Id);

        [SprocName("Roster_Delete")]
        public abstract void Delete(int section_Id);

        [SprocName("Roster_Create")]
        public abstract Roster Create(bool groupAvailability, string comment);

        [SprocName("Roster_Update")]
        public abstract Roster Update(int section_Id, bool groupAvailability, string comment);
    }
}
