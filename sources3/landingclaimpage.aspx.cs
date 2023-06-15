using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class landingclaimpage : System.Web.UI.Page
{
    cbll bll = new cbll();
    private void vSendEmail(string sClaimCode, string sToken, string sReceived, string sCCReceived, string sCCNRNO)
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
        string sBody = "Dear Cliam Department, \n\r New Claim Needs Check" + sClaimCode +
                  "<table style='width:100%'><tr style='background-color:silver'><td class='auto-style1'>Salespoint CD.</td><td>&nbsp;</td><td>" + Request.Cookies["sp"].Value.ToString() + "</td><td>Salespoint NM.</td><td>&nbsp;</td><td>" + bll.vLookUp("select salespoint_nm from tmst_salespoint where salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'") + "</td></tr><tr style='background-color:silver'><td class='auto-style1'>Claim No</td><td>:</td><td>" + sClaimCode + "</td><td>CCNR NO</td><td>:</td><td>" + sCCNRNO + "</td></tr>" +
                  "<tr><td class='auto-style1'>Proposal</td><td>:</td><td>" + bll.vLookUp("select prop_no from tmst_claim where claim_no='" + sClaimCode + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'") + "</td><td>Created Date</td><td>:</td><td>" + bll.vLookUp("select claim_dt from tmst_claim where claim_no='" + sClaimCode + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'") + "</td></tr>" +
                  "<tr style='background-color:silver'><td class='auto-style1'>Total Item Claim</td><td>:</td><td>" + bll.vLookUp("select orderqty from tmst_claim where claim_no='" + sClaimCode + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'") + "</td><td>Total Free Item</td><td>:</td><td>" + bll.vLookUp("select freeqty from tmst_claim where claim_no='" + sClaimCode + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'") + "</td></tr>" +
                  "<tr><td class='auto-style1'>Total Quantity</td><td>:</td><td>" + bll.vLookUp("select ordervalue from tmst_claim where claim_no='" + sClaimCode + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'") + "</td><td>Total Discount</td><td>:</td><td>" + bll.vLookUp("select freevalue from tmst_claim where claim_no='" + sClaimCode + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'") + "</td></tr>" +
                  "</table>";      
        bll.vSendMail(sCCReceived, "cc:Claim Request Needs Approval", sBody);
        bll.vSendMail(sReceived, "Claim Needs Approval", sBody);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        System.Data.SqlClient.SqlDataReader rs = null;
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
            string sAppCode = Request.QueryString["appcode"];
            string sStatus = Request.QueryString["sta"];
            string strnname = Request.QueryString["trnname"];
            string ssalespointcd = Request.QueryString["salespointcd"];
            string sToken = string.Empty;
            string sDocNo = string.Empty;
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@token", sAppCode));
            bll.vGetEmailSent(arr, ref rs);
            while (rs.Read())
            {
                sToken = rs["token"].ToString();
                sDocNo = rs["doc_no"].ToString();
            } rs.Close();

            string sCheckStatus = "";
            if (strnname == "claim")
            { sCheckStatus = bll.vLookUp("select claim_sta_id from tmst_claim where claim_no=(select claim_no from tmst_ccnr where ccnr_no='" + sDocNo + "' and salespointcd='" + ssalespointcd + "')"); }

            if (sCheckStatus.Equals("A") || (sCheckStatus.Equals("C")))
            {
                lbstatus.Text = "Ooops there are already proceed in approval process, please contact Wazaran Admin !";
                return;
            }

            if (!sToken.Equals(string.Empty))
            {
                arr.Clear();
                if (strnname == "claim")
                {
                    //if (sStatus == "W")
                    //{
                    //    lbstatus.Text = "Ooops there are already proceed in approval process, please contact Wazaran Admin !";
                    //    return;
                    //}
                    if (sStatus == "N")
                    {
                        arr.Add(new cArrayList("@claim_no", bll.vLookUp("select claim_no from tmst_ccnr where ccnr_no='" + sDocNo + "' and salespointcd='" + ssalespointcd + "'")));
                        arr.Add(new cArrayList("@salespointcd", ssalespointcd));
                        arr.Add(new cArrayList("@app_sta_id", "CH"));
                        arr.Add(new cArrayList("@level", 1));
                        arr.Add(new cArrayList("@user_id", bll.vLookUp("select emp_cd from tapprovalpattern where doc_typ='branchspv'")));
                        bll.vApproveClaim(arr);
                        sCheckStatus = bll.vLookUp("select claim_sta_id from tmst_claim where claim_no=(select claim_no from tmst_ccnr where ccnr_no='" + sDocNo + "' and salespointcd='" + ssalespointcd + "')");
                        sStatus = "N";
                        //send 2nd app email 2 mousa
                        List<string> lapproval = bll.lGetApproval("branchspv", 1);
                        Random nRdm = new Random();
                        int token = nRdm.Next(1000, 9999);
                        double tokenmail = nRdm.Next();
                        Uri urlnya = Request.Url;
                        string host = urlnya.GetLeftPart(UriPartial.Authority);
                        if (sCheckStatus == "CH")
                        {
                            //string sBody = "Dear Claim section, \n\r Need Approval for Claim " + hdcust.Value.ToString() +
                            //    "<table><tr><td>Code Customer</td><td>:</td><td>" + hdcust.Value.ToString() + "</td><td>Salesman Code</td><td>:</td><td>" + cbsalesman.SelectedValue.ToString() + "</td></tr>" +
                            //    "<tr><td>Customer Name</td><td>:</td><td>" + txcustomer.Text + "</td><td>Salesman Name</td><td>:</td><td>" + cbsalesman.SelectedItem.Text + "</td></tr>" +
                            //    "<tr><td>Type Customer</td><td>:</td><td>" + lbcusttype.Text + "</td><td>Customer Balance</td><td>:</td><td>" + txclremain.Text + "</td></tr></table>";  
                            //bll.vSendMail(lapproval[1], "Salesorder Request", sBody);
                            List<string> lccedp = bll.lGetApproval("edp", 1);
                            // vSendEmail(hdcust.Value.ToString(), tokenmail.ToString(), lapproval[1]);
                            vSendEmail(bll.vLookUp("select claim_no from tmst_ccnr where ccnr_no='" + sDocNo + "' and salespointcd='" + ssalespointcd + "'"), tokenmail.ToString(), lapproval[1], lccedp[1], sDocNo);
                            arr.Clear();
                            arr.Add(new cArrayList("@trxcd", "Claim second approval"));
                            arr.Add(new cArrayList("@token", tokenmail.ToString()));
                            arr.Add(new cArrayList("@doc_no", bll.vLookUp("select claim_no from tmst_ccnr where ccnr_no='" + sDocNo + "' and salespointcd='" + ssalespointcd + "'")));
                            bll.vInsertEmailSent(arr);
                        }


                    }
                    else if (sStatus == "W")
                    {
                        arr.Add(new cArrayList("@claim_no", sDocNo));
                        arr.Add(new cArrayList("@salespointcd", ssalespointcd));
                        arr.Add(new cArrayList("@app_sta_id", "A"));                        
                        arr.Add(new cArrayList("@level", 2));
                        arr.Add(new cArrayList("@user_id", bll.vLookUp("select emp_cd from tapprovalpattern where doc_typ='edp'")));
                        bll.vApproveClaim(arr);
                    }
                }

                if (sStatus == "A" || sStatus == "W")
                {
                    lbstatus.Text = "Transaction No. " + sDocNo + " HAS BEEN APPROVED ! \r\n Now";
                }
                else { lbstatus.Text = "Transaction No. " + sDocNo + " HAS BEEN REJECTED ! \r\n Now"; }
            }
            else
            { lbstatus.Text = "Ooops there are already proceed in approval, please contact Wazaran Admin !"; }
        }
    }
}