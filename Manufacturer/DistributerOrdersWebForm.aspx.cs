using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Register_Login.Manufacturer
{
    public partial class DistributerOrdersWebForm : System.Web.UI.Page
    {
        BlanketServiceReference.BlanketWebServiceSoapClient obj = new BlanketServiceReference.BlanketWebServiceSoapClient();
        DistributerRequestsServiceReference.DistributerRequestsWebServiceSoapClient obj1 = new DistributerRequestsServiceReference.DistributerRequestsWebServiceSoapClient();
        ManufacturerInventoryServiceReference.ManufacturerInventoryWebServiceSoapClient obj2 = new ManufacturerInventoryServiceReference.ManufacturerInventoryWebServiceSoapClient();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserType"] == null || Session["UserType"].ToString() != "manufacturer")
            {
                Response.Redirect("../Account/LoginWebForm.aspx");
                return;
            }

            if (!IsPostBack)
            {
                LoadBlanketDropdown();
                LoadPendingRequests();
                LoadInventoryGrid();
            }
        }

        private void LoadInventoryGrid()
        {
            try
            {
                DataSet ds = obj2.GetManufacturerInventory();
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

        private void LoadBlanketDropdown()
        {
            DataSet dsModels = obj.getBlanketModel();
            dlBlanketModel.DataSource = dsModels;
            dlBlanketModel.DataValueField = "BlanketID";
            dlBlanketModel.DataTextField = "BlanketModel";
            dlBlanketModel.DataBind();
            dlBlanketModel.Items.Insert(0, new ListItem("-- All Blankets --", ""));
        }

        private void LoadPendingRequests(string blanketId = "")
        {
            try
            {
                DataSet pendingRequests;
                if (string.IsNullOrEmpty(blanketId))
                {
                    pendingRequests = obj1.GetPendingDistributerRequests();
                }
                else
                {
                    pendingRequests = obj1.GetPendingDistributerRequestsByBlanket(blanketId);
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
                DataSet requestDetails = obj1.GetDistributerRequestById(requestId);
                if (requestDetails.Tables[0].Rows.Count == 0)
                {
                    ShowMessage("Request not found", false);
                    return;
                }

                DataRow request = requestDetails.Tables[0].Rows[0];
                string distributerId = request["DistributerID"].ToString();
                string blanketId = request["BlanketID"].ToString();
                int requestQty = Convert.ToInt32(request["RequestQty"]);

                DataSet manufacturerInventory = obj2.GetManufacturerInventory();
                DataRow[] inventoryRows = manufacturerInventory.Tables[0].Select($"BlanketID = '{blanketId}'");

                if (inventoryRows.Length == 0)
                {
                    ShowMessage("This blanket is not available in manufacturer inventory", false);
                    return;
                }

                int currentStock = Convert.ToInt32(inventoryRows[0]["CurrentQty"]);
                if (currentStock < requestQty)
                {
                    ShowMessage($"Cannot approve request. Only {currentStock} items available, but {requestQty} requested.", false);
                    return;
                }

                bool updateSuccess = obj1.UpdateDistributerRequestStatus(
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

                bool inventoryUpdate = obj1.UpdateManufacturerInventoryQty(
                    blanketId,
                    -requestQty
                );

                if (!inventoryUpdate)
                {
                    ShowMessage("Failed to update manufacturer inventory", false);
                    return;
                }

                bool distributerUpdate = obj1.UpdateDistributerInventoryQty(
                    distributerId,
                    blanketId,
                    requestQty
                );

                if (!distributerUpdate)
                {
                    ShowMessage("Failed to update distributer inventory", false);
                    return;
                }

                ShowMessage("Request approved and inventory updated successfully", true);
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
                bool updateSuccess = obj1.UpdateDistributerRequestStatus(
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