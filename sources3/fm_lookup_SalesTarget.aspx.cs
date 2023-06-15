using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_lookup_SalesTarget : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btsearch_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@slsTargetCD", txsearch.Text));
        bll.vBindingGridToSp(ref grd, "sp_tblSalesTarget_get", arr);

    }
    protected void btok_Click(object sender, EventArgs e)
    {
       
    }
    protected void grd_SelectedIndexChanged(object sender, EventArgs e)
    {
        Label lbslsTargetCD = (Label)grd.SelectedRow.FindControl("lbslsTargetCD");
        Label lbsalespointCD = (Label)grd.SelectedRow.FindControl("lbsalespointCD");
        

        Session["looSalesTargetCD"] = lbslsTargetCD.Text;
        Session["looSalesTargetSalespointCD"] = lbsalespointCD.Text;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "updpnl", "closewin()", true);
    }
}