using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_lookup_return1 : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingFieldValueToCombo(ref cbstatus, "retur_sta_id");
            cbstatus_SelectedIndexChanged(sender, e);
          //  bll.vBindingGridToSp(ref grd, "sp_tsalesreturn_get");
        }
    }
    protected void btsearch_Click(object sender, EventArgs e)
    {

    }
    protected void grd_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Label lbreturno = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbreturno");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "clo", "closewin('" + lbreturno.Text + "');", true);
    }
    protected void cbstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@retur_sta_id", cbstatus.SelectedValue.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_tsalesreturn_get", arr);
    }
    protected void grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd.PageIndex = e.NewPageIndex;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@retur_sta_id", cbstatus.SelectedValue.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_tsalesreturn_get", arr);
    }
}