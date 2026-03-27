using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Register_Login.Distributer
{
    public partial class SellerOrdersWebForm : System.Web.UI.Page
    {
        BlanketServiceReference.BlanketWebServiceSoapClient obj = new BlanketServiceReference.BlanketWebServiceSoapClient();
        DistributerInventoryServiceReference.DistributerInventoryWebServiceSoapClient obj1 = new DistributerInventoryServiceReference.DistributerInventoryWebServiceSoapClient();
        SellerManagementServiceReference.SellerManagementWebServiceSoapClient obj2 = new SellerManagementServiceReference.SellerManagementWebServiceSoapClient();
        SellerRequestsServiceReference.SellerRequestsWebServiceSoapClient obj3 = new SellerRequestsServiceReference.SellerRequestsWebServiceSoapClient();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserType"] == null || Session["UserType"].ToString() != "distributer")
            {
                Response.Redirect("../Account/LoginWebForm.aspx");
                return;
            }

            if (!IsPostBack)
            {
                string distributerId = Session["UserId"].ToString();
                System.Diagnostics.Debug.WriteLine($"Distributer ID: {distributerId}");

                LoadBlanketDropdown();
                LoadInventoryGrid();
                LoadPendingRequests();
            }
        }

        private void LoadBlanketDropdown()
        {
            DataSet dsModels = obj.getBlanketModel();
            dlBlanketModel.DataSource = dsModels;
            dlBlanketModel.DataValueField = "BlanketID";
            dlBlanketModel.DataTextField = "BlanketModel";
            dlBlanketModel.DataBind();
            dlBlanketModel.Items.Insert(0, new ListItem("-- All Blankets --", ""));
        }

        private void LoadInventoryGrid()
        {
            string distributerId = Session["UserId"].ToString();
            try
            {
                DataSet ds = obj1.GetDistributerInventory(distributerId);
                gvInventory.DataSource = ds;
                gvInventory.DataBind();

                if (gvInventory.Rows.Count > 0)
                {
                    gvInventory.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
                else
                {
                    lblInventoryMessage.Text = "No inventory items found";
                    lblInventoryMessage.Visible = true;
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Error loading inventory: " + ex.Message, false);
                System.Diagnostics.Debug.WriteLine("LoadInventoryGrid error: " + ex);
            }
        }

        private void LoadPendingRequests(string blanketId = "")
        {
            string distributerId = Session["UserId"].ToString();
            try
            {
                DataSet assignedSellers = obj2.GetSellersByDistributer(distributerId);

                if (assignedSellers.Tables[0].Rows.Count == 0)
                {
                    lblRequestsMessage.Text = "No sellers assigned to your account";
                    lblRequestsMessage.Visible = true;
                    gvPendingRequests.DataSource = null;
                    gvPendingRequests.DataBind();
                    return;
                }

                List<string> sellerIds = new List<string>();
                foreach (DataRow row in assignedSellers.Tables[0].Rows)
                {
                    sellerIds.Add(row["SellerID"].ToString());
                }
                string sellerIdList = string.Join(",", sellerIds);

                DataSet pendingRequests;
                if (string.IsNullOrEmpty(blanketId))
                {
                    pendingRequests = obj3.GetPendingRequestsBySellerList(sellerIdList);
                }
                else
                {
                    pendingRequests = obj3.GetPendingRequestsBySellerListAndBlanket(sellerIdList, blanketId);
                }

                if (pendingRequests.Tables[0].Rows.Count > 0)
                {
                    gvPendingRequests.DataSource = pendingRequests;
                    gvPendingRequests.DataBind();
                    gvPendingRequests.HeaderRow.TableSection = TableRowSection.TableHeader;
                    lblRequestsMessage.Visible = false;
                }
                else
                {
                    gvPendingRequests.DataSource = null;
                    gvPendingRequests.DataBind();
                    lblRequestsMessage.Text = "No pending requests found";
                    lblRequestsMessage.Visible = true;
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Error loading pending requests: " + ex.Message, false);
                System.Diagnostics.Debug.WriteLine("LoadPendingRequests error: " + ex);
            }
        }

        protected void dlBlanketModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedBlanketId = dlBlanketModel.SelectedValue;
            if (string.IsNullOrEmpty(selectedBlanketId))
            {
                LoadPendingRequests(); 
            }
            else
            {
                LoadPendingRequests(selectedBlanketId); 
            }
        }

        protected void gvPendingRequests_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox txtDate = (TextBox)e.Row.FindControl("txtRequestDate");
                if (txtDate != null)
                {
                    DataRowView rowView = (DataRowView)e.Row.DataItem;
                    if (rowView["UpdateDate"] != DBNull.Value)
                    {
                        DateTime updateDate = Convert.ToDateTime(rowView["UpdateDate"]);
                        txtDate.Text = updateDate.ToString("yyyy-MM-dd");
                    }
                    else
                    {
                        txtDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    }
                }

                TextBox txtNotes = (TextBox)e.Row.FindControl("txtRequestNotes");
                if (txtNotes != null)
                {
                    DataRowView rowView = (DataRowView)e.Row.DataItem;
                    txtNotes.Text = rowView["Note"] != DBNull.Value ? rowView["Note"].ToString() : "";
                }
            }
        }

        private void ShowMessage(string message, bool isSuccess)
        {
            lblMessage.Text = message;
            lblMessage.CssClass = isSuccess ? "alert alert-success" : "alert alert-danger";
            lblMessage.Visible = true;
        }

        protected void gvPendingRequests_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Approve" || e.CommandName == "Reject")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvPendingRequests.Rows[rowIndex];
                string requestId = gvPendingRequests.DataKeys[rowIndex].Value.ToString();

                TextBox txtDate = (TextBox)row.FindControl("txtRequestDate");
                TextBox txtNotes = (TextBox)row.FindControl("txtRequestNotes");

                DateTime actionDate;
                if (!DateTime.TryParse(txtDate.Text, out actionDate))
                {
                    ShowMessage("Invalid date format. Please use YYYY-MM-DD", false);
                    return;
                }

                string notes = txtNotes.Text;

                if (e.CommandName == "Approve")
                {
                    ApproveRequest(requestId, actionDate, notes);
                }
                else if (e.CommandName == "Reject")
                {
                    RejectRequest(requestId, actionDate, notes);
                }
            }
        }

        private void ApproveRequest(string requestId, DateTime actionDate, string notes)
        {
            try
            {
                DataSet requestDetails = obj3.GetSellerRequestById(requestId);
                if (requestDetails.Tables[0].Rows.Count == 0)
                {
                    ShowMessage("Request not found", false);
                    return;
                }

                DataRow request = requestDetails.Tables[0].Rows[0];
                string sellerId = request["SellerID"].ToString();
                string blanketId = request["BlanketID"].ToString();
                int requestQty = Convert.ToInt32(request["RequestQty"]);
                string distributerId = Session["UserId"].ToString();

                DataSet distributerInventory = obj1.GetDistributerInventoryByBlanket(distributerId, blanketId);
                if (distributerInventory.Tables[0].Rows.Count == 0)
                {
                    ShowMessage("You don't have this blanket in your inventory", false);
                    return;
                }

                int distributerQty = Convert.ToInt32(distributerInventory.Tables[0].Rows[0]["CurrentQty"]);
                if (distributerQty < requestQty)
                {
                    ShowMessage($"Not enough inventory. You only have {distributerQty} available", false);
                    return;
                }

                bool updateSuccess = obj3.UpdateSellerRequestStatus(
                    requestId,
                    "Confirmed",
                    actionDate,
                    notes
                );

                if (!updateSuccess)
                {
                    ShowMessage("Failed to update request status", false);
                    return;
                }

                bool distributerUpdate = obj1.UpdateDistributerInventoryQty(
                    distributerId,
                    blanketId,
                    -requestQty
                );

                if (!distributerUpdate)
                {
                    ShowMessage("Failed to update distributer inventory", false);
                    return;
                }

                bool sellerUpdate = obj3.UpdateSellerInventoryQty(
                    sellerId,
                    blanketId,
                    requestQty
                );

                if (!sellerUpdate)
                {
                    ShowMessage("Failed to update seller inventory", false);
                    return;
                }

                ShowMessage("Request approved and inventory updated successfully", true);

                LoadInventoryGrid();
                LoadPendingRequests(dlBlanketModel.SelectedValue);
            }
            catch (Exception ex)
            {
                ShowMessage("Error approving request: " + ex.Message, false);
                System.Diagnostics.Debug.WriteLine("Approve error: " + ex.ToString());
            }
        }

        private void RejectRequest(string requestId, DateTime actionDate, string notes)
        {
            try
            {
                bool updateSuccess = obj3.UpdateSellerRequestStatus(
                    requestId,
                    "Rejected",
                    actionDate,
                    notes
                );

                if (updateSuccess)
                {
                    ShowMessage("Request rejected successfully", true);
                }
                else
                {
                    ShowMessage("Failed to reject request", false);
                }

                LoadPendingRequests(dlBlanketModel.SelectedValue);
            }
            catch (Exception ex)
            {
                ShowMessage("Error rejecting request: " + ex.Message, false);
            }
        }

       
    }
}