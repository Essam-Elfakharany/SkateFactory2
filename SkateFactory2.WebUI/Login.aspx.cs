using SkateFactory2.WebUI.UserService;
using System;
using System.Text.RegularExpressions;
using System.Web.Security;
using System.Web.UI;

namespace SkateFactory2.WebUI
{
    public partial class Login : Page
    {
        #region WS proxy methods vs Data proxy methods
        //private void SignIn_LocalDB()
        //{
        //    try
        //    {
        //        string pepper = ConfigurationManager.AppSettings["Pepper"];
        //        string passwordHash = StaticMethods.ComputePasswordHash(txtEmail.Text, txtPassword.Text, pepper);

        //        var user = new User() { Email = txtEmail.Text, Password = passwordHash };
        //        bool success = UserData.UserAndPasswordAreValid(user, StaticMethods.GetConnectionString(this));
        //        if (success)
        //            FormsAuthentication.RedirectFromLoginPage(txtEmail.Text, true);
        //        else
        //            StaticMethods.DisplayMessage("Invalid e-mail or password", this);

        //    }
        //    catch (Exception ex)
        //    {
        //        StaticMethods.DisplayMessage(ex.Message, this);
        //    }
        //}

        private void SignIn_WS()
        {
            try
            {
                var wsClient = new UserServiceSoapClient();
                bool success = wsClient.UserAndPasswordAreValid(txtEmail.Text, txtPassword.Text);
                if (success)
                    FormsAuthentication.RedirectFromLoginPage(txtEmail.Text, true);
                else
                    StaticMethods.DisplayMessage("Invalid e-mail or password", this);
            }
            catch (Exception ex)
            {
                StaticMethods.DisplayMessage(ex.Message, this);
            }
        }


        //private void RegisterUser_LocalDB()
        //{
        //    string cnString = StaticMethods.GetConnectionString(this);
        //    if (!UserData.UserIsUnique(txtNewEmail.Text, cnString))
        //    {
        //        StaticMethods.DisplayMessage("This e-mail is already registered", Page);
        //        return;
        //    }

        //    try
        //    {
        //        string pepper = ConfigurationManager.AppSettings["Pepper"];
        //        string passwordHash = StaticMethods.ComputePasswordHash(txtNewEmail.Text, txtNewPassword1.Text, pepper);
        //        var newUser = new User() { Email = txtNewEmail.Text, Password = passwordHash };
        //        UserData.Insert(newUser, cnString);
        //        StaticMethods.DisplayMessage($"{txtNewEmail.Text} has been created as a new user and is ready to sign in", Page);
        //        txtNewEmail.Text = "";
        //        txtNewPassword1.Text = "";
        //        txtNewPassword2.Text = "";
        //    }
        //    catch (Exception ex)
        //    {
        //        StaticMethods.DisplayMessage(ex.Message, this);
        //    }
        //}

        void RegisterUser_WS()
        {
            try
            {
                var wsClient = new UserServiceSoapClient();
                bool success = wsClient.Register(txtNewEmail.Text, txtNewPassword1.Text);
                if (success)
                    StaticMethods.DisplayMessage($"{txtNewEmail.Text} has been created as a new user and is ready to sign in", Page);
                else
                    StaticMethods.DisplayMessage("This e-mail is already registered", Page);
            }
            catch (Exception ex)
            {
                StaticMethods.DisplayMessage(ex.Message, this);
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSignIn_Click(object sender, EventArgs e)
        {
            SignIn_WS();
        }



        protected void btnRegister_Click(object sender, EventArgs e)
        {
            if (txtNewEmail.Text.Trim() == "")
            {
                StaticMethods.DisplayMessage("E-mail is required", Page);
                return;
            }
            else if (!Regex.IsMatch(txtNewEmail.Text, "^\\w+([-+.]\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$"))
            {
                StaticMethods.DisplayMessage("E-mail is invalid", Page);
                return;
            }
            else if (txtNewPassword1.Text.Trim() == "")
            {
                StaticMethods.DisplayMessage("Password is required", Page);
                return;
            }
            else if (txtNewPassword1.Text != txtNewPassword2.Text)
            {
                StaticMethods.DisplayMessage("The passwords do not match", Page);
                return;
            }

            RegisterUser_WS();
        }

    }
}