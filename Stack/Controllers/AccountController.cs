using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Facebook;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using Stack.Models;
using CaptchaMvc.HtmlHelpers;
using Stack.Helpers;

namespace Stack.Controllers
{
    public class AccountController : Controller
    {
        private AccountModel _model;
        private bool _userLoggedIn;
        public AccountController()
        {
            _model = new AccountModel();
        }
        private Uri RedirectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url)
                {
                    Query = null,
                    Fragment = null,
                    Path = Url.Action("FacebookCallback")
                };
                return uriBuilder.Uri;
            }
        }
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(AccountModel _model)
        {
            if (ModelState.IsValid)
            {
                GetUserDetails(_model.Email, _model.Password);
                if (_userLoggedIn == true)
                    return RedirectToAction("Index", "Home");
                else
                {
                    ModelState.AddModelError("", "Invalid User Name or Password.");
                }
            }
            return View();
        }
        public void GetUserDetails(string email, string password)
        {
            _model.Email = email;
            _model.Password = password;
            _model.Edit();
            if (_model.CreatedDate != null)
            {
                var userDetails = UserDetails.Instance;
                userDetails.Email = _model.Email;
                userDetails.UserId = _model.UserId;
                userDetails.FullName = _model.FirstName + " " + _model.LastName;
                userDetails.ProfilePicUrl = _model.ProfilePicUrl;
                userDetails.Description = _model.Description;
                userDetails.RoleId = _model.RoleId;
                FormsAuthentication.SetAuthCookie(_model.Email, false);
                Session["UserEmail"] = _model.Email;
                _userLoggedIn = true;
            }
        }
        public bool IsUserAlereadyExists(string email)
        {
            return _model.UserAlreadyExists(email);
        }
        //
        // POST: /Account/ExternalLogin
        //[HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }
        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }
            _model.Email = loginInfo.Email;
            _model.FirstName = loginInfo.DefaultUserName;
            //_model.FirstName = me.middle_name;
            _model.LastName = loginInfo.DefaultUserName;
            _model.Save();
            // Set the auth cookie
            FormsAuthentication.SetAuthCookie(_model.Email, false);
            Session["UserEmail"] = _model.Email;
            GetUserDetails(_model.Email, "");
            if (_userLoggedIn == true)
                return RedirectToAction("Index", "Home");
            else
                return View("LogIn");
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View(_model);
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Register(AccountModel model)
        {
            if (model.UserAlreadyExists(model.Email) == true)
            {
                ModelState.AddModelError("", "User already registered.");
            }
            this.IsCaptchaValid("Validate your captcha");
            if (ModelState.IsValid)
            {
                model.Save();
                return View("RegistrationSuccess", _model);
            }
            return View("Register", model);
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View(_model);
        }
        [AllowAnonymous]
        [HttpPost]
        public string ForgotPassword(AccountModel model)
        {
            try
            {
                var subject = "Forgot Password";
                var emailToAddress = model.Email;
                var emailTemplate = Server.MapPath("~/Templates/Forgotpassword.html");
                string logo = Server.MapPath("~/Images/Email/logo1.png");
                string bodyImage = Server.MapPath("~/Images/Email/Forgotpassword.jpg");
                string guid = Guid.NewGuid().ToString();

                EmailHelper.SendForgotPasswordEmail(subject, emailToAddress, emailTemplate, logo, bodyImage, guid);
                return "Password reset email sent successfully";
            }
            catch(Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return "Internal error. Please contact the website administrator.";
            }
        }
        public ActionResult ResetPassword(string id)
        {
            ResetPasswordViewModel model = new ResetPasswordViewModel();
            return View(model);
            //try
            //{
            //    return "Password reset email sent successfully";
            //}
            //catch (Exception ex)
            //{
            //    Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            //    return "Internal error. Please contact the website administrator.";
            //}
        }
        [HttpPost]
        public ActionResult ResetPassword(ResetPasswordViewModel model)
        {
            return View(model);
            //try
            //{
            //    return "Password reset email sent successfully";
            //}
            //catch (Exception ex)
            //{
            //    Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            //    return "Internal error. Please contact the website administrator.";
            //}
        }
        [AllowAnonymous]
        public ActionResult Facebook()
        {
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = "210409689437092",
                client_secret = "3a56483781e4ac31ae986d45dc46cba7",
                redirect_uri = RedirectUri.AbsoluteUri,
                response_type = "code",
                scope = "email" // Add other permissions as needed
            });
            return Redirect(loginUrl.AbsoluteUri);
        }
        public ActionResult FacebookCallback(string code)
        {
            var fb = new FacebookClient();
            dynamic result = fb.Post("oauth/access_token", new
            {
                client_id = "210409689437092",
                client_secret = "3a56483781e4ac31ae986d45dc46cba7",
                redirect_uri = RedirectUri.AbsoluteUri,
                code = code
            });

            var accessToken = result.access_token;

            // Store the access token in the session for farther use
            Session["AccessToken"] = accessToken;

            // update the facebook client with the access token so 
            // we can make requests on behalf of the user
            fb.AccessToken = accessToken;

            // Get the user's information
            dynamic me = fb.Get("me?fields=first_name,middle_name,last_name,id,email");
            var test = me.Values;
            int count = 0;
            string fbId = "";
            foreach (var item in test)
            {
                if (count == 2)
                    fbId = item;
                count++;
            }
            WebResponse response = null;
            string pictureUrl = string.Empty;
            try
            {
                WebRequest request = WebRequest.Create(string.Format("https://graph.facebook.com/{0}/picture", fbId));
                //WebRequest request = WebRequest.Create(string.Format("https://graph.facebook.com/{0}/picture", (new System.Collections.Generic.Mscorlib_DictionaryValueCollectionDebugView<string, object>(me.Values as System.Collections.Generic.Dictionary<string, object>.ValueCollection)).Items[2]));
                response = request.GetResponse();
                pictureUrl = response.ResponseUri.ToString();
            }
            catch (Exception ex)
            {
                //? handle
            }
            finally
            {
                if (response != null) response.Close();
            }
            //return pictureUrl;


            _model.Email = me.email;
            _model.FirstName = me.first_name;
            //_model.FirstName = me.middle_name;
            _model.LastName = me.last_name;
            _model.ProfilePicUrl = pictureUrl;
            _model.Save();
            // Set the auth cookie
            GetUserDetails(me.email, "");
            if (_userLoggedIn == true)
                return RedirectToAction("Index", "Home");
            else
                return View("LogIn");
        }
        public ActionResult RegistrationSuccess()
        {
            return View("RegistrationSuccess");
        }
        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}