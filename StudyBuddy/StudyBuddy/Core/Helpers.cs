using StudyBuddy.DAL.Common;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

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

        public static HtmlString DisplayForPhone(this HtmlHelper helper, string phone)
        {
            if (phone == null)
            {
                return new HtmlString(string.Empty);
            }
            string formatted = phone;
            if (phone.Length == 10)
            {
                formatted = $"({phone.Substring(0, 3)}) {phone.Substring(3, 3)}-{phone.Substring(6, 4)}";
            }
            else if (phone.Length == 7)
            {
                formatted = $"{phone.Substring(0, 3)}-{phone.Substring(3, 4)}";
            }
            string s = $"<a href='tel:{phone}'>{formatted}</a>";
            return new HtmlString(s);
        }
    }
}