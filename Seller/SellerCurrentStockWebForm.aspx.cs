using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Register_Login.Seller
{
    public partial class SellerCurrentStockWebForm : System.Web.UI.Page
    {
        SellerInventoryServiceReference.SellerInventoryWebServiceSoapClient obj1 = 
            new SellerInventoryServiceReference.SellerInventoryWebServiceSoapClient();

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

        private void ShowMessage(string message, bool isSuccess)
        {
            lblMessage.Text = message;
            lblMessage.CssClass = isSuccess ? "alert alert-success" : "alert alert-danger";
            lblMessage.Visible = true;
        }
    }
}