using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class lookpayment_ho : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            bll.vBindingFieldValueToCombo(ref cbstatus, "payment_sta_id");
            cbstatus_SelectedIndexChanged(sender, e);
          
        }
    }
    protected void cbstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@payment_typ", Request.QueryString["pt"]));
        arr.Add(new cArrayList("@payment_sta_id", cbstatus.SelectedValue.ToString()));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_tmst_payment_get", arr);
    }
    protected void grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd.PageIndex = e.NewPageIndex;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@payment_typ", Request.QueryString["pt"]));
        arr.Add(new cArrayList("@payment_sta_id", cbstatus.SelectedValue.ToString()));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_tmst_payment_get", arr);
    }
}