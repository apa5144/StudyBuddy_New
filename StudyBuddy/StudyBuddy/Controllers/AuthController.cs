using System;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using StudyBuddy.Core;
using StudyBuddy.Models;

namespace StudyBuddy.Controllers
{
    [AllowAnonymous]
    public class AuthController : BaseController
    {
        public Mail _mail = new Mail();

        [HttpGet]
        public ActionResult Signup()
        {
            var model = new Student();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Signup(Student model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                var password = string.Empty;
                var email = model.Email;
                Guid guid = Guid.Empty;

                //Email and Password validation
                if (model.Id == default(long))
                {
                    var doesEmailExist = UnitOfWork.Student.DoesEmailExist(email);

                    if (doesEmailExist)
                    {
                        Danger(string.Format("Email [{0}] already exists. Please try a different one.", email));
                        return View(model);
                    }

                    if (model.Password == null)
                    {
                        Danger("Password is required and cannot be empty. Please try again.");
                        return View(model);
                    }
                }

                if (model.Id == 0 || (model.Id > 0 && !string.IsNullOrEmpty(model.Password)))
                {
                    password = Cryption.Encrypt(model.Password);
                    guid = Guid.NewGuid();
                }
                UnitOfWork.Student.Create(model.FirstName, model.LastName, email, password, guid.ToString());

                _mail.SendVerificationMail("do-not-reply@studybuddy.com", email, "Account confirmation", guid.ToString(), model.FirstName);
                Success(string.Format("Account [{0}] has been successfully created.\nPlease check your email for the confirmation link.", model.Email));

                return RedirectToAction("Login", "Auth");
            }
            catch (Exception ex)
            {
                Logger.Fatal(ex.Message);
                Danger("Error creating account.");
                return View(model);
            }
        }

        public ActionResult Verify(string guid)
        {
            if (string.IsNullOrWhiteSpace(guid))
                return RedirectToAction("Error", "Auth");
            try
            {
                var student = new Student();

                UnitOfWork.Student.VerifyByGuid(guid);

                student = UnitOfWork.Student.GetByGuid(guid);

                IdentitySignin(student);

                Success("Your account has been successfully verified.");
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                Logger.Fatal(ex.Message);
                Danger("An error occured while trying to verify email.");
                return RedirectToAction("Login", "Auth");
            }
        }

        [HttpGet]
        public ActionResult ForgotPassword()
        {
            var model = new Student();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(Student model)
        {
            try
            {
                var doesEmailExist = UnitOfWork.Student.DoesEmailExist(model.Email);

                if (!doesEmailExist)
                {
                    ModelState.AddModelError("Email", string.Format("Email [{0}] does not exist. Please try again.", model.Email));
                    return View(model);
                }

                var student = UnitOfWork.Student.GetByEmail(model.Email);
                var guid = student.Guid;

                _mail.SendForgotPasswordMail("do-not-reply@studybuddy.com", model.Email, "Forgot password", guid.ToString(), student.FirstName);
                Success(string.Format("Please check the email associated with your account to complete the process.", model.Email));

                return RedirectToAction("Login", "Auth");
            }
            catch (Exception ex)
            {
                Logger.Fatal(ex.Message);
                Danger("An error has occurred.");
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult ResetPassword(string guid)
        {
            if (string.IsNullOrWhiteSpace(guid))
                return RedirectToAction("Error", "Auth");
            try
            {
                var student = new Student();
                student = UnitOfWork.Student.GetByGuid(guid);

                if (student == null)
                {
                    Danger("This account does not exist.");
                }

                return View(student);
            }
            catch (Exception ex)
            {
                Logger.Fatal(ex.Message);
                Danger("An error occured while trying to verify email.");
                return RedirectToAction("Login", "Auth");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(Student model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                var password = string.Empty;

                password = Cryption.Encrypt(model.Password);

                UnitOfWork.Student.UpdatePasswordByGuid(model.Guid, password);
                _mail.SendForgotPasswordNotification("do-not-reply@studybuddy.com", model.Email, "Password change notification", model.FirstName);

                Success("Password successfully changed.");

                return RedirectToAction("Login", "Auth");
            }
            catch (Exception ex)
            {
                Logger.Fatal(ex.Message);
                Danger("Error with resetting password.");
                return RedirectToAction("Login", "Auth");
            }
        }

        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            var model = new Student { ReturnUrl = returnUrl };

            return View(model);
        }

        public ActionResult Logout()
        {
            var ctx = Request.GetOwinContext();
            var authManager = ctx.Authentication;

            authManager.SignOut("ApplicationCookie");
            return RedirectToAction("Login", "Auth");
        }

        private string GetRedirectUrl(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
            {
                return Url.Action("Index", "Home");
            }

            return returnUrl;
        }

        [HttpPost]
        public ActionResult Login(Student model)
        {
            try
            {
                var student = UnitOfWork.Student.GetByEmail(model.Email);
                if (student != null)
                {
                    if (student.SecurityLevel == 0)
                    {
                        Danger("This account has been disabled.<p><a href='" + _mail.GetBaseUrl() + "Auth/Activate?guid=" + student.Guid + "'>Click here to request reactivation</a></p>", true);
                        return View();
                    }
                    else if (student.Password != Cryption.Encrypt(model.Password))
                    {
                        ModelState.AddModelError("Password", "Password is incorrect. Please try again.");
                        return View();
                    }
                    else
                    {
                        IdentitySignin(student);
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    Warning("Invalid login attempt. Please check your email or password.");
                    ModelState.AddModelError("Email", " ");
                    ModelState.AddModelError("Password", " ");
                    return View();
                }
            }
            catch (Exception ex)
            {
                Logger.Fatal(ex.Message);
                Warning("An error occured while trying to log in. Please try again.");
            }
            return View(model);
        }

        public void IdentitySignin(Student student)
        {
            var claims = new List<Claim>();

            // create required claims
            claims.Add(new Claim(ClaimTypes.NameIdentifier, student.Guid.ToString()));
            claims.Add(new Claim(ClaimTypes.Role, student.SecurityLevel.ToString()));

            var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

            AuthenticationManager.SignIn(new AuthenticationProperties()
            {
                AllowRefresh = true,
                ExpiresUtc = DateTime.UtcNow.AddDays(7)
            }, identity);
        }

        public void IdentitySignout()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie,
                                            DefaultAuthenticationTypes.ExternalCookie);
        }

        private IAuthenticationManager AuthenticationManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult LogOn(string email, string password, bool rememberMe, string returnUrl)
        {
            var student = UnitOfWork.Student.GetByCredentials(email, password);
            if (student == null)
            {
                return View();
                //Return to the login page.
            }

            IdentitySignin(student);

            if (!string.IsNullOrEmpty(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("New", "Snippet", null);
        }

        public ActionResult Activate(string guid)
        {
            if (string.IsNullOrWhiteSpace(guid))
                return RedirectToAction("Error", "Auth");

            try
            {
                var student = UnitOfWork.Student.GetByGuid(guid);

                UnitOfWork.Student.VerifyByGuid(guid);

                _mail.SendVerificationMail("do-not-reply@studybuddy.com", student.Email, "Reactivation Request", guid, student.FirstName);

                Information("An email with instructions to activating your account has been sent to your email.");
                return RedirectToAction("Login", "Auth");
            }
            catch (Exception ex)
            {
                Logger.Fatal(ex.Message);
                Danger("An error occured while trying to reactivate account.");
                return RedirectToAction("Login", "Auth");
            }
        }


        public ActionResult Reactivate(string guid)
        {
            if (string.IsNullOrWhiteSpace(guid))
                return RedirectToAction("Error", "Auth");

            try
            {
                var student = UnitOfWork.Student.GetByGuid(guid);

                UnitOfWork.Student.VerifyByGuid(guid);

                _mail.SendReactivationMail("do-not-reply@studybuddy.com", student.Email, "Reactivation Notice", guid, student.FirstName);

                Information("Your account has been successfully reactivated.");
                return RedirectToAction("Login", "Auth");
            }
            catch (Exception ex)
            {
                Logger.Fatal(ex.Message);
                Danger("An error occured while trying to reactivate email.");
                return RedirectToAction("Login", "Auth");
            }
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}