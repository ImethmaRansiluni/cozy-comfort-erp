using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Register_Login.Seller
{
    public partial class SellerAddOrderWebForm : System.Web.UI.Page
    {
        NewIDServiceReference.GetNewIDWebServiceSoapClient obj = new NewIDServiceReference.GetNewIDWebServiceSoapClient();
        BlanketServiceReference.BlanketWebServiceSoapClient obj1 = new BlanketServiceReference.BlanketWebServiceSoapClient();
        OrderServiceReference.OrderWebServiceSoapClient obj2 = new OrderServiceReference.OrderWebServiceSoapClient();
        SellerInventoryServiceReference.SellerInventoryWebServiceSoapClient obj3 = new SellerInventoryServiceReference.SellerInventoryWebServiceSoapClient();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserType"] == null || Session["UserType"].ToString() != "seller")
                {
                    Response.Redirect("../Account/LoginWebForm.aspx");
                    return;
                }

                txtOrderID.Text = obj.autoOrderID();
                LoadBlanketModels();
            }
        }

        private void LoadBlanketModels()
        {
            DataSet dsModels = obj1.getBlanketModel();
            dlBlanketModel.DataSource = dsModels;
            dlBlanketModel.DataValueField = "BlanketID";
            dlBlanketModel.DataTextField = "BlanketModel";
            dlBlanketModel.DataBind();
            dlBlanketModel.Items.Insert(0, new ListItem("-- Select Model --", ""));
        }

        protected void dlBlanketModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dlBlanketModel.SelectedValue != "")
            {
                string sellerId = Session["UserId"].ToString();
                string blanketId = dlBlanketModel.SelectedValue;

                DataSet dsInventory = obj3.GetInventoryBySellerAndModel(sellerId, blanketId);
                gvInventory.DataSource = dsInventory;
                gvInventory.DataBind();

                inventorySection.Visible = true;
            }
            else
            {
                inventorySection.Visible = false;
            }
        }

        protected void btnAddOrder_Click(object sender, EventArgs e)
        {
            try
            {
                string sellerId = Session["UserId"].ToString();
                string blanketId = dlBlanketModel.SelectedValue;
                int qty = int.Parse(txtOrderQty.Text);
                string orderId = txtOrderID.Text;
                string result = obj2.addOrder(
                    orderId,
                    txtCustomerName.Text.Trim(),
                    sellerId,
                    blanketId,
                    qty.ToString());

                if (result.StartsWith("error"))
                {
                    ShowMessage("Failed to save order: " + result.Replace("error", ""),
                        "error");
                    return;
                }

                orderSummary.Visible = true;
                litSelectedModel.Text = dlBlanketModel.SelectedItem.Text;
                litOrderQty.Text = qty.ToString();

                int availableQty = obj3.GetAvailableQuantity(sellerId, blanketId);
                litAvailableQty.Text = availableQty.ToString();

                inventorySection.Visible = true;
            }
            catch (Exception ex)
            {
                ShowMessage("System Error: Please try again", "error" + ex.Message);
            }
        }

        protected void btnConfirmOrder_Click(object sender, EventArgs e)
        {
            try
            {
                string sellerId = Session["UserId"].ToString();
                string orderId = txtOrderID.Text;

                if (!obj2.CanConfirmOrder(orderId, sellerId))
                {
                    ShowMessage("Order cannot be confirmed (already confirmed or " +
                        "doesn't exist)", "error");
                    return;
                }

                if (obj2.ConfirmOrder(orderId, sellerId))
                {
                    ShowMessage("Order confirmed successfully!", "success");
                }
                else
                {
                    ShowMessage("Failed to confirm order", "error");
                }
            }
            catch (Exception ex)
            {
                ShowMessage($"System error: {ex.Message}", "error");
                System.Diagnostics.Trace.WriteLine(ex.ToString());
            }
        }

        private void ShowMessage(string message, string type)
        {
            lblMessage.Text = message;
            lblMessage.Visible = true;
            lblMessage.CssClass = $"status-message {(type == "error" ? "error-message" : "success-message")}";
        }
    }
}