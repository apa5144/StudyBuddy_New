using StudyBuddy.DAL.Common;
using System.Security.Claims;
using System.Security.Principal;

namespace StudyBuddy.Core
{
    public static class Helpers
    {
        private static readonly UnitOfWork UnitOfWork = new UnitOfWork();

        public static string GetGuid(this IPrincipal user)
        {
            var claim = ((ClaimsIdentity)user.Identity).FindFirst(ClaimTypes.NameIdentifier);
            return claim == null ? null : claim.Value;
        }
        public static string GetFullName(this IPrincipal user)
        {
            var claim = ((ClaimsIdentity)user.Identity).FindFirst(ClaimTypes.NameIdentifier);
            var student = UnitOfWork.Student.GetOne(claim.Value);
            return claim == null ? null : string.Format("{0} {1}", student.FirstName, student.LastName);
        }
        public static string GetEmail(this IPrincipal user)
        {
            var claim = ((ClaimsIdentity)user.Identity).FindFirst(ClaimTypes.NameIdentifier);
            var student = UnitOfWork.Student.GetOne(claim.Value);
            return claim == null ? null : student.Email;
        }
        public static string GetPicture(this IPrincipal user)
        {
            var claim = ((ClaimsIdentity)user.Identity).FindFirst(ClaimTypes.NameIdentifier);
            var student = UnitOfWork.Student.GetOne(claim.Value);

            if (student.ProfilePic == "" || student.ProfilePic == null)
                return "noprofilepicture.jpg";
            else
                return student.ProfilePic;
        }
    }
}