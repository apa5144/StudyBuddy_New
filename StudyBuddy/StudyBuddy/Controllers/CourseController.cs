using StudyBuddy.Core;
using System.Web.Mvc;

namespace StudyBuddy.Controllers
{
    [Authorization(Roles = "2,3")]
    public class CourseController : BaseController
    {
        // GET: Course
        public ActionResult Index()
        {
            var model = UnitOfWork.Course.GetAll();
            return View(model);
        }
    }
}