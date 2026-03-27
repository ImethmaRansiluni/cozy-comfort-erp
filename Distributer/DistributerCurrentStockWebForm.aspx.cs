using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Register_Login.Distributer
{
    public partial class DistributerCurrentStockWebForm : System.Web.UI.Page
    {
        DistributerInventoryServiceReference.DistributerInventoryWebServiceSoapClient obj = new DistributerInventoryServiceReference.DistributerInventoryWebServiceSoapClient();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserType"] == null || Session["UserType"].ToString() != "distributer")
            {
                Response.Redirect("../Account/LoginWebForm.aspx");
                return;
            }

            string sellerId = Session["UserId"].ToString();

            if (!IsPostBack)
            {
                LoadInventoryGrid();
            }
        }

        private void LoadInventoryGrid()
        {
            string distributerId = Session["UserId"].ToString();
            try
            {
                DataSet ds = obj.GetDistributerInventory(distributerId);
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