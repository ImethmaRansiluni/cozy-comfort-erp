using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Register_Login.Manufacturer
{
    public partial class MaterialsWebForm : System.Web.UI.Page
    {
        NewIDServiceReference.GetNewIDWebServiceSoapClient obj = new NewIDServiceReference.GetNewIDWebServiceSoapClient();
        MaterialServiceReference.MaterialWebServiceSoapClient obj1 = new MaterialServiceReference.MaterialWebServiceSoapClient();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserType"] == null || Session["UserType"].ToString() != "manufacturer")
            {
                Response.Redirect("../Account/LoginWebForm.aspx");
                return;
            }

            txtMID.Text = obj.autoMaterialID();
            if (!IsPostBack)
            {
                loadGrid();
            }
        }

        protected void btnAddMaterial_Click(object sender, EventArgs e)
        {
            string value = obj1.insertMaterialInfo(txtMID.Text, txtMName.Text);
            int norecord = Int32.Parse(value);

            loadGrid();

            if (norecord > 0)
            {
                lblM.Text = "record Successfuly added";
            }
            else
            {
                lblM.Text = "record not added Try again";
            }
        }

        public void loadGrid()
        {
            DataSet ds = obj1.searchMaterial();
            gvMaterial.DataSource = ds;
            gvMaterial.DataBind();
        }
    }
}