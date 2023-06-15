using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_mstclaimhobatch : System.Web.UI.Page
{
    cbll bll = new cbll();
    String listCheckClaim = "";


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["salespoint"] = "";
            //bll.vBindingFieldValueToComboAll(ref cbstatus, "cl_sta_id");            
            btnMenu.Attributes.Add("style", "display:none");
            dtSalesSnd.Text = String.Format("{0:dd/MM/yyyy}", DateTime.Now);
            dtSalesRcv.Text = String.Format("{0:dd/MM/yyyy}", DateTime.Now);
            dtSendClaim.Text = String.Format("{0:dd/MM/yyyy}", DateTime.Now);
            dtSendClaim.Attributes.Add("readonly", "readonly");
            bll.vBindingComboToSp(ref cbbranch, "sp_tmst_salespoint_get ", "salespointcd", "salespoint_nm");
            bll.vBindingComboToSp(ref cbvendor, "sp_tmst_vendor_get", "vendor_cd", "vendor_nm");
            bll.vBindingComboToSp(ref cbstatus, "sp_tfield_value_get6", "fld_valu", "fld_note");
            bll.vBindingComboToSp(ref cbYear, "sp_tmst_year_getbyclaim", "fld_valu", "fld_valu");
            cbvendor.Items.Insert(0,"ALL");
            //cbstatus.Items.Insert(0, new ListItem("All Status","ALL"));
            cbbranch.Items.Insert(0, new ListItem("All Branch", "ALL"));
            cbMonth.Items.Insert(0, new ListItem("ALL Month", "ALL"));
            cbYear.Items.Insert(0, new ListItem("ALL Year", "ALL"));
            btnew.Visible = true;
            btprint.Visible = false;
            cbYear.SelectedValue = "ALL";
            cbMonth.SelectedValue = "ALL";
            //cbstatus.SelectedValue = "ALL";
            chall.Attributes.Add("style", "display:none");
            //cbstatus_SelectedIndexChanged(sender, e);
            bll.vBindingComboToSp(ref cbgroup, "sp_get_issue_group", "issue_group", "issue_group");
            cbgroup_SelectedIndexChanged(sender, e);
            bll.vBindingFieldValueToCombo(ref cbbankcq, "bank_cd");
            cbbankcq.Items.Insert(0, new ListItem("None", "none"));
            bll.vBindingFieldValueToCombo(ref cbpriceotlcd, "otlcd");
            cbpriceotlcd.Items.Insert(0, new ListItem("none", "0"));
            cbpaytype.Items.Insert(0, new ListItem("ALL", "0"));

        }        
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetListClaim(string prefixText, int count, string contextKey)
    {
        HttpCookie cook;
        cook = HttpContext.Current.Request.Cookies["sp"];
        cbll bll = new cbll();
        List<string> lItem = new List<string>();
        List<cArrayList> arr = new List<cArrayList>();
        string sItem = string.Empty;
        System.Data.SqlClient.SqlDataReader rs = null;
        arr.Add(new cArrayList("@claim", prefixText));
        arr.Add(new cArrayList("@branch", HttpContext.Current.Session["salespoint"].ToString()));
        arr.Add(new cArrayList("@vendor", HttpContext.Current.Session["vendor"].ToString()));
        arr.Add(new cArrayList("@status", HttpContext.Current.Session["status"].ToString()));
        arr.Add(new cArrayList("@month", HttpContext.Current.Session["month"].ToString()));
        arr.Add(new cArrayList("@year", HttpContext.Current.Session["year"].ToString()));
        bll.vSearchMstClaimHO(arr, ref rs);
        while (rs.Read())
        {
            sItem = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["prop_no"].ToString() , rs["prop_no"].ToString());
            lItem.Add(sItem);
        }
        rs.Close();
        return (lItem.ToArray());
    }

    protected void grdclaim_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdclaim.PageIndex = e.NewPageIndex;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@status", cbstatus.SelectedValue.ToString()));
        arr.Add(new cArrayList("@branch", cbbranch.SelectedValue.ToString()));
        arr.Add(new cArrayList("@vendor", cbvendor.SelectedValue.ToString()));
        arr.Add(new cArrayList("@month", cbMonth.SelectedValue.ToString()));
        arr.Add(new cArrayList("@year", cbYear.SelectedValue.ToString()));
        arr.Add(new cArrayList("@claim_no", txsearch.Text));
        bll.vBindingGridToSp(ref grdclaim, "sp_tmst_claim_ho_get", arr);
    }

    protected void grdclaimheader_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdclaimheader.PageIndex = e.NewPageIndex;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@status", cbstatus.SelectedValue.ToString()));
        arr.Add(new cArrayList("@salespointcd", cbbranch.SelectedValue.ToString()));
        arr.Add(new cArrayList("@month", cbMonth.SelectedValue.ToString()));
        arr.Add(new cArrayList("@year", cbYear.SelectedValue.ToString()));
        arr.Add(new cArrayList("@claim_no", txsearch.Text));
        arr.Add(new cArrayList("@vendor", cbvendor.SelectedValue.ToString()));
        arr.Add(new cArrayList("@paymenttype", cbpaytype.SelectedValue.ToString()));
        bll.vBindingGridToSp(ref grdclaimheader, "sp_tclaim_header_get_batch", arr);
    }

    protected void chall_CheckedChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();

        if (chall.Checked)
        {
            arr.Add(new cArrayList("@status", cbstatus.SelectedValue.ToString()));
            arr.Add(new cArrayList("@branch", cbbranch.SelectedValue.ToString()));
            arr.Add(new cArrayList("@vendor", cbvendor.SelectedValue.ToString()));
            arr.Add(new cArrayList("@month", cbMonth.SelectedValue.ToString()));
            arr.Add(new cArrayList("@year", cbYear.SelectedValue.ToString()));
            arr.Add(new cArrayList("@claim_no", txsearch.Text));
            bll.vBindingGridToSp(ref grdclaim, "sp_tmst_claim_ho_get",arr);
            arr.Clear();
            if (grdclaim.Rows.Count == 0)
            {
                btnMenu.Attributes.Add("style", "display:none");
            }
            else
            {
                btnMenu.Attributes.Remove("style");
            }
        }
        else
        {
            if (hdclaim.Value.ToString() == "")
            {
                arr.Add(new cArrayList("@status", cbstatus.SelectedValue.ToString()));
                arr.Add(new cArrayList("@branch", cbbranch.SelectedValue.ToString()));
                arr.Add(new cArrayList("@vendor", cbvendor.SelectedValue.ToString()));
                arr.Add(new cArrayList("@month", cbMonth.SelectedValue.ToString()));
                arr.Add(new cArrayList("@year", cbYear.SelectedValue.ToString()));
                arr.Add(new cArrayList("@claim_no", txsearch.Text));
                bll.vBindingGridToSp(ref grdclaim, "sp_tmst_claim_ho_get", arr);                
                arr.Clear();                
            }
            else
            {
                arr.Add(new cArrayList("@status", cbstatus.SelectedValue.ToString()));
                arr.Add(new cArrayList("@claim_no", txsearch.Text));
                arr.Add(new cArrayList("@branch", cbbranch.SelectedValue.ToString()));
                arr.Add(new cArrayList("@vendor", cbvendor.SelectedValue.ToString()));
                arr.Add(new cArrayList("@month", cbMonth.SelectedValue.ToString()));
                arr.Add(new cArrayList("@year", cbYear.SelectedValue.ToString()));
                bll.vBindingGridToSp(ref grdclaim, "sp_tmst_claim_ho_get", arr);
                arr.Clear();                
            }
            chkSelect(sender, e);
        }
    }

    protected void chkSelect(object sender, EventArgs e)
    {
        bool checkSl = false,checkProp = false;
        string proposal = "";
        foreach (GridViewRow row in grdclaim.Rows)
        {
            CheckBox chckrw = (CheckBox)row.FindControl("chkSelect");
            Label lbclaim = (Label)row.FindControl("lbclaim");
            Label lbproposal = (Label)row.FindControl("lbprop");
            if (chckrw.Checked == true)
            {
                if (proposal != "")
                {
                    if (proposal == lbproposal.Text)
                    {
                        checkProp = false;
                        checkSl = true;
                    }
                    else
                    {
                        checkProp = true;
                        checkSl = false;
                    }
                }
                else
                {
                    checkProp = false;
                    checkSl = true;
                }                
                proposal = lbproposal.Text;
            }
        }
        
        if (checkSl == false)
        {
            btnMenu.Attributes.Add("style", "display:none");
        }
        else
        {
            btnMenu.Attributes.Remove("style");
        }
        if (checkProp == true)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Proposal Must be same in Sending Claim','Proposal No','warning');", true);
            return;
        }
        
    }

    protected void btclaim_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        if (cbstatus.SelectedValue.ToString() == "H-01")
        {
            grdclaim.Visible = true;
            grdclaimheader.Visible = false;
            arr.Add(new cArrayList("@status", cbstatus.SelectedValue.ToString()));
            arr.Add(new cArrayList("@claim_no", txsearch.Text));
            arr.Add(new cArrayList("@branch", cbbranch.SelectedValue.ToString()));
            arr.Add(new cArrayList("@vendor", cbvendor.SelectedValue.ToString()));
            arr.Add(new cArrayList("@month", cbMonth.SelectedValue.ToString()));
            arr.Add(new cArrayList("@year", cbYear.SelectedValue.ToString()));
            bll.vBindingGridToSp(ref grdclaim, "sp_tmst_claim_ho_get", arr);
        }
        else if (cbstatus.SelectedValue.ToString() == "R")
        {
            grdclaim.Visible = false;
            grdclaimheader.Visible = true;
            arr.Add(new cArrayList("@status", cbstatus.SelectedValue.ToString()));
            arr.Add(new cArrayList("@salespointcd", cbbranch.SelectedValue.ToString()));
            arr.Add(new cArrayList("@vendor", cbvendor.SelectedValue.ToString()));
            arr.Add(new cArrayList("@month", cbMonth.SelectedValue.ToString()));
            arr.Add(new cArrayList("@year", cbYear.SelectedValue.ToString()));
            arr.Add(new cArrayList("@claim_no", txsearch.Text));
            arr.Add(new cArrayList("@paymenttype", cbpaytype.SelectedValue.ToString()));
            bll.vBindingGridToSp(ref grdclaimheader, "sp_tclaim_header_get_batch", arr);
        }
        
        
    }

    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_claimentry_ho.aspx");
    }

    protected void btprint_Click(object sender, EventArgs e)
    {        
    }

    protected void btSaveSales_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        if (cbstatus.SelectedValue.ToString() == "H-01")
        {            
            foreach (GridViewRow row in grdclaim.Rows)
            {
                CheckBox chckrw = (CheckBox)row.FindControl("chkSelect");
                Label lbclaim = (Label)row.FindControl("lbclaim");
                if (chckrw.Checked == true)
                {
                    arr.Clear();
                    arr.Add(new cArrayList("@approve", "R"));
                    arr.Add(new cArrayList("@claim_no", lbclaim.Text));
                    arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                    arr.Add(new cArrayList("@claim_dt", DateTime.ParseExact(dtSalesSnd.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                    arr.Add(new cArrayList("@remark", txRemarkSalesSnd.Text));
                    arr.Add(new cArrayList("@status", "0"));
                    bll.vInsertClaimSalesDepartment(arr);
                }
            }
        }
        else if (cbstatus.SelectedValue.ToString() == "R")
        {
            foreach (GridViewRow row in grdclaim.Rows)
            {
                CheckBox chckrw = (CheckBox)row.FindControl("chkSelect");
                Label lbclaim = (Label)row.FindControl("lbclaim");
                if (chckrw.Checked == true)
                {
                    arr.Clear();
                    arr.Add(new cArrayList("@approve", "E"));
                    arr.Add(new cArrayList("@claim_no", lbclaim.Text));
                    arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                    arr.Add(new cArrayList("@claim_dt", DateTime.ParseExact(dtSalesRcv.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                    arr.Add(new cArrayList("@remark", txRemarkSalesRcv.Text));
                    arr.Add(new cArrayList("@status", "1"));
                    bll.vInsertClaimSalesDepartment(arr);
                }
            }
        }
        else if (cbstatus.SelectedValue.ToString() == "E")
        {
            foreach (GridViewRow row in grdclaim.Rows)
            {
                CheckBox chckrw = (CheckBox)row.FindControl("chkSelect");
                Label lbclaim = (Label)row.FindControl("lbclaim");
                if (chckrw.Checked == true)
                {
                    arr.Clear();
                    arr.Add(new cArrayList("@approve", "R"));
                    arr.Add(new cArrayList("@claim_no", lbclaim.Text));
                    arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                    arr.Add(new cArrayList("@claim_dt", DateTime.ParseExact(dtSalesSnd.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                    arr.Add(new cArrayList("@remark", txRemarkSalesSnd.Text));
                    arr.Add(new cArrayList("@status", "0"));
                    bll.vInsertClaimSalesDepartment(arr);
                }
            }
        }
        cbstatus_SelectedIndexChanged(sender, e);
    }

    protected void chkAllSelect(object sender, EventArgs e)
    {
        CheckBox chckheader = (CheckBox)grdclaimheader.HeaderRow.FindControl("chkAll");
        foreach (GridViewRow row in grdclaimheader.Rows)
        {
            CheckBox chckrw = (CheckBox)row.FindControl("chkSelect");
            Label lbclh = (Label)row.FindControl("lbclh");
            if (chckheader.Checked == true)
            {
                listCheckClaim += lbclh.Text + ", ";                
                chckrw.Checked = true;
                btnMenu.Attributes.Remove("style");
            }
            else
            {
                listCheckClaim = "";
                chckrw.Checked = false;
                btnMenu.Attributes.Add("style", "display:none");
            }
        }
        
    }

    protected void grdclaim_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        
        GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
        Label lbstatus = (Label)row.FindControl("lbstatus");
        int index = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName.Equals("edit"))
        {
            string code = grdclaim.DataKeys[index].Value.ToString();

        }
        else if (e.CommandName.Equals("Print"))
        {
            string clHeaderNo = grdclaimheader.DataKeys[index].Value.ToString();
            string type = bll.vLookUp("select distinct discount_mec from tclaim_header left join tmst_claim on tclaim_header.claim_no = tmst_claim.claim_no where clh_no = '" + clHeaderNo + "' ");
            if (type == "CSH")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport('fm_report2.aspx?src=sendclaimCO&clh=" + clHeaderNo + "');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport('fm_report2.aspx?src=sendclaim&clh=" + clHeaderNo + "');", true);
            }    
        }
        else if (e.CommandName.Equals("Check"))
        {
            if (bll.nCheckAccess("CLAIMPAYMENTVATINV", Request.Cookies["usr_id"].Value.ToString()) == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','Payment Claim !!','warning');", true);
                return;
            }
            string clHeaderNo = grdclaimheader.DataKeys[index].Value.ToString();
            //if ((cbstatus.SelectedValue.ToString() == "H-05") || (cbstatus.SelectedValue.ToString() == "H-06"))
            if ((lbstatus.Text == "Approved Paid"))
            {
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openwindow('fm_claiminfopayment.aspx?dc=" + clHeaderNo + "');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openwindow('fm_paymentclaim2.aspx?dc=" + clHeaderNo + "');", true);
            }
        }
        else if (e.CommandName.Equals("Pay"))
        {
            if (bll.nCheckAccess("CLAIMPAYMENTVATINV", Request.Cookies["usr_id"].Value.ToString()) == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','Payment Claim !!','warning');", true);
                return;
            }
            string clHeaderNo = grdclaimheader.DataKeys[index].Value.ToString();
            //if ((cbstatus.SelectedValue.ToString() == "H-05") || (cbstatus.SelectedValue.ToString() == "H-06"))
            if ((lbstatus.Text == "Approved-Full (Prc)") || (lbstatus.Text == "Approved-Partial (Prc)") || (lbstatus.Text == "Approved Paid") || (lbstatus.Text == "Approved Unpaid"))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openwindow('fm_paymentclaim2.aspx?dc=" + clHeaderNo + "');", true);
            }
        }
        else if (e.CommandName.Equals("Pay to Branch"))
        {
            if (bll.nCheckAccess("CLAIMPAYMENTVATINV", Request.Cookies["usr_id"].Value.ToString()) == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','Payment Claim !!','warning');", true);
                return;
            }
            string clHeaderNo = grdclaimheader.DataKeys[index].Value.ToString();
            if ((lbstatus.Text == "Approved Paid"))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openwindow('fm_paymentclaimtobranch.aspx?dc=" + clHeaderNo + "');", true);
            }
        }
    }

    protected void cbstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        claimSending.Attributes.Remove("data-target");

        if (cbstatus.SelectedValue.ToString() == "H-02")
        {
            btaprorpay.Text = "Approve H-02";
        }
        else if (cbstatus.SelectedValue.ToString() == "H-06")
        {
            btaprorpay.Text = "Pay";
        }
        else if (cbstatus.SelectedValue.ToString() == "H-07")
        {
            btaprorpay.Text = "Pay Unpaid";
        }

        if (cbstatus.SelectedValue.ToString() == "H-01")
        {
            grdclaim.Visible = true;
            grdclaimheader.Visible = false;
            txsearch.Attributes.Remove("readonly");
            chall_CheckedChanged(sender, e);
            claimSending.Attributes.Add("data-target", "#sendingClaim");
        }
        else //if(cbstatus.SelectedValue.ToString() == "R")
        {
            grdclaim.Visible = false;
            grdclaimheader.Visible = true;
            //txsearch.Attributes.Add("readonly", "readonly");
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@status", cbstatus.SelectedValue.ToString()));
            arr.Add(new cArrayList("@salespointcd", cbbranch.SelectedValue.ToString()));
            arr.Add(new cArrayList("@month", cbMonth.SelectedValue.ToString()));
            arr.Add(new cArrayList("@year", cbYear.SelectedValue.ToString()));
            arr.Add(new cArrayList("@claim_no", txsearch.Text));
            arr.Add(new cArrayList("@vendor", cbvendor.SelectedValue.ToString()));
            arr.Add(new cArrayList("@paymenttype", cbpaytype.SelectedValue.ToString()));
            bll.vBindingGridToSp(ref grdclaimheader, "sp_tclaim_header_get_batch", arr);
        }
       

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "cl", "vDisableShow();", true);
        Session["status"] = cbstatus.SelectedValue.ToString();
    }

    protected void cbbranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cbstatus.SelectedValue.ToString() == "H-01") 
        {
            chall_CheckedChanged(sender, e);
        }
        else //if (cbstatus.SelectedValue.ToString() == "R")
        {
            cbstatus_SelectedIndexChanged(sender, e);
        }        
        Session["salespoint"] = cbbranch.SelectedValue.ToString();
    }

    protected void cbvendor_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cbstatus.SelectedValue.ToString() == "H-01")
        {
            chall_CheckedChanged(sender, e);
        }
        else //if (cbstatus.SelectedValue.ToString() == "R")
        {
            cbstatus_SelectedIndexChanged(sender, e);
        }
        Session["vendor"] = cbvendor.SelectedValue.ToString();
    }

    protected void cbmonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cbstatus.SelectedValue.ToString() == "H-01")
        {
            chall_CheckedChanged(sender, e);
        }
        else //if (cbstatus.SelectedValue.ToString() == "R")
        {
            cbstatus_SelectedIndexChanged(sender, e);
        }
        Session["month"] = cbMonth.SelectedValue.ToString();
    }

    protected void cbyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cbstatus.SelectedValue.ToString() == "H-01")
        {
            chall_CheckedChanged(sender, e);
        }
        else //if (cbstatus.SelectedValue.ToString() == "R")
        {
            cbstatus_SelectedIndexChanged(sender, e);
        }
        Session["year"] = cbYear.SelectedValue.ToString();
    }

    protected void btSendClaim_Click(object sender, EventArgs e)
    {
        string clHeaderNo = "",listAttn = "",listCC = ""; string[] vendor;
        int i = 1;
        vendor = txto.Text.Split('-');
        System.Data.SqlClient.SqlDataReader rs = null;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@sys", "clheader"));
        arr.Add(new cArrayList("@sysno", ""));
        bll.vGetDiscountNo(arr, ref rs);
        while (rs.Read())
        {
            clHeaderNo = rs["generated"].ToString();
        }
        foreach (ListItem latn in lstAttn.Items)
        {
            if (latn.Selected)
            {
                if (i % 2 == 0) { listAttn += ","; }
                listAttn += latn.Text;                
                i++;
            }
        }
        i = 1;
        foreach (ListItem lcc in lstCC.Items)
        {
            if (lcc.Selected)
            {
                if (i % 2 == 0) { listCC += ","; }
                listCC += lcc.Text;
                i++;
            }
        }
        arr.Clear();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        //bll.vDelWrkClaimSendPrincipal(arr);
        if (cbstatus.SelectedValue.ToString() == "H-01")
        {
            foreach (GridViewRow row in grdclaim.Rows)
            {
                CheckBox chckrw = (CheckBox)row.FindControl("chkSelect");
                Label lbclaim = (Label)row.FindControl("lbclaim");
                if (chckrw.Checked == true)
                {
                    arr.Clear();
                    arr.Add(new cArrayList("@clh_no", clHeaderNo));
                    arr.Add(new cArrayList("@claim_no", lbclaim.Text));
                    arr.Add(new cArrayList("@salespoint", bll.vLookUp("select salespointcd from tmst_claim where claim_no = '" + lbclaim.Text + "'")));
                    arr.Add(new cArrayList("@claimby", Request.Cookies["usr_id"].Value.ToString()));
                    arr.Add(new cArrayList("@claim_dt", DateTime.ParseExact(dtSendClaim.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                    arr.Add(new cArrayList("@att", listAttn));
                    arr.Add(new cArrayList("@cc", listCC));
                    arr.Add(new cArrayList("@remarks", txRemarks.Text.ToString()));
                    arr.Add(new cArrayList("@cprocess", "1"));
                    if ((vendor[0].Replace(" ", "") == "V1600") || (vendor[0].Replace(" ", "") == "V1700") || (vendor[0].Replace(" ", "") == "V1400"))
                    {
                        arr.Add(new cArrayList("@cstatus", "H-02"));
                    }
                    else
                    {
                        arr.Add(new cArrayList("@cstatus", "H-06"));
                    }

                    bll.vInsertClaimSendPrincipal(arr);
                }
            }
            string type = bll.vLookUp("select distinct discount_mec from tclaim_header left join tmst_claim on tclaim_header.claim_no = tmst_claim.claim_no where clh_no = '" + clHeaderNo + "' ");
            if (type == "CSH")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport('fm_report2.aspx?src=sendclaimCO&clh=" + clHeaderNo + "');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport('fm_report2.aspx?src=sendclaim&clh=" + clHeaderNo + "');", true);
            }
            if ((vendor[0].Replace(" ", "") == "V1600") || (vendor[0].Replace(" ", "") == "V1700"))
            {
                arr.Clear();
                arr.Add(new cArrayList("@clh_no", clHeaderNo));
                arr.Add(new cArrayList("@typ", type));
                bll.vBatchEmailtoPAFL(arr);
            }
        }
        arr.Clear();
        cbstatus_SelectedIndexChanged(sender, e);
    }

    protected void btSendClose_Click(object sender, EventArgs e)
    {
        //listCheckClaim = "";
        //arr.Clear();
        //arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        //bll.vDelWrkClaimSendPrincipal(arr);
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (cbstatus.SelectedValue.ToString() == "H-01")
        {
            chall_CheckedChanged(sender, e);
        }
        else //if (cbstatus.SelectedValue.ToString() == "R")
        {
            cbstatus_SelectedIndexChanged(sender, e);
        }
    }

    protected void send_Click(object sender, EventArgs e)
    {
        string claim = "", vendor = "";
        foreach (GridViewRow row in grdclaim.Rows)
        {
            CheckBox chckrw = (CheckBox)row.FindControl("chkSelect");
            Label lbclaim = (Label)row.FindControl("lbclaim");
            if (chckrw.Checked == true)
            {
                claim = lbclaim.Text;
                break;
            }
        }
        vendor = bll.vLookUp("select vendor_cd from tmst_claim where claim_no = '" + claim + "'");
        txto.Text = bll.vLookUp("select (vendor_cd+' - '+vendor_nm) as vendor from tmst_vendor where vendor_cd = '" + vendor + "'");
        txto.Attributes.Add("readonly", "readonly");
        txRemarks.Text = bll.vLookUp("select bgremark from tmst_proposal where prop_no = (select prop_no from tmst_claim where claim_no = '"+ claim +"')");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@vendor_cd", vendor));
        bll.vBindingListToSp(ref lstAttn, "sp_tproposal_signbyvendor_get", "fullname", "fullname", arr);
        arr.Clear();
        bll.vBindingListToSp(ref lstCC, "sp_tproposal_signoff_get", "emp_desc", "emp_desc", arr);
    }
    protected void btaprorpay_Click(object sender, EventArgs e)
    {
        if (bll.nCheckAccess("CLAIMVATINV", Request.Cookies["usr_id"].Value.ToString()) == 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','Approve Claim Invoice !!','warning');", true);
            return;
        }
        //txClaim.Attributes.Add("readonly", "readonly");
        //payment.Attributes.Add("style", "display:none");
        //issueGroup.Attributes.Add("style", "display:none");

        //issueList.Attributes.Add("style", "display:none");
        //remarks.Attributes.Add("style", "display:none");
        uploadfile.Attributes.Add("style", "display:none");
        int counter = 0;
        if (grdclaimheader.Rows.Count == 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('No Data !','No Data','warning');", true);
            return;
        }

        foreach (GridViewRow row in grdclaimheader.Rows)
        {
            CheckBox chkRow = (row.Cells[0].FindControl("chkSelect") as CheckBox);

            if (chkRow.Checked)
            {
                counter = counter + 1;
            }
        }

        if (counter == 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please Checkbox Claim Number !','Checkbox Claim Number can not empty ','warning');", true);
            return;
        }
        if (cbstatus.SelectedValue.ToString() == "H-02")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Pop", "$('#myApprove').modal();", true);
            upModal.Update();
        }
        else if (cbstatus.SelectedValue.ToString() == "H-06")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Pop", "$('#myPay').modal();", true);
            upPay.Update();
        }
        else if (cbstatus.SelectedValue.ToString() == "H-07")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Pop", "$('#myPay').modal();", true);
            upPay.Update();
        }

    }
    protected void btapprove_Click(object sender, EventArgs e)
    {
        string vendor_cd = "";
        int tx_year = 0;
        string vat_inv = "";

        foreach (GridViewRow row in grdclaimheader.Rows)
        {
            if (((CheckBox)row.FindControl("chkSelect")).Checked)
            {
                Label lbclh = (Label)row.FindControl("lbclh");
                Label lbclno = (Label)row.FindControl("lbclno");

                List<cArrayList> arr = new List<cArrayList>();

                if ((cbstatus.SelectedValue == "H-02") || (cbstatus.SelectedValue == "H-08"))
                {
                    string amount = bll.vLookUp("select top 1 amount from tmst_claim inner join tclaim_header on tmst_claim.claim_no = tclaim_header.claim_no and tmst_claim.salespointcd = tclaim_header.salespointcd   where tmst_claim.claim_no = '" + lbclno.Text + "' and tclaim_header.clh_no = '" + lbclh.Text + "'");

                    vendor_cd = bll.vLookUp("select tmst_claim.vendor_cd from tclaim_header left join tmst_claim on tclaim_header.claim_no = tmst_claim.claim_no and tclaim_header.salespointcd = tmst_claim.salespointcd where clh_no = '" + lbclh.Text + "'");

                    tx_year = int.Parse(bll.vLookUp("select isnull(year(tclaim_header.claim_dt),0) from tclaim_header left join tmst_claim on tclaim_header.claim_no = tmst_claim.claim_no and tclaim_header.salespointcd = tmst_claim.salespointcd where clh_no = '" + lbclh.Text + "'"));

                    arr.Add(new cArrayList("@clh_no", lbclh.Text));
                    arr.Add(new cArrayList("@claim_no", lbclno.Text));
                    arr.Add(new cArrayList("@issue", cbissue.SelectedValue.ToString()));
                    arr.Add(new cArrayList("@percentage", amount));
                    arr.Add(new cArrayList("@remark", txappvRemark.Text));
                    arr.Add(new cArrayList("@approveby", Request.Cookies["usr_id"].Value.ToString()));
                    arr.Add(new cArrayList("@payment", cbPayment.SelectedValue.ToString()));
                    bll.vInsApproveClaim(arr);
                    arr.Clear();

                    // Generate INV VAT 

                    if (vendor_cd == "V1600" || vendor_cd == "V1700")
                    {

                        if (cbPayment.SelectedValue.ToString() != "FG")
                        {
                            if (tx_year >= 2018)
                            {
                                System.Data.SqlClient.SqlDataReader rs = null;
                                arr.Clear();
                                arr.Add(new cArrayList("@sys", "clheaderinv"));
                                arr.Add(new cArrayList("@sysno", ""));
                                bll.vGetDiscountNo(arr, ref rs);
                                while (rs.Read())
                                {
                                    vat_inv = rs["generated"].ToString();
                                }

                                arr.Clear();
                                arr.Add(new cArrayList("@clh_no", lbclh.Text));
                                arr.Add(new cArrayList("@inv_no", vat_inv));
                                arr.Add(new cArrayList("@claim_no", lbclno.Text));
                                arr.Add(new cArrayList("@percentage", amount));
                                arr.Add(new cArrayList("@approveby", Request.Cookies["usr_id"].Value.ToString()));
                                bll.vInserClaimInvVat(arr);
                            }
                        }
                    }
                }
            }
        }

        btapprove.Visible = false;
        btapprove.Enabled = false;
        btapprove.Attributes.Add("style", "display:none");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myApprove').modal('hide');", true);
        upModal.Update();
        Response.Redirect(Request.Url.AbsoluteUri);
        cbstatus_SelectedIndexChanged(sender, e);
    }
    protected void btpay_Click(object sender,EventArgs e)
    {
        foreach (GridViewRow row in grdclaimheader.Rows)
        {
            if (((CheckBox)row.FindControl("chkSelect")).Checked)
            {
                Label lbclh = (Label)row.FindControl("lbclh");
                Label lbclno = (Label)row.FindControl("lbclno");
                //DropDownList cbbankcq = (DropDownList)row.FindControl("cbbankcq");
                //DropDownList cbdoctype = (DropDownList)row.FindControl("cbdoctype");
                //Label txRefNoPay = (Label)row.FindControl("txRefNoPay");
                //Label txpayremark = (Label)row.FindControl("txpayremark");
                

                string countPayment = bll.vLookUp("select count(*)+1 from tclaim_payment where doc_no = '" + txRefNoPay.Text + "'");

                if (txRefNoPay.Text == "")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "swal('Ref no can not empty','Ref No','warning');", true);
                    return;
                }
                List<cArrayList> arr = new List<cArrayList>();
                arr.Add(new cArrayList("@bankcq", cbbankcq.SelectedValue.ToString()));
                arr.Add(new cArrayList("@remark", txpayremark.Text));

                if (fup.FileName == "")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "swal('No File Uploaded.','Document Upload','warning');", true);
                    return;
                }
                else
                {
                    FileInfo fi = new FileInfo(fup.FileName);
                    string ext = fi.Extension;
                    byte[] fs = fup.FileBytes;
                    if (fs.Length <= 1073741824)
                    {
                        if (ext == ".jpg" || ext == ".jpeg" || ext == ".bmp" || ext == ".gif" || ext == ".png" || ext == ".pdf" || ext == ".JPEG" || ext == ".JPG" || ext == ".BMP" || ext == ".GIF" || ext == ".PNG" || ext == ".PDF")
                        {
                            if ((fup.FileName != "") || (fup.FileName != null))
                            {
                                arr.Add(new cArrayList("@clh_no", lbclh.Text));
                                arr.Add(new cArrayList("@pay_typ", "CH"));
                                arr.Add(new cArrayList("@doc_typ", cbdoctype.SelectedValue.ToString()));
                                arr.Add(new cArrayList("@paymentby", Request.Cookies["usr_id"].Value.ToString()));
                                arr.Add(new cArrayList("@doc_no", txRefNoPay.Text));
                                arr.Add(new cArrayList("@doc_file", "PY_" + txRefNoPay.Text + "_" + countPayment + ext));
                                bll.vInsClaimPayment(arr);
                                
                                fup.SaveAs(bll.sGetControlParameter("image_path") + "/claim_doc/payment/" + "PY_" + txRefNoPay.Text + "_" + countPayment + ext);
                                string sitem = "null";
                                string sfreeqty = "0";
                                string sprice = "0";
                                string amount = bll.vLookUp("select top 1 amount from tmst_claim inner join tclaim_header on tmst_claim.claim_no = tclaim_header.claim_no and tmst_claim.salespointcd = tclaim_header.salespointcd   where tmst_claim.claim_no = '" + lbclno.Text + "' and tclaim_header.clh_no = '" + lbclh.Text + "'");
                                string amount2 = bll.vLookUp("select sum(amount) from tclaim_payment_dtl where claim_no = '" + lbclno.Text + "' and clh_no = '" + lbclh.Text + "'");
                                if (cbstatus.SelectedValue.ToString() == "H-07")
                                {
                                    amount = (Convert.ToDouble(amount) - Convert.ToDouble(amount2)).ToString();
                                }
                                string sfreevalue = amount;
                                if (cbpaytyp.SelectedValue== "FG")
                                { 
                                    //string sitem=bll.vLookUp("select top 1 item_cd+'_'+convert(varchar(50),isnull(tclaim_approve.percentage/isnull(unitprice,1),0))+'_'+convert(varchar(50),isnull(unitprice,0)) from tclaim_dtl inner join tclaim_header on tclaim_dtl.claim_no = tclaim_header.claim_no and tclaim_dtl.salespointcd = tclaim_header.salespointcd inner join tclaim_approve on tclaim_approve.claim_no = tclaim_header.claim_no and tclaim_approve.salespointcd = tclaim_header.salespointcd where tclaim_dtl.claim_no = '" + lbclno.Text + "' and tclaim_header.clh_no = '"+ lbclh.Text+"'");
                                    sitem = bll.vLookUp("select top 1 item_cd from tclaim_dtl inner join tclaim_header on tclaim_dtl.claim_no = tclaim_header.claim_no and tclaim_dtl.salespointcd = tclaim_header.salespointcd where tclaim_dtl.claim_no = '" + lbclno.Text + "' and tclaim_header.clh_no = '" + lbclh.Text + "'");
                                    sprice = bll.vLookUp("select top 1 unitprice from tcustomertype_price where cust_typ = '"+cbpriceotlcd.SelectedValue+"' and item_cd = '"+sitem+"' order by start_dt desc" );
                                    sfreevalue = "0";
                                    sfreeqty = (Convert.ToDouble(amount) / Convert.ToDouble(sprice)).ToString();
                                }
                                
                                arr.Clear();
                                arr.Add(new cArrayList("@clh_no", lbclh.Text));
                                arr.Add(new cArrayList("@manual_no", lbclno.Text));
                                arr.Add(new cArrayList("@product", sitem));
                                arr.Add(new cArrayList("@ref_no", txRefNoPay.Text));
                                arr.Add(new cArrayList("@vref_no", txRefNoPay.Text));
                                arr.Add(new cArrayList("@pay_dt", DateTime.ParseExact(txdatePayment.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                                arr.Add(new cArrayList("@orderqty",0));
                                arr.Add(new cArrayList("@freeqty", sfreeqty));
                                arr.Add(new cArrayList("@freevalue", sfreevalue));
                                arr.Add(new cArrayList("@discount", 0));
                                arr.Add(new cArrayList("@unitprice", sprice));
                                arr.Add(new cArrayList("@amount", amount));
                                arr.Add(new cArrayList("@claim_no", lbclno.Text));
                                arr.Add(new cArrayList("@status", 'A'));
                                bll.vInsertPaymentClaimDtl(arr);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "swal('You should upload image','jpg,bmp,gif and png upload document again','warning');", true);
                            return;
                        }
                    }
                }
                arr.Clear();
                arr.Add(new cArrayList("@clh_no", lbclh.Text));
                bll.vUpdatePaymentClaimDtl(arr);

            }
        }
        cbstatus_SelectedIndexChanged(sender, e);
    }
    
    protected void cbgroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@group", cbgroup.SelectedValue.ToString()));
        bll.vBindingComboToSp(ref cbissue, "sp_get_issue_list", "issue_no", "issue_nm", arr);
    }

    protected void cbpaytype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cbstatus.SelectedValue.ToString() == "H-01")
        {
            chall_CheckedChanged(sender, e);
        }
        else //if (cbstatus.SelectedValue.ToString() == "R")
        {
            cbstatus_SelectedIndexChanged(sender, e);
        }
        Session["paytype"] = cbvendor.SelectedValue.ToString();
    }
}

