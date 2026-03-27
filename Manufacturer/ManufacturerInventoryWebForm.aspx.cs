using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Register_Login.Manufacturer
{
    public partial class ManufacturerInventoryWebForm : System.Web.UI.Page
    {
        NewIDServiceReference.GetNewIDWebServiceSoapClient obj = new NewIDServiceReference.GetNewIDWebServiceSoapClient();
        BlanketServiceReference.BlanketWebServiceSoapClient obj1 = new BlanketServiceReference.BlanketWebServiceSoapClient();
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
                txtInventoryID.Text = obj.autoManufacturerInventoryID();
                LoadInventoryGrid();
            }
        }

        private void LoadBlanketDropdown()
        {
            DataSet dsModels = obj1.getBlanketModel();
            dlBlanketModel.DataSource = dsModels;
            dlBlanketModel.DataValueField = "BlanketID";
            dlBlanketModel.DataTextField = "BlanketModel";
            dlBlanketModel.DataBind();
            dlBlanketModel.Items.Insert(0, new ListItem("-- Select Model --", ""));
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string inventoryId = txtInventoryID.Text.Trim();
                string blanketId = dlBlanketModel.SelectedValue;

                if (!ValidateQuantity())
                    return;

                string existingInventoryId = obj2.GetManufacturerInventoryId(blanketId);

                if (existingInventoryId == "notfound")
                {
                    AddNewInventory(inventoryId, blanketId);
                }
                else
                {
                    UpdateExistingInventory(existingInventoryId, true);
                }

                LoadInventoryGrid();
            }
            catch (Exception ex)
            {
                ShowMessage("System error: " + ex.Message, false);
            }
        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                string blanketId = dlBlanketModel.SelectedValue;

                if (!ValidateQuantity())
                    return;

                string inventoryId = obj2.GetManufacturerInventoryId(blanketId);

                if (inventoryId == "notfound" || inventoryId.StartsWith("error"))
                {
                    ShowMessage("No inventory found for this blanket", false);
                    return;
                }

                UpdateExistingInventory(inventoryId, false);
                LoadInventoryGrid();
            }
            catch (Exception ex)
            {
                ShowMessage("System error: " + ex.Message, false);
            }
        }

        private bool ValidateQuantity()
        {
            if (!int.TryParse(txtQty.Text, out int qty) || qty <= 0)
            {
                ShowMessage("Please enter a valid quantity", false);
                return false;
            }
            return true;
        }

        private void AddNewInventory(string inventoryId, string blanketId)
        {
            int qty = int.Parse(txtQty.Text);
            string result = obj2.AddManufacturerInventory(inventoryId, blanketId, qty);

            if (result.StartsWith("error"))
            {
                ShowMessage("Error adding inventory: " + result.Replace("error:", ""), false);
            }
            else
            {
                ShowMessage("Inventory added successfully", true);
                txtInventoryID.Text = obj.autoManufacturerInventoryID();
            }
        }

        private void UpdateExistingInventory(string inventoryId, bool isAddition)
        {
            int qty = int.Parse(txtQty.Text);
            string operation = isAddition ? "increased" : "decreased";

            string result = obj2.UpdateManufacturerInventoryQuantity(inventoryId, qty, isAddition);

            if (result.StartsWith("error"))
            {
                ShowMessage("Error updating inventory: " + result.Replace("error:", ""), false);
            }
            else
            {
                ShowMessage($"Inventory quantity {operation} successfully", true);
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
                    ShowMessage("No inventory items found", true);
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Error loading inventory: " + ex.Message, false);
                System.Diagnostics.Debug.WriteLine("LoadInventoryGrid error: " + ex);
            }
        }

        private void ShowMessage(string message, bool isSuccess)
        {
            lblMessage.Text = message;
            lblMessage.CssClass = isSuccess ? "message success" : "message error";
            lblMessage.Visible = true;
        }
    }
}