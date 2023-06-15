using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_paymentreceipt : System.Web.UI.Page
{
    cbll bll = new cbll();
    double dTotalPaymentPp = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@qry_cd", "SalesJob"));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingFieldValueToCombo(ref cbpaymenttype, "payment_typ");
            cbpaymenttype_SelectedIndexChanged(sender, e);
            bll.vBindingFieldValueToCombo(ref cbsource, "source_order");
            bll.vBindingFieldValueToCombo(ref cbbankcheq, "bank_cd");

            cbsource_SelectedIndexChanged(sender, e);
            //    bll.vBindingComboToSp(ref cbsalesman, "sp_tmst_employee_getbyqry", "emp_cd", "emp_desc", arr);
            arr.Clear();
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            //   bll.vBindingComboToSp(ref cbbankacc, "sp_tmst_bankaccount_get", "acc_no","bank_desc", arr);
            cbsalesman_SelectedIndexChanged(sender, e);
            bll.vBindingFieldValueToCombo(ref cbstatus, "payment_sta_id");
            dtho.Text = Request.Cookies["waz_dt"].Value.ToString();
            dtpayment.Text = Request.Cookies["waz_dt"].Value.ToString(); // System.DateTime.Today.ToShortDateString();
            lblegend.Text = "|   ";
            lblegend.BackColor = System.Drawing.Color.GreenYellow;
            // bll.sFormat2ddmmyyyy(ref dtpayment);
            txpaymentno.Text = "NEW";
            btprint.CssClass = "button2 print";
            if (Request.QueryString["pid"] != null)
            {
                System.Data.SqlClient.SqlDataReader rs = null;
                txpaymentno.Text = Request.QueryString["pid"];
                hdpaid.Value = txpaymentno.Text;
                btsearch_Click(sender, e);
                arr.Clear();
                arr.Add(new cArrayList("@payment_no", hdpaid.Value.ToString()));
                bll.vGetMstPaymentByNo(arr, ref rs);
                while (rs.Read())
                {
                    dtpayment.Text = rs["payment_dt"].ToString();
                    bll.sFormat2ddmmyyyy(ref dtpayment);
                    cbpaymenttype.SelectedValue = rs["payment_typ"].ToString();
                    cbsalesman.SelectedValue = rs["salesman_cd"].ToString();
                    hdcust.Value = rs["cust_cd"].ToString();
                    txcustomer.Text = rs["cust_cd"].ToString();
                    txtabno.Text = rs["tab_no"].ToString();
                    txamount.CssClass = "ro";
                    //btinv_Click(sender, e);
                } rs.Close();
            }
            else
            {
                rdpaidfor.SelectedValue = "C";
                rdpaidfor_SelectedIndexChanged(sender, e);
                rdpaidfor.CssClass = "divnormal";
            }
            if (Request.QueryString["p"] != null)
            {

                if (((Request.QueryString["p"]) != null) || (Request.QueryString["p"] != ""))
                {
                    System.Data.SqlClient.SqlDataReader rs = null;
                    arr.Clear();
                    arr.Add(new cArrayList("@dep_cd", Request.QueryString["p"]));
                    bll.vGetPaymentDeposit(arr, ref rs);
                    while (rs.Read())
                    {
                        txamount.Text = rs["amt"].ToString();
                        hdcust.Value = rs["cust_cd"].ToString();
                        txremark.Text = rs["remark"].ToString();
                        cbsource.SelectedValue = "MAN";
                        cbsource_SelectedIndexChanged(sender, e);
                        txcustomer.Text = bll.vLookUp("select cust_cd+' | '+ cust_nm from tmst_customer where cust_cd='" + hdcust.Value.ToString() + "'");
                        txamount.CssClass = "ro";
                        cbsource.CssClass = "ro";
                         btinv_Click(sender, e);
                        cbpaymenttype.SelectedValue = rs[""].ToString();
                    } rs.Close();

                }
            }
        }
    }

    public void CheckPaid()
    {
        Response.Write("eek lah");
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        HttpCookie cok;
        cok = HttpContext.Current.Request.Cookies["sp"];
        List<string> lcust = new List<string>();
        List<cArrayList> arr = new List<cArrayList>();
        string scust = string.Empty;
        System.Data.SqlClient.SqlDataReader rs = null;
        arr.Add(new cArrayList("@cust_cd", prefixText));
        //arr.Add(new cArrayList("@salesman_cd", contextKey));
        arr.Add(new cArrayList("@salespointcd", cok.Value.ToString()));
        // bll.vSearchCustomerBySales(arr, ref rs);
        bll.vSearchMstCustomer(arr, ref rs);
        while (rs.Read())
        {
            scust = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["cust_cd"].ToString() + "-" + rs["cust_nm"].ToString(), rs["cust_cd"].ToString());
            lcust.Add(scust);
        }
        rs.Close();

        return (lcust.ToArray());
    }
    protected void cbsalesman_SelectedIndexChanged(object sender, EventArgs e)
    {
        txcustomer_AutoCompleteExtender.ContextKey = cbsalesman.SelectedValue.ToString();
        hdcust.Value = "";
        txcustomer.Text = "";
    }

    void BindingGrid()
    {
        List<cArrayList> arr = new List<cArrayList>();
        if (rdpaidfor.SelectedValue.ToString() == "C")
        {
            arr.Add(new cArrayList("@cust_cd", hdcust.Value.ToString()));
            bll.vBindingGridToSp(ref grd, "sp_tdosales_invoice_getbycust", arr);
        }
        else if (rdpaidfor.SelectedValue.ToString() == "G")
        {
            arr.Add(new cArrayList("@cusgrcd", cbgroup.SelectedValue.ToString()));
            bll.vBindingGridToSp(ref grd, "sp_tdosales_invoice_getbygroup", arr);
        }
    }
    protected void btinv_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@qry_cd", "SalesJob"));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        bll.vBindingComboToSp(ref cbsalesman, "sp_tmst_employee_getbyqry", "emp_cd", "emp_desc", arr);
        arr.Clear();
        arr.Add(new cArrayList("@payment_no", hdpaid.Value.ToString()));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        bll.vBindingGridToSp(ref grdinvpaid, "sp_tpayment_dtl_get", arr);
        //Label lbtotremain = (Label) grdinvpaid.FooterRow.FindControl("lbtotremain");
        //lbtotamtpaid.Text = "Sehe";
        arr.Clear();
        arr.Add(new cArrayList("@cust_cd", hdcust.Value.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_tdosales_invoice_getbycust", arr);
        if (hdcust.Value.ToString() != "")
        {
            string sSalesmanCode = bll.vLookUp("select salesman_cd from tmst_customer where cust_cd='" + hdcust.Value.ToString() + "'");
            // cbsalesman.Enabled = true;
            txcustomer.CssClass = "ro";
            cbsalesman.SelectedValue = sSalesmanCode;
            if (grd.Rows.Count > 0)
            {
                Label lbtotremain = (Label)grd.FooterRow.FindControl("lbtotremain");

                lbtotremain.Text = bll.vLookUp("select sum(balance) from tdosales_invoice where cust_cd='" + hdcust.Value.ToString() + "' and inv_sta_id in ('R','P')");
                lbtotremain.ForeColor = System.Drawing.Color.White;
                lbtotremain.Font.Bold = true;
            }
        }
        // cbsalesman.Enabled = false;
        txpaymentno.Text = hdpaid.Value.ToString();
        if ((Request.QueryString["p"]) == "")
        { txamount.CssClass = "ro"; }
        else {
        txamount.CssClass = "divnormal";}

    }
    protected void btapply_Click(object sender, EventArgs e)
    {
        double dAmt = 0; double dCheck = 0; double dTotAmtPaid = 0;
        if (!double.TryParse(txamount.Text, out dAmt))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Amount must be in currency !','Currency','warning');", true);
            return;
        }

        if (grd.Rows.Count == 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Please select customer with invoiced !','No Invoice','warning');", true);
            return;
        }
        lbsuspense.Text = "0";
        foreach (GridViewRow row in grd.Rows)
        {
            TextBox txamt = (TextBox)row.FindControl("txamt");
            TextBox txdiscount = (TextBox)row.FindControl("txdiscount");
            txamt.Text = "0"; txdiscount.Text = "0";
        }
        txamount.CssClass = "ro";
        dCheck = dAmt; //Jumlah pembayaran
        lbsuspense.Text = "";
        double dHasPaid = 0;
        foreach (GridViewRow row in grd.Rows)
        {
            TextBox txamt = (TextBox)row.FindControl("txamt");
            Label lbtotpaid = (Label)row.FindControl("lbtotpaid");
            Label lbtotamt = (Label)row.FindControl("lbtotamt");
            Label lbremain = (Label)row.FindControl("lbremain");
            Label lbdisconepct = (Label)row.FindControl("lbdisconepct");
            HiddenField hdremain = (HiddenField)row.FindControl("hdremain");
            Label lbbalance = (Label)row.FindControl("lbbalance");
            double dRemain = Math.Round(Convert.ToDouble(lbremain.Text), 2);

            if (Math.Round(dCheck, 2) <= dRemain)
            {
                if (txamt.CssClass != "ro")
                {
                    dTotAmtPaid += Math.Round(dRemain, 2);
                }
                if (txamt.CssClass != "ro")
                {
                    txamt.Text = (dCheck - Convert.ToDouble(lbdisconepct.Text)).ToString();
                    double am= Math.Round(Convert.ToDouble(txamt.Text), 2);
                    double dc = Math.Round(Convert.ToDouble(lbdisconepct.Text), 2);
                    double x = Math.Round(Convert.ToDouble(lbremain.Text), 2) - am - dc;
                    double y = dRemain - Math.Round(Convert.ToDouble(txamt.Text), 2) - x;                    
                    if (y >= 0)
                    {
                        txamt.Text = (am + Math.Round(y,2)).ToString();
                    }
                }
                //   lbbalance.Text = (dRemain - Convert.ToDouble(txamt.Text)).ToString();
                //if (row.RowType == DataControlRowType.Footer)
                //{
                //    Label lbtotamtpaid = (Label)row.FindControl("lbtotamtpaid");
                //    lbtotamtpaid.Text = Math.Round( dTotAmtPaid,2).ToString();
                dHasPaid += Convert.ToDouble(txamt.Text);
                //}
                break;
            }
            else
            {
                //dCheck -= dRemain;
                // if ((dCheck-dRemain) < 0)
                // {
                if (txamt.CssClass != "ro")
                {
                    txamt.Text = (dRemain - Convert.ToDouble(lbdisconepct.Text)).ToString();
                }
                dCheck -= Math.Round(dRemain, 2);
                // }
                // else
                //  {
                //     txamt.Text = (dRemain-dCheck).ToString();
                //  }
                dTotAmtPaid += Math.Round(dRemain, 2);

                //   lbbalance.Text = (dRemain - Convert.ToDouble(txamt.Text)).ToString();

            }
            dHasPaid += Convert.ToDouble(txamt.Text);
            //if (row.RowType == DataControlRowType.Footer)
            //{
            //    Label lbtotamtpaid = (Label)row.FindControl("lbtotamtpaid");
            //    lbtotamtpaid.Text = "600";
            //}

            //if (row.RowType == DataControlRowType.Footer)
            //{
            //    Label lbfooterpaid = (Label)row.FindControl("lbtotpaid");
            //    lbfooterpaid.Text = "Uhuh";
            //}

        }

        //Suspense check 
        if ((Math.Round(dAmt, 2) - Math.Round(dTotAmtPaid, 2)) > 0)
        {
            lbsuspense.Text = Math.Round((dAmt - dTotAmtPaid), 2).ToString();
        }

        Label lbtotamtpaid1 = (Label)grd.FooterRow.FindControl("lbtotamtpaid");
        lbtotamtpaid1.Text = dHasPaid.ToString();
        // txamount.CssClass = "ro";
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        string sPaymentNo = "";
        if (cbsource.SelectedValue.ToString() == "MAN")
        {
            if (cbpaymenttype.SelectedValue.ToString() == "CQ")
            {
                if (dtcheqdue.Text == "")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Cheque due date must be select !','Cheque Due Date','warning');", true);
                    return;
                }

                if (!uplpayment.HasFile)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Please attach document Cheque to system','HO will make clearance','warning');", true);
                    return;
                }

            }

            if (cbpaymenttype.SelectedValue.ToString() == "BT")
            {
                if (!uplpayment.HasFile)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Please attach document Bank Transfer to system','HO will make clearance','warning');", true);
                    return;
                }
            }
            if (rdpaidfor.SelectedValue.ToString() == "C")
            {
                if ((hdcust.Value.ToString() == "") || (hdcust.Value.Equals(null)))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('customer must be selected !','customer','warning');", true);
                    return;
                }
            }
            double dOut;
            if (!double.TryParse(txamount.Text, out dOut))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Amount to paid must in currency !','must currency','warning');", true);
                return;
            }
            if ((cbpaymenttype.SelectedValue.ToString() == "BT" || (cbpaymenttype.SelectedValue.ToString() == "CQ")))
            {
                if (txdocno.Text == "")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Cheque/Transfer No. can not empty !','Bank Transfer/Cheque','warning');", true);
                    return;
                }
            }
            double dCheck; double dTot = 0; double dDiscount = 0;
            foreach (GridViewRow row in grd.Rows)
            {
                TextBox txamt = (TextBox)row.FindControl("txamt");
                TextBox txdisc = (TextBox)row.FindControl("txdiscount");
                Label lbinvoiceno = (Label)row.FindControl("lbinvoiceno");

                if (!double.TryParse(txdisc.Text, out dCheck))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Discount must be numeric !!','discount','warning');", true);
                    return;
                }
                dDiscount += dCheck; dCheck = 0;
                if (!double.TryParse(txamt.Text, out dCheck))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('All amount pay must be numeric !!','payment','warning');", true);
                    return;
                }
                string sMsg = bll.vLookUp("select dbo.fn_checkcndn('" + lbinvoiceno.Text + "')");
                if ((sMsg != "ok") && (dCheck > 0))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('" + sMsg + "','CN DN Outstanding Approval','warning');", true);
                    return;
                }
                string sCheck1pct = bll.vLookUp("select dbo.fn_checkexistingdisconepct('" + lbinvoiceno.Text + "')");
                if (sCheck1pct != "ok")
                {
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('" + sCheck1pct + "','Please switch off 1 pct for that invoice','warning');", true);
                //    return;
                }
                else { dTot += dCheck; }
            }

            if (dDiscount > 1)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Discount can not more than 1 SAR','discount','warning');", true);
                return;
            }

            if (Math.Round(dTot, 2) > Math.Round(dOut, 2))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Total paid can not bigger than cust payment amount','Total Amount','warning');", true);
                return;
            }

            if (dTot == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('No available invoice to be paid','Check your cust balance','warning');", true);
                return;
            }

            //if (Convert.ToDouble(txamount.Text) != dTot)
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Total amount to paid is not same with billing amount !!','Amount Paid','warning');", true);
            //    return;

            //}

            if (txmanualno.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Manual No can not empty !!','manual no','warning');", true);
                return;
            }

            string sManNo = bll.vLookUp("select dbo.fn_checkmanualno('payment','" + txmanualno.Text + "')");
            if (sManNo != "ok")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('" + sManNo + "','manual no','warning');", true);
                return;
            }

            if (((Convert.ToDouble(txamount.Text) + dDiscount) - dTot) > 0)
            {
                lbsuspense.Text = Math.Round(((Convert.ToDouble(txamount.Text) - dTot)), 2).ToString();
            }

            if (cbpaymenttype.SelectedValue.ToString() == "IT")
            {
                if (txhovoucher.Text == "")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Voucher HO not empty !!','voucher ho no','warning');", true);
                    return;
                }


            }
            //Check payment booking 

            double dTotqty = 0; double dqty = 0;
            foreach (GridViewRow row in grd.Rows)
            {
                TextBox txamt = (TextBox)row.FindControl("txamt");
                TextBox txdiscount = (TextBox)row.FindControl("txdiscount");
                if (double.TryParse(txamt.Text, out dqty))
                {
                    dTotqty += dqty;
                }
                if (double.TryParse(txdiscount.Text, out dqty))
                {
                    dTotqty += dqty;
                }

                //if (Convert.ToDouble(txamount.Text) != dTotqty)
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alr", "sweetAlert('Amount payment and amount to be paid is not same','"+txamount.Text + " vs " + dTotqty.ToString() +"','warning');", true);
                //    return;
                //}
            }

            double dRemain = 0;

            List<cArrayList> arr = new List<cArrayList>();
            try
            {

                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                arr.Add(new cArrayList("@payment_dt", DateTime.ParseExact(dtpayment.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                arr.Add(new cArrayList("@payment_typ", cbpaymenttype.SelectedValue.ToString()));
                if (rdpaidfor.SelectedValue.ToString() == "C")
                {
                    arr.Add(new cArrayList("@cust_cd", hdcust.Value.ToString()));
                    arr.Add(new cArrayList("@salesman_cd", cbsalesman.SelectedValue.ToString()));
                }
                arr.Add(new cArrayList("@totamt", txamount.Text));
                arr.Add(new cArrayList("@payment_sta_id", cbstatus.SelectedValue.ToString()));
                arr.Add(new cArrayList("@btcheq_no", txdocno.Text));
                arr.Add(new cArrayList("@acc_no", cbbankacc.SelectedValue.ToString()));
                arr.Add(new cArrayList("@manual_no", txmanualno.Text));
                arr.Add(new cArrayList("@source_order", cbsource.SelectedValue.ToString()));
                arr.Add(new cArrayList("@bankaccho", cbbankHO.SelectedValue.ToString()));
                arr.Add(new cArrayList("@remark", txremark.Text));
                arr.Add(new cArrayList("@voucherho_no", txhovoucher.Text));
                arr.Add(new cArrayList("@horec_dt", DateTime.ParseExact(dtho.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                arr.Add(new cArrayList("@rdpaidfor", rdpaidfor.SelectedValue.ToString()));

                if (cbpaymenttype.SelectedValue.ToString() == "CQ")
                {
                    arr.Add(new cArrayList("@bank_cd", cbbankcheq.SelectedValue.ToString()));
                    arr.Add(new cArrayList("@chequedue_dt", DateTime.ParseExact(dtcheqdue.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                }
                if (rdpaidfor.SelectedValue.ToString() == "G")
                {
                    arr.Add(new cArrayList("@cusgrcd", cbgroup.SelectedValue.ToString()));
                }

                bll.vInsertMstPayment(arr, ref sPaymentNo);
                txpaymentno.Text = sPaymentNo;
                hdpaid.Value = sPaymentNo;
                if (uplpayment.HasFile)
                {
                    uplpayment.SaveAs(bll.sGetControlParameter("image_path") + sPaymentNo + uplpayment.FileName);
                }
                if ((cbpaymenttype.SelectedValue == "CQ") || (cbpaymenttype.SelectedValue.ToString() == "BT"))
                {
                    arr.Clear();
                    arr.Add(new cArrayList("@payment_no", sPaymentNo));
                    arr.Add(new cArrayList("@docfile", sPaymentNo + uplpayment.FileName));
                    arr.Add(new cArrayList("@initamt", txamount.Text));
                    bll.vInsertPaymentInfo(arr);
                }
                else
                {
                    arr.Clear();
                    arr.Add(new cArrayList("@payment_no", sPaymentNo));
                   // arr.Add(new cArrayList("@docfile", sPaymentNo + uplpayment.FileName));
                    arr.Add(new cArrayList("@initamt", txamount.Text));
                    bll.vInsertPaymentInfo(arr);
                }
                arr.Clear();
                foreach (GridViewRow row in grd.Rows)
                {
                    Label lbinvoiceno = (Label)row.FindControl("lbinvoiceno");
                    TextBox txamt = (TextBox)row.FindControl("txamt");
                    TextBox txdiscount = (TextBox)row.FindControl("txdiscount");
                    Label lbdisconepct = (Label)row.FindControl("lbdisconepct");
                    Label lbremain = (Label)row.FindControl("lbremain");
                    CheckBox chdisc = (CheckBox)row.FindControl("chdisc");
                    if (txamt.Text == "") { txamt.Text = "0"; }
                    dRemain += Convert.ToDouble(txamt.Text);
                    if (Convert.ToDouble(txamt.Text) > 0)
                    {
                        arr.Clear();
                        arr.Add(new cArrayList("@payment_no", sPaymentNo));
                        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                        arr.Add(new cArrayList("@inv_no", lbinvoiceno.Text));
                        arr.Add(new cArrayList("@amt", txamt.Text));
                        arr.Add(new cArrayList("@discount_amt", txdiscount.Text));
                        if (chdisc.Checked)
                        {
                            if ((Convert.ToDouble(lbremain.Text) - (Convert.ToDouble(lbdisconepct.Text) + Convert.ToDouble(txamt.Text) + Convert.ToDouble(txdiscount.Text))) == 0)
                            {
                                arr.Add(new cArrayList("@disconepct", lbdisconepct.Text));
                            }
                            else
                            {
                                arr.Add(new cArrayList("@disconepct", 0));
                            }
                        }
                        else { arr.Add(new cArrayList("@disconepct", 0)); }
                        if ((cbpaymenttype.SelectedValue.ToString() != "CQ") && (cbpaymenttype.SelectedValue.ToString() != "BT"))
                        {
                            bll.vInsertPaymentDtl(arr);
                        }
                        else //if (cbstatus.SelectedValue.ToString() == "N") //Under Collection Booking payment
                        {
                            bll.vInsertPaymentBooking(arr);
                        }
                        //----------------------------- IA 21-Sep-2016-----------
                        arr.Clear();
                        arr.Add(new cArrayList("@inv_no", lbinvoiceno.Text));
                        arr.Add(new cArrayList("@isdisconepct", chdisc.Checked));
                        bll.vUpdateDosalesInvoiceInfoByIsDiscOnePct(arr);
                        //-------------------------------------------------------
                    }
                }



                if (((Convert.ToDouble(txamount.Text) - dRemain) > 0) &&( cbpaymenttype.SelectedValue.ToString() != "CQ" && cbpaymenttype.SelectedValue.ToString() != "BT"))
                {

                    arr.Clear();
                    arr.Add(new cArrayList("@cust_cd", hdcust.Value.ToString()));
                    arr.Add(new cArrayList("@payment_no", sPaymentNo));
                    arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                    arr.Add(new cArrayList("@amt", lbsuspense.Text));
                    arr.Add(new cArrayList("@payment_dt", DateTime.ParseExact(dtpayment.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                    arr.Add(new cArrayList("@deleted", 0));
                    bll.vInsertPaymentSuspense(arr);
                }
            }
            catch (Exception ex)
            {
                arr.Clear();
                arr.Add(new cArrayList("@err_source", "Payment Receipt"));
                arr.Add(new cArrayList("@err_description", ex.Message.ToString()));
                bll.vInsertErrorLog(arr);
            }

            BindingGrid();

            btsave.Visible = false;
            btprint.CssClass = "button2 print";

            cbpaymenttype.CssClass = "ro";
            txamount.CssClass = "ro";
            txhovoucher.CssClass = "ro";
            txremark.CssClass = "ro";
            cbbankHO.CssClass = "ro";
            cbbankacc.CssClass = "ro";
            dtho.CssClass = "ro";
            dtcheqdue.CssClass = "ro";
            cbbankcheq.CssClass = "ro";
            txmanualno.CssClass = "ro";
            btinv_Click(sender, e);

        } // This end of SOurce Order TAB / MANUAL
        else
        {
            //List<cArrayList> arr = new List<cArrayList>();
            //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            //arr.Add(new cArrayList("@payment_dt", DateTime.ParseExact(dtpayment.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            //arr.Add(new cArrayList("@payment_typ", cbpaymenttype.SelectedValue.ToString()));
            //arr.Add(new cArrayList("@cust_cd", hdcust.Value.ToString()));
            //arr.Add(new cArrayList("@salesman_cd", cbsalesman.SelectedValue.ToString()));
            //arr.Add(new cArrayList("@totamt", txamount.Text));
            //arr.Add(new cArrayList("@payment_sta_id", cbstatus.SelectedValue.ToString()));
            //arr.Add(new cArrayList("@doc_no", txdocno.Text));
            //arr.Add(new cArrayList("@acc_no", cbbankacc.SelectedValue.ToString()));
            //arr.Add(new cArrayList("@manual_no", txmanualno.Text));
            //bll.vInsertMstPayment(arr, ref sPaymentNo);
            //txpaymentno.Text = sPaymentNo;
            //foreach (GridViewRow row in grdtab.Rows)
            //{
            //    Label lbinvno = (Label)row.FindControl("lbinvno");
            //    Label lbamt = (Label)row.FindControl("lbamt");
            //    arr.Clear();
            //    arr.Add(new cArrayList("@payment_no", sPaymentNo));
            //    arr.Add(new cArrayList("@inv_no", lbinvno.Text));
            //    arr.Add(new cArrayList("@amt", lbamt.Text));
            //    arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            //    bll.vInsertPaymentDtl(arr);
            //}
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Source Tablet Under Development','Next Feature','warning');", true);
            return;
        }
        //grd.Enabled = false;
        grd.CssClass = "ro";
        //txamount.Enabled = false;
        txamount.CssClass = "ro";
        //txcustomer.Enabled=false;
        txcustomer.CssClass = "ro";
        // txdocno.Enabled=false;
        // txpaymentno.Enabled = false;
        txpaymentno.CssClass = "ro";
        // btapply.Enabled = false;
        btapply.CssClass = "divhid";
        //  btsave.Enabled = false;
        btsave.CssClass = "divhid";
        btprint.CssClass = "button2 print";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Payment has been saved succesfully ...','Payment No. : " + sPaymentNo + "','success');", true);

    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "pr", "openreport('fm_report2.aspx?src=py&noy=" + txpaymentno.Text + "');", true);
    }
    protected void grd_RowCreated(object sender, GridViewRowEventArgs e)
    {

    }
    protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbinvoiceno = (Label)e.Row.FindControl("lbinvoiceno");
            Label lbmanualno = (Label)e.Row.FindControl("lbmanualno");
            Label lbpaid = (Label)e.Row.FindControl("lbpaid");
            HiddenField hdtotamt = (HiddenField)e.Row.FindControl("hdtotamt");
            TextBox txamt = (TextBox)e.Row.FindControl("txamt");
            txamt.Text = "0";
            lbpaid.Text = bll.vLookUp("select isnull(sum(amt),0) from tpayment_dtl d join tmst_payment m on d.payment_no=m.payment_no where d.inv_no='" + lbinvoiceno.Text + "' and m.payment_sta_id <> 'L'");
            TextBox txdiscount = (TextBox)e.Row.FindControl("txdiscount");
            Label lbtotamt = (Label)e.Row.FindControl("lbtotamt");
            string sBook = bll.vLookUp("select dbo.fn_checkpaymentbooking('"+lbinvoiceno.Text+"')");
            if (sBook != "ok")
            {
                { txamt.Text = "0"; txdiscount.Text = "0"; txamt.CssClass = "ro"; txdiscount.CssClass = "ro"; e.Row.ToolTip = "Invoice " + lbmanualno.Text + " has payment booking from CHEQUE/BANK TRANSFER"; }
            }
            string sInv = bll.vLookUp("select dbo.fn_checkinvreceived('" + lbinvoiceno.Text + "')");
            if (sInv != "ok")
            { txamt.Text = "0"; txdiscount.Text = "0"; txamt.CssClass = "ro"; txdiscount.CssClass = "ro"; e.Row.ToolTip = "Invoice " + lbmanualno.Text + " has not yet back to office"; }
            //else { txamt.CssClass = "divnormal"; }
            string sDisc = bll.vLookUp("select isnull(sum(a.amt),0) from tcanvasorder_disccash a join tmst_dosales b on a.so_cd=b.so_cd join tdosales_invoice c on c.inv_no=b.inv_no " +
                "where c.inv_no='" + lbinvoiceno.Text + "'");
            double dTotamt = Convert.ToDouble(hdtotamt.Value.ToString()) - Convert.ToDouble(sDisc);
            lbtotamt.Text = dTotamt.ToString();
            dTotalPaymentPp = dTotalPaymentPp + dTotamt;
            if (bll.vLookUp("select dbo.fn_checkdiscountpayment1pct('" + lbinvoiceno.Text + "')") != "ok")
            {
                Label lbldisconepct = (Label)e.Row.FindControl("lbdisconepct");
                txamt.BackColor = System.Drawing.Color.GreenYellow;
                lbldisconepct.Text = (0.01 * dTotamt).ToString();
                double dSelisih = (Convert.ToDouble(txamt.Text) - Convert.ToDouble(lbldisconepct.Text));
                if (dSelisih > 0)
                { txamt.Text = dSelisih.ToString(); }
                e.Row.ToolTip = "This is discount 1% applied";
            }
            else { Label lbldisconepct = (Label)e.Row.FindControl("lbdisconepct"); lbldisconepct.Text = "0"; }
        }

    }
    protected void cbbankacc_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    void MakeClear()
    {
        hdcust.Value = "";
        txcustomer.Text = ""; txdocno.Text = ""; txmanualno.Text = ""; txtabno.Text = ""; txpaymentno.Text = "";

    }
    protected void cbpaymenttype_SelectedIndexChanged(object sender, EventArgs e)
    {
        //  MakeClear();

        if ((cbpaymenttype.SelectedValue.ToString() == "CH"))
        {
            cbbankacc.CssClass = "ro";
            cbstatus.SelectedValue = "R"; cbstatus.CssClass = "ro";
            txdocno.CssClass = "ro"; txdocno.Text = "";
            cbbankacc.Items.Clear();
            cbbankHO.CssClass = "ro";
            txhovoucher.CssClass = "ro";
            txdocno.CssClass = "ro";
            cbbankcheq.CssClass = "ro";
            dtcheqdue.CssClass = "ro";

        }
        else if ((cbpaymenttype.SelectedValue.ToString() == "BT") || (cbpaymenttype.SelectedValue.ToString() == "CQ"))
        {
            List<cArrayList> arr = new List<cArrayList>();
            cbbankacc.CssClass = "makeitreadwrite";
            cbstatus.CssClass = "makeitreadwrite";
            txdocno.CssClass = "makeitreadwrite";
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingComboToSp(ref cbbankacc, "sp_tmst_bankaccount_get", "acc_no", "bank_desc", arr);
            cbbankacc.Enabled = true; cbbankacc.CssClass = "maketitreadwrite";
            cbstatus.SelectedValue = "N"; cbstatus.Enabled = true; cbstatus.CssClass = "ro";
            txdocno.Enabled = true; txdocno.CssClass = "makeitreadwrite";
            cbbankHO.CssClass = "ro";
            txhovoucher.CssClass = "ro";
            txdocno.CssClass = "divnormal";
            cbbankcheq.CssClass = "divnormal";
            if (cbpaymenttype.SelectedValue.ToString() == "CQ")
            { dtcheqdue.CssClass = "divnormal"; }
            else { dtcheqdue.CssClass = "ro"; }
            // dtcheqdue.CssClass = "divnormal";
        }
        else if (cbpaymenttype.SelectedValue.ToString() == "RT")
        {
            cbbankacc.Items.Clear();
            cbbankacc.CssClass = "ro";
            cbbankHO.CssClass = "ro";
            txhovoucher.CssClass = "ro";
            txdocno.CssClass = "ro";
            cbbankcheq.CssClass = "ro";
            dtcheqdue.CssClass = "ro";

        }
        else if (cbpaymenttype.SelectedValue.ToString() == "IT")
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@salespointcd", 0));
            bll.vBindingComboToSp(ref cbbankHO, "sp_tmst_bankaccount_getbysp", "acc_no", "bank_desc", arr);
            cbbankHO.CssClass = "divnormal";
            txhovoucher.CssClass = "divnormal";
            txdocno.CssClass = "divnormal";
            cbbankcheq.CssClass = "ro";
            dtcheqdue.CssClass = "ro";
        }
    }
    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_paymentreceipt.aspx");
    }

    void makeitreadonly()
    {
        txamount.CssClass = "ro";
        txcustomer.CssClass = "ro";
        txdocno.CssClass = "ro";
        txmanualno.CssClass = "ro";
        txpaymentno.CssClass = "ro";
        txtabno.CssClass = "ro";
        grd.Enabled = false;
        cbbankacc.Items.Clear();
        cbbankacc.CssClass = "ro";
        cbsalesman.CssClass = "ro";
        txcustomer.CssClass = "ro";
       
    }

    void makeitreadwrite()
    {
        txamount.Enabled = true;
        txamount.CssClass = "makeitreadwrite";
        txcustomer.Enabled = true;
        txcustomer.CssClass = "makeitreadwrite";
        txdocno.Enabled = true;
        txdocno.CssClass = "makeitreadwrite";
        txmanualno.Enabled = true;
        txmanualno.CssClass = "makeitreadwrite";
        txcustomer.CssClass = "makeitreadwrite";
        txtabno.Enabled = false;
        txtabno.CssClass = "makeitreadwrite";
        grd.Enabled = true;
    }
    protected void cbsource_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cbsource.SelectedValue.ToString() == "TAB")
        {
            makeitreadonly();
            txtabno.CssClass = "makeitreadwrite";

        }
        else { makeitreadwrite(); txtabno.CssClass = "ro"; }
        cbpaymenttype_SelectedIndexChanged(sender, e);
    }
    protected void bttabsearch_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opr", "popupwindow('fm_lookuppaymenttab.aspx?pt=" + cbpaymenttype.SelectedValue.ToString() + "');", true);
    }
    protected void bttab_Click(object sender, EventArgs e)
    {
        System.Data.SqlClient.SqlDataReader rs = null;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@tab_no", txtabno.Text));
        bll.vBindingGridToSp(ref grdtab, "sp_ttab_paymentreceipt_dtl_get", arr);
        bll.vGetTabPaymentReceipt(arr, ref rs);
        while (rs.Read())
        {
            txcustomer.Text = rs["cust_nm"].ToString();
            hdcust.Value = rs["cust_cd"].ToString();
            cbsalesman.SelectedValue = rs["salesman_cd"].ToString();
            txamount.Text = bll.vLookUp("select sum(amt) from ttab_paymentreceipt_dtl where tab_no='" + txtabno.Text + "'");
        } rs.Close();
    }
    protected void grdtab_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grdtab.EditIndex = e.NewEditIndex;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@tab_no", txtabno.Text));
        bll.vBindingGridToSp(ref grdtab, "sp_ttab_paymentreceipt_dtl_get", arr);
    }
    protected void grdtab_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        TextBox txamt = (TextBox)grdtab.Rows[e.RowIndex].FindControl("txamt");
        grdtab.EditIndex = -1;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@tab_no", txtabno.Text));
        bll.vBindingGridToSp(ref grdtab, "sp_ttab_paymentreceipt_dtl_get", arr);
    }
    protected void btsearch_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "popupwindow('lookpayment.aspx?pt=" + cbpaymenttype.SelectedValue.ToString() + "');", true);
        btprint.Visible = true;
        btsave.Visible = false;
    }
    protected void btlookup_Click(object sender, EventArgs e)
    {
        System.Data.SqlClient.SqlDataReader rs = null;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@payment_no", hdpaid.Value.ToString()));
        bll.vGetPaymentReceipt(arr, ref rs);
        while (rs.Read())
        {
            hdcust.Value = rs["cust_cd"].ToString();
            txcustomer.Text = rs["cust_cd"].ToString() + ":" + rs["cust_nm"].ToString();
            txcustomer.CssClass = "ro";
            cbpaymenttype.SelectedValue = rs["payment_typ"].ToString();
            cbstatus.SelectedValue = rs["payment_sta_id"].ToString();
            txtabno.Text = rs["tab_no"].ToString();
            txmanualno.Text = rs["manual_no"].ToString();
        } rs.Close();
        btinv_Click(sender, e);

    }
    protected void cbstatus_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    //protected void txdiscount_TextChanged(object sender, EventArgs e)
    //{
    //    foreach (GridViewRow dr in grd.Rows)
    //    {
    //        dtotdisc += Convert.ToDouble(((TextBox)dr.FindControl("txdiscount")).Text);
    //    }

    //    GridViewRow row1 = grd.FooterRow;
    //    ((Label)row1.FindControl("lbtotdisc")).Text = Convert.ToString(dtotdisc);
    //}
    //protected void txamt_TextChanged(object sender, EventArgs e)
    //{
    //    foreach (GridViewRow dr in grd.Rows)
    //    {
    //        dtotamt += Convert.ToDouble(((TextBox)dr.FindControl("txamt")).Text);
    //    }

    //    GridViewRow row1 = grd.FooterRow;
    //    ((Label)row1.FindControl("lbtotamtapp")).Text = Convert.ToString(dtotamt);
    //}
    protected void chpaidby_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void rdpaidfor_SelectedIndexChanged(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(500);
        if (rdpaidfor.SelectedValue.ToString() == "C")
        { txcustomer.CssClass = "divnormal"; cbgroup.CssClass = "divhid"; cbgroup.Items.Clear(); }
        else if (rdpaidfor.SelectedValue.ToString() == "G")
        {
            txcustomer.CssClass = "divhid"; cbgroup.CssClass = "divnormal"; hdcust.Value = ""; txcustomer.Text = "";
            bll.vBindingFieldValueToCombo(ref cbgroup, "cusgrcd");
            cbgroup_SelectedIndexChanged(sender, e);
        }
        rdpaidfor.CssClass = "ro";
    }
    protected void cbgroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(100);
        string sSalesman = string.Empty;
        try
        {
            sSalesman = bll.vLookUp("select top 1 salesman_cd from tmst_customer where salespointcd='" + Request.Cookies["sp"].Value.ToString() + "' and cusgrcd='" + cbgroup.SelectedValue.ToString() + "' and salesman_cd not in ('"+bll.sGetControlParameter("salesmanoffice")+"')");
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('There are problem in this group customer','"+ex.Message.ToString()+"','warning');", true);
            return;
        }
        List<cArrayList> arr = new List<cArrayList>();
        // arr.Add(new cArrayList("@", cbgroup.SelectedValue.ToString()));
       
            arr.Add(new cArrayList("@cusgrcd", cbgroup.SelectedValue.ToString()));
            bll.vBindingGridToSp(ref grd, "sp_tdosales_invoice_getbygroup", arr);
            arr.Clear();
            cbsalesman.CssClass = "divnormal";
          //  if (rdpaidfor.SelectedValue.ToString() == "C")
         //   {
                arr.Add(new cArrayList("@qry_cd", "SalesJob"));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vBindingComboToSp(ref cbsalesman, "sp_tmst_employee_getbyqry", "emp_cd", "emp_desc", arr);
                cbsalesman.SelectedValue = sSalesman;
          //  }
    }
    protected void btrefresh_Click(object sender, EventArgs e)
    {
        double dQty = 0; double dTot = 0;
        Label lbtotamt = (Label)grd.FooterRow.FindControl("lbtotamtpaid");
        foreach (GridViewRow row in grd.Rows)
        {
            TextBox txamt = (TextBox)row.FindControl("txamt");
            TextBox txdiscount = (TextBox)row.FindControl("txdiscount");
            Label lbremain = (Label)row.FindControl("lbremain");
            Label lbinvno = (Label)row.FindControl("lbinvoiceno");
            if ((Convert.ToDouble(lbremain.Text) - ((Convert.ToDouble(txamt.Text) + Convert.ToDouble(txdiscount.Text)))) < 0)
            {
                txamt.Text = "0"; txdiscount.Text = "0";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('Payment invoice bigger than invoice value','"+lbinvno.Text+"','warning');", true);
                return;
            }
            //// Add discount 1 % -------------------------------------------------
            //Label lbremain = (Label)row.FindControl("lbremain");
            //if (Convert.ToDouble(lbremain.Text) == Convert.ToDouble(txamt.Text))
            //{ 
            //    txamount.Text = (Convert.ToDouble(txamount.Text) + (1/100 * Convert.ToDouble(txamount.Text))).ToString(); 
            //}
            //---------------------------------------------------------------------
            if (double.TryParse(txamt.Text, out dQty))
            {
                dTot += dQty;
            }
            if (double.TryParse(txdiscount.Text, out dQty))
            {
                dTot += dQty;
            }
        }
        lbtotamt.Text = dTot.ToString();
        double dSuspense = Convert.ToDouble(txamount.Text) - dTot;
        if (dSuspense <= 0)
        { lbsuspense.Text = "0"; }
        else { lbsuspense.Text = dSuspense.ToString(); }
        // ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "window.alert('test');", true);
    }
    protected void btsuspense_Click(object sender, EventArgs e)
    {
        lbsuspense.Text = "12222";
    }
    protected void chdisc_CheckedChanged(object sender, EventArgs e)
    {

    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList2(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        HttpCookie cok;
        cok = HttpContext.Current.Request.Cookies["sp"];
        List<string> lcust = new List<string>();
        List<cArrayList> arr = new List<cArrayList>();
        string scust = string.Empty;
        System.Data.SqlClient.SqlDataReader rs = null;
        arr.Add(new cArrayList("@manual_no", prefixText));
        arr.Add(new cArrayList("@salespointcd", cok.Value.ToString()));
        bll.vSearchMstCustomerByCusgrcd(arr, ref rs);
        while (rs.Read())
        {
            scust = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["cust_cd"].ToString() + "-" + rs["cust_nm"].ToString(), rs["cust_cd"].ToString());
            lcust.Add(scust);
        }
        rs.Close();

        return (lcust.ToArray());
    }
}