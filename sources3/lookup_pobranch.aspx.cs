using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class lookup_pobranch : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingComboToSp(ref cbsp, "sp_tmst_salespoint_get", "salespointcd","salespoint_nm");
            cbsp_SelectedIndexChanged(sender, e);
            //List<cArrayList> arr = new List<cArrayList>();
            //arr.Add(new cArrayList("@po_sta_id", "N"));
            //bll.vBindingGridToSp(ref grd, "sp_tmst_po_get", arr);
            
        }
    }
    protected void cbsp_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salespointcd", cbsp.SelectedValue.ToString()));
        arr.Add(new cArrayList("@po_sta_id", "N"));
        bll.vBindingGridToSp(ref grd, "sp_tmst_po_getbysalespoint", arr);
    }
    protected void grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd.PageIndex = e.NewPageIndex;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salespointcd", cbsp.SelectedValue.ToString()));
        arr.Add(new cArrayList("@po_sta_id", "N"));
        bll.vBindingGridToSp(ref grd, "sp_tmst_po_getbysalespoint", arr);
    }
}