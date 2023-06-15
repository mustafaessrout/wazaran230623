using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_claimpayment_app : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            bll.vBindingGridToSp(ref grdpayment, "sp_tclaim_payment_wrk_get", arr);
            this.RegisterPostBackControl();
        }
    }
    private void RegisterPostBackControl()
    {
        foreach (GridViewRow row in grdpayment.Rows)
        {
            Button btnApprove = row.FindControl("btnApprove") as Button;
            ScriptManager.GetCurrent(this).RegisterPostBackControl(btnApprove);
        }
    }
    protected void grdpayment_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdpayment.PageIndex = e.NewPageIndex;
        List<cArrayList> arr = new List<cArrayList>();
        bll.vBindingGridToSp(ref grdpayment, "sp_tclaim_payment_wrk_get", arr);
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        int index = Convert.ToInt32(((sender as Button).NamingContainer as GridViewRow).RowIndex);
        GridViewRow row = grdpayment.Rows[index];
        Label lbclhno = (Label)grdpayment.Rows[index].FindControl("lbclhno");
        arr.Add(new cArrayList("@clh_no", lbclhno.Text));
        arr.Add(new cArrayList("@approve_by", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@status", "A"));
        bll.vAppPaymentClaim(arr);
    }
}