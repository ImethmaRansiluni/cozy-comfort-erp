using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Register_Login.Seller
{
    public partial class SellerStockUpdatesWebForm : System.Web.UI.Page
    {
        SellerRequestsServiceReference.SellerRequestsWebServiceSoapClient obj = new SellerRequestsServiceReference.SellerRequestsWebServiceSoapClient();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserType"] == null || Session["UserType"].ToString() != "seller")
                {
                    Response.Redirect("../Account/LoginWebForm.aspx");
                    return;
                }

                LoadSellerRequests();
            }
        }

        private void LoadSellerRequests(string statusFilter = "")
        {
            try
            {
                string sellerId = Session["UserId"].ToString();
                DataSet ds = obj.GetSellerRequestsByStatus(sellerId, statusFilter);

                gvRequests.DataSource = ds;
                gvRequests.DataBind();

                if (gvRequests.Rows.Count == 0)
                {
                    ShowMessage("No requests found with the selected criteria", false);
                }
                else
                {
                    lblMessage.Visible = false;
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Error loading requests: " + ex.Message, false);
            }
        }

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedStatus = ddlStatus.SelectedValue;
            LoadSellerRequests(selectedStatus);
        }

        private void ShowMessage(string message, bool isSuccess)
        {
            lblMessage.Text = message;
            lblMessage.CssClass = isSuccess ? "alert alert-success" : "alert alert-danger";
            lblMessage.Visible = true;
        }
    }
}