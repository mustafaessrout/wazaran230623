using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using AjaxControlToolkit;
public partial class frmWHSHO : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { 
            bll.vBindingFieldValueToCombo(ref cbwhsHOGrpCD, "whsHOGrpCD");
            cbwhsHOGrpCD.Enabled = true;
        }
    }
    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("frmWHSHO.aspx");
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        if (txwhsHOName.Text=="")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "alert('Warehouse Name Mus Be Fill !');", true);
            return;
        }
        if (txKey.Text == null || txKey.Text == "")
        {

            string sWHSHOCD = "";
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@whsHOName", txwhsHOName.Text));
            arr.Add(new cArrayList("@whsHOGrpCD", cbwhsHOGrpCD.SelectedValue));
            bll.vInserttblWHSHO(arr, ref sWHSHOCD);
            txwhsHOCD.Text = sWHSHOCD;
            txKey.Text = bll.vLookUp("select WHSHOID from tblWHSHO where WHSHOCD='" + sWHSHOCD + "'");
        }
        else
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@whsHOID", txKey.Text));
            arr.Add(new cArrayList("@whsHOName",txwhsHOName.Text));
            bll.vUpdatetblwhsHO(arr);
        }
        cbwhsHOGrpCD.CssClass = "makeitreadonly";
        cbwhsHOGrpCD.Enabled = false;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al2", "alert('Warehouse HO has been saved successfully ...');", true);
    }
    protected void bttmp_Click(object sender, EventArgs e)
    {
        SqlDataReader rs = null;
        txKey.Text = Convert.ToString(Session["loowhsHOID"]);
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@whsHOID", txKey.Text));
        arr.Add(new cArrayList("@whsHOName", null));
        bll.vGettblwhsHO(arr, ref rs);
        while (rs.Read())
        {
            txwhsHOCD.Text = rs["whsHOCD"].ToString();
            txwhsHOName.Text = rs["whsHOName"].ToString();
            cbwhsHOGrpCD.SelectedValue = rs["whsHOGrpCD"].ToString();
        } rs.Close();
        cbwhsHOGrpCD.CssClass = "makeitreadonly";
        cbwhsHOGrpCD.Enabled = false;

    }

    protected void btDelete_Click(object sender, EventArgs e)
    {
        if (txKey.Text != "")
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@whsHOID", txKey.Text));
            bll.vDeletetblwhsHO(arr);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "window.alert('Data Deleted successfully !')", true);
            Response.Redirect("frmWHSHO.aspx");
        }
    }
}