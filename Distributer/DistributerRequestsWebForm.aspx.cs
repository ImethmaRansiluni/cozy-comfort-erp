using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Register_Login.Distributer
{
    public partial class DistributerRequestsWebForm : System.Web.UI.Page
    {
        BlanketServiceReference.BlanketWebServiceSoapClient obj = new BlanketServiceReference.BlanketWebServiceSoapClient();
        DistributerInventoryServiceReference.DistributerInventoryWebServiceSoapClient obj1 = new DistributerInventoryServiceReference.DistributerInventoryWebServiceSoapClient();
        DistributerRequestsServiceReference.DistributerRequestsWebServiceSoapClient obj2 = new DistributerRequestsServiceReference.DistributerRequestsWebServiceSoapClient();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserType"] == null || Session["UserType"].ToString() != "distributer")
            {
                Response.Redirect("../Account/LoginWebForm.aspx");
                return;
            }

            string distributerId = Session["UserId"].ToString();

            if (!IsPostBack)
            {
                LoadDistributerInventory(distributerId);
                LoadBlanketModelsDropdown();
                LoadDistributerRequests(distributerId);
            }
        }

        private void LoadDistributerInventory(string distributerId)
        {
            try
            {
                DataSet dsInventory = obj1.GetDistributerInventory(distributerId);
                gvInventory.DataSource = dsInventory;
                gvInventory.DataBind();

                if (gvInventory.Rows.Count == 0)
                {
                    lblInventoryMessage.Text = "No inventory items found";
                    lblInventoryMessage.Visible = true;
                }
                else
                {
                    lblInventoryMessage.Visible = false;
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Error loading inventory: " + ex.Message, false);
            }
        }

        private void LoadBlanketModelsDropdown()
        {
            try
            {
                DataSet dsModels = obj.getBlanketModel();
                dlBlanketModel.DataSource = dsModels;
                dlBlanketModel.DataValueField = "BlanketID";
                dlBlanketModel.DataTextField = "BlanketModel";
                dlBlanketModel.DataBind();
                dlBlanketModel.Items.Insert(0, new ListItem("-- All Blankets --", ""));
            }
            catch (Exception ex)
            {
                ShowMessage("Error loading blanket models: " + ex.Message, false);
            }
        }

        protected void btnRequest_Click(object sender, EventArgs e)
        {
            try
            {
                if (dlBlanketModel.SelectedValue == "")
                {
                    ShowMessage("Please select a blanket model", false);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtQuantity.Text) || !int.TryParse(txtQuantity.Text, out int qty) || qty <= 0)
                {
                    ShowMessage("Please enter a valid quantity (must be a positive number)", false);
                    return;
                }

                string distributerId = Session["UserId"].ToString();
                string blanketId = dlBlanketModel.SelectedValue;
                string status = "Pending";

                string result = obj2.AddDistributerRequest(
                    distributerId,
                    blanketId,
                    qty,
                    status);

                if (result.StartsWith("error"))
                {
                    ShowMessage("Failed to submit request: " + result.Replace("error", ""), false);
                    return;
                }

                ShowMessage("Request submitted successfully! Status: Pending", true);
                LoadDistributerInventory(distributerId);
                LoadDistributerRequests(distributerId);

                dlBlanketModel.SelectedIndex = 0;
                txtQuantity.Text = "";
            }
            catch (Exception ex)
            {
                ShowMessage("System error: " + ex.Message, false);
            }
        }

        private void LoadDistributerRequests(string distributerId)
        {
            try
            {
                DataSet ds = obj2.GetDistributerRequests(distributerId);
                gvRequests.DataSource = ds;
                gvRequests.DataBind();

                if (gvRequests.Rows.Count > 0)
                {
                    gvRequests.HeaderRow.TableSection = TableRowSection.TableHeader;
                    lblRequestsMessage.Visible = false;
                }
                else
                {
                    lblRequestsMessage.Text = "No requests found";
                    lblRequestsMessage.Visible = true;
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Error loading requests: " + ex.Message, false);
                System.Diagnostics.Debug.WriteLine("LoadDistributerRequests error: " + ex);

                gvRequests.DataSource = null;
                gvRequests.DataBind();
                lblRequestsMessage.Text = "Error loading requests";
                lblRequestsMessage.Visible = true;
            }
        }

        private void ShowMessage(string message, bool isSuccess)
        {
            lblMessage.Text = message;
            lblMessage.CssClass = isSuccess ? "alert alert-success" : "alert alert-danger";
            lblMessage.Visible = true;
        }
    }
}