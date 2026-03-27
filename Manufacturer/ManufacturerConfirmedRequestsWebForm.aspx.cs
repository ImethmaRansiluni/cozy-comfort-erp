using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Register_Login.Manufacturer
{
    public partial class ManufacturerConfirmedRequestsWebForm : System.Web.UI.Page
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

                LoadConfirmedRequests();
            }
        }

        private void LoadConfirmedRequests()
        {
            try
            {
                DataSet confirmedRequests = obj.GetConfirmedRequestsByDistributer();

                if (confirmedRequests.Tables[0].Rows.Count > 0)
                {
                    gvConfirmedRequests.DataSource = confirmedRequests;
                    gvConfirmedRequests.DataBind();
                    lblMessage.Visible = false;
                }
                else
                {
                    gvConfirmedRequests.DataSource = null;
                    gvConfirmedRequests.DataBind();
                    lblMessage.Text = "No confirmed requests found";
                    lblMessage.CssClass = "message info";
                    lblMessage.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error loading confirmed requests: " + ex.Message;
                lblMessage.CssClass = "message error";
                lblMessage.Visible = true;
            }
        }

        protected void gvConfirmedRequests_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvConfirmedRequests.PageIndex = e.NewPageIndex;
            LoadConfirmedRequests();
        }
    }

}