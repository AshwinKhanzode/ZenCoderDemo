using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Web;
using System.Web.UI;
using System.Diagnostics;
using ZenCoderDemo;

public partial class Account_Login : Page
{
        protected string RecaptchaSiteKey => System.Configuration.ConfigurationManager.AppSettings["RecaptchaSiteKey"];
        protected void Page_Load(object sender, EventArgs e)
        {
            RegisterHyperLink.NavigateUrl = "Register";
            OpenAuthLogin.ReturnUrl = Request.QueryString["ReturnUrl"];
            var returnUrl = HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
            if (!String.IsNullOrEmpty(returnUrl))
            {
                RegisterHyperLink.NavigateUrl += "?ReturnUrl=" + returnUrl;
            }
        }

        protected void LogIn(object sender, EventArgs e)
        {
            if (IsValid)
            {
                // Validate captcha first
                var captchaResponse = Request.Form["g-recaptcha-response"];
                if (!ReCaptchaService.VerifyResponse(captchaResponse))
                {
                    FailureText.Text = "Please verify that you are not a robot.";
                    ErrorMessage.Visible = true;
                    Trace.Warn("Captcha", $"Failed CAPTCHA for user {UserName.Text}");
                    return;
                }
                // Validate the user password
                var manager = new UserManager();
                ApplicationUser user = manager.Find(UserName.Text, Password.Text);
                if (user != null)
                {
                    IdentityHelper.SignIn(manager, user, RememberMe.Checked);
                    IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
                }
                else
                {
                    FailureText.Text = "Invalid username or password.";
                    ErrorMessage.Visible = true;
                }
            }
        }
}