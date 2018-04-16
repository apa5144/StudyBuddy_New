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

        public UnitOfWork()
        {
            DbManager.DefaultConfiguration = ConfigurationManager.AppSettings["ActiveDatabase"];

            Student = DataAccessor.CreateInstance<StudentService>();
        }
    }
}
