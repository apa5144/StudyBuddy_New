using StudyBuddy.Core;
using StudyBuddy.Models;
using System;
using System.IO;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace StudyBuddy.Controllers
{
    [Authorization(Roles = "1,2,3")]
    public class StudentController : BaseController
    {
        public Mail _mail = new Mail();

        [HttpGet]
        public ActionResult EditProfile()
        {
            var guid = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value;

            if (string.IsNullOrWhiteSpace(guid))
                return RedirectToAction("Error", "Auth");

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
            var guid = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value;

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
                if(string.IsNullOrWhiteSpace(model.ProfilePic))
                {
                    var student = UnitOfWork.Student.GetOne(guid);
                    model.ProfilePic = student.ProfilePic;
                }

                UnitOfWork.Student.Update(guid, model.FirstName, model.LastName, model.PhoneNumber, model.ProfilePic);
                Success("Information successfully changed.");
                return View(model);
            }
            catch (Exception ex)
            {
                Logger.Fatal(ex.Message);
                Danger("Error updating information.");
                return RedirectToAction("EditProfile", "Student");
            }
        }

        [HttpGet]
        public ActionResult Security()
        {
            var guid = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value;

            if (string.IsNullOrWhiteSpace(guid))
                return RedirectToAction("Error", "Auth");

            try
            {
                var model = UnitOfWork.Student.GetSecurityInformationByGuid(guid);

                return View(model);
            }
            catch (Exception ex)
            {
                Logger.Fatal(ex.Message);
                Danger("Error getting security information.");
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Security(SecurityViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (model.ChangeEmail)
            {
                if (string.IsNullOrWhiteSpace(model.OldEmail) || string.IsNullOrWhiteSpace(model.NewEmail) || string.IsNullOrWhiteSpace(model.ConfirmEmail))
                {
                    Danger("Email fields are required and cannot be empty.");
                    return View(model);
                }
            }

            if (model.ChangePassword)
            {
                if (string.IsNullOrWhiteSpace(model.OldPassword) || string.IsNullOrWhiteSpace(model.NewPassword) || string.IsNullOrWhiteSpace(model.ConfirmPassword))
                {
                    Danger("Password fields are required and cannot be empty.");
                    return View(model);
                }

                if (model.NewPassword.Length < 6 || model.NewPassword.Length > 50)
                {
                    Danger("Password length must be at least 6 characters and no more than 50.");
                    return View(model);
                }
            }

            var guid = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value;

            try
            {
                if (model.ChangeEmail)
                {
                    if (model.NewEmail == model.Email)
                    {
                        ModelState.AddModelError("NewEmail", "Sorry, you cannot use your current Email.");
                        return View(model);
                    }
                    else
                    {
                        var student = UnitOfWork.Student.GetOne(guid);
                        UnitOfWork.Student.UpdateEmailByGuid(guid, model.NewEmail);                        

                        _mail.SendEmailChangeVerificationMail("do-not-reply@studybuddy.com", model.NewEmail, "Email Change Verification", guid, student.FirstName);
                    }
                }

                if (model.ChangePassword)
                {
                    if (Cryption.Encrypt(model.NewPassword) == model.Password)
                    {
                        ModelState.AddModelError("NewPassword", "Sorry, you cannot use your current Password.");
                        return View(model);
                    }
                    else
                        UnitOfWork.Student.UpdatePasswordByGuid(guid, Cryption.Encrypt(model.NewPassword));
                }

                var ctx = Request.GetOwinContext();
                var authManager = ctx.Authentication;
                authManager.SignOut("ApplicationCookie");

                UnitOfWork.Notification.AddSystemNotificationByTargetGuid("You've recently made changes to your account security setteings. If you didn't request to do so, please make sure you contact us immediately.", guid);
                Success("Your security information were successfuly changed.\n\nPlease check your email if you have changed it, otherwise, We ask you to please relog with your new information to confirm your changes.");
                return RedirectToAction("Login", "Auth");
            }
            catch (Exception ex)
            {
                Logger.Fatal(ex.Message);
                Danger("Error changing login information");
                return RedirectToAction("Security", "Student");
            }
        }

        public ActionResult SendReactivation()
        {
            var guid = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value;

            try
            {
                var student = UnitOfWork.Student.GetOne(guid);

                _mail.SendVerificationMail("do-not-reply@studybuddy.com", student.Email, "Email confirmation", guid, student.FirstName);

                Success("Your account has been successfully verified.");
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                Logger.Fatal(ex.Message);
                Danger("Error with sending reactivation email");
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Deactivate()
        {
            var guid = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value;

            if (string.IsNullOrWhiteSpace(guid))
                return RedirectToAction("Error", "Auth");

            try
            {
                var student = UnitOfWork.Student.GetByGuid(guid);

                UnitOfWork.Student.Deactivate(guid);

                var ctx = Request.GetOwinContext();
                var authManager = ctx.Authentication;
                authManager.SignOut("ApplicationCookie");

                Information("Your account has been successfully deactivated.");
                _mail.SendDeactivationMail("do-not-reply@studybuddy.com", student.Email, "Deactivation Notice", guid, student.FirstName);
                return RedirectToAction("Login", "Auth");
            }
            catch (Exception ex)
            {
                Logger.Fatal(ex.Message);
                Danger("An error occured while trying to deactivate email.");
                return RedirectToAction("Login", "Auth");
            }
        }

        [HttpPost]
        public JsonResult RemoveProfilePicture()
        {
            var guid = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value;

            UnitOfWork.Student.RemoveProfilePictureByGuid(guid);

            return Json(new { success = false, JsonRequestBehavior.AllowGet });
        }

        [HttpPost]
        public JsonResult UpdateAvailability()
        {
            var guid = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value;

            UnitOfWork.Student.UpdateAvailabilityByGuid(guid);

            return Json(new { success = true });
        }
    }
}