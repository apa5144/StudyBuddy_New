using System.Collections.Generic;
using StudyBuddy.Models;

namespace StudyBuddy.DAL.Interfaces
{
    public interface ISection
    {
        List<Section> GetAll();

        Section GetOne(int id);

        Section Create(string section, string instructor);

        void Delete(int id);

        Section Update(int id ,string section, string instructor);

        List<string> GetSectionByCourseSubjectAndNumber(string subject, string number);

        int GetSectionIdByCourseSubjectAndNumber(string subject, string number, string section);
    }
}
