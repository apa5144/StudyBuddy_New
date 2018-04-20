using System.Collections.Generic;
using BLToolkit.DataAccess;
using StudyBuddy.Models;
using StudyBuddy.DAL.Interfaces;

namespace StudyBuddy.DAL.Services
{
    public abstract class CourseService : DataAccessor, ICourse
    {
        [SprocName("Course_GetAll")]
        public abstract List<Course> GetAll();

        [SprocName("Course_GetOne")]
        public abstract Course GetOne(int id);

        [SprocName("Course_Delete")]
        public abstract void Delete(int id);

        [SprocName("Course_Create")]
        public abstract Course Create(string subject, string numbers, string title);

        [SprocName("Course_Update")]
        public abstract Course Update(int id, string subject, string numbers , string title);
    }
}
