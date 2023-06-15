using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class lookup_proposal : System.Web.UI.Page
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
        string type = "";
        if (Request.QueryString["reportType"] == "FG")
        {
            type = "FG";

        }
        else if (Request.QueryString["reportType"] == "DC")
        {
            type = "CH";
        }
        else
        {
            type = "";
        }
        arr.Add(new cArrayList("@type", type));
        arr.Add(new cArrayList("@month", Request.QueryString["month"]));
        arr.Add(new cArrayList("@year", Request.QueryString["year"]));
        bll.vBindingGridToSp(ref grd, "sp_getProposal", arr);
    }
    protected void grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd.PageIndex = e.NewPageIndex;
        vBindingGrid();
        //bll.vBindingGridToSp(ref grd, "sp_tmst_salesorder_get", arr);
    }
    protected void cbstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        vBindingGrid();
    }
    protected void btsearch_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@prop_no", txsearchprop.Text));
        arr.Add(new cArrayList("@month", Request.QueryString["month"]));
        arr.Add(new cArrayList("@year", Request.QueryString["year"]));
        bll.vBindingGridToSp(ref grd, "sp_tmst_discount_search", arr);
    }
}