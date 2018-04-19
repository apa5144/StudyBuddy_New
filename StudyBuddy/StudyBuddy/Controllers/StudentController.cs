using StudyBuddy.Core;
using StudyBuddy.Models;
using System;
using System.IO;
using System.Security.Claims;
using System.Security.Principal;
using System.Text.RegularExpressions;
using System.Web.Helpers;
using System.Web.Mvc;

namespace StudyBuddy.Controllers
{
    [Authorization(Roles = "1,2,3")]
    public class StudentController : BaseController
    {
        public Mail _mail = new Mail();

        // GET: Student/EditProfile
        public ActionResult EditProfile(string guid)
        {
            try
            {
                var model = UnitOfWork.Student.GetOne(guid);
                return View(model);
            }
            catch (Exception ex)
            {
                Logger.Fatal(ex.Message);
                Danger("Error grabing information.");
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile(Student model)
        {
            try
            {
                if (Request.Files.Count > 0)
                {
                    var file = Request.Files[0];

                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);

                        if (fileName != null)
                        {
                            var regex = @"\.(gif|jpg|jpeg|tiff|png)$";
                            var match = Regex.Match(fileName, regex, RegexOptions.IgnoreCase);

                            if (match.Success)
                            {
                                WebImage img = new WebImage(file.InputStream);
                                if (img.Width > 600)
                                {
                                    img.Resize(600, 600, true);
                                    img.Save(Path.Combine(Server.MapPath("~/Content/UserProfilePictures/"), fileName));
                                }
                                else
                                {
                                    img.Save(Path.Combine(Server.MapPath("~/Content/UserProfilePictures/"), fileName));
                                }                                
                            }
                            else
                            {
                                Warning("Profile pictures can only be in format of common picture types.");
                                return View(model);
                            }
                        }
                        model.ProfilePic = fileName;
                    }
                }

                UnitOfWork.Student.Update(model.Id, model.FirstName, model.LastName, model.PhoneNumber, model.Availability, model.ProfilePic);
                Success("Information successfully changed.");
                return View(model);
            }
            catch (Exception ex)
            {
                Logger.Fatal(ex.Message);
                Danger("Error updating information.");
                return RedirectToAction("EditProfile", "Student", model.Id);
            }
        }

        // GET: Student/EditProfile
        public ActionResult EditLoginInformation(string guid)
        {
            try
            {
                var model = UnitOfWork.Student.GetOne(guid);
                model.Password = null;
                model.ConfirmPassword = null;
                return View(model);
            }
            catch (Exception ex)
            {
                Logger.Fatal(ex.Message);
                Danger("Error grabing information.");
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditLoginInformation(Student model)
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                Logger.Fatal(ex.Message);
                Danger("Error changing login information");
                return RedirectToAction("EditLoginInformation", "Student", model);
            }
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult ChangeEmail(int id, string newEmail)
        //{
        //    try
        //    {
        //        var doesEmailExist = UnitOfWork.Student.DoesEmailExist(newEmail);

        //        if (doesEmailExist)
        //        {
        //            Danger(string.Format("Email [{0}] already exists. Please try a different one.", newEmail));
        //            return RedirectToAction("EditProfile", "Student", id);
        //        }

        //        var model = UnitOfWork.Student.GetOne(id);

        //        UnitOfWork.Student.UpdateEmailById(id, newEmail);

        //        _mail.SendVerificationMail("do-not-reply@studybuddy.com", newEmail, "Account confirmation", model.Guid.ToString(), model.FirstName);
        //        Success(string.Format("Account [{0}] has been successfully created.\nPlease check your email for the confirmation link.", model.Email));

        //        return RedirectToAction("EditProfile", "Student", id);
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Fatal(ex.Message);
        //        Danger("Error changing email.");
        //        return RedirectToAction("EditProfile", "Student", id);
        //    }
        //}
    }
}