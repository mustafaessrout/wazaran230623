using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_mstitemlist : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            vInitGrid();
            bll.vLang(ref btprint);
        }
    }

    void vInitGrid()
    {
        bll.vBindingGridToSp(ref grd, "sp_tmst_item_get");
    }
    protected void btadd_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_mstitementry.aspx");
    }
    protected void btsearch_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@item_nm", txsearch.Text));
        bll.vBindingGridToSp(ref grd, "sp_tmst_item_search", arr);
    }
    protected void grd_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Label lbitemcode = (Label)grd.Rows[e.NewEditIndex].FindControl("lbitemcode");
        Response.Redirect("fm_mstitementry.aspx?item=" + lbitemcode.Text);
    }
    protected void grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd.PageIndex = e.NewPageIndex;
        vInitGrid();
    }
}