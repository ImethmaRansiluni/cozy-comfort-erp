using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Register_Login.Seller
{
    public partial class SellerRequestWebForm : System.Web.UI.Page
    {
        BlanketServiceReference.BlanketWebServiceSoapClient obj = new BlanketServiceReference.BlanketWebServiceSoapClient();
        SellerInventoryServiceReference.SellerInventoryWebServiceSoapClient obj1 = new SellerInventoryServiceReference.SellerInventoryWebServiceSoapClient();
        SellerRequestsServiceReference.SellerRequestsWebServiceSoapClient obj2 = new SellerRequestsServiceReference.SellerRequestsWebServiceSoapClient();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserType"] == null || Session["UserType"].ToString() != "seller")
            {
                Response.Redirect("../Account/LoginWebForm.aspx");
                return;
            }

            string sellerId = Session["UserId"].ToString();

            if (!IsPostBack)
            {
                LoadSellerInventory(sellerId);
                LoadBlanketModelsDropdown();
                LoadSellerRequests();
            }

        }

        private void LoadSellerInventory(string sellerId)
        {
            try
            {
                DataSet dsInventory = obj1.GetSellerInventory(sellerId);
                gvInventory.DataSource = dsInventory;
                gvInventory.DataBind();

                if (gvInventory.Rows.Count == 0)
                {
                    lblInventoryMessage.Text = "No inventory items found";
                    lblInventoryMessage.Visible = true;
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

                string sellerId = Session["UserId"].ToString();
                string blanketId = dlBlanketModel.SelectedValue;
                string status = "Pending";

                string result = obj2.AddSellerRequest(
                    sellerId,
                    blanketId,
                    qty.ToString(),
                    status);

                if (result.StartsWith("error"))
                {
                    ShowMessage("Failed to submit request: " + result.Replace("error", ""), false);
                    return;
                }

                ShowMessage("Request submitted successfully! Status: Pending", true);
                LoadSellerInventory(sellerId);

                dlBlanketModel.SelectedIndex = 0;
                txtQuantity.Text = "";
            }
            catch (Exception ex)
            {
                ShowMessage("System error: " + ex.Message, false);
            }
        }

        private void LoadSellerRequests()
        {
            string sellerId = Session["UserId"].ToString();
            try
            {
                DataSet ds = obj2.GetSellerRequests(sellerId);
                gvRequests.DataSource = ds;
                gvRequests.DataBind();

                if (gvRequests.Rows.Count > 0)
                {
                    gvRequests.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
                else
                {
                    ShowMessage("No requests found", true);
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Error loading requests: " + ex.Message, false);
                System.Diagnostics.Debug.WriteLine("LoadSellerRequests error: " + ex);
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