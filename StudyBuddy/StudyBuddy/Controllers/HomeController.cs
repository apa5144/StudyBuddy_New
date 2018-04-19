using System.Web.Mvc;
using StudyBuddy.Core;
using StudyBuddy.DAL.Common;

namespace StudyBuddy.Controllers
{
    [Authorization(Roles = "1,2,3")]
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            var model = UnitOfWork.Student.GetAll();
            return View(model);
        }
    }
}