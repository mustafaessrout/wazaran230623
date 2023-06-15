using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_lookup_investigation : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        lbsalespointcd.Text = Convert.ToString(Session["looivgsp"]);
    }
    protected void btsearch_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@ivgno",  txsearch.Text));
        arr.Add(new cArrayList("@salespointcd", lbsalespointcd.Text));
        bll.vBindingGridToSp(ref grd, "sp_tinvestigation_get", arr);
    }

    protected void grd_SelectedIndexChanged(object sender, EventArgs e)
    {
        Label lbivgno = (Label)grd.SelectedRow.FindControl("lbivgno");
        //Label lbinvtype = (Label)grd.SelectedRow.FindControl("lbinvtype");

        Session["looivgno"] = lbivgno.Text;
        //Session["lootrnstkinvtype"] = lbinvtype.Text;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "updpnl", "closewin()", true);
    }
}