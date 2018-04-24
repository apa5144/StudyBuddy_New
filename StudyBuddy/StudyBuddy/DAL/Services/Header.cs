using BLToolkit.DataAccess;
using StudyBuddy.Models;
using StudyBuddy.DAL.Interfaces;

namespace StudyBuddy.DAL.Services
{
    public abstract class HeaderService : DataAccessor, IHeader
    {
        [SprocName("Header_GetHeaderByGuid")]
        public abstract HeaderViewModel GetHeaderByGuid(string guid);
    }
}
