using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using AjaxControlToolkit;
public partial class cashamountclosing : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lbchclosing_dt.Text = Request.Cookies["waz_dt"].Value.ToString();
            string cashin, cashout,cashbal, ssp, cashin2 ,cashout2;
            double damount;
            DateTime dtdate= Convert.ToDateTime(DateTime.ParseExact(lbchclosing_dt.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture));
            DateTime sdtdate = new DateTime(dtdate.Year, dtdate.Month, dtdate.Day);
            DateTime sdtdateprev = sdtdate.AddDays(-1);
            string sdate = sdtdate.ToString("M/d/yyyy");
            string sdateprev = sdtdateprev.ToString("M/d/yyyy");
            ssp = Request.Cookies["sp"].Value.ToString();
            cashin = bll.vLookUp("select isnull(sum(amt),0) from tcashregister inner join tmst_payment on tcashregister.ref_no=tmst_payment.payment_no and tcashregister.salespointcd=tmst_payment.salespointcd where cash_dt='" + sdate + "' and tcashregister.cash_sta_id='C'  and  tmst_payment.salespointcd='" + ssp + "'");
            cashin2 = bll.vLookUp("select isnull(sum(amt),0) from tcashregister a join tmst_itemcashout b on a.itemco_cd=b.itemco_cd where " +
            "b.inout='I' and datediff(d,cash_dt,dbo.fn_getsystemdate('"+ssp+"'))=0 and a.ref_no not in (select payment_no from tmst_payment where salespointcd='"+ssp+"') and a.salespointcd='"+ssp+"'");
            cashout = bll.vLookUp("select isnull(sum(tcashregister.amt),0) from tcashregister inner join tcashout_request on tcashregister.ref_no=tcashout_request.cashout_cd and tcashregister.salespointcd=tcashout_request.salespointcd join tmst_itemcashout on tcashout_request.itemco_cd=tmst_itemcashout.itemco_cd where  inout='O' and cash_dt='" + sdate + "' and tcashregister.cash_sta_id in ('A','R','C')  and  tcashout_request.salespointcd='" + ssp + "'");
            cashout2 = bll.vLookUp("select isnull(sum(tcashregister.amt),0) from tcashregister inner join tcashregout_dtl on tcashregister.ref_no=tcashregout_dtl.casregout_cd and tcashregister.salespointcd=tcashregout_dtl.salespointcd where cash_dt='" + sdate + "' and tcashregister.cash_sta_id in ('A','R','C') and tcashregister.salespointcd='"+ssp+"'");
            cashbal = bll.vLookUp("select  isnull(sum(currentbalance),0) from tcashregister_balance where reg_dt='" + sdateprev + "' and salespointcd='"+ssp+"'");
            damount = Convert.ToDouble(cashbal)+Convert.ToDouble(cashin) + Convert.ToDouble(cashin2)  - Convert.ToDouble(cashout) - Convert.ToDouble(cashout2);
            lbtotalamount.Text =  Math.Round( damount, 2).ToString();
            string sCheck = bll.vLookUp("select dbo.fn_cashregisterpending('"+ssp+"')");
            if (sCheck != "ok")
            {
                lbalert.Text = "Pending:" + sCheck;
                //bll.vBindingGridToSp(ref grd, "sp_tcashregister_getbycashstaid");
            }
            else { lbalert.Text = "No Pending Cashregister"; }
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabErr", "tabErr();", true);
    }

    public void getit()
    {
        lbtot500.Text = "semprul";
    }
    protected void txamt500_TextChanged(object sender, EventArgs e)
    {
        double d500 = 0;
        if (!double.TryParse(txamt500.Text, out d500))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Amount 500 must be numeric','wrong input','warning');", true);
            txamt500.Text = "";
            lbtot500.Text = "";
            sGetTotalAmount();
            txamt100.Focus();
            
            return;
        }
        lbtot500.Text = (500 * Convert.ToDouble(txamt500.Text)).ToString();
        sGetTotalAmount();
    }
    protected void txamt100_TextChanged(object sender, EventArgs e)
    {
        double d100 = 0;
        if (!double.TryParse(txamt100.Text, out d100))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Amount 100 must be numeric','wrong input','warning');", true);
            txamt100.Text = "";
            lbtot100.Text = "";
            sGetTotalAmount();
            txamt50.Focus();
            
            return;
        }
        lbtot100.Text = (100 * Convert.ToDouble(txamt100.Text)).ToString();
        sGetTotalAmount();
    }
    protected void txamt50_TextChanged(object sender, EventArgs e)
    {
        double d50 = 0;
        if (!double.TryParse(txamt50.Text, out d50))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Amount 50 must be numeric','wrong input','warning');", true);
            txamt50.Text = "";
            lbtot50.Text = "";
            sGetTotalAmount();
            txamt20.Focus();
            return;
        }
        lbtot50.Text = (50 * Convert.ToDouble(txamt50.Text)).ToString();
        sGetTotalAmount();
    }
    protected void txamt20_TextChanged(object sender, EventArgs e)
    {
        double d20 = 0;
        if (!double.TryParse(txamt20.Text, out d20))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Amount 20 must be numeric','wrong input','warning');", true);
            txamt20.Text = "";
            lbtot20.Text = "";
            sGetTotalAmount();
            txamt10.Focus();
            return;
        }
        lbtot20.Text = (20 * Convert.ToDouble(txamt20.Text)).ToString();
        sGetTotalAmount();
    }
    protected void txamt10_TextChanged(object sender, EventArgs e)
    {
        double d10 = 0;
        if (!double.TryParse(txamt10.Text, out d10))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Amount 10 must be numeric','wrong input','warning');", true);
            txamt10.Text = "";
            lbtot10.Text = "";
            sGetTotalAmount();
            txamt5.Focus();
            return;
        }
        lbtot10.Text = (10 * Convert.ToDouble(txamt10.Text)).ToString();
        sGetTotalAmount();
    }
    protected void txamt5_TextChanged(object sender, EventArgs e)
    {
        double d5 = 0;
        if (!double.TryParse(txamt5.Text, out d5))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Amount 5 must be numeric','wrong input','warning');", true);
            txamt5.Text = "";
            lbtot5.Text = "";
            sGetTotalAmount();
            txamt1.Focus();
            return;
        }
        lbtot5.Text = (5 * Convert.ToDouble(txamt5.Text)).ToString();
        sGetTotalAmount();
    }
    protected void txamt1_TextChanged(object sender, EventArgs e)
    {
        double d1 = 0;
        if (!double.TryParse(txamt1.Text, out d1))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Amount 1 must be numeric','wrong input','warning');", true);
            txamt1.Text = "";
            lbtot1.Text = "";
            sGetTotalAmount();
            txamthalf.Focus();
            return;
        }
        lbtot1.Text = (1 * Convert.ToDouble(txamt1.Text)).ToString();
        sGetTotalAmount();
    }
    protected void txamthalf_TextChanged(object sender, EventArgs e)
    {
        double dhalf = 0;
        if (!double.TryParse(txamthalf.Text, out dhalf))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Amount 0.5 must be numeric','wrong input','warning');", true);
            txamthalf.Text = "";
            lbtot05.Text = "";
            sGetTotalAmount();
            txamt025.Focus();
            return;
        }
        lbtot05.Text = (0.5 * Convert.ToDouble(txamthalf.Text)).ToString();
        sGetTotalAmount();
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        DateTime dtdate = Convert.ToDateTime(DateTime.ParseExact(lbchclosing_dt.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture));
        DateTime sdtdate = new DateTime(dtdate.Year, dtdate.Month, dtdate.Day);
        string sdate = sdtdate.ToString("M/d/yyyy");
        string ssp = Request.Cookies["sp"].Value.ToString();
        string cek = bll.vLookUp("select count(*) from tcashregister_closing where salespointcd='" + ssp + "' and chclosing_dt='" + sdate + "'");
        if (cek != "0")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Data was Closing','Entry ','warning');", true);
            return;
        }
        if (lbchclosingno.Text != "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Data was Save','Entry ','warning');", true);
            return;
        }
        else
        {
            string sTapPayment = bll.vLookUp("select dbo.fn_checktabpayment('"+ssp+"')").ToString();
            if (sTapPayment != "ok")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('There are tab payment not yet transfered today','" + sTapPayment + "','warning');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "cl", "vDisableShow();", true);
                return;
            }
            if (lbtotal.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('There is no Amount','Entry ','warning');", true);
                return;
            }
            double dTotalPaid = Convert.ToDouble(lbtotalamount.Text);
            if (Convert.ToDouble(lbtotal.Text) != dTotalPaid)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Amount  was not the same with data entry','Check your Amount','warning');", true);
                return;
            }
            string sCanvasPending = bll.vLookUp("select dbo.fn_checkcustpaymentcash('"+ssp+"')").ToString();
            if (sCanvasPending != "ok")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Customer cash is not paid by cash today','" + sCanvasPending + "','warning');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "cl", "vDisableShow();", true);
                return;
            }

            string sPaidCash = bll.vLookUp("select dbo.fn_checkpaidbycash('"+ssp+"')").ToString();
            if (sCanvasPending != "ok")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('There are paid cash on transaction but not yet paid !','" + sPaidCash + "','warning');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "cl", "vDisableShow();", true);
                return;
            }
            string sPaymentNo = bll.vLookUp("select dbo.fn_checkpaidnotreceived('"+ssp+"')");
            if (sPaymentNo != "ok")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('There are CASH IN not yet received !','" + sPaymentNo + "','warning');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "cl", "vDisableShow();", true);
                return;
            }
            string schclosingno = "";
            if (lbalert.Text != "No Pending Cashregister")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('There are pending paid/received all !','Cash Register','warning');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "cl", "vDisableShow();", true);
                return;
            }
            List<cArrayList> arr = new List<cArrayList>();
            try
            {
               
                //arr.Add(new cArrayList("@chclosingno", sCashID));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                arr.Add(new cArrayList("@chclosing_dt", DateTime.ParseExact(Request.Cookies["waz_dt"].Value.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                arr.Add(new cArrayList("@Amount", ((lbtotalamount.Text == "") ? "0" : lbtotalamount.Text)));
                arr.Add(new cArrayList("@amt100", ((txamt100.Text == "") ? "0" : txamt100.Text)));
                arr.Add(new cArrayList("@amt500", ((txamt500.Text == "") ? "0" : txamt500.Text)));
                arr.Add(new cArrayList("@amt50", ((txamt50.Text == "") ? "0" : txamt50.Text)));
                arr.Add(new cArrayList("@amt20", ((txamt20.Text == "") ? "0" : txamt20.Text)));
                arr.Add(new cArrayList("@amt10", ((txamt10.Text == "") ? "0" : txamt10.Text)));
                arr.Add(new cArrayList("@amt5", ((txamt5.Text == "") ? "0" : txamt5.Text)));
                arr.Add(new cArrayList("@amt1", ((txamt1.Text == "") ? "0" : lbtot1.Text)));
                arr.Add(new cArrayList("@amt05", ((txamthalf.Text == "") ? "0" : txamthalf.Text)));
                arr.Add(new cArrayList("@amt025", ((txamt025.Text == "") ? "0" : txamt025.Text)));
                arr.Add(new cArrayList("@amt01", ((txamt01.Text == "") ? "0" : txamt01.Text)));
                arr.Add(new cArrayList("@chclosing_description", txchclosing_description.Text));
                arr.Add(new cArrayList("@createdby", Request.Cookies["fullname"].Value.ToString()));
                arr.Add(new cArrayList("@acknowledge", "N"));
                bll.vInserttcashregister_closing(arr, ref schclosingno);
                arr.Clear();
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                arr.Add(new cArrayList("@closing_dt", System.DateTime.ParseExact(Request.Cookies["waz_dt"].Value.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                bll.vBatchClosingCashier(arr);
               
                lbchclosingno.Text = schclosingno;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Data Save successfully ..','Transaction No. " + schclosingno + "','info');", true);
                btsave.Visible = false;
                int token;
                Uri urlnya = Request.Url; string sMsg;
                string host = urlnya.GetLeftPart(UriPartial.Authority);
                Random rnd = new Random();
                token = rnd.Next(1000,9999);
                string stoken = bll.vLookUp("select short_ip from tmst_salespoint where salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'") + token.ToString();
                double tokenmail = rnd.Next();
                List<string> lapp = bll.lGetApproval("cashier", 1);
                List<string> ledp = bll.lGetApproval("edp", 1);
                string sMax = bll.vLookUp("select dbo.fn_getcontrolparameter('maxlimitcashier')");
               // cd.vSendSms("Your Cashier Balance as : " + Request.Cookies["waz_dt"].Value.ToString() + " is " + lbtotalamount.Text + "(Max Allowed : "+sMax+"),reply for acknowledge with  Y" + token.ToString(), lapp[0]);
                string sSMS = "#Cashier Balance  for " +bll.sGetSalespointname(Request.Cookies["sp"].Value.ToString())+ " as : " + Request.Cookies["waz_dt"].Value.ToString() + ":" + lbtotalamount.Text + "(Max Allowed : "+sMax+"),reply for acknowledge with  Y" + stoken.ToString();
                arr.Clear();
                arr.Add(new cArrayList("@doc_typ", "cashier"));
                arr.Add(new cArrayList("@token", stoken.ToString()));
                arr.Add(new cArrayList("@to", lapp[0]));
                arr.Add(new cArrayList("@msg", sSMS));
                arr.Add(new cArrayList("@doc_no", lbchclosingno.Text)); //modify by yanto 2016-4-7
                bll.vInsertSmsOutbox(arr);
                // bll.vInsertSMSSent(arr);

                // Email session --------------------
                double dCash = 0;
                string sSubject = "Cashier Balance " + Request.Cookies["waz_dt"].Value.ToString();
                string sBody = "<table><tr><td>Cash In Cash Register is : " + lbtotalamount.Text + "</td><td></td></tr>";
                sBody += " <a href='" + host + "/landingpage.aspx?appcode=" + tokenmail.ToString() + "&trnname=cashier&sta=N&RefNo=" + lbchclosingno.Text + "&salespointcd=" + Request.Cookies["sp"].Value.ToString() + "'>Reject</a> OR <a href='" + host + "/landingpage.aspx?appcode=" + tokenmail.ToString() + "&sta=Y&RefNo=" + lbchclosingno.Text + "&trnname=cashier&salespointcd=" + Request.Cookies["sp"].Value.ToString() + "'>Approve</a>" +
                  "\r\n\r\n Wazaran Admin";

                bll.vSendMail(lapp[1], sSubject, sBody);
                arr.Clear();
                arr.Add(new cArrayList("@token", tokenmail.ToString()));
                arr.Add(new cArrayList("@doc_typ", "cashier"));
                arr.Add(new cArrayList("@to", lapp[1]));
                arr.Add(new cArrayList("@doc_no", lbchclosingno.Text));
                arr.Add(new cArrayList("@emailsubject", sSubject));
                arr.Add(new cArrayList("@msg", sBody));
                arr.Add(new cArrayList("@file_attachment", null));
                bll.vInsertEmailOutbox(arr); //by yanto 23-1-2017

                arr.Clear();
                arr.Add(new cArrayList("@trxcd", "cashier"));
                arr.Add(new cArrayList("@token", tokenmail.ToString()));
                //arr.Add(new cArrayList("@doc_no", Request.Cookies["waz_dt"].Value.ToString()));
                arr.Add(new cArrayList("@doc_no", lbchclosingno.Text)); //modify by yanto 2016-4-7
                bll.vInsertEmailSent(arr);
            }
            catch (Exception ex)
            {
                arr.Clear();
                arr.Add(new cArrayList("@err_source", "Save Cashamount Closing"));
                arr.Add(new cArrayList("@err_description", ex.Message.ToString()));
                bll.vInsertErrorLog(arr);
            }
            //----------------------- BAtas originial
            //List<string> lapproval = bll.lGetApproval("branchspv", 1);
            //string sSMSText = "Your Cashier Balance at : " + Request.Cookies["waz_dt"].Value.ToString() + ", SAR : " + lbtotalamount.Text + " ,Please approved with send ";
            //cd.vSendSms(sSMSText, lapproval[0]);            
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "cl", "window.opener.RefreshData();window.close();", true);
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "updpnl", "closewin()", true);
        }
    }

    void sGetTotalAmount()
    { 
        double dSum = 0;
        if (lbtot500.Text != "")
        {
            dSum += Convert.ToDouble(lbtot500.Text);
        }

        if (lbtot100.Text != "")
        {
            dSum += Convert.ToDouble(lbtot100.Text);
        }
        if (lbtot50.Text != "")
        {
            dSum += Convert.ToDouble(lbtot50.Text);
        }
        if (lbtot20.Text != "")
        {
            dSum += Convert.ToDouble(lbtot20.Text);
        }
        if (lbtot10.Text != "")
        {
            dSum += Convert.ToDouble(lbtot10.Text);
        }
        if (lbtot5.Text != "")
        {
            dSum += Convert.ToDouble(lbtot5.Text);
        }
        if (lbtot1.Text != "")
        {
            dSum += Convert.ToDouble(lbtot1.Text);
        }
        if (lbtot05.Text != "")
        {
            dSum += Convert.ToDouble(lbtot05.Text);
        }
        if (lbtot025.Text != "")
        {
            dSum += Convert.ToDouble(lbtot025.Text);
        }
        if (lbtot01.Text != "")
        {
            dSum += Convert.ToDouble(lbtot01.Text);
        }
        lbtotal.Text = dSum.ToString();
    }
    protected void txamt01_TextChanged(object sender, EventArgs e)
    {
        double d01 = 0;
        if (!double.TryParse(txamt01.Text, out d01))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Amount 0.1 must be numeric','wrong input','warning');", true);
            txamt01.Text = "";
            lbtot01.Text = "";
            sGetTotalAmount();
            btsave.Focus();
            ;return;
        }
        lbtot01.Text = (0.1 * Convert.ToDouble(txamt01.Text)).ToString();
        sGetTotalAmount();
    }
    protected void txamt025_TextChanged(object sender, EventArgs e)
    {
        double d025 = 0;
        if (!double.TryParse(txamt025.Text, out d025))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Amount 0.25 must be numeric','wrong input','warning');", true);
            txamt025.Text = "";
            lbtot025.Text = "";
            sGetTotalAmount();
            txamt01.Focus();
            ; return;
        }
        lbtot025.Text = (0.25 * Convert.ToDouble(txamt025.Text)).ToString();
        sGetTotalAmount();
    }
    protected void btclose_Click(object sender, EventArgs e)
    {
        Response.Redirect("default.aspx");
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "cl", "window.opener.RefreshData();window.close();", true);
    }
    protected void bttmp_Click(object sender, EventArgs e)
    {
        SqlDataReader rs = null;
        lbchclosingno.Text = Convert.ToString(Session["loochclosingno"]);
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@chclosingno", lbchclosingno.Text));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        bll.vGettcashregister_closing(arr, ref rs);
        while (rs.Read())
        {
            string sdate = string.Format("{0:d/M/yyyy}", rs["chclosing_dt"]);
            //DateTime dtdate = Convert.ToDateTime(sdate);
            //string strDate = dtdate.ToString("d/M/yyyy");
            lbchclosing_dt.Text = sdate;
            txchclosing_description.Text= rs["chclosing_description"].ToString();
            lbtotalamount.Text = rs["Amount"].ToString();
            txamt500.Text = rs["amt500"].ToString();
            txamt500_TextChanged(sender, e);
            txamt100.Text = rs["amt100"].ToString();
            txamt100_TextChanged(sender, e);
            txamt50.Text = rs["amt50"].ToString();
            txamt50_TextChanged(sender, e);
            txamt20.Text = rs["amt20"].ToString();
            txamt20_TextChanged(sender, e);
            txamt10.Text = rs["amt10"].ToString();
            txamt10_TextChanged(sender, e);
            txamt5.Text = rs["amt5"].ToString();
            txamt5_TextChanged(sender, e);
            txamt1.Text = rs["amt1"].ToString();
            txamt1_TextChanged(sender, e);
            txamthalf.Text = rs["amt05"].ToString();
            txamthalf_TextChanged(sender, e);
            txamt025.Text = rs["amt025"].ToString();
            txamt025_TextChanged(sender, e);
            txamt01.Text = rs["amt01"].ToString();
            txamt01_TextChanged(sender, e);
        } rs.Close();
    }
    
}