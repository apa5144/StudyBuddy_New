using StudyBuddy.Models;

namespace StudyBuddy.DAL.Interfaces
{
    public interface IHeader
    {
        HeaderViewModel GetHeaderByGuid(string guid);
    }
}
