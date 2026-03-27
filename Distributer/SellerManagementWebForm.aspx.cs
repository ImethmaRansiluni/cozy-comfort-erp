using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Register_Login.Distributer
{
    public partial class SellerManagementWebForm : System.Web.UI.Page
    {
        SellerManagementServiceReference.SellerManagementWebServiceSoapClient obj = new SellerManagementServiceReference.SellerManagementWebServiceSoapClient();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserType"] == null || Session["UserType"].ToString() != "distributer")
            {
                Response.Redirect("../Account/LoginWebForm.aspx");
                return;
            }

            if (!IsPostBack)
            {
                LoadAssignedSellers();
            }
        }

        private void LoadAssignedSellers()
        {
            string distributerId = Session["UserId"].ToString();
            DataSet ds = obj.GetAssignedSellers(distributerId);
            gvAssignedSellers.DataSource = ds;
            gvAssignedSellers.DataBind();
        }

        protected void btnAddSeller_Click(object sender, EventArgs e)
        {
            try
            {
                string sellerId = txtSellerID.Text.Trim();
                string sellerName = txtSellerName.Text.Trim();
                string distributerId = Session["UserId"].ToString();

                if (!obj.ValidateSeller(sellerId, sellerName))
                {
                    ShowMessage("Seller ID and Name don't match or seller doesn't exist", "error");
                    return;
                }

                if (obj.IsSellerAssigned(sellerId))
                {
                    ShowMessage("This seller is already assigned to another distributer", "error");
                    return;
                }

                string result = obj.AssignSeller(distributerId, sellerId);
                if (result == "success")
                {
                    ShowMessage("Seller assigned successfully", "success");
                    LoadAssignedSellers();
                    txtSellerID.Text = "";
                    txtSellerName.Text = "";
                }
                else
                {
                    ShowMessage("Error assigning seller: " + result, "error");
                }
            }
            catch (Exception ex)
            {
                ShowMessage("System error: " + ex.Message, "error");
            }
        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                string sellerManagementId = btn.CommandArgument;

                string result = obj.RemoveSellerAssignment(sellerManagementId);
                if (result == "success")
                {
                    ShowMessage("Seller removed successfully", "success");
                    LoadAssignedSellers();
                }
                else
                {
                    ShowMessage("Error removing seller: " + result, "error");
                }
            }
            catch (Exception ex)
            {
                ShowMessage("System error: " + ex.Message, "error");
            }
        }

        private void ShowMessage(string message, string type)
        {
            lblMessage.Text = message;
            lblMessage.CssClass = type == "error" ? "message error" : "message success";
            lblMessage.Visible = true;
        }
    }
}