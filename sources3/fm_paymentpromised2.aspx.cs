using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class fm_paymentpromised2 : System.Web.UI.Page
{
    cbll bll = new cbll();
    private DateTime MinDate;
    private DateTime MaxDate;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txpromisedno.CssClass = "form-control ro";
            lbbalance.Text = "-";
            lbcl.Text = "-";
            lbremain.Text = "-";
            lbmaxamt.Text = "-";
            lbtop.Text = "-";
            txCustomer.CssClass = "form-control";
            cbfor.SelectedValue = "C";
            cbfor.CssClass = "form-control ro";
            cbcusgrcd.CssClass = "divhid";
            btsave.CssClass = "btn btn-default ro";
            dtpaid_CalendarExtender.StartDate = System.DateTime.ParseExact(Request.Cookies["waz_dt"].Value, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            // dtnewpayment_CalendarExtender.StartDate = System.DateTime.ParseExact(Request.Cookies["waz_dt"].Value, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            MinDate = System.DateTime.ParseExact(Request.Cookies["waz_dt"].Value, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            int nOldPayment = Convert.ToInt16(bll.sGetControlParameter("maxoldpaymentpromised"));
            int nNewPayment = Convert.ToInt16(bll.sGetControlParameter("maxnewpaymentpromised"));
            MaxDate = MinDate.AddDays(nOldPayment);
            dtpaid_CalendarExtender.StartDate = MinDate;
            dtpaid_CalendarExtender.EndDate = MaxDate;
            dtnewpayment_CalendarExtender.StartDate = MinDate.Date;
            dtnewpayment_CalendarExtender.EndDate = MinDate.AddDays(nNewPayment);
        }
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

    protected void btsearch_Click(object sender, EventArgs e)
    {
        System.Data.SqlClient.SqlDataReader rs = null;
        string sImage = string.Empty;
        if (cbfor.SelectedValue.ToString() == "C")
        {
            string sCheck = bll.vLookUp("select dbo.fn_checkexistgrpcl('" + hdcust.Value.ToString() + "')");
            if (sCheck != "ok")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('This customer has CL Group, you must choose for Group','','warning');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
                txCustomer.Text = ""; hdcust.Value = "";
                return;
            }
            string sCheckAvlPromise = bll.vLookUp("select dbo.fn_getpromisetolerance('" + hdcust.Value + "')");
            if (sCheckAvlPromise != "ok")
            {
                string sTol = bll.sGetControlParameter("maxtolerancepromise");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('This customer has invoice with overdue more than tolerance," + sTol.ToString() + " days!','" + sCheckAvlPromise + "','warning');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
                txCustomer.Text = ""; hdcust.Value = "";
                return;
            }

            string sCheckExist = bll.vLookUp("select dbo.fn_checkexistpaymentpromised('" + hdcust.Value.ToString() + "',null)");
            if (sCheckExist != "ok")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('This customer already has promised!','" + sCheckExist + "','warning');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
                txCustomer.Text = ""; hdcust.Value = "";
                return;
            }
        }

        //IAG - ENhanced 5% VAT =======================================================================================
        double dRemainCL = Convert.ToDouble(bll.vLookUp("select dbo.fn_getremaincl('" + hdcust.Value.ToString() + "')"));
        if (dRemainCL < 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Cust : " + hdcust.Value.ToString() + "' has negatif credit limit!,'It was prohibited to created Promise','warning');", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
            txCustomer.Text = ""; hdcust.Value = "";
            return;
        }

        double dOverdue = Convert.ToDouble(bll.vLookUp("select dbo.fn_getamtoverdueinv('" + hdcust.Value.ToString() + "')"));
        if (dOverdue == 0)
        {
            string sCust = bll.vLookUp("select cust_nm from tmst_customer where cust_cd='" + hdcust.Value.ToString() + "'");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Cust : " + hdcust.Value.ToString() + ":" + sCust + " has no overdue!','It can not created Promise','warning');", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
            txCustomer.Text = "";
            hdcust.Value = "";
            return;
        }

        double dVat = (dOverdue *  Convert.ToDouble(bll.vLookUp("select dbo.fn_getcontrolparameter('vat')")));
        txpromise.Text = dOverdue.ToString();
        txpromise.CssClass = "form-control ro";
        lbmaxamt.Text = ((Convert.ToDouble(bll.sGetControlParameter("pctmaxtranspromised")) * dOverdue) + dVat).ToString();

        //=============================================================================================================
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        arr.Add(new cArrayList("@cust_cd", hdcust.Value.ToString()));
        bll.vGetMstCustomer(arr, ref rs);
        while (rs.Read())
        {
            lbcl.Text = rs["credit_limit"].ToString();
            lbsalesman.Text = rs["salesman_cd"].ToString() + ":" + bll.vLookUp("select emp_nm from tmst_employee where emp_cd='" + rs["salesman_cd"].ToString() + "'");
            lbtop.Text = rs["payment_term"].ToString();
            hdemp.Value = rs["salesman_cd"].ToString();
            lbtype.Text = rs["otlcd"].ToString();
            lbcusgrcd.Text = rs["cusgrcd"].ToString();
            lbremain.Text = bll.vLookUp("select dbo.fn_getremaincl('" + hdcust.Value.ToString() + "')");
            lboverdue.Text = bll.vLookUp("select dbo.fn_getamtoverdueinv('" + hdcust.Value.ToString() + "')");
            lbbalance.Text = bll.vLookUp("select dbo.fn_getinvbalance('" + hdcust.Value.ToString() + "')");
            dtnewpayment.Text = Request.Cookies["waz_dt"].Value.ToString();
            dtpaid.Text = Request.Cookies["waz_dt"].Value.ToString();
            string sCurrentPromised = bll.vLookUp("select dbo.fn_getcurrentpaymentpromised('" + rs["cust_cd"].ToString() + "')");
            //  lbmaxamt.Text =  ((Convert.ToDouble(lbremain.Text) - Convert.ToDouble(lboverdue.Text)) + Convert.ToDouble(sCurrentPromised)).ToString();
            lbcreditlimit.InnerText = "GROUP CL";
            if (Convert.ToDouble(lbremain.Text) < 0)
            {
                sImage = "redflag.png";
            }
            else if ((Convert.ToDouble(lbremain.Text) >= 0) && (Convert.ToDouble(lbremain.Text) <= 10))
            {
                sImage = "yellowflag.png";
            }
            else
            {
                sImage = "greenflag.png";
            }
            img.ImageUrl = sImage;

            lbusage.Text = bll.vLookUp("select count(1) from tpayment_promised where cust_cd='" + hdcust.Value.ToString() + "' and promised_sta_id='N'");
            txCustomer.CssClass = "form-control ro";
            btsave.CssClass = "btn btn-success";
        }
        rs.Close();
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
    }
    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_paymentpromised2.aspx");
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        FileInfo fi = null;
        string sOut = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        double dPromised = 0;
        if (!double.TryParse(txpromise.Text, out dPromised))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Promised amount must be numeric','Promised amount','warning');", true);
            return;
        }
        if (dPromised == 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Promised amount can not 0','Must greather than 0','warning');", true);
            return;
        }
        int nCount = 0;
        if (cbfor.SelectedValue == "C")
        {
            nCount = Convert.ToInt16(bll.vLookUp("select count(1) from tpayment_promised where promised_sta_id='N' and cust_cd='" + hdcust.Value.ToString() + "'"));
        }
        else
        {
            nCount = Convert.ToInt16(bll.vLookUp("select count(1) from tpayment_promised where promised_sta_id='N' and cusgrcd='" + cbcusgrcd.SelectedValue.ToString() + "'"));
        }
        if (nCount >= 1)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Promised payment can only create ONCE!','Please realized all promised','warning');", true);
            return;
        }

        if (dtpaid.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Paid date can not empty','Paid Date','warning');", true);
            return;
        }

        if (dtnewpayment.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('New payment date can not empty','New Trans Date','warning');", true);
            return;
        }
        DateTime dtPaid = DateTime.ParseExact(dtpaid.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        DateTime dt = DateTime.ParseExact(Request.Cookies["waz_dt"].Value.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        DateTime dtnewtrans = DateTime.ParseExact(dtnewpayment.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        if (dtPaid <= dt)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Promised due date can not same or less than system date','Check Promised Due Date','warning');", true);
            return;
        }
        if (dtnewtrans <= dt)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Payment new transaction can not same or less than system date','Check New Trans Date','warning');", true);
            return;
        }

        string sImage = bll.sGetControlParameter("image_path");
        if (txremark.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Remark can not empty!','Please fill remark data','warning');", true);
            return;
        }

        if (!upl.HasFile)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('SOA must be uploaded!','SOA with sign on BRN /Sales SPV','warning');", true);
            return;
        }

        fi = new FileInfo(upl.FileName);
        string sExt = fi.Extension;
        arr.Add(new cArrayList("@promised_dt", System.DateTime.ParseExact(Request.Cookies["waz_dt"].Value.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@cust_cd", hdcust.Value.ToString()));
        arr.Add(new cArrayList("@totbalance", lbbalance.Text));
        arr.Add(new cArrayList("@cl", lbcl.Text));
        arr.Add(new cArrayList("@remaincl", lbremain.Text));
        arr.Add(new cArrayList("@amt", txpromise.Text));
        arr.Add(new cArrayList("@maxorderamt", lbmaxamt.Text));
        arr.Add(new cArrayList("@oldpayment_dt", System.DateTime.ParseExact(dtpaid.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@newpayment_dt", System.DateTime.ParseExact(dtnewpayment.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@filename", upl.FileName));
        arr.Add(new cArrayList("@promised_sta_id", "N"));
        arr.Add(new cArrayList("@remark", txremark.Text));
        arr.Add(new cArrayList("@rdfor", cbfor.SelectedValue.ToString()));
        arr.Add(new cArrayList("@cusgrcd", cbcusgrcd.SelectedValue.ToString()));
        arr.Add(new cArrayList("@createdby", Request.Cookies["usr_id"].Value.ToString()));
        bll.vInsertPaymentPromised(arr, ref sOut);
        txpromisedno.Text = sOut;
        string sFileName = sOut + sExt;
        upl.SaveAs(bll.sGetControlParameter("image_path") + upl.FileName);
        // Send email 
        string sSubject = "#New payment promised has created (" + bll.sGetSalespointname(Request.Cookies["sp"].Value.ToString());
        string sBody = "Dear Accounting , <br/><br/>New payment promised has been created with detail<br/><br/><table><tr><th style=background-color:yellow></th>";
        btnew.CssClass = cd.cssbuttonnew;
        btprint.CssClass = cd.cssbuttonprint;
        btsave.CssClass = "divhid";
        txpromise.CssClass = "form-control ro";
        dtpaid.CssClass = "form-control ro";
        dtnewpayment.CssClass = "form-control ro";
        txremark.CssClass = "form-control ro";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Promised has been saved','" + txpromisedno.Text + "','success');", true);
    }
    protected void txpromise_TextChanged(object sender, EventArgs e)
    {
        try
        {

            double dPromised = Convert.ToDouble(lbremain.Text) + Convert.ToDouble(txpromise.Text) - Convert.ToDouble(lboverdue.Text);
            lbmaxamt.Text = dPromised.ToString();
        }
        catch (Exception ex)
        {
            lbmaxamt.Text = "0";
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : txpromised_TextChanged");
        }
    }
    protected void btprint_Click(object sender, EventArgs e)
    {

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opr", "openreport('fm_report2.aspx?src=payprom&no=" + txpromisedno.Text + "&emp=" + hdemp.Value.ToString() + "');", true);
    }
    protected void btsearchpromised_ServerClick(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "popupwindow('lookup_promised.aspx');", true);
    }
    protected void btsearchprom_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        System.Data.SqlClient.SqlDataReader rs = null;
        string sCust = bll.vLookUp("select cust_cd from tpayment_promised where promised_cd='" + txpromisedno.Text + "'");
        hdemp.Value = bll.vLookUp("select salesman_cd from tmst_customer where cust_cd='" + sCust + "'");
        arr.Add(new cArrayList("@promised_cd", txpromisedno.Text));
        bll.vGetPaymentPromised(ref rs, arr);
        while (rs.Read())
        {
            lbremain.Text = rs["remaincl"].ToString();
            lbbalance.Text = rs["totbalance"].ToString();
            lbsalesman.Text = rs["salesman"].ToString();
            txCustomer.Text = rs["cust"].ToString();
            dtnewpayment.Text = Convert.ToDateTime( rs["newpayment_dt"]).ToString("d/M/yyyy");
            dtpaid.Text = Convert.ToDateTime( rs["oldpayment_dt"]).ToString("d/M/yyyy");
            txremark.Text = rs["remark"].ToString();
            txpromise.Text = rs["amt"].ToString();
            lbmaxamt.Text = rs["maxorderamt"].ToString();
            txpromise.CssClass = "form-control ro";
            txremark.CssClass = "form-control ro";
            dtnewpayment.CssClass = "form-control ro";
            dtpaid.CssClass = "form-control ro";
            txCustomer.CssClass = "form-control ro";
        }
        rs.Close();

    }
    protected void cbcusgrcd_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void cbfor_SelectedIndexChanged(object sender, EventArgs e)
    {
        lbbalance.Text = "-";
        lbcl.Text = "-";
        lbremain.Text = "-";
        lbmaxamt.Text = "-";
        lbusage.Text = "";
        // lbtargetcoll.Text = "-";
        dtnewpayment.Text = "";
        dtpaid.Text = "";
        lbtop.Text = "-";
        hdcust.Value = ""; txCustomer.Text = "";
        img.ImageUrl = "";
        //  lbcl.Text = bll.vLookUp("select dbo.fn_getcreditlimit('" + hdcust.Value.ToString() + "')");
        if (cbfor.SelectedValue == "C")
        {
            txCustomer.CssClass = "form-control";
            cbcusgrcd.CssClass = "divhid";
            //btsearchCust.Attributes.Add("class", "btn btn-primary");
            txpromise.CssClass = "form-control";
            //searcPnl.CssClass = "";

        }
        else if (cbfor.SelectedValue == "G")
        {
            txCustomer.CssClass = "divhid";
            cbcusgrcd.CssClass = "form-control";
            bll.vBindingFieldValueToCombo(ref cbcusgrcd, "cusgrcd");
            cbcusgrcd_SelectedIndexChanged1(sender, e);
            //searcPnl.CssClass = "btn5";
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
    }
    protected void cbcusgrcd_SelectedIndexChanged1(object sender, EventArgs e)
    {
        string sImage = string.Empty;
        string sCL = bll.vLookUp("select dbo.fn_getgrpcl('" + cbcusgrcd.SelectedValue.ToString() + "')");
        if (Convert.ToDouble(sCL).Equals(0))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('This group has no CL','" + cbcusgrcd.SelectedItem.Text + "','warning');", true);
            txpromise.CssClass = "form-control ro";
            btsave.CssClass = "divhid";
            btprint.CssClass = "divhid";
            lbbalance.Text = "-";
            lbcl.Text = "-";
            lboverdue.Text = "-";
            lbremain.Text = "-";
            //  lbtargetcoll.Text = "-";
            lbmaxamt.Text = "-";
            img.ImageUrl = "";
            return;
        }
        txpromise.CssClass = "form-control";
        btsave.CssClass = "btn btn-warning";
        lbcl.Text = sCL;
        lbcusgrcd.Text = cbcusgrcd.SelectedValue.ToString();
        lbusage.Text = bll.vLookUp("select count(1) from tpayment_promised where rdfor='G' and promised_sta_id='N' and cusgrcd='" + cbcusgrcd.SelectedValue.ToString() + "'");
        lbbalance.Text = bll.vLookUp("select dbo.fn_getremainbalancegrp('" + cbcusgrcd.SelectedValue.ToString() + "')");
        lboverdue.Text = bll.vLookUp("select dbo.fn_getbalanceduegrp('" + cbcusgrcd.SelectedValue.ToString() + "')");
        double dBal = Convert.ToDouble(lbbalance.Text);
        double dDue = Convert.ToDouble(lboverdue.Text);
        double dCL = Convert.ToDouble(lbcl.Text);
        double dRemain = dCL - dBal;
        string sOverDue = bll.vLookUp("select dbo.fn_getoverduecustgrp('" + cbcusgrcd.SelectedValue.ToString() + "')");
        lboverdue.Text = sOverDue;
        lbremain.Text = dRemain.ToString();

        if (Convert.ToDouble(lbremain.Text) < 0)
        {
            sImage = "redflag.png";
        }
        else if ((Convert.ToDouble(lbremain.Text) >= 0) && (Convert.ToDouble(lbremain.Text) <= 10))
        {
            sImage = "yellowflag.png";
        }
        else
        {
            sImage = "greenflag.png";
        }
        img.ImageUrl = sImage;

    }

    protected void dtpaid_CalendarExtender_PreRender(object sender, EventArgs e)
    {

    }
    protected void btsearchcust_Click(object sender, EventArgs e)
    {

    }
    protected void btselectpromised_Click(object sender, EventArgs e)
    {
        System.Data.SqlClient.SqlDataReader rs = null;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@promised_cd", hdpromised.Value.ToString()));
        bll.vGetPaymentReceipt(arr, ref rs);
        while (rs.Read())
        {
            txCustomer.Text = rs["cust"].ToString();
        }
        rs.Close();
    }
}