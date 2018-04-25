using System.Collections.Generic;
using StudyBuddy.Models;

namespace StudyBuddy.DAL.Interfaces
{
    public interface ICourse
    {
        List<Course> GetAll();

        List<AddCourseViewModel> GetListOfCourses();

        List<AddCourseViewModel> GetByCriteriaAndGuid(string criteria, string guid);

        Course GetOne(int id);

        Course Create(string subject ,string number, string title);

        void Delete(int id);

        Course Update(int id, string subject, string number, string title);

    }
}
