using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_paymentsuspense : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grd, "sp_tpayment_suspense_get",arr);
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabErr", "tabErr();", true);
    }
    public void checkboxclear()
    {
        foreach (GridViewRow row in grd.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkrow = (CheckBox)row.FindControl("chk");
                if (chkrow.Checked)
                    chkrow.Checked = false;
            }
        }
        //CheckBox chkrow1 = (CheckBox)grd.HeaderRow.FindControl("chk");
        //if (chkrow1.Checked)
        //    chkrow1.Checked = false;
    }
    protected void grd_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        //foreach (GridViewRow row in grd.Rows)
        //if (row.RowType == DataControlRowType.DataRow)
        //{
        //         CheckBox chk1 = (CheckBox)grd.Rows[e.NewSelectedIndex].FindControl("chk");
        //         chk1.Checked = false;
        //}
        checkboxclear();
        Label lbcustcode = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbcustcode");
        Label lbamtt = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbamt");
        Label lbbalance = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbbalance");
        Label lbrdpaidfor = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbrdpaidfor");
        CheckBox chk = (CheckBox)grd.Rows[e.NewSelectedIndex].FindControl("chk");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@cust_cd", lbcustcode.Text));
        arr.Add(new cArrayList("@rdpaidfor", lbrdpaidfor.Text));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        bll.vBindingGridToSp(ref grdinv, "sp_tdosales_invoice_getbycust2", arr);
        lbamt.Text = lbbalance.Text;
        chk.Checked = true;
        Label lbtotamt = (Label)grd.FooterRow.FindControl("lbtotamt");
    }
    protected void grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd.PageIndex = e.NewPageIndex;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_tpayment_suspense_get");
    }
    protected void btpaid_Click(object sender, EventArgs e)
    {
        double dPaid = Convert.ToDouble(lbamt.Text);
        double dBal = 0;
        foreach (GridViewRow row in grdinv.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                Label lbbalance = (Label)row.FindControl("lbbalance");
                TextBox txamt = (TextBox)row.FindControl("txamt");
                if (txamt.Text == "")
                { txamt.Text = "0"; }
                dBal = Convert.ToDouble(lbbalance.Text);
                if ((dPaid - dBal) == 0)
                {
                    txamt.Text = dBal.ToString();
                    dPaid = 0;
                }
                else if ((dPaid - dBal) > 0)
                {
                    txamt.Text = dBal.ToString();
                    dPaid -= dBal;
                }
                else if ((dPaid - dBal) < 0)
                {
                    txamt.Text = dPaid.ToString();
                    dPaid = 0;
                }
            }
        }
    }
    protected void btapply_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        double dpaid = Convert.ToDouble(lbamt.Text);
        double dAmtPaid = 0;
        foreach (GridViewRow row in grdinv.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                TextBox txamt = (TextBox)row.FindControl("txamt");
                dAmtPaid += Convert.ToDouble(txamt.Text);

            }
        }

        if (dpaid < dAmtPaid)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('Amount paid and Total to be paid can not less','Paid should >= will be paid','warning');", true);
            return;
        }

        if (dAmtPaid == 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt2", "sweetAlert('Amount There are no invoice to be paid','Please give value to invoice','warning');", true);
            return;
        }

        string sPaymentNo = string.Empty;
        Label lbpaymentno = (Label)grd.SelectedRow.FindControl("lbpaymentno");
        //arr.Add(new cArrayList("@payment_no_suspense", lbpaymentno.Text));
        //bll.vInsertTmstPaymentFromSuspense(arr, ref sPaymentNo);
        foreach (GridViewRow row in grdinv.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                Label lbinvoiceno = (Label)row.FindControl("lbinvno");
                TextBox txamt = (TextBox)row.FindControl("txamt");
                arr.Clear();
                arr.Add(new cArrayList("@payment_no", lbpaymentno.Text));
                arr.Add(new cArrayList("@inv_no", lbinvoiceno.Text));
                arr.Add(new cArrayList("@amt", txamt.Text));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                if (Convert.ToDouble(txamt.Text) > 0)
                {
                    bll.vInsertPaymentSuspenseDtl(arr);
                }
                arr.Clear();
            }
        }
        grd.CssClass = "table table-striped mygrid ro";
        grdinv.CssClass = "table table-striped mygrid ro";
        btapply.CssClass = "divhid";
        btnew.CssClass = "btn btn-success btn-add";
        btprint.CssClass = "btn btn-info btn-print";

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('New Payment suspense has been created','" + sPaymentNo + "','success');", true);
    }
    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_paymentsuspense.aspx");
    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        Label lbpaymentno = (Label)grd.SelectedRow.FindControl("lbpaymentno");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opr", "openreport('fm_report2.aspx?src=sus&pn=" + lbpaymentno.Text + "');", true);
    }
}