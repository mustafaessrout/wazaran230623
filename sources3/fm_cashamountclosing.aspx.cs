using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_cashamountclosing : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                List<cArrayList> arr = new List<cArrayList>();
                lbchclosing_dt.Text = Request.Cookies["waz_dt"].Value.ToString();
                string cashin, cashout, cashbal, ssp, cashin2, cashout2;
                double damount;
                DateTime dtdate = Convert.ToDateTime(DateTime.ParseExact(lbchclosing_dt.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture));
                DateTime sdtdate = new DateTime(dtdate.Year, dtdate.Month, dtdate.Day);
                DateTime sdtdateprev = sdtdate.AddDays(-1);
                string sdate = sdtdate.ToString("M/d/yyyy");
                string sdateprev = sdtdateprev.ToString("M/d/yyyy");
                ssp = Request.Cookies["sp"].Value.ToString();
                cashin = bll.vLookUp("select isnull(sum(amt),0) from tcashregister inner join tmst_payment on tcashregister.ref_no=tmst_payment.payment_no and tcashregister.salespointcd=tmst_payment.salespointcd where cash_dt='" + sdate + "' and tcashregister.cash_sta_id='C'  and  tmst_payment.salespointcd='" + ssp + "'");
                cashin2 = bll.vLookUp("select isnull(sum(amt),0) from tcashregister a join tmst_itemcashout b on a.itemco_cd=b.itemco_cd where " +
                "b.inout='I' and datediff(d,cash_dt,dbo.fn_getsystemdate('" + ssp + "'))=0 and a.ref_no not in (select payment_no from tmst_payment where salespointcd='" + ssp + "') and a.salespointcd='" + ssp + "'");
                cashout = bll.vLookUp("select isnull(sum(tcashregister.amt),0) from tcashregister inner join tcashout_request on tcashregister.ref_no=tcashout_request.cashout_cd and tcashregister.salespointcd=tcashout_request.salespointcd join tmst_itemcashout on tcashout_request.itemco_cd=tmst_itemcashout.itemco_cd where  inout='O' and cash_dt='" + sdate + "' and tcashregister.cash_sta_id in ('A','R','C')  and  tcashout_request.salespointcd='" + ssp + "'");
                cashout2 = bll.vLookUp("select isnull(sum(tcashregister.amt),0) from tcashregister inner join tcashregout_dtl on tcashregister.ref_no=tcashregout_dtl.casregout_cd and tcashregister.salespointcd=tcashregout_dtl.salespointcd where cash_dt='" + sdate + "' and tcashregister.cash_sta_id in ('A','R','C') and tcashregister.salespointcd='" + ssp + "'");
                cashbal = bll.vLookUp("select  isnull(sum(currentbalance),0) from tcashregister_balance where reg_dt='" + sdateprev + "' and salespointcd='" + ssp + "'");
                damount = Convert.ToDouble(cashbal) + Convert.ToDouble(cashin) + Convert.ToDouble(cashin2) - Convert.ToDouble(cashout) - Convert.ToDouble(cashout2);
                txtotalAmount.Text = Math.Round(damount, 2).ToString();
                
                string sCheck = bll.vLookUp("select dbo.fn_cashregisterpending('" + ssp + "')");
                if (sCheck != "ok")
                {
                    lbalert.Text = "Pending:" + sCheck;
                    //bll.vBindingGridToSp(ref grd, "sp_tcashregister_getbycashstaid");
                }
                else { lbalert.Text = "No Pending Cashregister"; }

                // show all denomations
                arr.Clear();
                bll.vBindingGridToSp(ref grd, "sp_currency_get", arr);
                Label lbtotsubtotal = (Label)grd.FooterRow.FindControl("lbtotsubtotal");
                lbtotsubtotal.Text = txtotalAmount.Text.ToString();

            }
            catch (Exception ex)
            {
                Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
                bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_cashamountclosing");
                Response.Redirect("fm_ErrorPage.aspx");
            }

        }
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabErr", "tabErr();", true);    
    }

    protected void btsave_Click(object sender, EventArgs e)
    {
        try
        {
            FileInfo fi = null;

            if ((fucashier.FileName == ""))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Document of Cashier Closing must be uploaded!','Document ','warning');", true);
                return;
            }

            if (fucashier.HasFile)
            {
                fi = new FileInfo(fucashier.FileName);
                byte[] by = fucashier.FileBytes;
                if (by.Length > 1000000)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('File uploaded can not more than 1 MB!','Closing Cashier','warning');", true);
                    return;
                }
            }

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
                string sTapPayment = bll.vLookUp("select dbo.fn_checktabpayment('" + ssp + "')").ToString();
                if (sTapPayment != "ok")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('There are tab payment not yet transfered today','" + sTapPayment + "','warning');", true);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "cl", "vDisableShow();", true);
                    return;
                }
                double dTotalPaid = Convert.ToDouble(txtotalAmount.Text);
                Label lbtotsubtotal = (Label)grd.FooterRow.FindControl("lbtotsubtotal");
                if (dTotalPaid != 0)
                {
                    if (lbtotsubtotal.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('There is no Amount','Entry ','warning');", true);
                        return;
                    }
                }                            
                if (Convert.ToDouble(lbtotsubtotal.Text) != dTotalPaid)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Amount  was not the same with data entry','Check your Amount','warning');", true);
                    return;
                }
                if (Convert.ToDouble(lbtotsubtotal.Text) < 0 )
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Amount must be greater than 0 !!!','Check your Amount','warning');", true);
                    return;
                }


                string sCanvasPending = bll.vLookUp("select dbo.fn_checkcustpaymentcash('" + ssp + "')").ToString();
                if (sCanvasPending != "ok")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Customer cash is not paid by cash today','" + sCanvasPending + "','warning');", true);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "cl", "vDisableShow();", true);
                    return;
                }

                string sPaidCash = bll.vLookUp("select dbo.fn_checkpaidbycash('" + ssp + "')").ToString();
                if (sCanvasPending != "ok")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('There are paid cash on transaction but not yet paid !','" + sPaidCash + "','warning');", true);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "cl", "vDisableShow();", true);
                    return;
                }
                string sPaymentNo = bll.vLookUp("select dbo.fn_checkpaidnotreceived('" + ssp + "')");
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


                //arr.Add(new cArrayList("@chclosingno", sCashID));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                arr.Add(new cArrayList("@chclosing_dt", DateTime.ParseExact(Request.Cookies["waz_dt"].Value.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                arr.Add(new cArrayList("@Amount", ((txtotalAmount.Text == "") ? "0" : txtotalAmount.Text)));
                //arr.Add(new cArrayList("@amt100", ((txamt100.Text == "") ? "0" : txamt100.Text)));
                //arr.Add(new cArrayList("@amt500", ((txamt500.Text == "") ? "0" : txamt500.Text)));
                //arr.Add(new cArrayList("@amt50", ((txamt50.Text == "") ? "0" : txamt50.Text)));
                //arr.Add(new cArrayList("@amt20", ((txamt20.Text == "") ? "0" : txamt20.Text)));
                //arr.Add(new cArrayList("@amt10", ((txamt10.Text == "") ? "0" : txamt10.Text)));
                //arr.Add(new cArrayList("@amt5", ((txamt5.Text == "") ? "0" : txamt5.Text)));
                //arr.Add(new cArrayList("@amt1", ((txamt1.Text == "") ? "0" : lbtot1.Text)));
                //arr.Add(new cArrayList("@amt05", ((txamthalf.Text == "") ? "0" : txamthalf.Text)));
                //arr.Add(new cArrayList("@amt025", ((txamt025.Text == "") ? "0" : txamt025.Text)));
                //arr.Add(new cArrayList("@amt01", ((txamt01.Text == "") ? "0" : txamt01.Text)));
                arr.Add(new cArrayList("@chclosing_description", txchclosing_description.Text));
                arr.Add(new cArrayList("@createdby", Request.Cookies["fullname"].Value.ToString()));
                arr.Add(new cArrayList("@acknowledge", "N"));
                bll.vInserttcashregister_closing(arr, ref schclosingno);
                arr.Clear();
                if (grd.Rows.Count > 0)
                {
                    double totalAmount = 0, amount;
                    foreach (GridViewRow row in grd.Rows)
                    {
                        arr.Clear();
                        Label lbtotal = (Label)row.FindControl("lbtotal");
                        Label lbdn_desc = (Label)row.FindControl("lbdn_desc");
                        HiddenField hdcur_cd = (HiddenField)row.FindControl("hdcur_cd");
                        HiddenField hddn_cd = (HiddenField)row.FindControl("hddn_cd");
                        HiddenField hddn_amt = (HiddenField)row.FindControl("hddn_amt");
                        TextBox txamount = (TextBox)row.FindControl("txamount");
                        if (!double.TryParse(txamount.Text, out amount))
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Amount must be numeric','Cash Amount','warning');", true);
                            return;
                        }
                        else
                        {
                            if (double.Parse(txamount.Text) > 0)
                            {
                                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                                arr.Add(new cArrayList("@chclosingno", schclosingno));
                                arr.Add(new cArrayList("@cur_cd", hdcur_cd.Value));
                                arr.Add(new cArrayList("@dn_cd", hddn_cd.Value));
                                arr.Add(new cArrayList("@dn_desc", lbdn_desc.Text));
                                arr.Add(new cArrayList("@dn_amount", hddn_amt.Value));
                                arr.Add(new cArrayList("@amount", txamount.Text));
                                bll.vInserttcashregisterclosing_dtl(arr);
                            }
                        }                      
                    }
                }

                string ext = fi.Extension;
                if ((fucashier.FileName != "") || (fucashier.FileName.Equals(null)))
                {
                    string sFileName = schclosingno + ext;
                    arr.Clear();
                    arr.Add(new cArrayList("@cashierfile", schclosingno + ext));
                    arr.Add(new cArrayList("@chclosingno", schclosingno));
                    arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                    bll.vUpdateCashierClosing(arr);
                    fucashier.SaveAs(bll.sGetControlParameter("image_path") + @"\cashier\" + sFileName);
                }

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
                token = rnd.Next(1000, 9999);
                string stoken = bll.vLookUp("select short_ip from tmst_salespoint where salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'") + token.ToString();
                double tokenmail = rnd.Next();
                List<string> lapp = bll.lGetApproval("cashier", 1);
                List<string> ledp = bll.lGetApproval("edp", 1);
                string sMax = bll.vLookUp("select dbo.fn_getcontrolparameter('maxlimitcashier')");
                // cd.vSendSms("Your Cashier Balance as : " + Request.Cookies["waz_dt"].Value.ToString() + " is " + lbtotalamount.Text + "(Max Allowed : "+sMax+"),reply for acknowledge with  Y" + token.ToString(), lapp[0]);
                string sSMS = "#Cashier Balance  for " + bll.sGetSalespointname(Request.Cookies["sp"].Value.ToString()) + " as : " + Request.Cookies["waz_dt"].Value.ToString() + ":" + txtotalAmount.Text + "(Max Allowed : " + sMax + "),reply for acknowledge with  Y" + stoken.ToString();
                //arr.Clear();
                //arr.Add(new cArrayList("@doc_typ", "cashier"));
                //arr.Add(new cArrayList("@token", stoken.ToString()));
                //arr.Add(new cArrayList("@to", lapp[0]));
                //arr.Add(new cArrayList("@msg", sSMS));
                //arr.Add(new cArrayList("@doc_no", lbchclosingno.Text)); //modify by yanto 2016-4-7
                //bll.vInsertSmsOutbox(arr);
                // bll.vInsertSMSSent(arr);

                // Email session --------------------
                double dCash = 0;
                string sSubject = "Cashier Balance " + Request.Cookies["waz_dt"].Value.ToString();
                string sBody = "<table><tr><td>Cash In Cash Register is : " + txtotalAmount.Text + "</td><td></td></tr>";
                sBody += " <a href='" + host + "/landingpage.aspx?appcode=" + tokenmail.ToString() + "&trnname=cashier&sta=N&RefNo=" + lbchclosingno.Text + "&salespointcd=" + Request.Cookies["sp"].Value.ToString() + "'>Reject</a> OR <a href='" + host + "/landingpage.aspx?appcode=" + tokenmail.ToString() + "&sta=Y&RefNo=" + lbchclosingno.Text + "&trnname=cashier&salespointcd=" + Request.Cookies["sp"].Value.ToString() + "'>Approve</a>" +
                  "\r\n\r\n Wazaran Admin";

                //bll.vSendMail(lapp[1], sSubject, sBody);
                //arr.Clear();
                //arr.Add(new cArrayList("@token", tokenmail.ToString()));
                //arr.Add(new cArrayList("@doc_typ", "cashier"));
                //arr.Add(new cArrayList("@to", lapp[1]));
                //arr.Add(new cArrayList("@doc_no", lbchclosingno.Text));
                //arr.Add(new cArrayList("@emailsubject", sSubject));
                //arr.Add(new cArrayList("@msg", sBody));
                //arr.Add(new cArrayList("@file_attachment", null));
                //bll.vInsertEmailOutbox(arr); //by yanto 23-1-2017

                //arr.Clear();
                //arr.Add(new cArrayList("@trxcd", "cashier"));
                //arr.Add(new cArrayList("@token", tokenmail.ToString()));
                ////arr.Add(new cArrayList("@doc_no", Request.Cookies["waz_dt"].Value.ToString()));
                //arr.Add(new cArrayList("@doc_no", lbchclosingno.Text)); //modify by yanto 2016-4-7
                //bll.vInsertEmailSent(arr);
            }
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_cashamountclosing");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    protected void btsearch_Click(object sender, EventArgs e)
    {

    }

    protected void txamount_TextChanged(object sender, EventArgs e)
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            if (grd.Rows.Count > 0)
            {
                double totalAmount = 0, amount;
                foreach (GridViewRow row in grd.Rows)
                {
                    Label lbtotal = (Label)row.FindControl("lbtotal");
                    Label lbdn_desc = (Label)row.FindControl("lbdn_desc");
                    HiddenField hdcur_cd = (HiddenField)row.FindControl("hdcur_cd");
                    HiddenField hddn_amt = (HiddenField)row.FindControl("hddn_amt");
                    TextBox txamount = (TextBox)row.FindControl("txamount");
                    if (!double.TryParse(txamount.Text, out amount))
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Amount " + lbdn_desc.Text + " must be numeric','wrong input','warning');", true);
                        return;
                    }
                    lbtotal.Text = Convert.ToString(double.Parse(hddn_amt.Value) * double.Parse(txamount.Text));
                    totalAmount = totalAmount + double.Parse(lbtotal.Text);
                }
                Label lbtotsubtotal = (Label)grd.FooterRow.FindControl("lbtotsubtotal");
                lbtotsubtotal.Text = totalAmount.ToString();
            }

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_cashamountclosing");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    protected void bttmp_Click(object sender, EventArgs e)
    {
        try
        {
            SqlDataReader rs = null;
            lbchclosingno.Text = Convert.ToString(Session["loochclosingno"]);
            Label lbtotsubtotal = (Label)grd.FooterRow.FindControl("lbtotsubtotal");
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
                txchclosing_description.Text = rs["chclosing_description"].ToString();
                txtotalAmount.Text = rs["Amount"].ToString();
                lbtotsubtotal.Text = rs["Amount"].ToString();
            }
            rs.Close();
            bll.vBindingGridToSp(ref grd, "sp_tcashregisterclosing_dtl_get", arr);
            btsave.Visible = false;

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_cashamountclosing");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
}