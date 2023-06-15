using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class lookup_mst_Supplier : System.Web.UI.Page
{
    cbll bll = new cbll();
    cdal cdl = new cdal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindControl();
        }
    }

    private void bindControl()
    {
       BindGrid();
    }

    protected void btsearch_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void grd_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Label lblAssetno = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lblSupplierCode");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "window.opener.SelectData('" + lblAssetno.Text + "');window.close();", true);
    }
    protected void grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd.PageIndex = e.NewPageIndex;
        BindGrid();
    }
    private void BindGrid()
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@supplier_cd", Convert.ToString(txtSupplier.Text)));
        bll.vBindingGridToSp(ref grd, "sp_tacc_mst_supplier_get", arr);
    }
}