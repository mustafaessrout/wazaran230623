using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_mstpaydisc : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            bll.vBindingComboToSp(ref cbsp, "sp_tmst_salespoint_get","salespointcd","salespoint_nm");
            bll.vDelWrkSalespoint(arr);
        }
    }
    protected void btaddsp_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        if (chall.Checked)
        {
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            bll.vInsertWrkSalespointAll(arr);
        }
        else
        {
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", cbsp.SelectedValue.ToString()));
            bll.vInsertWrkSalespoint(arr);
        }
        arr.Clear();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grdsp, "sp_twrk_salespoint_get", arr);
    }
    protected void grdsp_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        Label lbsalespointcd = (Label) grdsp.Rows[e.RowIndex].FindControl("lbsalespointcd");
        arr.Add(new cArrayList("@salespointcd", lbsalespointcd.Text));
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vDelWrkSalespoint(arr);
        arr.Clear();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grdsp, "sp_twrk_salespoint_get", arr);
    }
    protected void btsave_Click(object sender, EventArgs e)
    {

    }
}