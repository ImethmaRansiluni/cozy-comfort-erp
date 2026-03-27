using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Register_Login.Account
{
    public partial class DashboardWebForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserType"] == null || Session["UserId"] == null)
            {
                Response.Redirect("LoginWebForm.aspx");
                return;
            }

            string userType = Session["UserType"].ToString().ToLower();
            int userId = Convert.ToInt32(Session["UserId"]);

            System.Diagnostics.Debug.WriteLine($"Routing user {userId} of type {userType}");

            switch (userType)
            {
                case "seller":
                    Response.Redirect("../Seller/SellerDashboardWebForm.aspx");
                    break;
                case "distributer":
                    Response.Redirect("../Distributer/DistributerDashboardWebForm.aspx");
                    break;
                case "manufacturer":
                    Response.Redirect("../Manufacturer/ManufacturerDashboardWebForm.aspx");
                    break;
                default:
                    Session.Clear();
                    Response.Redirect("LoginWebForm.aspx");
                    break;
            }
        }
    }
}