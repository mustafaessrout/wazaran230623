using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_cashregisterentry : System.Web.UI.Page
{
    cbll bll = new cbll();
    double dTotAmt = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            bll.vBindingFieldValueToCombo(ref cbinout, "inout");
            bll.vBindingFieldValueToCombo(ref cbtype, "cashout_typ");
            arr.Add(new cArrayList("@doc_typ", "cashout"));
            arr.Add(new cArrayList("@level_no", "1"));
            bll.vBindingComboToSp(ref cbapproval, "sp_tapprovalpattern_get", "emp_cd" , "emp_nm", arr);
            cbtype_SelectedIndexChanged(sender, e);
           // bll.vBindingGridToSp(ref grd, "sp_tcashregister_get");
            cbinout_SelectedIndexChanged(sender, e);
            bll.vBindingFieldValueToCombo(ref cbsource, "source_order");
            bll.vBindingFieldValueToCombo(ref cbpaymenttype, "payment_typ");
            getamt();
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabErr", "tabErr();", true);
    }
    private void getamt()
    {
        string dtcash = Request.Cookies["waz_dt"].Value.ToString();
        string cashin, cashout, cashbal, ssp, cashin2;
        double damount;
        DateTime dtdate = Convert.ToDateTime(DateTime.ParseExact(dtcash, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture));
        DateTime sdtdate = new DateTime(dtdate.Year, dtdate.Month, dtdate.Day);
        DateTime sdtdateprev = sdtdate.AddDays(-1);
        string sdate = sdtdate.ToString("M/d/yyyy");
        string sdateprev = sdtdateprev.ToString("M/d/yyyy");
        ssp = Request.Cookies["sp"].Value.ToString();
        cashin = bll.vLookUp("select isnull(sum(amt),0) from tcashregister left outer join tmst_payment on tcashregister.ref_no=tmst_payment.payment_no where cash_dt='" + sdate + "' and tcashregister.cash_sta_id='C'  and  tmst_payment.salespointcd='" + ssp + "'");
        
        cashin2 = bll.vLookUp("select isnull(sum(amt),0) from tcashregister a join tmst_itemcashout b on a.itemco_cd=b.itemco_cd where " + 
            "b.inout='I' and datediff(d,cash_dt,dbo.fn_getsystemdate())=0 and a.ref_no not in (select payment_no from tmst_payment)");
        double dTotIn = Convert.ToDouble(cashin) + Convert.ToDouble(cashin2);
        cashout = bll.vLookUp("select isnull(sum(amt),0) from tcashregister inner join tcashregout on tcashregister.ref_no=tcashregout.casregout_cd where cash_dt='" + sdate + "' and tcashregister.cash_sta_id in ('A','R','C')  and  tcashregout.salespointcd='" + ssp + "'");
        cashbal = bll.vLookUp("select  isnull(sum(currentbalance),0) from tcashregister_balance where reg_dt='" + sdateprev + "'");
        damount = Convert.ToDouble(cashbal) + Convert.ToDouble(cashin) + Convert.ToDouble(cashin2) - Convert.ToDouble(cashout);
        lbopenbalance.Text = bll.vLookUp("select dbo.fn_getprevbalcashier()");
        lbcashin.Text = dTotIn.ToString(); // cashin.ToString();
        lbcashout.Text = cashout.ToString();
        lbcashClosing.Text = Math.Round( damount,2).ToString();
    }
    protected void cbtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@cashout_typ", cbtype.SelectedValue.ToString()));
        arr.Add(new cArrayList("@inout", cbinout.SelectedValue.ToString()));
        bll.vBindingComboToSp(ref cbitem, "sp_tmst_itemcashout_get","itemco_cd","itemco_nm", arr);
        cbitem_SelectedIndexChanged(sender, e);
    }
    protected void btadd_Click(object sender, EventArgs e)
    {
        if (txamount.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Amount can not empty','Amount to be paid','warning');", true);
            return;
        }

        if (txpic.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('PIC can not empty','Person In Charge','warning');", true);
            return;
        }



        List<cArrayList> arr = new List<cArrayList>();
       // arr.Add(new cArrayList("@cashout_typ", cbtype.SelectedValue.ToString()));
        arr.Add(new cArrayList("@itemco_cd", cbitem.SelectedValue.ToString()));
        arr.Add(new cArrayList("@amt", txamount.Text));
		arr.Add(new cArrayList("@cash_sta_id", "R"));
	    arr.Add(new cArrayList("@app_cd", cbapproval.SelectedValue.ToString()));
	    arr.Add(new cArrayList("@routine", cbroutine.SelectedValue.ToString()));
	    arr.Add(new cArrayList("@ref_no", txref.Text));
        arr.Add(new cArrayList("@remark", txremark.Text));
        arr.Add(new cArrayList("@isasset", chasset.Checked));
        arr.Add(new cArrayList("@cash_dt", System.DateTime.ParseExact( Request.Cookies["waz_dt"].Value.ToString(),"d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@isclaim", chclaim.Checked));
        //arr.Add(new cArrayList("@source_order", cbsource.SelectedValue.ToString()));
        //arr.Add(new cArrayList("@salesman_cd", cbsalesman.SelectedValue.ToString()));
        bll.vInsertCashRegister(arr);
        arr.Clear();
       
        arr.Add(new cArrayList("@itemco_cd", cbitem.SelectedValue.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_tcashregister_get", arr);
        getamt();
        txref.Focus();
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        System.Data.SqlClient.SqlDataReader rs = null;
        List<string> lEmployee = new List<string>();
        List<cArrayList> arr = new List<cArrayList>();
        string sEmployee = string.Empty;
        arr.Add(new cArrayList("@emp_nm", prefixText));
        bll.vSearchMstEmployee(arr, ref rs);
        while (rs.Read())
        {
            sEmployee = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["emp_cd"].ToString() + "-" + rs["emp_nm"], rs["emp_cd"].ToString());
            lEmployee.Add(sEmployee);
        }
        return (lEmployee.ToArray());
    }
    protected void cbitem_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (hdsalesman_cd.Value == "") { hdsalesman_cd.Value = null; }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@itemco_cd", cbitem.SelectedValue.ToString()));
        arr.Add(new cArrayList("@qry_cd", "cash_sta_id"));
        arr.Add(new cArrayList("@salesman_cd", hdsalesman_cd.Value));
        bll.vBindingGridToSp(ref grd, "sp_tcashregister_getbyqry", arr);
    }
    protected void cbinout_SelectedIndexChanged(object sender, EventArgs e)
    {
        cbtype_SelectedIndexChanged(sender, e);
        cbitem_SelectedIndexChanged(sender, e);
        if (cbinout.SelectedValue.ToString() == "I")
        { 
            txamount.CssClass = "form-control input-sm";
            txamount.Enabled = true;
            txpic.CssClass = "form-control input-sm";
            txpic.Enabled = true;
            txremark.CssClass = "form-control input-sm";
            txremark.Enabled = true;
            cbapproval.Items.Clear(); 
            cbapproval.CssClass = "form-control input-sm ro";
            cbapproval.Enabled = false;
        btpaid.Text = "Received All";
        }
        else
        {
            txamount.CssClass = "form-control input-sm ro";
            txamount.Enabled = false;
            txpic.CssClass = "form-control input-sm ro";
            txpic.Enabled = false;
            txremark.CssClass = "form-control input-sm ro";
            txremark.Enabled = false;
        btpaid.Text = "Paid All";
        }
    }
    protected void btrefresh_Click(object sender, EventArgs e)
    {
        //List<cArrayList> arr = new List<cArrayList>();
        //foreach (GridViewRow row in grd.Rows)
        //{
        //    arr.Clear();
        //    HiddenField hdcashid = (HiddenField)row.FindControl("hdcashid");
        //    arr.Add(new cArrayList("@cash_id", hdcashid.Value.ToString()));
        //    arr.Add(new cArrayList("@cash_sta_id", "C"));
        //    bll.vUpdateCashRegister(arr);
        //}
        cbitem_SelectedIndexChanged(sender, e);
        getamt();
    }
    protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbamt = (Label)e.Row.FindControl("lbamt");
            HiddenField hditemco = (HiddenField)e.Row.FindControl("hditemco");
            Label lbmanualno = (Label)e.Row.FindControl("lbmanualno");
            Label lbdocno = (Label)e.Row.FindControl("lbdocno");
            Label lbcust = (Label)e.Row.FindControl("lbcust");
            if (hditemco.Value.ToString() == "cashsnd")
            {
                lbmanualno.Text = bll.vLookUp("select manual_no from tmst_payment where payment_no='"+lbdocno.Text+"'");
                lbcust.Text = bll.vLookUp("select p.cust_cd + '-' + c.cust_nm from tmst_payment p join tmst_customer c on p.cust_cd=c.cust_cd where p.payment_no='"+lbdocno.Text+"'");
            }
            dTotAmt += Convert.ToDouble(lbamt.Text);
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lbtotamt = (Label)e.Row.FindControl("lbtotamt");
            lbtotamt.Text = dTotAmt.ToString();
        }
    }
    protected void btpaid_Click(object sender, EventArgs e)
    {
        //foreach (GridViewRow  row in grd.Rows)
        //{
        //    if (row.RowType == DataControlRowType.Footer)
        //    {
        //        Label lbtotamt = (Label)row.FindControl("lbtotamt");
        //        dTotAmt = Convert.ToDouble(lbtotamt.Text);
        //    }
        //}

        Label lbtotamt = (Label) grd.FooterRow.FindControl("lbtotamt");
       
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "popupwindow('cashamount.aspx?am=" + lbtotamt.Text  +"&qsitemco_cd="+cbitem.SelectedValue+"&qsSalesman_cd="+hdsalesman_cd.Value+"');", true);
      //  return;
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionListsalesman_cd(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        HttpCookie cok;
        cok = HttpContext.Current.Request.Cookies["sp"];
        List<string> lcust = new List<string>();
        List<cArrayList> arr = new List<cArrayList>();
        string scust = string.Empty;
        System.Data.SqlClient.SqlDataReader rs = null;
        //arr.Add(new cArrayList("@cust_cd", prefixText));
        arr.Add(new cArrayList("@salesman_cd", prefixText));
        arr.Add(new cArrayList("@salespointcd", cok.Value.ToString()));
        bll.vSearchSalesman(arr, ref rs);
        //bll.vSearchMstCustomer(arr, ref rs);
        while (rs.Read())
        {
            scust = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["emp_cd"].ToString() + "-" + rs["emp_nm"].ToString(), rs["emp_cd"].ToString());
            lcust.Add(scust);
        }
        rs.Close();

        return (lcust.ToArray());
    }
    protected void txsalesman_cd_TextChanged(object sender, EventArgs e)
    {
        if (txsalesman_cd.Text == "") { hdsalesman_cd.Value = null; }
        cbitem_SelectedIndexChanged(sender, e);
    }

    public void OnConfirm(object sender, EventArgs e)
    {
        
        if (grd.Rows.Count==0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('NO Amount tobe paid','NO Amount to be paid','warning');", true);
            return;
        }
        string confirmValue = Request.Form["confirm_value"];
        if (confirmValue == "Yes")
        {
           if (cbinout.SelectedValue=="I")
           {
               Label lbtotamt = (Label)grd.FooterRow.FindControl("lbtotamt");
               ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "popupwindow('cashamount.aspx?am=" + lbtotamt.Text + "&qsitemco_cd=" + cbitem.SelectedValue + "&qsSalesman_cd=" + hdsalesman_cd.Value + "');", true);
               return;
           }
            else
           { 
            foreach (GridViewRow row in grd.Rows)
            {
                Label lblcash_id = (Label)row.FindControl("lblcash_id");
                List<cArrayList> arr = new List<cArrayList>();
                arr.Add(new cArrayList("@cash_id", lblcash_id.Text));
                arr.Add(new cArrayList("@cash_sta_id", "C"));
                bll.vUpdateCashRegister(arr);
                txsalesman_cd_TextChanged(sender, e);
              //  bll.vBatchMakeCompletePayment();
            }
           }
            //Label lbtotamt = (Label)grd.FooterRow.FindControl("lbtotamt");
            //if (Convert.ToDouble(lbtotamt.Text)>0)
            //{
                
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "popupwindow('cashamount.aspx?am=" + lbtotamt.Text + "&qsSalesman_cd=" + hdsalesman_cd.Value + "');", true);
            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Amount can not empty','Amount to be paid','warning');", true);
            //}
        }
        else
        {

        }
    }
      protected void cbsource_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
      protected void btall_Click(object sender, EventArgs e)
      {
          ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opar", "popupwindow('fm_cashregoutall.aspx');", true);
      }
      protected void btpaid_Click1(object sender, EventArgs e)
      {

      }
}