using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_claimList : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string accessBy = Request.Cookies["sp"].Value.ToString();
            string currentMonth = DateTime.Now.Month.ToString();

            if (currentMonth.Length == 1)
            {
                currentMonth = "0" + currentMonth;
            }
            ddMonth.SelectedValue = currentMonth;

            if (accessBy == "0")
            {
                accessBy = null;
            }
            List<cArrayList> arr = new List<cArrayList>();
            bll.vBindingFieldValueToCombo(ref ddStatus, "claim_sta_id");
            bll.vBindingComboToSp(ref ddYear, "sp_tmst_period_getbyyear", "yearvalue", "yearvalue");
            arr.Add(new cArrayList("@salespointcd", accessBy));
            bll.vBindingComboToSp(ref ddBranch, "sp_tmst_salespoint_get", "salespointcd", "salespoint_nm", arr);
            bll.vBindingComboToSp(ref cbProd_cd, "sp_tmst_product_get3", "ID", "ProdName");
            arr.Clear();

            getClaimList();
        }

        if (ddStatus.SelectedValue == "N" || ddStatus.SelectedValue == "W" || ddStatus.SelectedValue == "CH") btnApprove.Visible = false; else btnApprove.Visible = false;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix();", true);


    }
    protected void ddBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        getClaimList();
    }
    protected void ddStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        getClaimList();
    }

    void getClaimList()
    {
        //string[] words;
        List<cArrayList> arr = new List<cArrayList>();
        string accessBy = ddBranch.SelectedValue.ToString();
        string status = ddStatus.SelectedValue.ToString();
        string month = ddMonth.SelectedValue.ToString();
        string year = ddYear.SelectedValue.ToString();
        string prod_cd = cbProd_cd.SelectedValue.ToString();
        //words = month.Split("/");
        // month = words[1];



        if (ddtype.SelectedValue.ToString() == "IV")
        {
            arr.Add(new cArrayList("@salespointcd", accessBy));
            arr.Add(new cArrayList("@claim_sta_id", status));
            arr.Add(new cArrayList("@month", month));
            arr.Add(new cArrayList("@year", year));
            arr.Add(new cArrayList("@prod_cd", prod_cd));
            bll.vBindingGridToSp(ref gridClaim, "sp_tmst_claim_get", arr);

        }
        else if (ddtype.SelectedValue.ToString() == "CSH")
        {
            arr.Add(new cArrayList("@salespointcd", accessBy));
            arr.Add(new cArrayList("@claim_sta_id", status));
            arr.Add(new cArrayList("@month", month));
            arr.Add(new cArrayList("@year", year));
            arr.Add(new cArrayList("@prod_cd", prod_cd));
            bll.vBindingGridToSp(ref grdclaimcashout, "sp_tmst_claimcxashout_get", arr);
        }
        else if (ddtype.SelectedValue.ToString() == "CNDN")
        {
            arr.Add(new cArrayList("@salespointcd", accessBy));
            arr.Add(new cArrayList("@claim_sta_id", status));
            arr.Add(new cArrayList("@month", month));
            arr.Add(new cArrayList("@year", year));
            arr.Add(new cArrayList("@prod_cd", prod_cd));
            bll.vBindingGridToSp(ref grdclaimcndn, "sp_tmst_claimcndn_get", arr);
        }
        else if (ddtype.SelectedValue.ToString() == "BA")
        {
            arr.Add(new cArrayList("@salespointcd", accessBy));
            arr.Add(new cArrayList("@claim_sta_id", status));
            arr.Add(new cArrayList("@month", month));
            arr.Add(new cArrayList("@year", year));
            arr.Add(new cArrayList("@prod_cd", prod_cd));
            bll.vBindingGridToSp(ref grdclaimcontract, "sp_tmst_claimcontract_get", arr);
        }
        else
        {
            arr.Add(new cArrayList("@salespointcd", accessBy));
            arr.Add(new cArrayList("@claim_sta_id", status));
            arr.Add(new cArrayList("@month", month));
            arr.Add(new cArrayList("@year", year));
            arr.Add(new cArrayList("@prod_cd", prod_cd));
            bll.vBindingGridToSp(ref gridClaim, "sp_tmst_claim_get", arr);
            bll.vBindingGridToSp(ref grdclaimcashout, "sp_tmst_claimcxashout_get", arr);
            bll.vBindingGridToSp(ref grdclaimcndn, "sp_tmst_claimcndn_get", arr);
            bll.vBindingGridToSp(ref grdclaimcontract, "sp_tmst_claimcontract_get", arr);
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix();", true);


    }
    protected void ddMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        getClaimList();
    }

    protected void gridClaim_SelectedIndexChanged(object sender, GridViewCommandEventArgs e)
    {
        getClaimList();
    }
    protected void ddYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        getClaimList();
    }

    protected void btnPrint_Click(Object sender, EventArgs e)
    {

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport('fm_report2.aspx?src=claimMonthly&branch=" + ddBranch.Text + "&status=" + ddStatus.Text + "&month=" + ddMonth.Text + "&year=" + ddYear.Text + "');", true);
    }
    private void vSendEmail(string sClaimCode, string sToken, string sReceived, string sCCNRNO)
    {
        /// SqlDataReader rs = null;
        string sHttp = bll.sGetControlParameter("link_branch");
        // List<cArrayList> arr = new List<cArrayList>();
        // string sLastDate = string.Empty; ;
        //string sLastAmount = string.Empty;
        //  arr.Add(new cArrayList("@cust_cd", hdcust.Value.ToString()));
        // bll.vGetLastTrans(arr, ref rs);
        // while (rs.Read())
        // {
        //     sLastAmount = rs["trn_amt"].ToString();
        //     sLastDate = rs["trn_typ"].ToString() + "," + rs["trn
        ;
        // } rs.Close();
        string sBody = "Dear Claim Department, \n\r New Claim Needs Check" + sClaimCode +
                  "<table style='width:100%'><tr style='background-color:silver'><td class='auto-style1'>Salespoint CD.</td><td>&nbsp;</td><td>" + Request.Cookies["sp"].Value.ToString() + "</td><td>Salespoint NM.</td><td>&nbsp;</td><td>" + bll.vLookUp("select salespoint_nm from tmst_salespoint where salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'") + "</td></tr><tr style='background-color:silver'><td class='auto-style1'>Claim No</td><td>:</td><td>" + sClaimCode + "</td><td>CCNR NO</td><td>:</td><td>" + sCCNRNO + "</td></tr>" +
                  "<tr><td class='auto-style1'>Proposal</td><td>:</td><td>" + bll.vLookUp("select prop_no from tmst_claim where claim_no='" + sClaimCode + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'") + "</td><td>Created Date</td><td>:</td><td>" + bll.vLookUp("select claim_dt from tmst_claim where claim_no='" + sClaimCode + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'") + "</td></tr>" +
                  "<tr style='background-color:silver'><td class='auto-style1'>Total Item Claim</td><td>:</td><td>" + bll.vLookUp("select orderqty from tmst_claim where claim_no='" + sClaimCode + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'") + "</td><td>Total Free Item</td><td>:</td><td>" + bll.vLookUp("select freeqty from tmst_claim where claim_no='" + sClaimCode + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'") + "</td></tr>" +
                  "<tr><td class='auto-style1'>Total Quantity</td><td>:</td><td>" + bll.vLookUp("select ordervalue from tmst_claim where claim_no='" + sClaimCode + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'") + "</td><td>Total Discount</td><td>:</td><td>" + bll.vLookUp("select freevalue from tmst_claim where claim_no='" + sClaimCode + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'") + "</td></tr>" +
                  "</table>";
        //bll.vSendMail(sCCReceived, "cc:Claim Request Needs Approval", sBody);
        bll.vSendMail(sReceived, "Claim Needs Approval", sBody);
    }
    private void vSendEmail1(string sClaimCode, string sToken, string sReceived, string sCCNRNO)
    {
        /// SqlDataReader rs = null;
        string sHttp = bll.sGetControlParameter("link_branch");
        // List<cArrayList> arr = new List<cArrayList>();
        // string sLastDate = string.Empty; ;
        //string sLastAmount = string.Empty;
        //  arr.Add(new cArrayList("@cust_cd", hdcust.Value.ToString()));
        // bll.vGetLastTrans(arr, ref rs);
        // while (rs.Read())
        // {
        //     sLastAmount = rs["trn_amt"].ToString();
        //     sLastDate = rs["trn_typ"].ToString() + "," + rs["trn_dt"].ToString();
        // } rs.Close();
        string sBody = "Dear Branch users, \n\r Claim has been approved" + sClaimCode +
                  "<table style='width:100%'><tr style='background-color:silver'><td class='auto-style1'>Salespoint CD.</td><td>&nbsp;</td><td>" + Request.Cookies["sp"].Value.ToString() + "</td><td>Salespoint NM.</td><td>&nbsp;</td><td>" + bll.vLookUp("select salespoint_nm from tmst_salespoint where salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'") + "</td></tr><tr style='background-color:silver'><td class='auto-style1'>Claim No</td><td>:</td><td>" + sClaimCode + "</td><td>CCNR NO</td><td>:</td><td>" + sCCNRNO + "</td></tr>" +
                  "<tr><td class='auto-style1'>Proposal</td><td>:</td><td>" + bll.vLookUp("select prop_no from tmst_claim where claim_no='" + sClaimCode + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'") + "</td><td>Created Date</td><td>:</td><td>" + bll.vLookUp("select claim_dt from tmst_claim where claim_no='" + sClaimCode + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'") + "</td></tr>" +
                  "<tr style='background-color:silver'><td class='auto-style1'>Total Item Claim</td><td>:</td><td>" + bll.vLookUp("select orderqty from tmst_claim where claim_no='" + sClaimCode + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'") + "</td><td>Total Free Item</td><td>:</td><td>" + bll.vLookUp("select freeqty from tmst_claim where claim_no='" + sClaimCode + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'") + "</td></tr>" +
                  "<tr><td class='auto-style1'>Total Quantity</td><td>:</td><td>" + bll.vLookUp("select ordervalue from tmst_claim where claim_no='" + sClaimCode + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'") + "</td><td>Total Discount</td><td>:</td><td>" + bll.vLookUp("select freevalue from tmst_claim where claim_no='" + sClaimCode + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'") + "</td></tr>" +
                  "</table>";
        // bll.vSendMail(sCCReceived, "cc:Claim Request Needs Approval", sBody);
        bll.vSendMail(sReceived, "Claim Needs Check", sBody);
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        if (ddStatus.SelectedValue == "N")
        {
            if (bll.nCheckAccess("ccnrapp", Request.Cookies["usr_id"].Value.ToString()) == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','To approve this claim contact Administrator !!','warning');", true);
                return;
            }
            string approve_by = Request.Cookies["usr_id"].Value.ToString();
            string dtapp1 = Request.Cookies["waz_dt"].Value.ToString();
            //string dtp2 = Request.QueryString["enddate"];
            DateTime dtpayp1 = DateTime.ParseExact(dtapp1, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            foreach (GridViewRow row in gridClaim.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("approve") as CheckBox);
                    if (chkRow.Checked)
                    {
                        //string claimNo = row.Cells[1].FindControl("claimNo").ToString();
                        Label claimNo = (Label)row.FindControl("claimNo");
                        List<cArrayList> arr = new List<cArrayList>();
                        arr.Add(new cArrayList("@claim_no", claimNo.Text));
                        arr.Add(new cArrayList("@claim_sta_id", "CH"));
                        arr.Add(new cArrayList("@approve_by", Request.Cookies["usr_id"].Value.ToString()));
                        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                        //Request.Cookies["waz_dt"].Value.ToString();
                        
                        arr.Add(new cArrayList("@app_dt", dtpayp1.Year + "-" + dtpayp1.Month + "-" + dtpayp1.Day));
                        bll.vUpdateClaim(arr);

                        List<string> lapproval = bll.lGetApproval("claimch", 1);
                        Random nRdm = new Random();
                        int token = nRdm.Next(1000, 9999);
                        double tokenmail = nRdm.Next();
                        Uri urlnya = Request.Url;
                        string host = urlnya.GetLeftPart(UriPartial.Authority);

                        //string sBody = "Dear Claim section, \n\r Need Approval for Claim " + hdcust.Value.ToString() +
                        //    "<table><tr><td>Code Customer</td><td>:</td><td>" + hdcust.Value.ToString() + "</td><td>Salesman Code</td><td>:</td><td>" + cbsalesman.SelectedValue.ToString() + "</td></tr>" +
                        //    "<tr><td>Customer Name</td><td>:</td><td>" + txcustomer.Text + "</td><td>Salesman Name</td><td>:</td><td>" + cbsalesman.SelectedItem.Text + "</td></tr>" +
                        //    "<tr><td>Type Customer</td><td>:</td><td>" + lbcusttype.Text + "</td><td>Customer Balance</td><td>:</td><td>" + txclremain.Text + "</td></tr></table>";  
                        //bll.vSendMail(lapproval[1], "Salesorder Request", sBody);
                        List<string> lccedp = bll.lGetApproval("claimer", 1);
                        //vSendEmail(hdcust.Value.ToString(), tokenmail.ToString(), lapproval[1]);
                        vSendEmail(claimNo.Text, tokenmail.ToString(), lapproval[1], bll.vLookUp("select manual_no from tmst_ccnr where claim_no='" + claimNo.Text + "'"));
                        arr.Clear();
                        arr.Add(new cArrayList("@trxcd", "Claim second approval"));
                        arr.Add(new cArrayList("@token", tokenmail.ToString()));
                        arr.Add(new cArrayList("@doc_no", claimNo.Text));
                        bll.vInsertEmailSent(arr);
                    }
                }
            }
            Response.Redirect("fm_claimList.aspx", true);
        }
        else if (ddStatus.SelectedValue == "CH" || ddStatus.SelectedValue == "R")
        {
            if (bll.nCheckAccess("claimch", Request.Cookies["usr_id"].Value.ToString()) == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','To approve this claim contact Administrator !!','warning');", true);
                return;
            }
            string approve_by = Request.Cookies["usr_id"].Value.ToString();
            foreach (GridViewRow row in gridClaim.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("approve") as CheckBox);
                    if (chkRow.Checked)
                    {
                        //string claimNo = row.Cells[1].FindControl("claimNo").ToString();
                        Label claimNo = (Label)row.FindControl("claimNo");
                        List<cArrayList> arr = new List<cArrayList>();
                        arr.Add(new cArrayList("@claim_no", claimNo.Text));
                        arr.Add(new cArrayList("@claim_sta_id", "W"));
                        arr.Add(new cArrayList("@approve_by", Request.Cookies["usr_id"].Value.ToString()));
                        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                        arr.Add(new cArrayList("@app_dt", Request.Cookies["waz_dt"].Value.ToString()));
                        bll.vUpdateClaim(arr);

                        List<string> lapproval = bll.lGetApproval("claimapp", 1);
                        Random nRdm = new Random();
                        int token = nRdm.Next(1000, 9999);
                        double tokenmail = nRdm.Next();
                        Uri urlnya = Request.Url;
                        string host = urlnya.GetLeftPart(UriPartial.Authority);

                        //string sBody = "Dear Claim section, \n\r Need Approval for Claim " + hdcust.Value.ToString() +
                        //    "<table><tr><td>Code Customer</td><td>:</td><td>" + hdcust.Value.ToString() + "</td><td>Salesman Code</td><td>:</td><td>" + cbsalesman.SelectedValue.ToString() + "</td></tr>" +
                        //    "<tr><td>Customer Name</td><td>:</td><td>" + txcustomer.Text + "</td><td>Salesman Name</td><td>:</td><td>" + cbsalesman.SelectedItem.Text + "</td></tr>" +
                        //    "<tr><td>Type Customer</td><td>:</td><td>" + lbcusttype.Text + "</td><td>Customer Balance</td><td>:</td><td>" + txclremain.Text + "</td></tr></table>";  
                        //bll.vSendMail(lapproval[1], "Salesorder Request", sBody);
                        List<string> lccedp = bll.lGetApproval("claimer", 1);
                        // vSendEmail(hdcust.Value.ToString(), tokenmail.ToString(), lapproval[1]);
                        vSendEmail(claimNo.Text, tokenmail.ToString(), lapproval[1], bll.vLookUp("select manual_no from tmst_ccnr where claim_no='" + claimNo.Text + "'"));
                        arr.Clear();
                        arr.Add(new cArrayList("@trxcd", "Claim second approval"));
                        arr.Add(new cArrayList("@token", tokenmail.ToString()));
                        arr.Add(new cArrayList("@doc_no", claimNo.Text));
                        bll.vInsertEmailSent(arr);
                    }
                }
            }
            Response.Redirect("fm_claimList.aspx", true);
        }
        else if (ddStatus.SelectedValue == "W")
        {
            if (bll.nCheckAccess("claimapp", Request.Cookies["usr_id"].Value.ToString()) == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','To approve this claim contact Administrator !!','warning');", true);
                return;
            }
            string approve_by = Request.Cookies["usr_id"].Value.ToString();
            foreach (GridViewRow row in gridClaim.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("approve") as CheckBox);
                    if (chkRow.Checked)
                    {
                        //string claimNo = row.Cells[1].FindControl("claimNo").ToString();
                        Label claimNo = (Label)row.FindControl("claimNo");
                        List<cArrayList> arr = new List<cArrayList>();
                        arr.Add(new cArrayList("@claim_no", claimNo.Text));
                        arr.Add(new cArrayList("@claim_sta_id", "A"));
                        arr.Add(new cArrayList("@approve_by", Request.Cookies["usr_id"].Value.ToString()));
                        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                        arr.Add(new cArrayList("@app_dt", Request.Cookies["waz_dt"].Value.ToString()));
                        bll.vUpdateClaim(arr);

                        List<string> lapproval = bll.lGetApproval("claimer", 1);
                        Random nRdm = new Random();
                        int token = nRdm.Next(1000, 9999);
                        double tokenmail = nRdm.Next();
                        Uri urlnya = Request.Url;
                        string host = urlnya.GetLeftPart(UriPartial.Authority);

                        //string sBody = "Dear Claim section, \n\r Need Approval for Claim " + hdcust.Value.ToString() +
                        //    "<table><tr><td>Code Customer</td><td>:</td><td>" + hdcust.Value.ToString() + "</td><td>Salesman Code</td><td>:</td><td>" + cbsalesman.SelectedValue.ToString() + "</td></tr>" +
                        //    "<tr><td>Customer Name</td><td>:</td><td>" + txcustomer.Text + "</td><td>Salesman Name</td><td>:</td><td>" + cbsalesman.SelectedItem.Text + "</td></tr>" +
                        //    "<tr><td>Type Customer</td><td>:</td><td>" + lbcusttype.Text + "</td><td>Customer Balance</td><td>:</td><td>" + txclremain.Text + "</td></tr></table>";  
                        //bll.vSendMail(lapproval[1], "Salesorder Request", sBody);
                        List<string> lccedp = bll.lGetApproval("edp", 1);
                        // vSendEmail(hdcust.Value.ToString(), tokenmail.ToString(), lapproval[1]);
                        vSendEmail1(claimNo.Text, tokenmail.ToString(), lapproval[1], bll.vLookUp("select manual_no from tmst_ccnr where claim_no='" + claimNo.Text + "'"));
                        arr.Clear();
                        arr.Add(new cArrayList("@trxcd", "Claim second approval"));
                        arr.Add(new cArrayList("@token", tokenmail.ToString()));
                        arr.Add(new cArrayList("@doc_no", claimNo.Text));
                        bll.vInsertEmailSent(arr);
                    }
                }
            }
            Response.Redirect("fm_claimList.aspx", true);
        }
    }

    protected void chkboxSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox ChkBoxHeader = (CheckBox)gridClaim.HeaderRow.FindControl("chkboxSelectAll");
        foreach (GridViewRow row in gridClaim.Rows)
        {
            CheckBox ChkBoxRows = (CheckBox)row.FindControl("approve");
            if (ChkBoxHeader.Checked == true)
            {
                ChkBoxRows.Checked = true;
            }
            else
            {
                ChkBoxRows.Checked = false;
            }
        }
    }
    protected void btsearch_Click(object sender, EventArgs e)
    {

        List<cArrayList> arr = new List<cArrayList>();
        string accessBy = ddBranch.SelectedValue.ToString();
        string status = ddStatus.SelectedValue.ToString();
        string month = ddMonth.SelectedValue.ToString();
        string year = ddYear.SelectedValue.ToString();
        string prod_cd = cbProd_cd.SelectedValue.ToString();
        arr.Add(new cArrayList("@salespointcd", accessBy));
        arr.Add(new cArrayList("@claim_sta_id", status));
        arr.Add(new cArrayList("@month", month));
        arr.Add(new cArrayList("@year", year));
        arr.Add(new cArrayList("@claim", txsearhc.Text));
        arr.Add(new cArrayList("@prop_no", txsearhc.Text));
        arr.Add(new cArrayList("@type", ddtype.SelectedValue.ToString()));
        arr.Add(new cArrayList("@prod_cd", prod_cd));
        bll.vBindingGridToSp(ref gridClaim, "sp_tmst_claim_getbyprop", arr);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix();", true);

    }
    protected void rdbtrxn_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (rdbtrxn.SelectedValue.ToString() == "IV")
        //{
        //    gridClaim.Visible = true;
        //    grdclaimcashout.Visible = false;
        //    grdclaimcontract.Visible = false;
        //    grdclaimcndn.Visible = false;
        //    getClaimList();
        //}
        //else if (rdbtrxn.SelectedValue.ToString() == "CSH")
        //{
        //    gridClaim.Visible = false;
        //    grdclaimcashout.Visible = true;
        //    grdclaimcontract.Visible = false;
        //    grdclaimcndn.Visible = false;
        //    getClaimList();
        //}
        //else if (rdbtrxn.SelectedValue.ToString() == "CNDN")
        //{
        //    gridClaim.Visible = false;
        //    grdclaimcashout.Visible = false;
        //    grdclaimcontract.Visible = false;
        //    grdclaimcndn.Visible = true;
        //    getClaimList();
        //}
        //else if (rdbtrxn.SelectedValue.ToString() == "BA")
        //{
        //    gridClaim.Visible = false;
        //    grdclaimcashout.Visible = false;
        //    grdclaimcontract.Visible = true;
        //    grdclaimcndn.Visible = false;
        //    getClaimList();
        //}
    }
    protected void ddtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddtype.SelectedValue.ToString() == "IV")
        {
            gridClaim.Visible = true;
            grdclaimcashout.Visible = false;
            grdclaimcontract.Visible = false;
            grdclaimcndn.Visible = false;
            getClaimList();
        }
        else if (ddtype.SelectedValue.ToString() == "CSH")
        {
            gridClaim.Visible = false;
            grdclaimcashout.Visible = true;
            grdclaimcontract.Visible = false;
            grdclaimcndn.Visible = false;
            getClaimList();
        }
        else if (ddtype.SelectedValue.ToString() == "CNDN")
        {
            gridClaim.Visible = false;
            grdclaimcashout.Visible = false;
            grdclaimcontract.Visible = false;
            grdclaimcndn.Visible = true;
            getClaimList();
        }
        else if (ddtype.SelectedValue.ToString() == "BA")
        {
            gridClaim.Visible = false;
            grdclaimcashout.Visible = false;
            grdclaimcontract.Visible = true;
            grdclaimcndn.Visible = false;
            getClaimList();
        }
        else
        {
            gridClaim.Visible = true;
            grdclaimcashout.Visible = true;
            grdclaimcontract.Visible = true;
            grdclaimcndn.Visible = true;
            getClaimList();
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix();", true);

    }
}