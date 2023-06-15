using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_lookup_return : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingFieldValueToCombo(ref cbstatus, "retur_sta_id");
        }
    }
    protected void btsearch_Click(object sender, EventArgs e)
    {
        //List<cArrayList> arr = new List<cArrayList>();
        //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
        //arr.Add(new cArrayList("@returnCD", txsearch.Text));
        //bll.vBindingGridToSp(ref grd, "sp_tblreturn_get", arr);

    }
    protected void btok_Click(object sender, EventArgs e)
    {
       
    }
    protected void grd_SelectedIndexChanged(object sender, EventArgs e)
    {
        Label lbreturnID = (Label)grd.SelectedRow.FindControl("lbreturnID");
        Label lbsalespointCD = (Label)grd.SelectedRow.FindControl("lbsalespointCD");


        Session["looReturnReturnID"] = lbreturnID.Text;
        Session["looReturnSalespointCD"] = lbsalespointCD.Text;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "updpnl", "closewin()", true);
    }
}