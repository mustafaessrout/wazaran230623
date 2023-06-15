using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class lookup_proposal4 : System.Web.UI.Page
{
    cbll bll = new cbll();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            vBindingGrid();
        }
    }
    void vBindingGrid()
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@month", Request.QueryString["month"]));
        arr.Add(new cArrayList("@year", Request.QueryString["year"]));
        bll.vBindingGridToSp(ref grd, "sp_getProposalContract", arr);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix()", true);
    }

    protected void btsearch_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@prop_no", txsearchprop.Text));
        arr.Add(new cArrayList("@month", Request.QueryString["month"]));
        arr.Add(new cArrayList("@year", Request.QueryString["year"]));
        bll.vBindingGridToSp(ref grd, "sp_getProposalContract", arr);
    }
    protected void grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        vBindingGrid();
    }
}