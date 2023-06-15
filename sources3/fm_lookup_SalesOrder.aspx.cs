using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_lookup_SalesOrder : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btsearch_Click(object sender, EventArgs e)
    {
        string sSrc = Request.QueryString["src"].ToString();
        Int32 stranType;
        if (sSrc=="Canvass")
        {
            stranType = 3;
        }
        else
        {
            stranType = 1;
        }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
        arr.Add(new cArrayList("@SOCD", txsearch.Text));
        arr.Add(new cArrayList("@tranType", stranType));
        bll.vBindingGridToSp(ref grd, "sp_tblso_get5", arr);

    }
    protected void btok_Click(object sender, EventArgs e)
    {
       
    }
    protected void grd_SelectedIndexChanged(object sender, EventArgs e)
    {
        Label lbSOID = (Label)grd.SelectedRow.FindControl("lbSOID");
        Label lbsalespointCD = (Label)grd.SelectedRow.FindControl("lbsalespointCD");


        Session["looSOSOID"] = lbSOID.Text;
        Session["looSOSalespointCD"] = lbsalespointCD.Text;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "updpnl", "closewin()", true);
    }
}