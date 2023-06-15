using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_cashregister : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                bll.vBindingFieldValueToCombo(ref cbinout, "inout");
                cbinout_SelectedIndexChanged(sender, e);
                getamt();

            }
            catch (Exception ex)
            {
                Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
                bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_cashregister");
                Response.Redirect("fm_ErrorPage.aspx");
            }

        }
    }
    protected void cbinout_SelectedIndexChanged(object sender, EventArgs e)
    {
        vInitGrid();
        if (grd.Rows.Count > 0)
        { btaccept.CssClass = "btn btn-primary"; }
        else { btaccept.CssClass = "btn btn-default ro"; }
    }

    void vInitGrid()
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@inout", cbinout.SelectedValue.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grd, "sp_tcashregister_getbyio", arr);
            if (grd.Rows.Count > 0)
            {
                Label lbtotamt = (Label)grd.FooterRow.FindControl("lbtotamt");
                Label lbtotvatamt = (Label)grd.FooterRow.FindControl("lbtotvatamt");
                lbtotamt.Text = bll.vLookUp("select dbo.fn_getamtcashregister('" + cbinout.SelectedValue.ToString() + "','" + Request.Cookies["sp"].Value.ToString() + "')");
                lbtotvatamt.Text = bll.vLookUp("select dbo.fn_getvatamtcashregister('" + cbinout.SelectedValue.ToString() + "','" + Request.Cookies["sp"].Value.ToString() + "')");
            }

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_cashregister");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    private void getamt()
    {
        try
        {
            string dtcash = Request.Cookies["waz_dt"].Value.ToString();
            string cashin, cashout, cashbal, ssp, cashin2, cashout2;
            double damount;
            DateTime dtdate = Convert.ToDateTime(DateTime.ParseExact(dtcash, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture));
            DateTime sdtdate = new DateTime(dtdate.Year, dtdate.Month, dtdate.Day);
            DateTime sdtdateprev = sdtdate.AddDays(-1);
            string sdate = sdtdate.ToString("M/d/yyyy");
            string sdateprev = sdtdateprev.ToString("M/d/yyyy");
            ssp = Request.Cookies["sp"].Value.ToString();
            cashin = bll.vLookUp("select isnull(sum(amt),0) from tcashregister left outer join tmst_payment on tcashregister.ref_no=tmst_payment.payment_no and tcashregister.salespointcd=tmst_payment.salespointcd where cash_dt='" + sdate + "' and tcashregister.cash_sta_id='C'  and  tmst_payment.salespointcd='" + ssp + "'");

            cashin2 = bll.vLookUp("select isnull(sum(amt),0) from tcashregister a join tmst_itemcashout b on a.itemco_cd=b.itemco_cd where " +
                "b.inout='I' and datediff(d,cash_dt,dbo.fn_getsystemdate('" + Request.Cookies["sp"].Value.ToString() + "'))=0 and a.ref_no not in (select payment_no from tmst_payment where salespointcd='" + Request.Cookies["sp"].Value.ToString() + "') and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
            double dTotIn = Convert.ToDouble(cashin) + Convert.ToDouble(cashin2);
            cashout = bll.vLookUp("select isnull(sum(tcashregister.amt),0) from tcashregister inner join tcashout_request on tcashregister.ref_no=tcashout_request.cashout_cd and tcashregister.salespointcd=tcashout_request.salespointcd join tmst_itemcashout on tcashout_request.itemco_cd=tmst_itemcashout.itemco_cd where inout='O' and cash_dt='" + sdate + "' and tcashregister.cash_sta_id in ('A','R','C') and tcashregister.salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
            cashbal = bll.vLookUp("select  isnull(sum(currentbalance),0) from tcashregister_balance where reg_dt='" + sdateprev + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
            cashout2 = bll.vLookUp("select isnull(sum(tcashregister.amt),0) from tcashregister inner join tcashregout_dtl on tcashregister.ref_no=tcashregout_dtl.casregout_cd and tcashregister.salespointcd=tcashregout_dtl.salespointcd where cash_dt='" + sdate + "' and tcashregister.cash_sta_id in ('A','R','C') and tcashregister.salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
            damount = Convert.ToDouble(cashbal) + Convert.ToDouble(cashin) + Convert.ToDouble(cashin2) - (Convert.ToDouble(cashout) + Convert.ToDouble(cashout2));
            lbopenbalance.Text = bll.vLookUp("select dbo.fn_getprevbalcashier('" + Request.Cookies["sp"].Value.ToString() + "')");
            lbcashin.Text = dTotIn.ToString(); // cashin.ToString();
            lbcashout.Text = (Convert.ToDouble(cashout) + Convert.ToDouble(cashout2)).ToString();
            lbcashClosing.Text = Math.Round(damount, 2).ToString();

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_cashregister");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void btaccept_Click(object sender, EventArgs e)
    {
        try
        {
            string depositho = "", bankconfirm = "";
            Boolean statusCashout = true;
            List<cArrayList> arr = new List<cArrayList>();
            foreach (GridViewRow row in grd.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    HiddenField hdids = (HiddenField)row.FindControl("hdids");
                    Label lbrefno = (Label)row.FindControl("lbrefno");
                    depositho = bll.vLookUp("select ref_no from tcashregister where cash_id='" + hdids.Value.ToString() + "' and itemco_cd in (select qry_data from tmap_query where qry_cd = 'cashdepositho')");
                    if (depositho != "") {
                        bankconfirm = bll.vLookUp("select dep_sta_id from tbank_deposit where ref_no='" + depositho + "'");
                        if (bankconfirm != "H" && bankconfirm != "C") { statusCashout = false; }
                    }
                    if (statusCashout)
                    {
                        arr.Clear();
                        arr.Add(new cArrayList("@cash_id", hdids.Value.ToString()));
                        arr.Add(new cArrayList("@cash_sta_id", "C"));
                        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                        bll.vUpdateCashRegister(arr);
                        arr.Clear();
                        if (cbinout.SelectedValue.ToString() == "I")
                        {
                            arr.Add(new cArrayList("@trn_typ", "CASHIN"));
                        }
                        else if (cbinout.SelectedValue.ToString() == "O")
                        {
                            arr.Add(new cArrayList("@trn_typ", "CASHOUT"));
                        }
                        arr.Add(new cArrayList("@refno", lbrefno.Text));
                        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                        bll.vBatchAccTransactionLog(arr);
                    }                    
                }
            }
            vInitGrid();

            btaccept.CssClass = "btn btn-primary ro";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();sweetAlert('Received or Paid ALL completed!','received/paid','success');", true);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_cashregister");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
}