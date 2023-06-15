using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_paymentlist : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingFieldValueToCombo(ref cbstatus, "payment_sta_id");
            cbstatus_SelectedIndexChanged(sender, e);
        }
    }
    protected void cbstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@payment_sta_id", cbstatus.SelectedValue.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_tmst_payment_get", arr);
    }
    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_paymentreceipt.aspx");
    }
    protected void grd_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grd.EditIndex = e.NewEditIndex;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@payment_sta_id", cbstatus.SelectedValue.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_tmst_payment_get", arr);
        DropDownList cbstatusd = (DropDownList)grd.Rows[e.NewEditIndex].FindControl("cbstatus");
        bll.vBindingFieldValueToCombo(ref cbstatusd, "payment_sta_id");
    }
    protected void grd_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grd.EditIndex = -1;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@payment_sta_id", cbstatus.SelectedValue.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_tmst_payment_get", arr);
    }
    protected void grd_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        Label lbpaymentno = (Label)grd.Rows[e.RowIndex].FindControl("lbpaymentno");
        DropDownList cbstatusd = (DropDownList)grd.Rows[e.RowIndex].FindControl("cbstatus");
        arr.Add(new cArrayList("@payment_no", lbpaymentno.Text));
        arr.Add(new cArrayList("@payment_sta_id", cbstatusd.SelectedValue.ToString()));
        bll.vUpdateMstPayment(arr);
        grd.EditIndex = -1;
        arr.Clear();
        arr.Add(new cArrayList("@payment_sta_id", cbstatus.SelectedValue.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_tmst_payment_get", arr);
    }
    protected void grd_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Label lbpaymentno = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbpaymentno");
        Response.Redirect("fm_paymentreceipt.aspx?pid=" + lbpaymentno.Text);
    }
}