using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using AjaxControlToolkit;
public partial class lookup_SalesTargetSP : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btsearch_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
        arr.Add(new cArrayList("@target_no", txsearch.Text));
        bll.vBindingGridToSp(ref grd, "sp_tsalestargetsp_get2", arr);

    }
    protected void btok_Click(object sender, EventArgs e)
    {
       
    }
    protected void grd_SelectedIndexChanged(object sender, EventArgs e)
    {
        Label lbtarget_no = (Label)grd.SelectedRow.FindControl("lbtarget_no");
        Label lbsalespointCD = (Label)grd.SelectedRow.FindControl("lbsalespointCD");
        Session["lootarget_no"] = lbtarget_no.Text;
        Session["lootarget_salespointCD"] = lbsalespointCD.Text;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "updpnl", "closewin()", true);
    }
}