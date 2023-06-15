using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class landingpage3 : System.Web.UI.Page
{
    cbll bll = new cbll();
    string return_page = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //by yanto 6-1-2019
            HttpCookie scookusersql = new HttpCookie("usersql");
            scookusersql = Request.Cookies["usersql"];
            if (scookusersql == null)
            {
                Response.Cookies["usersql"].Value = "sa";
            }
            //-------------------------
            string sSrc = Request.QueryString["src"];
            string sSta = Request.QueryString["sta"];
            string sCode = Request.QueryString["ids"];
            string appBy = Request.QueryString["appBy"];
            string updatMethod = Request.QueryString["updatMethod"];

            switch (sSrc)
            {
                case "acccndnCustApp":
                    string sStatusCNDNCust = bll.vLookUp("select cndncust_sta_id from tmst_cndncustomer where cndn_no='" + sCode + "'");
                    if (sStatusCNDNCust == "A")
                    {
                        lbstatus.Text = "This DN Customer already approved!";
                    }
                    else
                    {
                        List<cArrayList> arr = new List<cArrayList>();
                        arr.Add(new cArrayList("@cndn_no", sCode));
                        arr.Add(new cArrayList("@cndncust_sta_id", sSta));
                        arr.Add(new cArrayList("@updatedBy", appBy));
                        arr.Add(new cArrayList("@updatMethod", updatMethod));
                        bll.vUpdtDNCutomerApp(arr);
                        if (sSta == "A")
                        {
                            lbstatus.Text = "Direct DN Customer no." + sCode + " has been APPROVED!";
                        }
                        else if (sSta == "R")
                        {
                            lbstatus.Text = "Direct DN Customer no." + sCode + " has been REJECTED!";
                        }
                    }
                    break;
                case "acccndnAdjApp":
                    string sStatusCNDN = bll.vLookUp("select cndnAdj_sta_id from tacc_cndn where cndn_cd='" + sCode + "'");
                    if (sStatusCNDN == "A")
                    {
                        lbstatus.Text = "This CNDN Adjustment already approved!";
                    }
                    else
                    {
                        List<cArrayList> arr = new List<cArrayList>();
                        arr.Add(new cArrayList("@cndn_cd", sCode));
                        arr.Add(new cArrayList("@cndnAdj_sta_id", sSta));
                        arr.Add(new cArrayList("@updatedBy", appBy));
                        arr.Add(new cArrayList("@updatMethod", updatMethod));
                        bll.vUpdtCNDNApp(arr);
                        if (sSta == "A")
                        {
                            lbstatus.Text = "CNDN Adjustment no." + sCode + " has been APPROVED!";
                        }
                        else if (sSta == "R")
                        {
                            lbstatus.Text = "CNDN Adjustment no." + sCode + " has been REJECTED!";
                        }
                    }
                    break;
                case "dep":
                    string sStatus = bll.vLookUp("select dep_sta_id from tbank_deposit where deposit_id='" + sCode + "'");
                    if (sStatus != "N")
                    {
                        lbstatus.Text = "This payment already processed!";
                    }
                    else
                    {
                        // Update the data bank deposit
                        string sRefno = bll.vLookUp("select ref_no from tbank_deposit where deposit_id='" + sCode + "'");
                        List<cArrayList> arr = new List<cArrayList>();
                        arr.Add(new cArrayList("@IDS", sCode));
                        arr.Add(new cArrayList("@dep_sta_id", sSta));
                        bll.vUpdateBankDepositByStatus(arr);
                        if (sSta == "H")
                        {
                            lbstatus.Text = "Payment Cheque/Bank Transfer No." + sRefno + " has been CONFIRMED!";
                        }
                        else if (sSta == "R")
                        {
                            lbstatus.Text = "Payment Cheque/Bank Transfer No." + sRefno + " has been DECLINE!";
                        }
                    }
                    break;
                case "cashout":
                    string sStaBefore = bll.vLookUp("select cashout_sta_id from tcashout_request where cashout_cd='"+sCode+"'");
                    if (sStaBefore != "N")
                    {
                        lbstatus.Text = "This cashout request already processed!";
                    }
                    else
                    {
                        List<cArrayList> arr = new List<cArrayList>();
                        arr.Add(new cArrayList("@cashout_cd", sCode));
                        arr.Add(new cArrayList("@cashout_sta_id", sSta));
                        arr.Add(new cArrayList("@updatedBy", appBy));
                        bll.vUpdateCashoutRequestByStatus(arr);
                        if (sSta == "A")
                        {
                            lbstatus.Text = "Cashout no."+sCode+" has been APPROVED!";
                        }
                        else if (sSta == "R")
                        {
                            lbstatus.Text = "Cashout no."+sCode + " has been REJECTED!";
                        }
                    }
                    return_page = "fm_AccCashAdvanceApproval_List.aspx";
                    lblReturnPage.Text = return_page;

                    break;
                case "CashierPettyCash":
                    string sStaBeforeCashierPettyCash = bll.vLookUp("select cashout_sta_id from tcashout_request where cashout_cd='" + sCode + "'");
                    if (sStaBeforeCashierPettyCash != "N")
                    {
                        lbstatus.Text = "This cashout request already processed!";
                    }
                    else
                    {
                        List<cArrayList> arr = new List<cArrayList>();
                        arr.Add(new cArrayList("@cashout_cd", sCode));
                        arr.Add(new cArrayList("@cashout_sta_id", sSta));
                        bll.vUpdateCashoutRequestByStatus(arr);
                        if (sSta == "A")
                        {
                            lbstatus.Text = "Cashout no." + sCode + " has been APPROVED!";
                        }
                        else if (sSta == "R")
                        {
                            lbstatus.Text = "Cashout no." + sCode + " has been REJECTED!";
                        }
                    }
                    break;
                case "PettycashCashout":
                    string sStaBeforePettycashCashout = bll.vLookUp("select pc_sta_id from temployee_advanced_cash where doc_no='" + sCode + "'");
                    if (sStaBeforePettycashCashout != "N")
                    {
                        lbstatus.Text = "This Pettycash Cash Out " + sCode + " request already processed!";
                    }
                    else
                    {
                        List<cArrayList> arr = new List<cArrayList>();
                        arr.Add(new cArrayList("@doc_no", sCode));
                        arr.Add(new cArrayList("@pc_sta_id", sSta));
                        arr.Add(new cArrayList("@updatedBy", appBy));
                        bll.vUpdatePettycashCashoutRequestByStatus(arr);
                        if (sSta == "A")
                        {
                            lbstatus.Text = "Pettycash Cash Out no." + sCode + " has been APPROVED!";
                        }
                        else if (sSta == "R")
                        {
                            lbstatus.Text = "Pettycash Cash Out no." + sCode + " has been REJECTED!";
                        }
                    }
                    return_page = "fm_AccCashAdvanceApproval_List.aspx";
                    lblReturnPage.Text = return_page;

                    break;
                case "ClaimCashout":
                    string sStaBeforeClaimCashout = bll.vLookUp("select claimco_sta_id from tproposal_paid where claimco_cd='" + sCode + "'");
                    if (sStaBeforeClaimCashout != "N")
                    {
                        lbstatus.Text = "This Claim Cash Out request already processed!";
                    }
                    else
                    {
                        List<cArrayList> arr = new List<cArrayList>();
                        arr.Add(new cArrayList("@claimco_cd", sCode));
                        arr.Add(new cArrayList("@claimco_sta_id", sSta));
                        arr.Add(new cArrayList("@updatedBy", appBy));
                        bll.vUpdateClaimCashoutRequestByStatus(arr);
                        if (sSta == "A")
                        {
                            lbstatus.Text = "Claim Cash Out no." + sCode + " has been APPROVED!";
                        }
                        else if (sSta == "R")
                        {
                            lbstatus.Text = "Claim Cash Out no." + sCode + " has been REJECTED!";
                        }
                    }
                    return_page = "~/Adminbranch/fm_AccCashAdvanceApproval_List.aspx";
                    lblReturnPage.Text = return_page;

                    break;
            }
        }
    }
    protected void Timer1_Tick(object sender, EventArgs e)
    {
        // Label1.Text = DateTime.Now.Second.ToString();
        if (lblReturnPage.Text != null)
        {
            Response.Redirect(lblReturnPage.Text.ToString());
        }
        else
        {
            Timer1.Enabled = false;
        }
    }
}