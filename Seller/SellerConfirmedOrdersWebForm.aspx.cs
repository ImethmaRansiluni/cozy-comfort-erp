using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Register_Login.Seller
{
    public partial class SellerConfirmedOrdersWebForm : System.Web.UI.Page
    {
        OrderServiceReference.OrderWebServiceSoapClient obj = new OrderServiceReference.OrderWebServiceSoapClient();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserType"] == null || Session["UserType"].ToString() != "seller")
                {
                    Response.Redirect("../Account/LoginWebForm.aspx");
                    return;
                }

                LoadConfirmedOrders();
            }
        }

        private void LoadConfirmedOrders()
        {
            try
            {
                string sellerId = Session["UserId"].ToString();
                DataSet ds = obj.GetConfirmedOrdersBySeller(sellerId);

                gvOrders.DataSource = ds;
                gvOrders.DataBind();

                if (gvOrders.Rows.Count == 0)
                {
                    gvOrders.EmptyDataText = "No confirmed orders found";
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error loading confirmed orders: " + ex.Message);
            }
        }
    }
}