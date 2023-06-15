using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_stocksample_out : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
            bll.vBindingComboToSpWithEmptyChoosen(ref cbsalespoint, "sp_tmst_salespoint_get", "salespointcd", "salespoint_nm");
            string _salespointcd = Request.Cookies["sp"].Value;
            cbsalespoint.SelectedValue = _salespointcd;
            cd.v_hiddencontrol(btprint);
            cd.v_hiddencontrol(btreceived);
        }
    }

    protected void cbsalespoint_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue));
        arr.Add(new cArrayList("@sample_sta_id", "A"));
        bll.vBindingGridToSp(ref grd, "sp_tstock_sample_getbysalespoint", arr);
    }

    protected void grd_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Label lbsamplecode = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbsamplecode");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@sample_cd", lbsamplecode.Text));
        bll.vBindingGridToSp(ref grddetail, "sp_tstocksample_dtl_get", arr);
    }

    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_stocksample_out.aspx");
    }
}