using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_lookup_adjustPrice : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btsearch_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@adjp_cd", txsearch.Text));
        bll.vBindingGridToSp(ref grd, "sp_titem_adjustment_price_get", arr);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "fixTab", "fixTab()", true);
    }

    protected void grd_SelectedIndexChanged(object sender, EventArgs e)
    {
        Label lbadjp_cd = (Label)grd.SelectedRow.FindControl("lbadjp_cd");

        Session["looadjp_cd"] = lbadjp_cd.Text;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "updpnl", "closewin()", true);
    }
}