using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Data.SqlClient;
using System.IO;

public partial class fm_invByProposal_ho : System.Web.UI.Page
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
            bll.vGetClaimDetailHo(arr, ref rs);
            while (rs.Read())
            {
                lblClaimStatus.Text = rs["claim_sta_id"].ToString();
                DateTime claimdt = Convert.ToDateTime(rs["claim_dt"].ToString());
                txtDate.Text = string.Format("{0:dd/MM/yyyy}", claimdt);
                txtCCNR.Text = rs["ccnr_no"].ToString();
                ddMonth.SelectedValue = rs["tx_month"].ToString();
                ddYear.SelectedValue = rs["tx_year"].ToString();
                lblRemark.Text = rs["budget_prop"].ToString();
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
            btPrint.Visible = false;
            if (ClaimStatus == "N" || ClaimStatus == "CH" || ClaimStatus == "A") { txupdate.Enabled = true; } else { txupdate.Enabled = false; }
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

            //if (stCcnr == "0")
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('This CCNR not exist.','To approve this claim Insert CCNR No. !!','warning');", true);
            //    return;
            //}

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
            Response.Redirect("fm_claimList_ho.aspx", true);
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
            Response.Redirect("fm_claimList_ho.aspx", true);
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
            arr.Add(new cArrayList("@claim_sta_id", "A"));
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
            Response.Redirect("fm_claimList_ho.aspx", true);
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
            arr.Clear();
            arr.Add(new cArrayList("@claim_no", txtClaimNo.Text));
            arr.Add(new cArrayList("@remark", txremark.Text));
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@remark_id", ""));
            arr.Add(new cArrayList("@claim_sta_id", hstatus.Value));
            arr.Add(new cArrayList("@issue_no", cbissuedesc.SelectedValue.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vInsertClaimrRemark(arr);
            Response.Redirect("fm_claimList_ho.aspx", true);
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
        lblTFreeItem.Text = "";
        lblTFreeCash.Text = "";
        arr.Clear();
        arr.Add(new cArrayList("@claim_no", Request.QueryString["number"]));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        bll.vBindingGridToSp(ref grdBA, "sp_tclaimdtl_getbycontractka", arr);
        arr.Clear();
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        arr.Add(new cArrayList("@claim_no", txtClaimNo.Text));
        bll.vBindingGridToSp(ref grddoc, "sp_getClaimDoc", arr);

        //totalFreeItem = dSubTotal + dSubTotalCOItem;
        //totalFreeCash = dSubTotalSO + dSubTotalCO;

        //lblTFreeItem.Text = totalFreeItem.ToString("N2");
        //lblTFreeCash.Text = totalFreeCash.ToString("N2");

        //if (lblTFreeItem.Text != "0.00") lblTFreeCash.Visible = true;
        //if (lblTFreeCash.Text != "0.00") lblTFreeItem.Visible = true;


        //if (discountMec == "CH")
        //{
        //    lbclaimtyp.Text = "QTY ORDER";
        //    Label1.Text = "Take Order Discount Cash";
        //    Label3.Text = "Canvas Order Discount Cash";
        //    if (gridClaimSO.Rows.Count.ToString() != "0")
        //    {
        //        Label1.Visible = true;
        //    }
        //    if (gridClaimCO.Rows.Count.ToString() != "0")
        //    {
        //        Label3.Visible = true;
        //    }
        //}
        if (discountMec == "CMHO")
        {
            lbclaimtyp.Text = "Claim Amount";
            lblTFreeItem.Text = "0.00";
            lblTQtyOrder.Text = totalAmountBA.ToString("N2");
            if (grdBA.Rows.Count > 0) 
            {
                Label lbsubamt = (Label)grdBA.FooterRow.FindControl("lbsubamt");
                lbsubamt.Text = totalAmountBA.ToString("N2");
                grdBA.Visible = true;

            }
            else { grdBA.Visible = false; }
        }
        //else
        //{
        //    lbclaimtyp.Text = "Claim Amount";
        //    Label2.Text = "Take Order Free Item";
        //    Label4.Text = "Canvas Order Free Item";

        //    if (gridClaimSOItem.Rows.Count.ToString() != "0")
        //    {
        //        Label2.Visible = true;
        //    }
        //    if (gridClaimCOItem.Rows.Count.ToString() != "0")
        //    {
        //        Label4.Visible = true;
        //    }
        //}
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        //Response.Redirect("fm_claimEntry.aspx", true);
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
        //List<cArrayList> arr = new List<cArrayList>();
        //arr.Add(new cArrayList("@ccnr_no", bll.vLookUp("select ccnr_no from tmst_ccnr where claim_no='" + txtClaimNo.Text + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'")));
        //arr.Add(new cArrayList("@salespoint_cd", Request.Cookies["sp"].Value.ToString()));
        //arr.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
        //Session["lparamccnr"] = arr;
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opf", "openreport('fm_report2.aspx?src=ccnr');", true);
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
                        arr.Add(new cArrayList("@salespoint", Request.Cookies["sp"].Value.ToString()));
                        arr.Add(new cArrayList("@fileloc", Request.Cookies["sp"].Value.ToString() + "-" + txtClaimNo.Text + "-" + lbdoccode.Text + ext));
                        arr.Add(new cArrayList("@doc_nm", lbdocname.Text));
                        upl.SaveAs(bll.sGetControlParameter("image_path") + "/claim_doc/" + Request.Cookies["sp"].Value.ToString() + "-" + txtClaimNo.Text + "-" + lbdoccode.Text + ext);
                        bll.vInsClaimDoc(arr);
                    }
                    else
                    {
                        // statusUpload = "0";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('Please attach all the documents required','Proposal, Summary claim and Invoice (zip files)');", true);
                        return;
                    }
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
        try
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

                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('CCNR manual No sucessfully updated','CCNR Manual No.','sucess');", true);
                        txtCCNR.CssClass = "makeitreadonly";
                        txtCCNR.ReadOnly = true;
                    }
                }
            }
            else if (hstatus.Value == "N" || hstatus.Value == "CH" || hstatus.Value == "A")
            {

                string sCCNRNO = ""; string statusCCNR = ""; int CMTot = 0; int CMCurrent = 0;

                //if (txtCCNR.Text.ToString() == "")
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('CCNR Manual Number must Inserted!','Please Insert CCNR.','error');", true);
                //    return;
                //}

                string ccnrCM = bll.vLookUp("select top 1 b.cm_ref_no from tclaim_dtl a inner join tmst_creditmemo b on a.so_cd=b.cm_no where claim_no='" + txtClaimNo.Text + "' order by so_cd asc ");

                string ccman = bll.vLookUp("select salespoint_sn from tmst_salespoint where salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");

                ccman = ccman + ccnrCM;

                CMTot = int.Parse(bll.vLookUp("select count(ccnr_no) from tmst_claim where left(ccnr_no,10)=left('" + ccman + "',10)"));

                if (CMTot > 0)
                {
                    for (int x = 1; x < CMTot; x++)
                    {
                        CMCurrent = int.Parse(bll.vLookUp("select count(ccnr_no) from tmst_claim where left(ccnr_no,12)='" + ccman + x.ToString() + "'"));
                        if (CMCurrent == 0)
                        {
                            ccnrCM = ccnrCM + x.ToString();
                            break;
                        }
                    }
                }

                ccman = bll.vLookUp("select salespoint_sn from tmst_salespoint where salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");

                ccman = ccman + ccnrCM;

                txtCCNR.Text = ccman;

                //if (ccman == bll.vLookUp("select manual_no from tmst_ccnr where manual_no='" + ccman + "'"))
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('CCNR Manual no already in the system','Please Enter correct CCNR No or contact WAZARAN ADMIN','warning');", true);
                //    return;
                //}

                List<cArrayList> arrCcnr = new List<cArrayList>();

                //string sbrn = bll.vLookUp("select salespoint_sn from tmst_salespoint where salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'") + ccnrCM;

                statusCCNR = bll.vLookUp("select manual_no from tmst_ccnr where claim_no='" + txtClaimNo.Text + "'");

                if (statusCCNR == "")
                {
                    string sCCNRNOO = txtClaimNo.Text.Substring(3);
                    arrCcnr.Clear();
                    arrCcnr.Add(new cArrayList("@ccnr_no", "CCNR" + sCCNRNOO));
                    arrCcnr.Add(new cArrayList("@ccnr_dt", DateTime.ParseExact(txtDate.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                    arrCcnr.Add(new cArrayList("@manual_no", ccman));
                    arrCcnr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                    arrCcnr.Add(new cArrayList("@claim_no", txtClaimNo.Text));
                    arrCcnr.Add(new cArrayList("@app_by", null));
                    bll.vInsertCcnr(arrCcnr);
                }
                else
                {
                    arrCcnr.Clear();
                    arrCcnr.Add(new cArrayList("@claim_no", txtClaimNo.Text));
                    arrCcnr.Add(new cArrayList("@manual_NO", ccman));
                    arrCcnr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                    bll.vUpdateccnno(arrCcnr);
                }

                txtCCNR.ReadOnly = true;
                txtCCNR.CssClass = "makeitreadonly";
                txupdate.Visible = true;
                txupdate.Enabled = false;
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
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('CCNR manual No sucessfully updated','CCNR Manual No.','sucess');", true);
                txtCCNR.CssClass = "makeitreadonly";
                txtCCNR.ReadOnly = true;
            }
        }
        catch (Exception ex)
        {
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : UPDATE CCNR CLAIM HO");
        }
        
    }
    protected void grdBA_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
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
    protected void grdBA_RowEditing(object sender, GridViewEditEventArgs e)
    {
        if (bll.nCheckAccess("claimedit", Request.Cookies["usr_id"].Value.ToString()) == 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','To Edit this Claim contact Administrator !!','warning');", true);
            return;
        }
        else
        {
            grdBA.EditIndex = e.NewEditIndex;
            //btrefresh2();
        }
        //txamt.CssClass = cd.csstextro;
    }
    protected void grdBA_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grdBA.EditIndex = -1;
        btrefresh2();
    }
    protected void grdBA_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        Label so_cd = (Label)grdBA.Rows[e.RowIndex].FindControl("so_cd");
        Label inv_no = (Label)grdBA.Rows[e.RowIndex].FindControl("inv_no");
        TextBox txamt = (TextBox)grdBA.Rows[e.RowIndex].FindControl("txamt");
        double dOut = 0;
        if (!double.TryParse(txamt.Text, out dOut))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('Please enter numeric format','Numeric Only','warning');", true);
            return;
        }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@claim_no", Request.QueryString["number"]));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        arr.Add(new cArrayList("@editby", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@so_cd", so_cd.Text.ToString()));
        arr.Add(new cArrayList("@inv_no", inv_no.Text.ToString()));
        arr.Add(new cArrayList("@freevalue", txamt.Text.ToString()));
        bll.vUpdateMstClaimCMHO(arr);
        grdBA.EditIndex = -1;
        btrefresh2();
        arr.Clear();

    }
}