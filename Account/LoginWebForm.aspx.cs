using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Register_Login.Account
{
    public partial class LoginWebForm : System.Web.UI.Page
    {
        RegisterLoginServiceReference.Register_LoginWebServiceSoapClient obj = new RegisterLoginServiceReference.Register_LoginWebServiceSoapClient();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblLoginMessage.Visible = false;
            }
        }

        protected void chkPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.TextMode = chkPassword.Checked ? TextBoxMode.SingleLine
                : TextBoxMode.Password;
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(dlUserType.SelectedValue))
            {
                lblLoginMessage.Text = "Please select user type";
                lblLoginMessage.Visible = true;
                return;
            }

            if (string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty
                (txtPassword.Text))
            {
                lblLoginMessage.Text = "Email and password are required";
                lblLoginMessage.Visible = true;
                return;
            }

            int userId = obj.GetUserId(txtEmail.Text, dlUserType.SelectedValue);
            bool isValidLogin = obj.VerifyLogin(txtEmail.Text, txtPassword.Text, 
                dlUserType.SelectedValue);

            if (isValidLogin)
            {
                Session["UserType"] = dlUserType.SelectedValue;
                Session["UserId"] = userId;
                Session["Email"] = txtEmail.Text;

                Response.Redirect("DashboardWebForm.aspx");
            }
            else
            {
                lblLoginMessage.Text = "Invalid email or password";
                lblLoginMessage.Visible = true;
            }
        }
    }
}