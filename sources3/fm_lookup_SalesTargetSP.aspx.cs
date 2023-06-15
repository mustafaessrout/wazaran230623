using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using AjaxControlToolkit;
public partial class fm_lookup_SalesTargetSP : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btsearch_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@slsTargetSPCD", txsearch.Text));
        bll.vBindingGridToSp(ref grd, "sp_tblSalesTargetSP_get", arr);

    }
    protected void btok_Click(object sender, EventArgs e)
    {
       
    }
    protected void grd_SelectedIndexChanged(object sender, EventArgs e)
    {
        Label lbslsTargetSPID = (Label)grd.SelectedRow.FindControl("lbslsTargetSPID");

        Session["looSalesTargetSPID"] = lbslsTargetSPID.Text;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "updpnl2", "closewin2()", true);
    }
}