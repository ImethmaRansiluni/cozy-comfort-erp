using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Register_Login.Manufacturer
{
    public partial class BlanketsWebForm : System.Web.UI.Page
    {
        NewIDServiceReference.GetNewIDWebServiceSoapClient obj = new NewIDServiceReference.GetNewIDWebServiceSoapClient();
        MaterialServiceReference.MaterialWebServiceSoapClient obj1 = new MaterialServiceReference.MaterialWebServiceSoapClient();
        BlanketServiceReference.BlanketWebServiceSoapClient obj2 = new BlanketServiceReference.BlanketWebServiceSoapClient();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserType"] == null || Session["UserType"].ToString() != "manufacturer")
            {
                Response.Redirect("../Account/LoginWebForm.aspx");
                return;
            }

            if (!IsPostBack)
            {
                DataSet ds = obj1.getMaterialName();

                dlMaterial.DataSource = ds;
                dlMaterial.DataValueField = "MaterialName";
                dlMaterial.DataBind();

                txtBID.Text = obj.autoBlanketID();
                loadGrid();

            }
        }

        protected void btnAddBlanket_Click(object sender, EventArgs e)
        {
            string result = obj2.addBlanket(
              txtBID.Text,
              txtBModel.Text,
              obj1.getMaterialID(dlMaterial.Text),
              txtCapasity.Text,
              txtPrice.Text);
            int norecord = Int32.Parse(result);
            loadGrid();

            if (norecord > 0)
            {
                lblAddBlnket.Text = "record Successfuly added";
            }
            else
            {
                lblAddBlnket.Text = "record not added Try again";
            }
        }

        public void loadGrid()
        {
            DataSet ds = obj2.getBlanket();
            gvBlanket.DataSource = ds;
            gvBlanket.DataBind();
        }
    }
}