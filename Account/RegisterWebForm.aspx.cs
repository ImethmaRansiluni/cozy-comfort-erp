using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Register_Login.Account
{
     public partial class RegisterWebForm : System.Web.UI.Page
    {
        RegisterLoginServiceReference.Register_LoginWebServiceSoapClient obj = new RegisterLoginServiceReference.Register_LoginWebServiceSoapClient();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblRegister.Visible = false;
                pnlSuccess.Visible = false;
            }
        }

        protected void chkPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.TextMode = chkPassword.Checked ? TextBoxMode.SingleLine : TextBoxMode.Password;
        }

        protected void chkConfirmPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtConfirmPassword.TextMode = chkConfirmPassword.Checked ?
                TextBoxMode.SingleLine : TextBoxMode.Password;
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(dlUserType.SelectedValue))
                {
                    lblRegister.Text = "Please select user type";
                    lblRegister.Visible = true;
                    return;
                }

                if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtEmail.Text) ||
                    string.IsNullOrEmpty(txtPassword.Text) || string.IsNullOrEmpty(txtConfirmPassword.Text))
                {
                    lblRegister.Text = "All fields are required";
                    lblRegister.Visible = true;
                    return;
                }

                if (txtPassword.Text != txtConfirmPassword.Text)
                {
                    lblRegister.Text = "Passwords do not match";
                    lblRegister.Visible = true;
                    return;
                }

                bool userExists = obj.IsUserExists(txtEmail.Text, dlUserType.SelectedValue);
                if (userExists)
                {
                    lblRegister.Text = "Email already registered";
                    lblRegister.Visible = true;
                    return;
                }

                bool result = obj.RegisterUser(txtName.Text, txtEmail.Text, 
                    txtPassword.Text, dlUserType.SelectedValue);

                if (result)
                {
                    pnlSuccess.Visible = true;
                    lblRegister.Visible = false;
                }
                else
                {
                    lblRegister.Text = "Registration failed";
                    lblRegister.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblRegister.Text = "Error: " + ex.Message;
                System.Diagnostics.Debug.WriteLine("FULL ERROR: " + ex.ToString());
            }
        }

        protected void btnClosePopup_Click(object sender, EventArgs e)
        {
            Response.Redirect("LoginWebForm.aspx");
        }
    }
}