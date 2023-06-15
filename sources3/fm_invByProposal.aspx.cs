using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Data.SqlClient;
using System.IO;

public partial class fm_invByProposal : System.Web.UI.Page
{
    cbll bll = new cbll();
    double dSubTotal = 0;
    double dSubTotalCO = 0;
    double dSubTotalCOItem = 0;
    double dSubTotalSO = 0;

    double dSubTotalQtyCO = 0;
    double dSubTotalQtySO = 0;

    double totalQtyOrderSO = 0;
    double totalQtyOrderCO = 0;

    double totalFreeQtyBA = 0;
    double totalAmountBA = 0;

    string discountMec = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        string ClaimStatus;

        if (!IsPostBack)
        {
            //txtProposal.Text = Request.QueryString["prop_no"];
            //lhDiscCd.Value = Request.QueryString["disc_cd"];
            txtClaimNo.Text = Request.QueryString["number"];
            bll.vBindingComboToSp(ref ddYear, "sp_tmst_period_getbyyear", "yearvalue", "yearvalue");

            List<cArrayList> arr = new List<cArrayList>();

            arr.Add(new cArrayList("@claim_no", Request.QueryString["number"]));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grdremark, "sp_tclaimremark_get", arr);
            SqlDataReader rs = null;
            bll.vGetClaimDetail(arr, ref rs);
            while (rs.Read())
            {
                lblClaimStatus.Text = rs["claim_sta_id"].ToString();
                DateTime claimdt = Convert.ToDateTime(rs["claim_dt"].ToString());
                txtDate.Text = string.Format("{0:dd/MM/yyyy}", claimdt);
                txtCCNR.Text = rs["ccnr_no"].ToString();
                ddMonth.SelectedValue = rs["tx_month"].ToString();
                ddYear.SelectedValue = rs["tx_year"].ToString();
                lblRemark.Text = rs["remark"].ToString();
                lblTQtyOrder.Text = rs["orderQty"].ToString();
                discountMec = rs["discount_mec"].ToString();
                txremark.Text = rs["remarka"].ToString();
                txtProposal.Text = rs["prop_no"].ToString();
                lhDiscCd.Value = rs["disc_cd"].ToString();
            }
            rs.Close();
            btrefresh2();
            ClaimStatus = lblClaimStatus.Text;
            hstatus.Value = ClaimStatus;
            //lblClaimStatus.Text = "";
            lblClaimStatus.Text = bll.vLookUp("select fld_desc from tfield_value where fld_nm='claim_sta_id' and fld_valu='" + ClaimStatus + "'");
            arr.Clear();
            string disc_typ = "";
            if (lhDiscCd.Value == "")
            {
                disc_typ = bll.vLookUp("select promotype from tmst_proposal where prop_no='" + txtProposal.Text + "' ");
            }
            else
            {
                disc_typ = bll.vLookUp("select disc_typ from tmst_discount where disc_cd=(select disc_cd from tmst_claim where claim_no='" + txtClaimNo.Text + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "')");
            }
            arr.Add(new cArrayList("@promo_cd", disc_typ));
            arr.Add(new cArrayList("@dic", null));
            bll.vBindingGridToSp(ref grdcate, "sp_tpromotion_doc_get", arr);
            //if (Request.Cookies["sp"].Value.ToString() != "0")
            //{
            //    //btApprove.Visible = false;
            //    btnremarkreturn.Visible = false;
            //}
            arr.Clear();
            arr.Add(new cArrayList("@issue_group", null));
            bll.vBindingComboToSp(ref cbissuegrp, "sp_tmst_issue_get", "issue_group", "issue_group", arr);
            cbissuegrp_SelectedIndexChanged(sender, e);

            if (ClaimStatus == "A") { btPaid.Visible = true; } else { btPaid.Visible = false; }

        }
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {

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
        //     sLastDate = rs["trn_typ"].ToString() + "," + rs["trn_dt"].ToString();
        // } rs.Close();
        string sBody = "Dear Cliam Department, \n\r New Claim Needs Check" + sClaimCode +
                  "<table style='width:100%'><tr style='background-color:silver'><td class='auto-style1'>Salespoint CD.</td><td>&nbsp;</td><td>" + Request.Cookies["sp"].Value.ToString() + "</td><td>Salespoint NM.</td><td>&nbsp;</td><td>" + bll.vLookUp("select salespoint_nm from tmst_salespoint where salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'") + "</td></tr><tr style='background-color:silver'><td class='auto-style1'>Claim No</td><td>:</td><td>" + sClaimCode + "</td><td>CCNR NO</td><td>:</td><td>" + sCCNRNO + "</td></tr>" +
                  "<tr><td class='auto-style1'>Proposal</td><td>:</td><td>" + bll.vLookUp("select prop_no from tmst_claim where claim_no='" + sClaimCode + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'") + "</td><td>Created Date</td><td>:</td><td>" + bll.vLookUp("select claim_dt from tmst_claim where claim_no='" + sClaimCode + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'") + "</td></tr>" +
                  "<tr style='background-color:silver'><td class='auto-style1'>Total Item Claim</td><td>:</td><td>" + bll.vLookUp("select orderqty from tmst_claim where claim_no='" + sClaimCode + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'") + "</td><td>Total Free Item</td><td>:</td><td>" + bll.vLookUp("select freeqty from tmst_claim where claim_no='" + sClaimCode + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'") + "</td></tr>" +
                  "<tr><td class='auto-style1'>Total Quantity</td><td>:</td><td>" + bll.vLookUp("select ordervalue from tmst_claim where claim_no='" + sClaimCode + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'") + "</td><td>Total Discount</td><td>:</td><td>" + bll.vLookUp("select freevalue from tmst_claim where claim_no='" + sClaimCode + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'") + "</td></tr>" +
                  "</table>";
        //bll.vSendMail(sCCReceived, "cc:Claim Request Needs Approval", sBody);
        sBody += "Please click <a href='" + sHttp + "'>Login to Approve it</a>!";
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
        sBody += "Please click <a href='" + sHttp + "'>Login to Approve it</a>!";
        // bll.vSendMail(sCCReceived, "cc:Claim Request Needs Approval", sBody);
        bll.vSendMail(sReceived, "Claim Needs Check", sBody);
    }

    private void vSendEmail2(string sClaimCode, string sToken, string sReceived, string sCCNRNO)
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
        string sBody = "Dear Branch users, \n\r Claim has been returned" + sClaimCode +
                  "<table style='width:100%'><tr style='background-color:silver'><td class='auto-style1'>Salespoint CD.</td><td>&nbsp;</td><td>" + Request.Cookies["sp"].Value.ToString() + "</td><td>Salespoint NM.</td><td>&nbsp;</td><td>" + bll.vLookUp("select salespoint_nm from tmst_salespoint where salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'") + "</td></tr><tr style='background-color:silver'><td class='auto-style1'>Claim No</td><td>:</td><td>" + sClaimCode + "</td><td>CCNR NO</td><td>:</td><td>" + sCCNRNO + "</td></tr>" +
                  "<tr><td class='auto-style1'>Proposal</td><td>:</td><td>" + bll.vLookUp("select prop_no from tmst_claim where claim_no='" + sClaimCode + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'") + "</td><td>Created Date</td><td>:</td><td>" + bll.vLookUp("select claim_dt from tmst_claim where claim_no='" + sClaimCode + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'") + "</td></tr>" +
                  "<tr style='background-color:silver'><td class='auto-style1'>Total Item Claim</td><td>:</td><td>" + bll.vLookUp("select orderqty from tmst_claim where claim_no='" + sClaimCode + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'") + "</td><td>Total Free Item</td><td>:</td><td>" + bll.vLookUp("select freeqty from tmst_claim where claim_no='" + sClaimCode + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'") + "</td></tr>" +
                  "<tr><td class='auto-style1'>Total Quantity</td><td>:</td><td>" + bll.vLookUp("select ordervalue from tmst_claim where claim_no='" + sClaimCode + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'") + "</td><td>Total Discount</td><td>:</td><td>" + bll.vLookUp("select freevalue from tmst_claim where claim_no='" + sClaimCode + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'") + "</td></tr>" +
                  "</table>";
        sBody += "Please click <a href='" + sHttp + "'>Login to check it</a>!";
        // bll.vSendMail(sCCReceived, "cc:Claim Request Needs Approval", sBody);
        bll.vSendMail(sReceived, "Claim Returned Missing Data", sBody);
    }
    private void vSendEmail3(string sClaimCode, string sToken, string sReceived, string sCCNRNO)
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
        string sBody = "Dear HO CLAIM Department, \n\r Claim has been Checked" + sClaimCode +
                  "<table style='width:100%'><tr style='background-color:silver'><td class='auto-style1'>Salespoint CD.</td><td>&nbsp;</td><td>" + Request.Cookies["sp"].Value.ToString() + "</td><td>Salespoint NM.</td><td>&nbsp;</td><td>" + bll.vLookUp("select salespoint_nm from tmst_salespoint where salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'") + "</td></tr><tr style='background-color:silver'><td class='auto-style1'>Claim No</td><td>:</td><td>" + sClaimCode + "</td><td>CCNR NO</td><td>:</td><td>" + sCCNRNO + "</td></tr>" +
                  "<tr><td class='auto-style1'>Proposal</td><td>:</td><td>" + bll.vLookUp("select prop_no from tmst_claim where claim_no='" + sClaimCode + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'") + "</td><td>Created Date</td><td>:</td><td>" + bll.vLookUp("select claim_dt from tmst_claim where claim_no='" + sClaimCode + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'") + "</td></tr>" +
                  "<tr style='background-color:silver'><td class='auto-style1'>Total Item Claim</td><td>:</td><td>" + bll.vLookUp("select orderqty from tmst_claim where claim_no='" + sClaimCode + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'") + "</td><td>Total Free Item</td><td>:</td><td>" + bll.vLookUp("select freeqty from tmst_claim where claim_no='" + sClaimCode + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'") + "</td></tr>" +
                  "<tr><td class='auto-style1'>Total Quantity</td><td>:</td><td>" + bll.vLookUp("select ordervalue from tmst_claim where claim_no='" + sClaimCode + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'") + "</td><td>Total Discount</td><td>:</td><td>" + bll.vLookUp("select freevalue from tmst_claim where claim_no='" + sClaimCode + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'") + "</td></tr>" +
                  "</table>";
        sBody += "Please click <a href='" + sHttp + "'>Login to check it</a>!";
        // bll.vSendMail(sCCReceived, "cc:Claim Request Needs Approval", sBody);
        bll.vSendMail(sReceived, "Claim Returned Missing Data", sBody);
    }
    protected void btApprove_Click(object sender, EventArgs e)
    {
        //string approve_by = Request.Cookies["usr_id"].Value.ToString();
        //if (lblClaimStatus.Text=="PENDING")
        //{           
        //    List<cArrayList> arr = new List<cArrayList>();
        //    arr.Add(new cArrayList("@claim_no", txtClaimNo.Text));
        //    arr.Add(new cArrayList("@claim_sta_id", "W"));
        //    arr.Add(new cArrayList("@approve_by", Request.Cookies["usr_id"].Value.ToString()));
        //    arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        //    bll.vUpdateClaim(arr);
        //    //bll.vLookUp("UPDATE tmst_claim set status='A', approve_by=" + approve_by + " WHERE prop_no='" + txtProposal.Text + "' and disc_cd='" + lhDiscCd.Value + "'");
        //    Response.Redirect("fm_claimList.aspx", true);

        //}
        //else if (lblClaimStatus.Text == "WAITING APPROVAL")
        //{            
        //    List<cArrayList> arr = new List<cArrayList>();
        //    arr.Add(new cArrayList("@claim_no", txtClaimNo.Text));
        //    arr.Add(new cArrayList("@claim_sta_id", "A"));
        //    arr.Add(new cArrayList("@approve_by", Request.Cookies["usr_id"].Value.ToString()));
        //    arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        //    bll.vUpdateClaim(arr);
        //    //bll.vLookUp("UPDATE tmst_claim set status='A', approve_by=" + approve_by + " WHERE prop_no='" + txtProposal.Text + "' and disc_cd='" + lhDiscCd.Value + "'");
        //    Response.Redirect("fm_claimList.aspx", true);

        //}

        //if (ddMonth.SelectedValue.ToString() == "07")
        //{

        //arrCcnr.Clear();
        //arrCcnr.Add(new cArrayList("@ccnr_no", bll.vLookUp("select ccnr_no from tmst_ccnr where claim_no='" + txtClaimNo.Text + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'")));
        //arrCcnr.Add(new cArrayList("@salespoint_cd", Request.Cookies["sp"].Value.ToString()));
        //arrCcnr.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
        //rep.vShowReportToPDF("rp_ccnr.rpt", arrCcnr, sPath + sPdfName3);   
        //} 



        string ddStatus = bll.vLookUp("select claim_sta_id from tmst_claim where claim_no='" + txtClaimNo.Text + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
        DateTime dtwaz_dt = DateTime.ParseExact(Request.Cookies["waz_dt"].Value.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        if (ddStatus == "N")
        {
            if (bll.nCheckAccess("ccnrapp", Request.Cookies["usr_id"].Value.ToString()) == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','To approve this claim contact Administrator !!','warning');", true);
                return;
            }

            string stCcnr = bll.vLookUp("select isnull(ccnr_no,'0') from tmst_claim where claim_no='" + txtClaimNo.Text + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");

            if (stCcnr == "0")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('This CCNR not exist.','To approve this claim Insert CCNR No. !!','warning');", true);
                return;
            }

            //string sCCNRNO = "";

            //if (txtCCNR.Text.ToString() == "")
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('CCNR Manual Number must Inserted!','Please Insert CCNR.','error');", true);
            //    return;
            //}

            //string ccman = bll.vLookUp("select salespoint_sn from tmst_salespoint where salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
            //ccman = ccman + txtCCNR.Text;

            //if (ccman == bll.vLookUp("select manual_no from tmst_ccnr where manual_no='" + ccman + "'"))
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('CCNR Manual no already in the system','Please Enter correct CCNR No or contact WAZARAN ADMIN','warning');", true);
            //    return;
            //}

            //List<cArrayList> arrCcnr = new List<cArrayList>();
            //arrCcnr.Clear();
            //string sbrn = bll.vLookUp("select salespoint_sn from tmst_salespoint where salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'") + txtCCNR.Text;

            //arrCcnr.Add(new cArrayList("@ccnr_dt", DateTime.ParseExact(txtDate.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            //arrCcnr.Add(new cArrayList("@manual_no", sbrn));
            //arrCcnr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            //arrCcnr.Add(new cArrayList("@claim_no", txtClaimNo.Text));
            //arrCcnr.Add(new cArrayList("@app_by", null));
            //bll.vInsertCcnr(arrCcnr, ref sCCNRNO);

            //arrCcnr.Clear();
            //arrCcnr.Add(new cArrayList("@claim_no", txtClaimNo.Text));
            //arrCcnr.Add(new cArrayList("@manual_NO", txtCCNR.Text));
            //arrCcnr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            //bll.vUpdateccnno(arrCcnr);

            //string sPdfName3 = Request.Cookies["sp"].Value.ToString() + "-" + sCCNRNO + "-" + "P35" + ".pdf";
            //string sPath = bll.sGetControlParameter("image_path") + @"\claim_doc\";
            //creport rep = new creport();

            //arrCcnr.Clear();//ccnrdoc
            //arrCcnr.Add(new cArrayList("@claim_no", sCCNRNO));
            //arrCcnr.Add(new cArrayList("@doc_cd", "P33"));
            //arrCcnr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            //arrCcnr.Add(new cArrayList("@fileloc", Request.Cookies["sp"].Value.ToString() + "-" + sCCNRNO + "-" + "P35" + ".pdf"));
            //arrCcnr.Add(new cArrayList("@doc_nm", " CCNR "));
            //bll.vInsertTclaimDoc(arrCcnr);


            //string approve_by = Request.Cookies["usr_id"].Value.ToString();
            //string claimNo = row.Cells[1].FindControl("claimNo").ToString();            
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@claim_no", txtClaimNo.Text));
            arr.Add(new cArrayList("@claim_sta_id", "CH"));
            arr.Add(new cArrayList("@approve_by", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));

            arr.Add(new cArrayList("@app_dt", dtwaz_dt));
            bll.vUpdateClaim(arr);
            arr.Clear();
            arr.Add(new cArrayList("@claim_no", txtClaimNo.Text));
            arr.Add(new cArrayList("@remark", txremark.Text));
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@remark_id", ""));
            arr.Add(new cArrayList("@claim_sta_id", hstatus.Value));
            arr.Add(new cArrayList("@issue_no", cbissuedesc.SelectedValue.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vInsertClaimrRemark(arr);
            arr.Clear();

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
            //List<string> lccedp = bll.lGetApproval("edp", 1);
            //vSendEmail(hdcust.Value.ToString(), tokenmail.ToString(), lapproval[1]);
            //vSendEmail(txtClaimNo.Text, tokenmail.ToString(), lapproval[1], bll.vLookUp("select manual_no from tmst_ccnr where claim_no='" + txtClaimNo.Text + "'"));
            arr.Clear();
            arr.Add(new cArrayList("@trxcd", "Claim second approval"));
            arr.Add(new cArrayList("@token", tokenmail.ToString()));
            arr.Add(new cArrayList("@doc_no", txtClaimNo.Text));
            bll.vInsertEmailSent(arr);
            Response.Redirect("fm_claimList.aspx", true);
        }
        else if (ddStatus == "R")
        {
            if (bll.nCheckAccess("claimapp", Request.Cookies["usr_id"].Value.ToString()) == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','To approve this claim contact Administrator !!','warning');", true);
                return;
            }
           
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@claim_no", txtClaimNo.Text));
            arr.Add(new cArrayList("@claim_sta_id", "CH"));
            arr.Add(new cArrayList("@approve_by", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));

            arr.Add(new cArrayList("@app_dt", dtwaz_dt));
            bll.vUpdateClaim(arr);
            arr.Clear();
            arr.Add(new cArrayList("@claim_no", txtClaimNo.Text));
            arr.Add(new cArrayList("@remark", txremark.Text));
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@remark_id", ""));
            arr.Add(new cArrayList("@claim_sta_id", hstatus.Value));
            arr.Add(new cArrayList("@issue_no", cbissuedesc.SelectedValue.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vInsertClaimrRemark(arr);
            arr.Clear();

            List<string> lapproval = bll.lGetApproval("claimch", 1);
            Random nRdm = new Random();
            int token = nRdm.Next(1000, 9999);
            double tokenmail = nRdm.Next();
            Uri urlnya = Request.Url;
            string host = urlnya.GetLeftPart(UriPartial.Authority);

            vSendEmail(txtClaimNo.Text, tokenmail.ToString(), lapproval[1], bll.vLookUp("select manual_no from tmst_ccnr where claim_no='" + txtClaimNo.Text + "'"));

            arr.Clear();
            arr.Add(new cArrayList("@trxcd", "Claim second approval"));
            arr.Add(new cArrayList("@token", tokenmail.ToString()));
            arr.Add(new cArrayList("@doc_no", txtClaimNo.Text));
            bll.vInsertEmailSent(arr);
            Response.Redirect("fm_claimList.aspx", true);
        }
        else if (ddStatus == "CH")
        {
            if (bll.nCheckAccess("claimch", Request.Cookies["usr_id"].Value.ToString()) == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','To approve this claim contact Administrator !!','warning');", true);
                return;
            }
            //string claimNo = row.Cells[1].FindControl("claimNo").ToString();           
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@claim_no", txtClaimNo.Text));
            arr.Add(new cArrayList("@claim_sta_id", "W"));
            arr.Add(new cArrayList("@approve_by", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            arr.Add(new cArrayList("@app_dt", dtwaz_dt));
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
            List<string> lccedp = bll.lGetApproval("edp", 1);
            // vSendEmail(hdcust.Value.ToString(), tokenmail.ToString(), lapproval[1]);
            vSendEmail(txtClaimNo.Text, tokenmail.ToString(), lapproval[1], bll.vLookUp("select manual_no from tmst_ccnr where claim_no='" + txtClaimNo.Text + "'"));
            arr.Clear();
            arr.Add(new cArrayList("@trxcd", "Claim second approval"));
            arr.Add(new cArrayList("@token", tokenmail.ToString()));
            arr.Add(new cArrayList("@doc_no", txtClaimNo.Text));
            bll.vInsertEmailSent(arr);
            arr.Clear();
            arr.Add(new cArrayList("@claim_no", txtClaimNo.Text));
            arr.Add(new cArrayList("@remark", txremark.Text));
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@remark_id", ""));
            arr.Add(new cArrayList("@claim_sta_id", hstatus.Value));
            arr.Add(new cArrayList("@issue_no", cbissuedesc.SelectedValue.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vInsertClaimrRemark(arr);
            Response.Redirect("fm_claimList.aspx", true);
        }
        else if (ddStatus == "W")
        {
            if (bll.nCheckAccess("claimapp", Request.Cookies["usr_id"].Value.ToString()) == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','To approve this claim contact Administrator !!','warning');", true);
                return;
            }
            string approve_by = Request.Cookies["usr_id"].Value.ToString();
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@claim_no", txtClaimNo.Text));
            arr.Add(new cArrayList("@claim_sta_id", "A"));
            arr.Add(new cArrayList("@approve_by", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            arr.Add(new cArrayList("@app_dt", dtwaz_dt));
            bll.vUpdateClaim(arr);

            List<string> lapproval = bll.lGetApproval("edp", 1);
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
            vSendEmail1(txtClaimNo.Text, tokenmail.ToString(), lapproval[1], bll.vLookUp("select manual_no from tmst_ccnr where claim_no='" + txtClaimNo.Text + "'"));
            arr.Clear();
            arr.Add(new cArrayList("@trxcd", "Claim second approval"));
            arr.Add(new cArrayList("@token", tokenmail.ToString()));
            arr.Add(new cArrayList("@doc_no", txtClaimNo.Text));
            bll.vInsertEmailSent(arr);
            arr.Clear();
            arr.Add(new cArrayList("@claim_no", txtClaimNo.Text));
            arr.Add(new cArrayList("@remark", txremark.Text));
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@remark_id", ""));
            arr.Add(new cArrayList("@claim_sta_id", hstatus.Value));
            arr.Add(new cArrayList("@issue_no", cbissuedesc.SelectedValue.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vInsertClaimrRemark(arr);
            Response.Redirect("fm_claimList.aspx", true);
        }
    }

    protected void btsearchso_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "window.open('lookup_proposal.aspx','mywindow','toolbar=n,scrollbars=y,width=800,height=800,top=75,left=300',true);", true);
    }

    protected void btInvDetails_Click(object sender, EventArgs e)
    {

    }

    double totalFreeItem = 0;
    double totalFreeCash = 0;
    void btrefresh2()
    {

        List<cArrayList> arr = new List<cArrayList>();
        //txtProposal.ReadOnly = false;
        arr.Add(new cArrayList("@disc_cd", lhDiscCd.Value));
        arr.Add(new cArrayList("@month", ddMonth.SelectedValue.ToString()));
        arr.Add(new cArrayList("@year", ddYear.SelectedValue.ToString()));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));


        lblTFreeItem.Text = "";
        lblTFreeCash.Text = "";
        arr.Clear();
        arr.Add(new cArrayList("@claim_no", Request.QueryString["number"]));
        bll.vBindingGridToSp(ref gridClaimSO, "sp_tclaimdtl_getbysocash", arr);
        bll.vBindingGridToSp(ref gridClaimSOItem, "sp_tclaimdtl_getbysofree", arr);
        bll.vBindingGridToSp(ref gridClaimCO, "sp_tclaimdtl_getbycncash", arr);
        bll.vBindingGridToSp(ref gridClaimCOItem, "sp_tclaimdtl_getbycnfree", arr);
        bll.vBindingGridToSp(ref grdcst, "sp_tclaimdtl_getbycashoutcust", arr);
        bll.vBindingGridToSp(ref grdgr, "sp_tclaimdtl_getbycashoutcusgrcd", arr);
        bll.vBindingGridToSp(ref grdcndn, "sp_tclaimdtl_getbycndn", arr);
        bll.vBindingGridToSp(ref grdBA, "sp_tclaimdtl_getbycontract", arr);
        bll.vBindingGridToSp(ref grdadjso, "sp_tclaimdtl_getbysocashadj", arr);
        bll.vBindingGridToSp(ref grdadjco, "sp_tclaimdtl_getbycncashadj", arr);
        grdBA.Visible = false;
        arr.Clear();
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        arr.Add(new cArrayList("@claim_no", txtClaimNo.Text));
        bll.vBindingGridToSp(ref grddoc, "sp_getClaimDoc", arr);

        totalFreeItem = dSubTotal + dSubTotalCOItem;
        totalFreeCash = dSubTotalSO + dSubTotalCO;

        lblTFreeItem.Text = totalFreeItem.ToString("N2");
        lblTFreeCash.Text = totalFreeCash.ToString("N2");

        if (lblTFreeItem.Text != "0.00") lblTFreeCash.Visible = true;
        if (lblTFreeCash.Text != "0.00") lblTFreeItem.Visible = true;


        if (discountMec == "CH")
        {
            lbclaimtyp.Text = "QTY ORDER";
            Label1.Text = "Take Order Discount Cash";
            Label3.Text = "Canvas Order Discount Cash";
            if (gridClaimSO.Rows.Count.ToString() != "0")
            {
                Label1.Visible = true;
            }
            if (gridClaimCO.Rows.Count.ToString() != "0")
            {
                Label3.Visible = true;
            }
        }
        else if (discountMec == "BA")
        {
            lbclaimtyp.Text = "Claim Amount";
            lblTFreeItem.Text = totalFreeQtyBA.ToString("N2");
            lblTQtyOrder.Text = totalAmountBA.ToString("N2");
            grdBA.Visible = true;
        }
        else
        {
            lbclaimtyp.Text = "Claim Amount";
            Label2.Text = "Take Order Free Item";
            Label4.Text = "Canvas Order Free Item";

            if (gridClaimSOItem.Rows.Count.ToString() != "0")
            {
                Label2.Visible = true;
            }
            if (gridClaimCOItem.Rows.Count.ToString() != "0")
            {
                Label4.Visible = true;
            }
        }
    }


    protected void gridClaimSOItem_RowDataBound(Object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbsubtotal = (Label)e.Row.FindControl("qty");
            dSubTotal += Convert.ToDouble(lbsubtotal.Text);

            Label lbsubtotalQty = (Label)e.Row.FindControl("subtotal");
            if (lbsubtotalQty.Text == string.Empty) lbsubtotalQty.Text = "0";
            dSubTotalQtySO += Convert.ToDouble(lbsubtotalQty.Text);
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lbtotsubtotal = (Label)e.Row.FindControl("lbtotsubtotal");
            lbtotsubtotal.Text = dSubTotal.ToString("N2");
            e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
            //            lblTFreeItem.Text = lbtotsubtotal.Text;

            Label lbtotsubtotalQtySO = (Label)e.Row.FindControl("lbtotQtySo");
            lbtotsubtotalQtySO.Text = dSubTotalQtySO.ToString("N2");
        }

    }
    double dValue = 0;
    protected void gridClaimCOItem_RowDataBound(Object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbsubtotalCO = (Label)e.Row.FindControl("qtyCO");
            dSubTotalCOItem += Convert.ToDouble(lbsubtotalCO.Text);

            Label lbsubtotalQtyCO = (Label)e.Row.FindControl("subtotal1");
            //dSubTotalQtyCO += Convert.ToDouble(lbsubtotalQtyCO.Text);

            if (double.TryParse(lbsubtotalQtyCO.Text, out dValue)) { dSubTotalQtyCO += Convert.ToDouble(lbsubtotalQtyCO.Text); }

        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lbtotsubtotalCO = (Label)e.Row.FindControl("lbtotsubtotalCO");
            lbtotsubtotalCO.Text = dSubTotalCOItem.ToString("N2");
            e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;

            Label lbtotQtyCO = (Label)e.Row.FindControl("QtyOrderCO");
            lbtotQtyCO.Text = dSubTotalQtyCO.ToString("N2");
        }

    }

    protected void gridClaimSO_RowDataBound(Object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbsubtotalSO = (Label)e.Row.FindControl("amtSO");
            dSubTotalSO += Convert.ToDouble(lbsubtotalSO.Text);

            Label lbtotQtyOrderSO = (Label)e.Row.FindControl("qtyOrdersO");
            totalQtyOrderSO += Convert.ToDouble(lbtotQtyOrderSO.Text);
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lbtotsubtotalSO = (Label)e.Row.FindControl("lbtotsubtotalSO");
            lbtotsubtotalSO.Text = dSubTotalSO.ToString("N2");
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Right;

            Label lbTotQtyOrderSO = (Label)e.Row.FindControl("lbTotQtyOrderSO");
            lbTotQtyOrderSO.Text = totalQtyOrderSO.ToString("N2");
        }
    }

    protected void gridClaimCO_RowDataBound(Object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbsubtotalCO = (Label)e.Row.FindControl("amtCO");
            dSubTotalCO += Convert.ToDouble(lbsubtotalCO.Text);

            Label lbtotQtyOrderCO = (Label)e.Row.FindControl("qtyOrderCO");
            totalQtyOrderCO += Convert.ToDouble(lbtotQtyOrderCO.Text);
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lbtotsubtotalCO = (Label)e.Row.FindControl("lbtotsubtotalCO");
            lbtotsubtotalCO.Text = dSubTotalCO.ToString("N2");
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Right;

            Label lbTotQtyOrderCO = (Label)e.Row.FindControl("lbTotQtyOrderCO");
            lbTotQtyOrderCO.Text = totalQtyOrderCO.ToString("N2");
        }
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_claimEntry.aspx", true);
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        DateTime dtwaz_dt = DateTime.ParseExact(Request.Cookies["waz_dt"].Value.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        if (txremark.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Sorry!','Please fill Remark Data.','warning');", true);
            return;
        }

        Random nRdm = new Random();
        int token = nRdm.Next(1000, 9999);
        double tokenmail = nRdm.Next();
        Uri urlnya = Request.Url;
        string host = urlnya.GetLeftPart(UriPartial.Authority);
        if (hstatus.Value == "N" || hstatus.Value == "CH" || hstatus.Value == "R" || hstatus.Value == "W")
        {
            if (bll.nCheckAccess("ccnrapp", Request.Cookies["usr_id"].Value.ToString()) != 0 && hstatus.Value == "N")
            {
                bll.vLookUp("update tmst_claim set remarka='" + txremark.Text + "' where claim_no='" + txtClaimNo.Text + "' and claim_sta_id in ('N') and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Success!','Claim Has Been Returned.','success');", true);
                List<cArrayList> arr = new List<cArrayList>();
                arr.Add(new cArrayList("@claim_no", txtClaimNo.Text));
                arr.Add(new cArrayList("@remark", txremark.Text));
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@remark_id", ""));
                arr.Add(new cArrayList("@claim_sta_id", hstatus.Value));
                arr.Add(new cArrayList("@issue_no", cbissuedesc.SelectedValue.ToString()));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vInsertClaimrRemark(arr);
                arr.Clear();
                List<string> lapproval = bll.lGetApproval("claimrt", 1);
                vSendEmail2(txtClaimNo.Text, null, lapproval[1], bll.vLookUp("select manual_no from tmst_ccnr where claim_no='" + txtClaimNo.Text + "'"));

                arr.Add(new cArrayList("@trxcd", "Claim second approval"));
                arr.Add(new cArrayList("@token", tokenmail));
                arr.Add(new cArrayList("@doc_no", txtClaimNo.Text));
                bll.vInsertEmailSent(arr);
            }

            else if (bll.nCheckAccess("claimch", Request.Cookies["usr_id"].Value.ToString()) != 0 && hstatus.Value == "CH")
            {
                bll.vLookUp("update tmst_claim set remarka='" + txremark.Text + "',claim_sta_id='R',retur_by='" + Request.Cookies["usr_id"].Value.ToString() + "',retur_dt='" + dtwaz_dt.Year + "-" + dtwaz_dt.Month + "-" + dtwaz_dt.Day + "' where claim_no='" + txtClaimNo.Text + "' and claim_sta_id in ('CH') and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Success!','Claim Has Been Returned.','success');", true);
                List<cArrayList> arr = new List<cArrayList>();
                arr.Add(new cArrayList("@claim_no", txtClaimNo.Text));
                arr.Add(new cArrayList("@remark", txremark.Text));
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@remark_id", ""));
                arr.Add(new cArrayList("@claim_sta_id", hstatus.Value));
                arr.Add(new cArrayList("@issue_no", cbissuedesc.SelectedValue.ToString()));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vInsertClaimrRemark(arr);
                arr.Clear();
                List<string> lapproval = bll.lGetApproval("claimrt", 1);
                vSendEmail2(txtClaimNo.Text, null, lapproval[1], bll.vLookUp("select manual_no from tmst_ccnr where claim_no='" + txtClaimNo.Text + "'"));
                arr.Clear();
                arr.Add(new cArrayList("@trxcd", "Claim second approval"));
                arr.Add(new cArrayList("@token", tokenmail));
                arr.Add(new cArrayList("@doc_no", txtClaimNo.Text));
                bll.vInsertEmailSent(arr);
            }
            else if (bll.nCheckAccess("claimapp", Request.Cookies["usr_id"].Value.ToString()) != 0 && hstatus.Value == "W")
            {
                bll.vLookUp("update tmst_claim set remarka='" + txremark.Text + "',claim_sta_id='R',retur_by='" + Request.Cookies["usr_id"].Value.ToString() + "',retur_dt='" + dtwaz_dt.Year + "-" + dtwaz_dt.Month + "-" + dtwaz_dt.Day + "' where claim_no='" + txtClaimNo.Text + "' and claim_sta_id in ('W') and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Success!','Claim Has Been Returned.','success');", true);
                List<cArrayList> arr = new List<cArrayList>();
                arr.Add(new cArrayList("@claim_no", txtClaimNo.Text));
                arr.Add(new cArrayList("@remark", txremark.Text));
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@remark_id", ""));
                arr.Add(new cArrayList("@claim_sta_id", hstatus.Value));
                arr.Add(new cArrayList("@issue_no", cbissuedesc.SelectedValue.ToString()));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vInsertClaimrRemark(arr);
                arr.Clear();
                List<string> lapproval = bll.lGetApproval("claimrt", 1);
                vSendEmail2(txtClaimNo.Text, null, lapproval[1], bll.vLookUp("select manual_no from tmst_ccnr where claim_no='" + txtClaimNo.Text + "'"));
                arr.Clear();
                arr.Add(new cArrayList("@trxcd", "Claim second approval"));
                arr.Add(new cArrayList("@token", tokenmail));
                arr.Add(new cArrayList("@doc_no", txtClaimNo.Text));
                bll.vInsertEmailSent(arr);
            }
            if (hstatus.Value == "R")
            {
                if (bll.nCheckAccess("claimrt", Request.Cookies["usr_id"].Value.ToString()) != 0 && hstatus.Value == "R")
                {
                    bll.vLookUp("update tmst_claim set remarka='" + txremark.Text + "' where claim_no='" + txtClaimNo.Text + "' and claim_sta_id in ('R') and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
                    bll.vLookUp("update tmst_claim set claim_sta_id='CH' where claim_no='" + txtClaimNo.Text + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Success!','Claim Has Been updated.','success');", true);
                    List<cArrayList> arr = new List<cArrayList>();
                    arr.Add(new cArrayList("@claim_no", txtClaimNo.Text));
                    arr.Add(new cArrayList("@remark", txremark.Text));
                    arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                    arr.Add(new cArrayList("@remark_id", ""));
                    arr.Add(new cArrayList("@claim_sta_id", hstatus.Value));
                    arr.Add(new cArrayList("@issue_no", cbissuedesc.SelectedValue.ToString()));
                    arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                    bll.vInsertClaimrRemark(arr);
                    arr.Clear();
                    List<string> lapproval = bll.lGetApproval("claimch", 1);
                    vSendEmail3(txtClaimNo.Text, null, lapproval[1], bll.vLookUp("select manual_no from tmst_ccnr where claim_no='" + txtClaimNo.Text + "'"));
                    arr.Clear();
                    arr.Add(new cArrayList("@trxcd", "Claim Checked"));
                    arr.Add(new cArrayList("@token", tokenmail));
                    arr.Add(new cArrayList("@doc_no", txtClaimNo.Text));
                    bll.vInsertEmailSent(arr);

                }

            }
            List<cArrayList> arr2 = new List<cArrayList>();
            arr2.Clear();
            arr2.Add(new cArrayList("@claim_no", Request.QueryString["number"]));
            arr2.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grdremark, "sp_tclaimremark_get", arr2);
        }

        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Error!','This claim can not be returned.','error');", true);
        }
    }
    protected void btPrint_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@ccnr_no", bll.vLookUp("select ccnr_no from tmst_ccnr where claim_no='" + txtClaimNo.Text + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'")));
        arr.Add(new cArrayList("@salespoint_cd", Request.Cookies["sp"].Value.ToString()));
        arr.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
        Session["lparamccnr"] = arr;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opf", "openreport('fm_report2.aspx?src=ccnr');", true);
    }
    protected void ddMonth_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddYear_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btupload_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        foreach (GridViewRow row in grdcate.Rows)
        {
            Label lbdoccode = (Label)row.FindControl("lbdoccode");
            Label lbdocname = (Label)row.FindControl("lbdocname");
            FileUpload upl = (FileUpload)row.FindControl("upl");
            if (upl.HasFile)
            {
                FileInfo fi = new FileInfo(upl.FileName);
                string ext = fi.Extension;
                byte[] fs = upl.FileBytes;
                if (fs.Length <= 6000000)
                {
                    // if (ext.ToLower() == ".jpg" || ext.ToLower() == ".jpeg" || ext.ToLower() == ".gif" || ext.ToLower() == ".png" || ext.ToLower() == ".zip")
                    // {

                    if ((upl.FileName != "") || (upl.FileName != null))
                    {
                        arr.Clear();
                        arr.Add(new cArrayList("@claim_no", txtClaimNo.Text));
                        arr.Add(new cArrayList("@doc_cd", lbdoccode.Text));
                        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                        //arr.Add(new cArrayList("@filename", host + "/images/" + Request.Cookies["sp"].Value.ToString() + "-" + upl.FileName.ToString()));
                        //arr.Add(new cArrayList("@filename", Request.Cookies["sp"].Value.ToString() + "-" + txtClaimNo.Text + "-" + upl.FileName.ToString()));
                        arr.Add(new cArrayList("@fileloc", Request.Cookies["sp"].Value.ToString() + "-" + txtClaimNo.Text + "-" + lbdoccode.Text + ext));
                        arr.Add(new cArrayList("@doc_nm", lbdocname.Text));
                        upl.SaveAs(bll.sGetControlParameter("image_path") + "/claim_doc/" + Request.Cookies["sp"].Value.ToString() + "-" + txtClaimNo.Text + "-" + lbdoccode.Text + ext);
                        // statusUpload = "1";
                        bll.vInsertTclaimDoc(arr);
                    }
                    else
                    {
                        // statusUpload = "0";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('Please attach all the documents required','Proposal, Summary claim and Invoice (zip files)');", true);
                        return;
                    }

                    //}
                    // else
                    // {
                    //     ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('You should upload image or zip only','jpg,gif,png and zip upload document');", true);
                    //     return;
                    //  }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('Image size too big','Image size must be less than 500MB');", true);
                    return;
                }
            }
        }
        arr.Clear();
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        arr.Add(new cArrayList("@claim_no", txtClaimNo.Text));
        bll.vBindingGridToSp(ref grddoc, "sp_getClaimDoc", arr);
    }
    protected void grdcate_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string docStatus = "";
            string docSystem = "";
            string[] promotype = txtProposal.Text.Split('/');


            Label lbdocname1 = (Label)e.Row.FindControl("lbdocname");
            Label lbdoccode = (Label)e.Row.FindControl("lbdoccode");
            FileUpload upl1 = (FileUpload)e.Row.FindControl("upl");
            docStatus = bll.vLookUp("select doc_status from tpromotion_doc where doc_cd='" + lbdoccode.Text + "' and promo_typ='" + promotype[1].ToString() + "'");
            docSystem = bll.vLookUp("select doc_system from tpromotion_doc where doc_cd='" + lbdoccode.Text + "' and promo_typ='" + promotype[1].ToString() + "'");
            if (docStatus == "Y")
            {
                if (docSystem == "N")
                {
                    upl1.CssClass = "makeitreadwrite";
                }
                else
                {
                    upl1.BackColor = System.Drawing.Color.Gray;
                }
            }
            else
            {
                upl1.BackColor = System.Drawing.Color.Gray;
            }
        }
    }
    protected void cbissuegrp_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@issue_group", cbissuegrp.SelectedValue.ToString()));
        bll.vBindingComboToSp(ref cbissuedesc, "sp_tmst_issue_get", "issue_no", "issue_nm", arr);
    }
    protected void txupdate_Click(object sender, EventArgs e)
    {
        if (hstatus.Value == "R")
        {
            if (bll.nCheckAccess("claimrt", Request.Cookies["usr_id"].Value.ToString()) != 0 && hstatus.Value == "R")
            {
                if (txtCCNR.CssClass == "makeitreadonly")
                {
                    txtCCNR.CssClass = "makeitreadwrite";
                    txtCCNR.ReadOnly = false;

                }
                else
                {
                    if (txtCCNR.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Please insert CCNR manual No','Please fill CCNR Data.','warning');", true);
                        return;
                    }
                    List<cArrayList> arr = new List<cArrayList>();
                    arr.Add(new cArrayList("@claim_no", txtClaimNo.Text));
                    arr.Add(new cArrayList("@manual_NO", txtCCNR.Text));
                    arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                    bll.vUpdateccnno(arr);
                    //string sCCNRNO = "";

                    //string ccman = bll.vLookUp("select salespoint_sn from tmsT_salespoint where salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
                    //ccman = ccman + txtCCNR.Text;

                    //if (ccman == bll.vLookUp("select manual_no from tmst_ccnr where manual_no='" + ccman + "'"))
                    //{
                    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('CCNR Manual no already in the system','Please Enter correct CCNR No or contact WAZARAN ADMIN','warning');", true);
                    //    return;
                    //}

                    //List<cArrayList> arrCcnr = new List<cArrayList>();
                    //arrCcnr.Clear();
                    //string sbrn = bll.vLookUp("select salespoint_sn from tmst_salespoint where salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'") + txtCCNR.Text;

                    //arrCcnr.Add(new cArrayList("@ccnr_dt", DateTime.ParseExact(txtDate.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                    //arrCcnr.Add(new cArrayList("@manual_no", sbrn));
                    //arrCcnr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                    //arrCcnr.Add(new cArrayList("@claim_no", txtClaimNo.Text));
                    //arrCcnr.Add(new cArrayList("@app_by", null));
                    //bll.vInsertCcnr(arrCcnr, ref sCCNRNO);

                    //string sPdfName3 = Request.Cookies["sp"].Value.ToString() + "-" + sCCNRNO + "-" + "P35" + ".pdf";
                    //string sPath = bll.sGetControlParameter("image_path") + @"\claim_doc\";
                    //creport rep = new creport();

                    //arrCcnr.Clear();//ccnrdoc
                    //arrCcnr.Add(new cArrayList("@claim_no", sCCNRNO));
                    //arrCcnr.Add(new cArrayList("@doc_cd", "P33"));
                    //arrCcnr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                    //arrCcnr.Add(new cArrayList("@fileloc", Request.Cookies["sp"].Value.ToString() + "-" + sCCNRNO + "-" + "P35" + ".pdf"));
                    //arrCcnr.Add(new cArrayList("@doc_nm", " CCNR "));
                    //bll.vInsertTclaimDoc(arrCcnr);

                    //arrCcnr.Clear();
                    //arrCcnr.Add(new cArrayList("@ccnr_no", bll.vLookUp("select ccnr_no from tmst_ccnr where claim_no='" + txtClaimNo.Text + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'")));
                    //arrCcnr.Add(new cArrayList("@salespoint_cd", Request.Cookies["sp"].Value.ToString()));
                    //arrCcnr.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                    //rep.vShowReportToPDF("rp_ccnr.rpt", arrCcnr, sPath + sPdfName3); 

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('CCNR manual No sucessfully updated','CCNR Manual No.','sucess');", true);
                    txtCCNR.CssClass = "makeitreadonly";
                    txtCCNR.ReadOnly = true;
                }
            }
        }
        else if (hstatus.Value == "N")
        {

            string sCCNRNO = ""; string statusCCNR = "";

            if (txtCCNR.Text.ToString() == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('CCNR Manual Number must Inserted!','Please Insert CCNR.','error');", true);
                return;
            }

            string ccman = bll.vLookUp("select salespoint_sn from tmst_salespoint where salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
            ccman = ccman + txtCCNR.Text;

            if (ccman == bll.vLookUp("select manual_no from tmst_ccnr where manual_no='" + ccman + "'"))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('CCNR Manual no already in the system','Please Enter correct CCNR No or contact WAZARAN ADMIN','warning');", true);
                return;
            }

            List<cArrayList> arrCcnr = new List<cArrayList>();
            
            string sbrn = bll.vLookUp("select salespoint_sn from tmst_salespoint where salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'") + txtCCNR.Text;
            statusCCNR = bll.vLookUp("select manual_no from tmst_ccnr where claim_no='"+txtClaimNo.Text+"'");

            if (statusCCNR == "")
            {
                arrCcnr.Clear();
                arrCcnr.Add(new cArrayList("@ccnr_dt", DateTime.ParseExact(txtDate.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                arrCcnr.Add(new cArrayList("@manual_no", sbrn));
                arrCcnr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                arrCcnr.Add(new cArrayList("@claim_no", txtClaimNo.Text));
                arrCcnr.Add(new cArrayList("@app_by", null));
                bll.vInsertCcnr(arrCcnr, ref sCCNRNO);
            }
            else
            {
                arrCcnr.Clear();
                arrCcnr.Add(new cArrayList("@claim_no", txtClaimNo.Text));
                arrCcnr.Add(new cArrayList("@manual_NO", txtCCNR.Text));
                arrCcnr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vUpdateccnno(arrCcnr);
            }

            txtCCNR.ReadOnly = true;
            txtCCNR.CssClass = "makeitreadonly";
            txupdate.Visible = false;
        }
        else
        {
            if (txtCCNR.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Please insert CCNR manual No','Please fill CCNR Data.','warning');", true);
                return;
            }
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@claim_no", txtClaimNo.Text));
            arr.Add(new cArrayList("@manual_NO", txtCCNR.Text));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vUpdateccnno(arr);
            //string sCCNRNO = "";

            //string ccman = bll.vLookUp("select salespoint_sn from tmsT_salespoint where salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
            //ccman = ccman + txtCCNR.Text;

            //if (ccman == bll.vLookUp("select manual_no from tmst_ccnr where manual_no='" + ccman + "'"))
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('CCNR Manual no already in the system','Please Enter correct CCNR No or contact WAZARAN ADMIN','warning');", true);
            //    return;
            //}

            //List<cArrayList> arrCcnr = new List<cArrayList>();
            //arrCcnr.Clear();
            //string sbrn = bll.vLookUp("select salespoint_sn from tmst_salespoint where salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'") + txtCCNR.Text;

            //arrCcnr.Add(new cArrayList("@ccnr_dt", DateTime.ParseExact(txtDate.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            //arrCcnr.Add(new cArrayList("@manual_no", sbrn));
            //arrCcnr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            //arrCcnr.Add(new cArrayList("@claim_no", txtClaimNo.Text));
            //arrCcnr.Add(new cArrayList("@app_by", null));
            //bll.vInsertCcnr(arrCcnr, ref sCCNRNO);

            //string sPdfName3 = Request.Cookies["sp"].Value.ToString() + "-" + sCCNRNO + "-" + "P35" + ".pdf";
            //string sPath = bll.sGetControlParameter("image_path") + @"\claim_doc\";
            //creport rep = new creport();

            //arrCcnr.Clear();//ccnrdoc
            //arrCcnr.Add(new cArrayList("@claim_no", sCCNRNO));
            //arrCcnr.Add(new cArrayList("@doc_cd", "P33"));
            //arrCcnr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            //arrCcnr.Add(new cArrayList("@fileloc", Request.Cookies["sp"].Value.ToString() + "-" + sCCNRNO + "-" + "P35" + ".pdf"));
            //arrCcnr.Add(new cArrayList("@doc_nm", " CCNR "));
            //bll.vInsertTclaimDoc(arrCcnr);

            //arrCcnr.Clear();
            //arrCcnr.Add(new cArrayList("@ccnr_no", bll.vLookUp("select ccnr_no from tmst_ccnr where claim_no='" + txtClaimNo.Text + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'")));
            //arrCcnr.Add(new cArrayList("@salespoint_cd", Request.Cookies["sp"].Value.ToString()));
            //arrCcnr.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
            //rep.vShowReportToPDF("rp_ccnr.rpt", arrCcnr, sPath + sPdfName3); 

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('CCNR manual No sucessfully updated','CCNR Manual No.','sucess');", true);
            txtCCNR.CssClass = "makeitreadonly";
            txtCCNR.ReadOnly = true;
        }
    }
    protected void grdBA_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Label lbfreeBA = (Label)e.Row.FindControl("lbqty");
            totalFreeQtyBA += Convert.ToDouble(lbfreeBA.Text);

            Label lbamountBA = (Label)e.Row.FindControl("lbamt");
            totalAmountBA += Convert.ToDouble(lbamountBA.Text);

        }

    }

    protected void btPaid_Click(object sender, EventArgs e)
    {
        string ddStatus = bll.vLookUp("select claim_sta_id from tmst_claim where claim_no='" + txtClaimNo.Text + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
        DateTime dtwaz_dt = DateTime.ParseExact(Request.Cookies["waz_dt"].Value.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        if (ddStatus == "A")
        {
            if (bll.nCheckAccess("claimpay", Request.Cookies["usr_id"].Value.ToString()) == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','To pay this claim contact Administrator !!','warning');", true);
                return;
            }

            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@claim_no", txtClaimNo.Text));
            arr.Add(new cArrayList("@claim_sta_id", "P"));
            arr.Add(new cArrayList("@approve_by", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            arr.Add(new cArrayList("@app_dt", dtwaz_dt));
            bll.vUpdateClaim(arr);
        }
    }
    protected void btncancel2_Click(object sender, EventArgs e)
    {
        DateTime dtwaz_dt = DateTime.ParseExact(Request.Cookies["waz_dt"].Value.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        if (txremark.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Sorry!','Please fill Remark Data.','warning');", true);
            return;
        }

        if (bll.nCheckAccess("claimrt", Request.Cookies["usr_id"].Value.ToString()) != 0)
        {
            bll.vLookUp("update tmst_claim set remarka='" + txremark.Text + "' where claim_no='" + txtClaimNo.Text + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
            bll.vLookUp("update tmst_claim set claim_sta_id='L' where claim_no='" + txtClaimNo.Text + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");

            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@claim_no", txtClaimNo.Text));
            arr.Add(new cArrayList("@remark", txremark.Text));
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@remark_id", ""));
            arr.Add(new cArrayList("@claim_sta_id", "L"));
            arr.Add(new cArrayList("@issue_no", cbissuedesc.SelectedValue.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vInsertClaimrRemark(arr);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Success!','Claim Has Been updated.','success');", true);
        }
    }
    protected void grddoc_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.DataRow) return;
        Label lbdoc_cd = (Label)e.Row.FindControl("lbdoc_cd");

        if (lbdoc_cd.Text == "P02")
        { 
            Button btnView = (Button)e.Row.FindControl("btnView");
            Label lbfileloc = (Label)e.Row.FindControl("lbfileloc");
            lbfileloc.Visible = false;
            btnView.Visible = true;
        }
        else
        {
            Button btnView = (Button)e.Row.FindControl("btnView");
            Label lbfileloc = (Label)e.Row.FindControl("lbfileloc");
            lbfileloc.Visible = true;
            btnView.Visible = false;
        }
    }
    protected void grddoc_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "view")
        {

            string cust = bll.vLookUp("select rdcust from tmst_proposal where prop_no='"+txtProposal.Text+"'");
            string product = bll.vLookUp("select rditem from tmst_proposal where prop_no='" + txtProposal.Text + "'");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "op", "openreport('fm_report2.aspx?src=prop&prop_no=" + txtProposal.Text + "&cust=" + cust + "&product=" + product + "&dbp=no');", true);
        }
    }
}