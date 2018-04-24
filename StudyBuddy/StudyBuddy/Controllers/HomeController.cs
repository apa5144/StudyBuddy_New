using System.Web.Mvc;
using StudyBuddy.Core;
using StudyBuddy.DAL.Common;
using StudyBuddy.Models;
using System;
using System.Security.Claims;

namespace StudyBuddy.Controllers
{
    [Authorization(Roles = "2,3")]
    public class HomeController : BaseController
    {
        [HttpGet]
        public ActionResult ViewSection(int sectionId)
        {
            var guid = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value;

            var doesSectionExistForStudent = UnitOfWork.Roster.GetOne(sectionId, guid);

            if (doesSectionExistForStudent == null)
            {
                Warning("Sorry, you don't have permission to view that section");
                return RedirectToAction("Index", "Home");
            }

            try
            {
                var model = UnitOfWork.Roster.GetSectionRoster(sectionId, guid);
                var sectionViewModel = UnitOfWork.Roster.GetBySectionId(sectionId);
                ViewBag.SectionTitle = sectionViewModel.Title;
                ViewBag.SectionSubject = sectionViewModel.Subject;
                ViewBag.Section = sectionViewModel.Section;

                return View(model);
            }
            catch (Exception ex)
            {
                Logger.Fatal(ex.Message);
                Danger("An error has occurred while getting section roster.");
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                var guid = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value;

                int coursesTotal = UnitOfWork.Roster.GetTotalByGuid(guid);

                if (coursesTotal == 0)
                {
                    Warning("Looks like you haven't added any courses, yet. Please do so now.");
                    return RedirectToAction("Add", "Home");
                }
                else
                {
                    var model = UnitOfWork.Roster.GetByGuid(guid);
                    return View(model);
                }

            }
            catch (Exception ex)
            {
                Logger.Fatal(ex.Message);
                Danger("Error grabing information.");
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public ActionResult Add()
        {
            try
            {
                var model = new AddCourseViewModel();
                return View(model);
            }
            catch (Exception ex)
            {
                Logger.Fatal(ex.Message);
                Danger("An error has occurred.");
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult Add(string[] courses, string[] sections)
        {
            if (string.IsNullOrWhiteSpace(courses[0]))
            {
                ModelState.AddModelError("Courses", "Course is required and cannot be empty.");
                return View();
            }
            if (string.IsNullOrWhiteSpace(sections[0]))
            {
                ModelState.AddModelError("Sections", "Section is required and cannot be empty.");
                return View();
            }

            try
            {
                var guid = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value;

                for (int i = 0; i < courses.Length; i++)
                {
                    string sectionColors = "#F49917,#6610f2,#343a40,#6f42c1,#1CAF9A,#23BF08,#dc3545,#5B93D3,#e83e8c,#f27510,#0866C6";
                    var currentSectionColors = UnitOfWork.Roster.GetSectionColorByGuid(guid);

                    var sectionColor = string.Empty;

                    if (currentSectionColors == null)
                        sectionColor = sectionColors.Split(',')[0];
                    else
                    {
                        foreach (var item in currentSectionColors)
                        {
                            sectionColors = sectionColors.Replace(item + ',', "");
                        }
                        sectionColor = sectionColors.Split(',')[0];
                    }

                    var subject = courses[i].Split(' ')[0];
                    var number = courses[i].Split(' ')[1];

                    int sectionId = UnitOfWork.Section.GetSectionIdByCourseSubjectAndNumber(subject, number, sections[i]);

                    UnitOfWork.Roster.Create(sectionId, guid, sectionColor);
                }

                Success("Successfully added courses.");
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                Logger.Fatal(ex.Message);
                Danger("An error has occurred while adding courses.");
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult DeleteSection(int sectionId)
        {
            var guid = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value;

            var doesSectionExist = UnitOfWork.Roster.GetOne(sectionId, guid);

            if (doesSectionExist == null)
                return RedirectToAction("Index", "Home");

            try
            {
                UnitOfWork.Roster.Delete(sectionId, guid);

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                Logger.Fatal(ex.Message);
                Danger("An error has occurred while removing course.");
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public JsonResult Poke(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return Json(new { success = false });

            var guid = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value;

            var student = UnitOfWork.Student.GetByGuid(guid);

            if (student == null)
                return Json(new { success = false });

            UnitOfWork.Notification.AddNotificationByGuidAndTargetEmail("<strong>" + student.FirstName + "</strong> has poked you. Looks like someone wants to study!", guid, email);
            return Json(new { success = true });
        }

        [HttpPost]
        public JsonResult UpdateAvailability(int sectionId)
        {
            var guid = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value;

            UnitOfWork.Roster.UpdateSectionAvailabilityByGuid(sectionId, guid);

            return Json(new { success = true });
        }

        public JsonResult GetCourseByCriteria(string criteria)
        {
            var model = UnitOfWork.Course.GetByCriteria(criteria);

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult FillSection(string section)
        {
            var subject = section.Split(' ')[0];
            var number = section.Split(' ')[1];

            var sections = UnitOfWork.Section.GetSectionByCourseSubjectAndNumber(subject, number);

            return Json(sections, JsonRequestBehavior.AllowGet);
        }
    }
}