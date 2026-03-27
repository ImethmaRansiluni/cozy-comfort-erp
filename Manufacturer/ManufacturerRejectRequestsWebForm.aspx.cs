using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Register_Login.Manufacturer
{
    public partial class ManufacturerRejectRequestsWebForm : System.Web.UI.Page
    {
        DistributerRequestsServiceReference.DistributerRequestsWebServiceSoapClient obj = new DistributerRequestsServiceReference.DistributerRequestsWebServiceSoapClient();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserType"] == null || Session["UserType"].ToString() != "manufacturer")
                {
                    Response.Redirect("../Account/LoginWebForm.aspx");
                    return;
                }

                LoadRejectedRequests();
            }
        }

        private void LoadRejectedRequests()
        {
            try
            {
                DataSet rejectedRequests = obj.GetAllRejectedRequests();

                if (rejectedRequests.Tables[0].Rows.Count > 0)
                {
                    gvRejectedRequests.DataSource = rejectedRequests;
                    gvRejectedRequests.DataBind();
                    lblMessage.Visible = false;
                }
                else
                {
                    gvRejectedRequests.DataSource = null;
                    gvRejectedRequests.DataBind();
                    lblMessage.Text = "No rejected requests found";
                    lblMessage.CssClass = "message info";
                    lblMessage.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error loading rejected requests: " + ex.Message;
                lblMessage.CssClass = "message error";
                lblMessage.Visible = true;
            }
        }

        protected void gvRejectedRequests_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvRejectedRequests.PageIndex = e.NewPageIndex;
            LoadRejectedRequests();
        }
    }
}