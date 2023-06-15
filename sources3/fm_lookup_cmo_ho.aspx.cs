using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_lookup_cmo_ho : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            cbstatus.SelectedValue = "N";
            vBindingGrid();
        }
    }

    void vBindingGrid()
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@cmo_no", txsearch.Text));
        arr.Add(new cArrayList("@status", cbstatus.SelectedValue.ToString()));
        //bll.vBindingGridToSp(ref grd, "sp_tpch_cmo_get", arr);
    }

    protected void btsearch_Click(object sender, EventArgs e)
    {
        vBindingGrid();
    }

    protected void cbstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        vBindingGrid();
    }

    protected void grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd.PageIndex = e.NewPageIndex;
        vBindingGrid();
    }
}