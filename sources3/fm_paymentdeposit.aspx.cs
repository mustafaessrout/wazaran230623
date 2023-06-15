using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_paymentdeposit : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            bll.vBindingFieldValueToCombo(ref cbdeptype, "dep_typ");
            bll.vBindingFieldValueToCombo(ref cbpaymenttype, "payment_typ");
            arr.Add(new cArrayList("@qry_cd", "SalesJob"));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingComboToSp(ref cbsalesman, "sp_tmst_employee_getbyqry", "emp_cd", "emp_desc", arr);
          
            dtdep.Text = Request.Cookies["waz_dt"].Value.ToString();
            txdepno.CssClass = "ro";
            cbpaymenttype_SelectedIndexChanged(sender, e);
        }
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        double dAmt = 0;
        if (hdcust.Value.ToString() == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "aj", "sweetAlert('Customer must be selected','Select Customer','warning');", true);
            return;
        }
        if (!double.TryParse(txamt.Text, out dAmt))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "aj", "sweetAlert('Amount must be on numeric','Check amount','warning');", true);
            return;
        }

        string sNo = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@dep_dt", System.DateTime.ParseExact(dtdep.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@dep_typ", cbdeptype.SelectedValue.ToString()));
        arr.Add(new cArrayList("@amt", txamt.Text));
        arr.Add(new cArrayList("@payment_typ", cbpaymenttype.SelectedValue.ToString()));
        arr.Add(new cArrayList("@cust_cd", hdcust.Value.ToString()));
        arr.Add(new cArrayList("@salesman_cd", cbsalesman.SelectedValue.ToString()));
        arr.Add(new cArrayList("@ref_no", txref.Text));
        arr.Add(new cArrayList("@remark", txremark.Text));
        bll.vInsertPaymentDeposit(arr, ref sNo);
        txdepno.Text = sNo;
        txdepno.CssClass = "ro";
        dtdep.CssClass = "ro";
        txamt.CssClass = "ro";
        txcustomer.CssClass = "ro";
        txref.CssClass = "ro";
        cbbank.CssClass = "ro";
        cbdeptype.CssClass = "ro";
        cbsalesman.CssClass = "ro";
        cbpaymenttype.CssClass = "ro"; dtcheque.CssClass = "ro"; dtdue.CssClass = "ro";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "war", "sweetAlert('New payment deposit successfully created','" + txdepno.Text + "','success');", true);

    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        System.Data.SqlClient.SqlDataReader rs = null;
        HttpCookie cok;
        cok = HttpContext.Current.Request.Cookies["sp"];
        List<string> lCust = new List<string>();
        string sCust = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        // arr.Add(new cArrayList("@salesman_cd", contextKey));
        arr.Add(new cArrayList("@cust_cd", prefixText));
        arr.Add(new cArrayList("@salespointcd", cok.Value.ToString()));
        bll.vSearchMstCustomer(arr, ref rs);
        while (rs.Read())
        {
            sCust = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["cust_cd"].ToString() + "-" + rs["cust_nm"].ToString(), rs["cust_cd"].ToString());
            lCust.Add(sCust);
        }
        rs.Close();
        return (lCust.ToArray());
    }
    protected void btrefresh_Click(object sender, EventArgs e)
    {
        System.Data.SqlClient.SqlDataReader rs = null;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@cust_cd", hdcust.Value.ToString()));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        bll.vGetMstCustomer(arr, ref rs);
        while (rs.Read())
        {
            txcustomer.CssClass = "ro";
            lbcl.Text = rs["credit_limit"].ToString();
            lbcity.Text = bll.vLookUp("select loc_nm from tmst_location where loc_cd='"+rs["city_cd"].ToString()+ "'");
            lbotlcd.Text = bll.vLookUp("select fld_desc from tfield_value where fld_nm='otlcd' and fld_valu='"+rs["otlcd"].ToString()+"'");
            lbcusgrcd.Text = bll.vLookUp("select fld_desc from tfield_value where fld_nm='cusgrcd' and fld_valu='" + rs["cusgrcd"].ToString() + "'");
            lbcat.Text = rs["cuscate_cd"].ToString();
            cbsalesman.SelectedValue = rs["salesman_cd"].ToString();
            lbremain.Text = bll.vLookUp("select dbo.fn_getremaincl('"+hdcust.Value.ToString()+"')");
            txcustomer.Text = rs["cust_desc"].ToString();
        } rs.Close();
       
    }
    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_paymentdeposit.aspx");
    }
    protected void btprint_Click(object sender, EventArgs e)
    {

    }
    protected void cbpaymenttype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cbpaymenttype.SelectedValue.ToString() == "CH")
        {
            dtcheque.CssClass = "form-control ro"; dtdue.CssClass = "form-control ro";
            txref.CssClass = "form-control ro"; cbbank.CssClass = "form-control ro";
            cbbank.Items.Clear();
        }
        else
        {
            dtcheque.CssClass = "form-control"; dtdue.CssClass = "form-control";
            txref.CssClass = "form-control"; cbbank.CssClass = "form-control-static";
            bll.vBindingComboToSp(ref cbbank, "sp_tmst_bankaccount_get", "acc_no", "bank_desc");
        }
    }
    protected void cbdeptype_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }
    protected void btsearch_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opf", "popupwindow('lookup_deposit.aspx?id=');", true);
    }
    protected void btdeprefresh_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        System.Data.SqlClient.SqlDataReader rs = null;
        arr.Add(new cArrayList("@dep_cd", hddepcode.Value.ToString()));
        bll.vGetPaymentDeposit(arr, ref rs);
        while (rs.Read())
        { 
            hdcust.Value = rs["cust_cd"].ToString();
            txdepno.Text = rs["dep_cd"].ToString();
            txamt.Text = Convert.ToDouble( rs["amt"]).ToString("F2");
            txamt.CssClass = "form-control ro";
            dtdep.Text = Convert.ToDateTime(rs["dep_dt"]).ToString("d/M/yyyy");
            cbpaymenttype.SelectedValue = rs["payment_typ"].ToString();
            cbdeptype.SelectedValue = rs["dep_typ"].ToString();
            cbsalesman.SelectedValue = rs["salesman_cd"].ToString();
            dtdep.CssClass = "form-control-static ro"; cbpaymenttype.CssClass = "form-control-static ro"; cbdeptype.CssClass = "form-control-static ro";
            cbsalesman.CssClass = "form-control-static ro";
            btsave.CssClass = "divhid";
            btnew.CssClass = "button2 add";
            btsave.CssClass = "divhid";
            btprint.CssClass = "button2 print";
        }
        rs.Close();
        btrefresh_Click(sender, e);
    }
    protected void btpayment_Click(object sender, EventArgs e)
    {
        string spaymentno = string.Empty;
        spaymentno = hddepcode.Value.ToString();
        List<cArrayList> arr= new List<cArrayList>();
        Response.Redirect("fm_paymentreceipt.aspx?p=" + spaymentno);
    }
}