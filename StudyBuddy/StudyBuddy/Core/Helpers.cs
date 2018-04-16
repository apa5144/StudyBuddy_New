using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Security.Claims;
using System.Security.Principal;

namespace StudyBuddy.Core
{
    public static class Helpers
    {
        public static string GetId(this IPrincipal user)
        {
            var claim = ((ClaimsIdentity)user.Identity).FindFirst(ClaimTypes.NameIdentifier);
            return claim == null ? null : claim.Value;
        }
        public static string GetFullName(this IPrincipal user)
        {
            var claim = ((ClaimsIdentity)user.Identity).FindFirst(ClaimTypes.Name);
            return claim == null ? null : claim.Value;
        }
        public static string GetEmail(this IPrincipal user)
        {
            var claim = ((ClaimsIdentity)user.Identity).FindFirst(ClaimTypes.Email);
            return claim == null ? null : claim.Value;
        }
        public static string GetPicture(this IPrincipal user)
        {
            var claim = ((ClaimsIdentity)user.Identity).FindFirst(ClaimTypes.UserData);

            if (claim.Value == "")
                return "noprofilepicture.jpg";
            else
                return claim.Value;
        }
    }
}