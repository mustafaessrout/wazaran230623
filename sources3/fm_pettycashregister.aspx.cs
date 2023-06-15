using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_pettycashregister : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingComboToSpWithEmptyChoosen(ref cbcashregister, "sp_tcashregister_id_get", "cashier_id", "name", arr);
            //cbcashregister_SelectedIndexChanged(sender, e);
            cd.v_hiddencontrol(btaccept);
        }
    }
    protected void cbinout_SelectedIndexChanged(object sender, EventArgs e)
    {
        vInitGrid();
        cd.v_showcontrol(btaccept);
    }

    void vInitGrid()
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@inout", cbinout.SelectedValue));
        arr.Add(new cArrayList("@cashier_id", cbcashregister.SelectedValue));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
        bll.vBindingGridToSp(ref grd, "sp_tcashregister_advance_getbyio", arr);
        if (grd.Rows.Count > 0)
        {
            Label lbtotamt = (Label)grd.FooterRow.FindControl("lbtotamt");
            Label lbtotvatamt = (Label)grd.FooterRow.FindControl("lbtotvatamt");
            lbtotamt.Text = bll.vLookUp("select dbo.fn_getamtcashregister_advance('" + cbinout.SelectedValue.ToString() + "','" + cbcashregister.SelectedValue.ToString() + "','"+ Request.Cookies["sp"].Value.ToString() + "')");
            lbtotvatamt.Text = bll.vLookUp("select dbo.fn_getvatamtcashregister_advance('" + cbinout.SelectedValue.ToString() + "','" + cbcashregister.SelectedValue.ToString() + "','"+ Request.Cookies["sp"].Value.ToString() + "')");
            lbcashClosing.Text = lbtotamt.Text;
        }
    }

    private void getamt()
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
        cashin = bll.vLookUp("select isnull(sum(amt),0) from tcashregister_advance left outer join tmst_payment on tcashregister_advance.ref_no=tmst_payment.payment_no where cashier_id='" + cbcashregister.SelectedValue.ToString() + "' and cash_dt='" + sdate + "' and tcashregister_advance.cash_sta_id='C'  and  tmst_payment.salespointcd='" + ssp + "'");
        cashin2 = bll.vLookUp("select isnull(sum(amt),0) from tcashregister_advance a join tmst_itemcashout b on a.itemco_cd=b.itemco_cd where cash_sta_id='C' and cashier_id='" + cbcashregister.SelectedValue.ToString() + "' and b.inout='I' and datediff(d,cash_dt,dbo.fn_getsystemdate("+ Request.Cookies["sp"].Value.ToString() + "))=0 and a.ref_no not in (select payment_no from tmst_payment)");
        double dTotIn = Convert.ToDouble(cashin) + Convert.ToDouble(cashin2);
        cashout = bll.vLookUp("select isnull(sum(tcashregister_advance.amt),0) from tcashregister_advance inner join tcashout_advance on tcashregister_advance.ref_no=tcashout_advance.cashout_cd join tmst_itemcashout on tcashout_advance.itemco_cd=tmst_itemcashout.itemco_cd where  tcashregister_advance.cashier_id='" + cbcashregister.SelectedValue.ToString() + "' and inout='O' and cash_dt='" + sdate + "' and tcashregister_advance.cash_sta_id in ('A','R','C')");
        cashbal = bll.vLookUp("select  isnull(sum(currentbalance),0) from tcashregister_balance_advance where reg_dt='" + sdateprev + "'");
        cashout2 = "0";
        damount = Convert.ToDouble(cashbal) + Convert.ToDouble(cashin) + Convert.ToDouble(cashin2) - (Convert.ToDouble(cashout) + Convert.ToDouble(cashout2));
        lbopenbalance.Text = bll.vLookUp("select dbo.fn_getprevbalcashier_advance('" + cbcashregister.SelectedValue.ToString() + "','"+ Request.Cookies["sp"].Value.ToString() + "')");
        lbcashin.Text = dTotIn.ToString(); // cashin.ToString();
        lbcashout.Text = (Convert.ToDouble(cashout) + Convert.ToDouble(cashout2)).ToString();
        lbcashClosing.Text = Math.Round(damount, 2).ToString();
    }
    protected void btaccept_Click(object sender, EventArgs e)
    {
        if (grd.Rows.Count == 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('No transaction to be paid/receveid !','No Transaction Found','warning');", true);
            return;
        }
        List<cArrayList> arr = new List<cArrayList>();
        foreach (GridViewRow row in grd.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hdids = (HiddenField)row.FindControl("hdids");
                Label lbrefno = (Label)row.FindControl("lbrefno");
                arr.Clear();
                arr.Add(new cArrayList("@cash_id", hdids.Value.ToString()));
                arr.Add(new cArrayList("@cash_sta_id", "C"));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vUpdateCashRegister_advance(arr);
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
        vInitGrid();

        cd.v_disablecontrol(btaccept);
        cbcashregister_SelectedIndexChanged(sender, e);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();sweetAlert('Received or Paid ALL completed!','received/paid','success');", true);
    }
    protected void cbcashregister_SelectedIndexChanged(object sender, EventArgs e)
    {
        DateTime _dt = DateTime.ParseExact(Request.Cookies["waz_dt"].Value, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        bll.vBindingFieldValueToComboWithChoosen(ref cbinout, "inout");
        lbopenbalance.Text =  bll.vLookUp("select dbo.fn_pettycashbalance('" + cbcashregister.SelectedValue + "','" + _dt.ToString() + "')");
        //lbopenbalance.Text = string.Format("{0:#,0.####}", Convert.ToDouble(bll.vLookUp("select dbo.fn_pettycashbalance('" + cbcashregister.SelectedValue + "','" + Request.Cookies["sp"].Value.ToString() + "')")));
        lbcashout.Text = Convert.ToDouble(bll.vLookUp("select dbo.fn_getamountcashregisteradvance('" + cbcashregister.SelectedValue + "','O')")).ToString("N2");
        lbcashin.Text = Convert.ToDouble(bll.vLookUp("select dbo.fn_getamountcashregisteradvance('" + cbcashregister.SelectedValue + "','I')")).ToString("N2");
        double _openingbalance = Convert.ToDouble(lbopenbalance.Text);
        double _cashout = Convert.ToDouble(lbcashout.Text);
        double _cashin = Convert.ToDouble(lbcashin.Text);
        double _closingcashier = _openingbalance + _cashin - _cashout;
        lbcashClosing.Text = _closingcashier.ToString("N2");
    }
}