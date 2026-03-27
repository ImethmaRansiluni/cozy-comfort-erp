using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Register_Login.Manufacturer
{
    public partial class ManufacturerDashboardWebForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserType"] == null || Session["UserType"].ToString() != "manufacturer")
            {
                Response.Redirect("LoginWebForm.aspx");
                return;
            }

            if (!IsPostBack)
            {
                lblWelcome.Text = $"Welcome, Manufacturer #{Session["UserId"]}";
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("../Account/LoginWebForm.aspx");
        }
    }
}