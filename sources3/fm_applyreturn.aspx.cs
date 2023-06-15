using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_applyreturn : System.Web.UI.Page
{
    cbll bll = new cbll();

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {

            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@payment_sta_id", "R"));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grd, "sp_tmst_payment_getbyreturn",arr);
            if (Request.QueryString["ids"] != null)
            {
                arr.Clear();
                string sPaymentNO = Request.QueryString["ids"];
                string sCustNo = bll.vLookUp("select cust_cd from tmst_payment where payment_no='" + sPaymentNO + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");
                string sAvailPaid = bll.vLookUp("select totamt from tmst_payment where payment_no='" + sPaymentNO + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");
                arr.Add(new cArrayList("@cust_cd", sCustNo));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vBindingGridToSp(ref grdinv, "sp_tdosales_invoice_getbycust", arr);
                btsave.Visible = false;
            }
        }
    }

    void BindingGridSales()
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_tmst_payment_getbyreturn",arr);
    }
    protected void grd_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        txdisc.Text = "0";
        txdisc.Enabled = true;
        List<cArrayList> arr = new List<cArrayList>();
        Label lbpaymentno = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbpaymentno");
        string sCustNo = bll.vLookUp("select cust_cd from tmst_payment where payment_no='" + lbpaymentno.Text + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");
        string sAvailPaid = bll.vLookUp("select totamt from tmst_payment where payment_no='" + lbpaymentno.Text + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");
        arr.Add(new cArrayList("@cust_cd", sCustNo));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        bll.vBindingGridToSp(ref grdinv, "sp_tdosales_invoice_getbycust_return", arr);
        lbavailpaid.Text = sAvailPaid;
        btsave.Visible = true;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tableScroll", "tableScroll();", true);
    }
    protected void btpaid_Click(object sender, EventArgs e)
    {
        decimal dBalance = 0;
        if ((lbavailpaid.Text == "") || (lbavailpaid.Text == "0"))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Amount is empty','Select item to be returned','success');", true);
            return;
        }

        if (grdinv.Rows.Count == 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('No Invoices to be paid','Payment will transferred to suspense','success');", true);
            return;
        }
        if (txdisc.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Return Discount can't be empty','Please insert discount','success');", true);
            return;
        }
        decimal dpaid = Convert.ToDecimal(lbavailpaid.Text) - Convert.ToDecimal(txdisc.Text);
        decimal dpaid1 = 0; decimal dTempBalance = 0;
        txdisc.Enabled = false;
        foreach (GridViewRow row in grdinv.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                Label lbbalance = (Label)row.FindControl("lbbalance");
                Label lbtembalance = (Label)row.FindControl("lbtempbalance");
                TextBox txpaid = (TextBox)row.FindControl("txpaid");
                dBalance = Convert.ToDecimal(lbbalance.Text);
                dTempBalance = Convert.ToDecimal(lbtembalance.Text);
              //  if ((dpaid - dBalance) > 0)
                if ((dpaid - dTempBalance) > 0)
                {
                   // txpaid.Text = dBalance.ToString();
                    txpaid.Text = dTempBalance.ToString();
                }
                else if (dpaid > 0)
                {
                    txpaid.Text = dpaid.ToString();

                }
                else if (dpaid <= 0)
                {
                    txpaid.Text = "0";
                }
                //dpaid = dpaid - dBalance;
                dpaid = dpaid - dTempBalance;
                dpaid1 = dpaid1 + Convert.ToDecimal(txpaid.Text);
            }
            lbsuspense.Text = (((Convert.ToDecimal(lbavailpaid.Text) - Convert.ToDecimal(txdisc.Text)) - dpaid1)).ToString();
            //lbsuspense.Text =  Convert.ToString()-);
        }
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        try
        {
            decimal dpaid = Convert.ToDecimal(lbavailpaid.Text) - Convert.ToDecimal(txdisc.Text);
            decimal dpaid1 = 0;
            txdisc.Enabled = false;
            foreach (GridViewRow row in grdinv.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    Label lbbalance = (Label)row.FindControl("lbbalance");
                    TextBox txpaid = (TextBox)row.FindControl("txpaid");
                    if (Convert.ToDecimal(lbbalance.Text) < 0 || Convert.ToDecimal(txpaid.Text) < 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Payment Should not be Minus','Please contact wazaran Admin','warning');", true);
                        return;
                    }
                    if (Convert.ToDecimal(txpaid.Text) > Convert.ToDecimal(lbbalance.Text))
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Invoice balance Should not be Minus','Please contact wazaran Admin','warning');", true);
                        return;
                    }
                    dpaid1 = dpaid1 + Convert.ToDecimal(txpaid.Text);
                }
                lbsuspense.Text = (((Convert.ToDecimal(lbavailpaid.Text) - Convert.ToDecimal(txdisc.Text)) - dpaid1)).ToString();
                //lbsuspense.Text =  Convert.ToString()-);
            }
            if (dpaid1 == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You should pay at least one invoice','No Invoice Paid','warning');", true);
                return;
            }
            List<cArrayList> arr = new List<cArrayList>();
            decimal dPaid = 0; decimal dDisc = 0; decimal dPaidTotal = 0; decimal dPaidDiscTotal = 0;
            decimal rtdisc = Convert.ToDecimal(txdisc.Text);
            if (grdinv.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('There are no invoice to be paid','No Invoice Found','warning');", true);
                return;
            }

            foreach (GridViewRow row in grdinv.Rows)
            {
                TextBox txpaid = (TextBox)row.FindControl("txpaid");
                TextBox txdiscv = (TextBox)row.FindControl("txdiscount");
                if (!decimal.TryParse(txpaid.Text, out dPaid))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Payment must currency','Check Payment','warning');", true);
                    return;
                }
                if (!decimal.TryParse(txdiscv.Text, out dDisc))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Discount must currency','Check Discount','warning');", true);
                    return;
                }
                if (Convert.ToDecimal(txdiscv.Text) > 1)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Discount must be less than one Rial','Check Discount','warning');", true);
                    return;
                }
                dPaidTotal += dPaid;
                dPaidDiscTotal += dDisc;
            }
            if (dPaidTotal > (Convert.ToDecimal(lbavailpaid.Text) - Convert.ToDecimal(txdisc.Text)))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Total paid must not bigger than return amount','Check return amt','warning');", true);
                return;
            }

            Label lbpaymentno = (Label)grd.Rows[grd.SelectedRow.RowIndex].FindControl("lbpaymentno");
            Label lbpaymentcst = (Label)grd.Rows[grd.SelectedRow.RowIndex].FindControl("lbpaymentcst");
            Label lbpaymentdt = (Label)grd.Rows[grd.SelectedRow.RowIndex].FindControl("lbpaymentdt");
            arr.Clear();
            arr.Add(new cArrayList("@payment_sta_id", "C"));
            arr.Add(new cArrayList("@payment_no", lbpaymentno.Text));
            arr.Add(new cArrayList("@totamt", Convert.ToString((Convert.ToDouble(lbavailpaid.Text) - Convert.ToDouble(txdisc.Text)))));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vUpdateMstPaymentrt(arr);
            arr.Clear();
            if (Convert.ToDecimal(txdisc.Text) > 0)
            {
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                arr.Add(new cArrayList("@payment_no", lbpaymentno.Text));
                arr.Add(new cArrayList("@amt", txdisc.Text));
                bll.vinsertpaymentrtdiscount(arr);
            }
            if (Convert.ToDecimal(lbsuspense.Text) > 0)
            {
                arr.Clear();
                arr.Add(new cArrayList("@cust_cd", lbpaymentcst.Text));
                arr.Add(new cArrayList("@payment_no", lbpaymentno.Text));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                arr.Add(new cArrayList("@amt", lbsuspense.Text));
                arr.Add(new cArrayList("@payment_dt", DateTime.ParseExact(lbpaymentdt.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                arr.Add(new cArrayList("@deleted", 0));
                bll.vInsertPaymentSuspense(arr);
            }

            foreach (GridViewRow row in grdinv.Rows)
            {
                Label lbinvoiceno = (Label)row.FindControl("lbinvno");
                TextBox txpaid = (TextBox)row.FindControl("txpaid");
                TextBox txdiscount = (TextBox)row.FindControl("txdiscount");
                arr.Clear();
                arr.Add(new cArrayList("@payment_no", lbpaymentno.Text));
                arr.Add(new cArrayList("@inv_no", lbinvoiceno.Text));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                arr.Add(new cArrayList("@amt", txpaid.Text));
                arr.Add(new cArrayList("@discount_amt", txdiscount.Text));
                if (Convert.ToDouble(txpaid.Text) > 0)
                {
                    bll.vInsertPaymentDtlreturn(arr);
                }

            }



            btsave.Visible = false;
            BindingGridSales();
            grdinv.DataSource = null;
            grdinv.DataBind();
            // batch Running for SOA -----------------------------------------------
            arr.Clear();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Retur has been applied successfully','Retur Applied','success');", true);
        }
        catch (Exception ex)
        {
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : Apply Payment Return");
        }

    }

    //  protected void btclearpayment_Click(object sender, EventArgs e)
    //  {
    //      foreach (GridViewRow row in grdinv.Rows)
    //      {
    //          if (row.RowType == DataControlRowType.DataRow)
    //          {
    //              TextBox txpaid = (TextBox)row.FindControl("txpaid");
    //             txpaid.Text = "0";                
    //          }            
    //      }
    //      lbsuspense.Text = Convert.ToString(Convert.ToDouble(lbavailpaid.Text) - Convert.ToDouble(txdisc.Text));
    //  }
    //  protected void txpaid_TextChanged(object sender, System.EventArgs e)
    //  {
    //      lbsuspense.Text = Convert.ToString((Convert.ToDouble(lbavailpaid.Text) - Convert.ToDouble(txdisc.Text)) - Convert.ToDouble(txpaid.Text));
    //  } 

    protected void grdinv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbinvno = (Label)e.Row.FindControl("lbinvno");
            if (bll.vLookUp("select dbo.fn_checkpaymentbooking('"+lbinvno.Text+"','"+ Request.Cookies["sp"].Value.ToString() +"')") != "ok")
            {
                e.Row.Enabled = false;
                e.Row.BackColor = System.Drawing.Color.LightGray;
                e.Row.ToolTip = lbinvno.Text + " has cheque/bank transfer not yet cleareance!";
            }
        }
    }
}