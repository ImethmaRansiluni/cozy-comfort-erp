using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Register_Login.Seller
{
    public partial class PendingSellerOrders : System.Web.UI.Page
    {
        BlanketServiceReference.BlanketWebServiceSoapClient obj = new BlanketServiceReference.BlanketWebServiceSoapClient();
        SellerInventoryServiceReference.SellerInventoryWebServiceSoapClient obj1 = new SellerInventoryServiceReference.SellerInventoryWebServiceSoapClient();
        OrderServiceReference.OrderWebServiceSoapClient obj2 = new OrderServiceReference.OrderWebServiceSoapClient();
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
                LoadPendingOrders(sellerId, dlBlanketModel.SelectedValue);
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

        private void LoadPendingOrders(string sellerId, string blanketId = "")
        {
            try
            {
                DataSet dsOrders = string.IsNullOrEmpty(blanketId)
                    ? obj2.GetOrdersBySeller(sellerId)
                    : obj2.GetPendingOrdersBySellerAndBlanket(sellerId, blanketId);

                DataTable dtFiltered;

                if (string.IsNullOrEmpty(blanketId))
                {
                    DataRow[] pendingRows = dsOrders.Tables[0].Select("Status = 'Pending'");
                    dtFiltered = pendingRows.Length > 0
                        ? pendingRows.CopyToDataTable()
                        : dsOrders.Tables[0].Clone();
                }
                else
                {
                    dtFiltered = dsOrders.Tables[0];
                }

                gvPendingOrders.DataSource = dtFiltered;
                gvPendingOrders.DataBind();

                lblOrdersMessage.Visible = dtFiltered.Rows.Count == 0;
                lblOrdersMessage.Text = "No pending orders found";
            }
            catch (Exception ex)
            {
                ShowMessage("Error loading pending orders: " + ex.Message, false);
                DataTable dt = new DataTable();
                dt.Columns.Add("OrderID");
                dt.Columns.Add("CustomerName");
                dt.Columns.Add("BlanketModel");
                dt.Columns.Add("Qty");
                dt.Columns.Add("Status");
                gvPendingOrders.DataSource = dt;
                gvPendingOrders.DataBind();
            }
        }

        protected void dlBlanketModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sellerId = Session["UserId"].ToString();
            string blanketId = dlBlanketModel.SelectedValue;
            LoadPendingOrders(sellerId, blanketId);
        }

        protected void gvPendingOrders_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Approve")
            {
                try
                {
                    string sellerId = Session["UserId"].ToString();
                    string orderId = e.CommandArgument.ToString();

                    bool success = obj2.ApprovePendingOrder(orderId, sellerId);

                    if (success)
                    {
                        ShowMessage("Order confirmed successfully", true);
                        LoadPendingOrders(sellerId, dlBlanketModel.SelectedValue);
                        LoadSellerInventory(sellerId);
                    }
                    else
                    {
                        ShowMessage("Failed to confirm order: Insufficient inventory or order not pending", false);
                    }
                }
                catch (Exception ex)
                {
                    ShowMessage("System error: " + ex.Message, false);
                }
            }
        }

        protected void gvPendingOrders_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Button btnApprove = e.Row.FindControl("btnApprove") as Button;
                if (btnApprove != null)
                {
                    string orderId = gvPendingOrders.DataKeys[e.Row.RowIndex].Value.ToString();
                    btnApprove.CommandArgument = orderId;
                    Page.ClientScript.RegisterForEventValidation(btnApprove.UniqueID, orderId);
                }
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