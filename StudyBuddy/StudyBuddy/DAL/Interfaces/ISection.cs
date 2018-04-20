using System.Collections.Generic;
using StudyBuddy.Models;

namespace StudyBuddy.DAL.Interfaces
{
    public interface ISection
    {
        List<Section> GetAll();

        Section GetOne(int Id);

        Section Create(string section, string instructor);

        void Delete(int section_Id);

        Section Update(int section_Id ,string instructor);
    }
}
