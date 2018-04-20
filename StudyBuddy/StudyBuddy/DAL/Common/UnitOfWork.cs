using System.Configuration;
using BLToolkit.Data;
using BLToolkit.DataAccess;
using StudyBuddy.DAL.Interfaces;
using StudyBuddy.DAL.Services;

namespace StudyBuddy.DAL.Common
{
    public class UnitOfWork
    {
        public IStudent Student { get; set; }
        public IRoster Roster { get; set; }
        public ISection Section { get; set; }
        public ICourse Course { get; set; }
        public UnitOfWork()
        {
            DbManager.DefaultConfiguration = ConfigurationManager.AppSettings["ActiveDatabase"];
    
            Student = DataAccessor.CreateInstance<StudentService>();
            Roster = DataAccessor.CreateInstance<RosterService>();
            Section = DataAccessor.CreateInstance<SectionService>();
            Course = DataAccessor.CreateInstance<CourseService>();
        }
    }
}
