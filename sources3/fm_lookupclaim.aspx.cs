using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_lookupclaim : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@word", txsearchprop.Text));
            bll.vBindingGridToSp(ref grd, "sp_tproposal_paid_get", arr);
        }
    }
    protected void btsearch_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@word", txsearchprop.Text));
        bll.vBindingGridToSp(ref grd, "sp_tproposal_paid_get", arr);
    }
    protected void grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd.PageIndex = e.NewPageIndex;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@word", txsearchprop.Text));
        bll.vBindingGridToSp(ref grd, "sp_tproposal_paid_get", arr);
    }
}