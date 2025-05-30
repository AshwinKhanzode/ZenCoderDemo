using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.UI;
using ZenCoderDemo;

public partial class Account_Register : Page
{
    protected void CreateUser_Click(object sender, EventArgs e)
    {
        if (!Regex.IsMatch(EmailTextBox.Text, @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$"))
        {
            ErrorMessageLiteral.Text = "A valid email is required.";
            return;
        }

        var manager = new UserManager();
        var user = new ApplicationUser() { UserName = UserNameTextBox.Text, Email = EmailTextBox.Text };
        IdentityResult result = manager.Create(user, PasswordTextBox.Text);
        if (result.Succeeded)
        {
            IdentityHelper.SignIn(manager, user, isPersistent: false);
            IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
        }
        else
        {
            string firstError = string.Empty;
            foreach (var err in result.Errors)
            {
                firstError = err;
                break;
            }
            ErrorMessageLiteral.Text = firstError;
        }
    }
}
