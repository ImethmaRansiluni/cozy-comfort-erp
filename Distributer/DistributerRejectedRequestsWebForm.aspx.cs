using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Register_Login.Distributer
{
    public partial class DistributerRejectedRequestsWebForm : System.Web.UI.Page
    {
        SellerRequestsServiceReference.SellerRequestsWebServiceSoapClient obj = new SellerRequestsServiceReference.SellerRequestsWebServiceSoapClient();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserType"] == null || Session["UserType"].ToString() != "distributer")
                {
                    Response.Redirect("../Account/LoginWebForm.aspx");
                    return;
                }

                if (!IsPostBack)
                {
                    LoadConfirmedRequests();
                }
            }
        }

        private void LoadConfirmedRequests()
        {
            string distributerId = Session["UserId"].ToString();
            try
            {
                DataSet confirmedRequests = obj.GetRejectedRequestsByDistributer(distributerId);

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

        protected void gvConfirmedRequests_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {
            gvConfirmedRequests.PageIndex = e.NewPageIndex;
            LoadConfirmedRequests();
        }
    }
}