using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Ionic.Zip;

public partial class fm_claimgenerate_ho : System.Web.UI.Page
{
    cbll bll = new cbll();
    double dSubTotal = 0;
    double dSubTotalCO = 0;
    double dSubTotalCOItem = 0;
    double dSubTotalSO = 0;
    double dubtotamt = 0;
    double dubtotamtex = 0;
    double dSubTotalQtyCO = 0;
    double dSubTotalQtySO = 0;

    double dSubTotalQtyCashSO = 0;
    double dSubTotalQtyCashCO = 0;

    double totalPrice = 0;
    double i = 0;

    double priceAVGSO = 0;
    double priceAVGCO = 0;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            lbchInvoice.Text = "Not OK";
            lbchCashout.Text = "Not OK";
            lbchcontract.Text = "Not OK";
            btncheck.Text = "Check";
            bll.vBindingComboToSp(ref cbYear, "sp_tmst_period_getbyyear", "yearvalue", "yearvalue");
            cbYear_SelectedIndexChanged(sender, e);
        }
    }
    protected void btncheck_Click(object sender, EventArgs e)
    {
        bool stInv = false, stCO = false, stCNDN = false, stBA = false;
        string stInvoice = "OK", stCashout = "OK", stCredit = "OK", stAgreement = "OK";
        int qryInv = 0, qryCO = 0, qryCN = 0, qryBA = 0, qryBA2 = 0, qryBA3 = 0;
        System.Data.SqlClient.SqlDataReader rsDisc = null;
        System.Data.SqlClient.SqlDataReader rsInv = null;
        System.Data.SqlClient.SqlDataReader rsCashOut = null;
        System.Data.SqlClient.SqlDataReader rsCNDN = null;
        System.Data.SqlClient.SqlDataReader rsBA = null;
        System.Data.SqlClient.SqlDataReader rsPrice = null;
        List<cArrayList> arr = new List<cArrayList>();
        List<cArrayList> arrDiscount = new List<cArrayList>();
        List<cArrayList> arrInvoice = new List<cArrayList>();
        List<cArrayList> arrClaim = new List<cArrayList>();
        if (btncheck.Text == "Check")
        {
            // Check Agreement 

            qryBA = int.Parse(bll.vLookUp("select count(*) from tmst_contract_ka a where a.tx_month='" + cbMonth.SelectedValue.ToString() + "' and a.tx_year='" + cbYear.SelectedValue.ToString() + "' and a.status in ('N') "));
            qryBA = int.Parse(bll.vLookUp("select count(*) from tmst_creditmemo a where month(a.cm_dt)='" + cbMonth.SelectedValue.ToString() + "' and year(a.cm_dt)='" + cbYear.SelectedValue.ToString() + "' and a.cm_sta_id in ('N') "));

            if (qryBA > 0) { stAgreement = "Not OK"; } else {
                qryBA2 = int.Parse(bll.vLookUp("select count(*) from tmst_contract_ka a where a.tx_month='" + cbMonth.SelectedValue.ToString() + "' and a.tx_year='" + cbYear.SelectedValue.ToString() + "' and a.status in ('N','A') "));
                qryBA2 = int.Parse(bll.vLookUp("select count(*) from tmst_creditmemo a where month(a.cm_dt)='" + cbMonth.SelectedValue.ToString() + "' and year(a.cm_dt)='" + cbYear.SelectedValue.ToString() + "' and a.cm_sta_id in ('A') "));

                qryBA3 = int.Parse(bll.vLookUp("select count(*) from tmst_creditmemo a inner join tclaim_dtl b on a.cm_no=b.so_cd inner join tmst_claim c on b.claim_no=c.claim_no and b.salespointcd=c.salespointcd where month(a.cm_dt)='" + cbMonth.SelectedValue.ToString() + "' and year(a.cm_dt)='" + cbYear.SelectedValue.ToString() + "' and a.cm_sta_id in ('A') "));



                if (qryBA2 - qryBA == qryBA2)
                {
                    stAgreement = "OK";
                }
                else
                {
                    stAgreement = "OK"; 
                }                
            }

            lbchInvoice.Text = stInvoice;//stInvoice;
            lbchCashout.Text = stCashout;
            lbchcontract.Text = stAgreement;

            btncheck.Text = (lbchInvoice.Text == "OK" || lbchCashout.Text == "OK" || lbchcontract.Text == "OK") ? "Generate" : "Generate";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "cl", "vDisableShow();", true);
            //return;         

        }
        else
        {
            
            try 
            {
                // Send Claim To Principal 
                bll.vBatchSendClaimPrincipal();

                //lbchInvoice.Text = "NOT OK"; lbchCashout.Text = "OK"; lbchCndn.Text = "NOT OK"; lbchcontract.Text = "NOT OK";
                string sPath = "", sPdfName = "", sPdfName1 = "", sPdfName2 = "";
                sPath = bll.sGetControlParameter("image_path") + @"\claim_doc\";
                creport rep = new creport();
                arrDiscount.Clear();
                arrInvoice.Clear();
                arr.Clear();
                arrDiscount.Add(new cArrayList("@month", cbMonth.SelectedValue.ToString()));
                arrDiscount.Add(new cArrayList("@year", cbYear.SelectedValue.ToString()));
                arr.Add(new cArrayList("@month", cbMonth.SelectedValue.ToString()));
                arr.Add(new cArrayList("@year", cbYear.SelectedValue.ToString()));
                //if (lbchInvoice.Text == "OK")
                //{
                //    // Generate TO & CO 
                //    try { 
                //    bll.vGetDiscountList(arrDiscount, ref rsDisc);
                //    while (rsDisc.Read())
                //    {
                //        arrClaim.Clear();
                //        string sCLNO = "", claim_typ = "";
                //        arrClaim.Add(new cArrayList("@claim_dt", DateTime.ParseExact(bll.vLookUp("select parm_valu from tcontrol_parameter where parm_nm='wazaran_dt'"), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                //        arrClaim.Add(new cArrayList("@prop_no", rsDisc["proposal"].ToString()));
                //        arrClaim.Add(new cArrayList("@ccnr_no", null));
                //        if ((bll.vLookUp("select rdcost from tmst_proposal where prop_no='" + rsDisc["proposal"].ToString() + "'")) == "branch")
                //        {
                //            arrClaim.Add(new cArrayList("@claim_sta_id", "L"));
                //        }
                //        else
                //        {
                //            arrClaim.Add(new cArrayList("@claim_sta_id", "N"));
                //        }

                //        if (rsDisc["discount_mec"].ToString() == "FG")
                //        {
                //            arrClaim.Add(new cArrayList("@ordervalue", Convert.ToDouble("0")));
                //            arrClaim.Add(new cArrayList("@orderqty", 1 * Convert.ToDouble(bll.vLookUp("exec sp_tot_claim_fg '" + rsDisc["disc_cd"].ToString() + "','" + cbMonth.SelectedValue.ToString() + "','" + cbYear.SelectedValue.ToString() + "','" + Request.Cookies["sp"].Value.ToString() + "','total'"))));
                //            arrClaim.Add(new cArrayList("@freevalue", Convert.ToDouble("0")));
                //            arrClaim.Add(new cArrayList("@freeqty", 1 * Convert.ToDouble(bll.vLookUp("exec sp_tot_claim_fg '" + rsDisc["disc_cd"].ToString() + "','" + cbMonth.SelectedValue.ToString() + "','" + cbYear.SelectedValue.ToString() + "','" + Request.Cookies["sp"].Value.ToString() + "','qty'"))));
                //            arrClaim.Add(new cArrayList("@price_avg", Convert.ToDouble(bll.vLookUp("exec sp_tot_claim_fg '" + rsDisc["disc_cd"].ToString() + "','" + cbMonth.SelectedValue.ToString() + "','" + cbYear.SelectedValue.ToString() + "','" + Request.Cookies["sp"].Value.ToString() + "','avg'"))));
                //        }
                //        else
                //        {
                //            arrClaim.Add(new cArrayList("@ordervalue", 1 * Convert.ToDouble(bll.vLookUp("exec sp_tot_claim_ch '" + rsDisc["disc_cd"].ToString() + "','" + cbMonth.SelectedValue.ToString() + "','" + cbYear.SelectedValue.ToString() + "','" + Request.Cookies["sp"].Value.ToString() + "','total'"))));
                //            arrClaim.Add(new cArrayList("@orderqty", Convert.ToDouble("0")));
                //            arrClaim.Add(new cArrayList("@freevalue", 1 * Convert.ToDouble(bll.vLookUp("exec sp_tot_claim_ch '" + rsDisc["disc_cd"].ToString() + "','" + cbMonth.SelectedValue.ToString() + "','" + cbYear.SelectedValue.ToString() + "','" + Request.Cookies["sp"].Value.ToString() + "','qty'"))));
                //            arrClaim.Add(new cArrayList("@freeqty", Convert.ToDouble("0")));
                //            arrClaim.Add(new cArrayList("@price_avg", Convert.ToDouble("0")));
                //        }
                //        arrClaim.Add(new cArrayList("@sendtoho_dt", DateTime.ParseExact(bll.vLookUp("select parm_valu from tcontrol_parameter where parm_nm='wazaran_dt'"), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                //        arrClaim.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                //        arrClaim.Add(new cArrayList("@cby", Request.Cookies["usr_id"].Value.ToString()));
                //        arrClaim.Add(new cArrayList("@txMonth", cbMonth.SelectedValue.ToString()));
                //        arrClaim.Add(new cArrayList("@txYear", cbYear.SelectedValue.ToString()));
                //        arrClaim.Add(new cArrayList("@disc_cd", rsDisc["disc_cd"].ToString()));
                //        arrClaim.Add(new cArrayList("@discount_mec", rsDisc["discount_mec"].ToString()));
                //        bll.vInsertClaim(arrClaim, ref sCLNO);
                //        arrClaim.Clear();
                //        arrClaim.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                //        arrClaim.Add(new cArrayList("@disc_cd", rsDisc["disc_cd"].ToString()));
                //        arrClaim.Add(new cArrayList("@claim_no", sCLNO));
                //        arrClaim.Add(new cArrayList("@Month", cbMonth.SelectedValue.ToString()));
                //        arrClaim.Add(new cArrayList("@Year", cbYear.SelectedValue.ToString()));
                //        bll.vInsertClaimdtlfreeso(arrClaim);
                //        bll.vInsertClaimdtlfreeco(arrClaim);
                //        bll.vInsertClaimdtlcashso(arrClaim);
                //        bll.vInsertClaimdtlcashco(arrClaim);
                //        arrClaim.Clear();

                //        sPdfName = Request.Cookies["sp"].Value.ToString() + "-" + sCLNO + "-" + "P09" + ".pdf";
                //        sPdfName1 = Request.Cookies["sp"].Value.ToString() + "-" + sCLNO + "-" + "P04" + ".xls";
                //        sPdfName2 = Request.Cookies["sp"].Value.ToString() + "-" + sCLNO + "-" + "P02" + ".pdf";

                //        arrClaim.Add(new cArrayList("@claim_no", sCLNO));
                //        arrClaim.Add(new cArrayList("@doc_cd", "P09"));
                //        arrClaim.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                //        arrClaim.Add(new cArrayList("@fileloc", Request.Cookies["sp"].Value.ToString() + "-" + sCLNO + "-" + "P09" + ".pdf"));
                //        arrClaim.Add(new cArrayList("@doc_nm", "Claim Invoices (if any)"));
                //        bll.vInsertTclaimDoc(arrClaim);
                //        arrClaim.Clear();
                //        arrClaim.Add(new cArrayList("@claim_no", sCLNO));
                //        arrClaim.Add(new cArrayList("@doc_cd", "P04"));
                //        arrClaim.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                //        arrClaim.Add(new cArrayList("@fileloc", Request.Cookies["sp"].Value.ToString() + "-" + sCLNO + "-" + "P04" + ".xls"));
                //        arrClaim.Add(new cArrayList("@doc_nm", "Summary Claim"));
                //        bll.vInsertTclaimDoc(arrClaim);
                //        arrClaim.Clear();
                //        arrClaim.Add(new cArrayList("@claim_no", sCLNO));
                //        arrClaim.Add(new cArrayList("@doc_cd", "P02"));
                //        arrClaim.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                //        arrClaim.Add(new cArrayList("@fileloc", Request.Cookies["sp"].Value.ToString() + "-" + sCLNO + "-" + "P02" + ".pdf"));
                //        arrClaim.Add(new cArrayList("@doc_nm", "Proposal Approved"));
                //        bll.vInsertTclaimDoc(arrClaim);
                //        arrClaim.Clear();
                //        arrClaim.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                //        arrClaim.Add(new cArrayList("@claim_no", sCLNO));
                //        bll.vUpdateTmstclaim(arrClaim);
                //        arrClaim.Clear();
                //        arrClaim.Add(new cArrayList("@claim_no", sCLNO));
                //        arrClaim.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                //        //arrClaim.Add(new cArrayList("@rp_typ", null));
                //        rep.vShowReportToPDF("rp_claim_invoice.rpt", arrClaim, sPath + sPdfName);
                //        arrClaim.Clear();
                //        arrClaim.Add(new cArrayList("@claim_no", sCLNO));
                //        arrClaim.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                //        claim_typ = bll.vLookUp("select distinct disc_typ from tclaim_dtl where claim_no='" + sCLNO + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
                //        if (claim_typ == "FG")
                //        {
                //            rep.vShowReportToEXCEL("rp_claimFG.rpt", arrClaim, sPath + sPdfName1);
                //        }
                //        else if (claim_typ == "CH")
                //        {
                //            rep.vShowReportToEXCEL("rp_claimDC.rpt", arrClaim, sPath + sPdfName1);
                //        }
                //        arrClaim.Clear();

                //        //string prop_get = bll.vLookUp("select prop_no from tmst_claim where claim_no='" + sCLNO + "'");
                //        //string prop_cust = bll.vLookUp("select rdcust from tmst_proposal where prop_no = '" + prop_get + "'");
                //        //string prop_item = bll.vLookUp("select rditem from tmst_proposal where prop_no = '" + prop_get + "'");

                //        //arrClaim.Add(new cArrayList("@prop_no", prop_get));
                //        //arrClaim.Add(new cArrayList("@cust", prop_cust));
                //        //arrClaim.Add(new cArrayList("@product", prop_item));
                //        //arrClaim.Add(new cArrayList("@salespoint", null));
                //        //arrClaim.Add(new cArrayList("@dbp", "no"));
                //        //arrClaim.Add(new cArrayList("@vendor", null));
                //        //arrClaim.Add(new cArrayList("@cost", null));
                //        //arrClaim.Add(new cArrayList("@promo", null));
                //        //arrClaim.Add(new cArrayList("@year", null));
                //        //arrClaim.Add(new cArrayList("@month", null));
                //        //rep.vShowReportToPDF("rp_proposal.rpt", arrClaim, sPath + sPdfName2);
                //        //arrClaim.Clear();
                //    }
                //    //rsDisc.Close();
                //    }
                //    catch (Exception ex)
                //    {
                //        bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : GENERATE CLAIM (INVOICE)");
                //        btncheck.Text = "Check";
                //    }
                //}
                //if (lbchCashout.Text == "OK")
                //{
                //    // Generate Cashout 
                //    try
                //    {

                //        bll.vGetCashoutHOList(arr, ref rsCashOut);
                //        while (rsCashOut.Read())
                //        {
                //            string sCLNO = "", claim_typ = "";
                //            System.Data.SqlClient.SqlDataReader rsG = null;
                //            List<cArrayList> arrG = new List<cArrayList>();
                //            arrG.Add(new cArrayList("@sys", "claimho"));
                //            arrG.Add(new cArrayList("@sysno", ""));
                //            bll.vGetDiscountNo(arrG, ref rsG);
                //            while (rsG.Read())
                //            {
                //                sCLNO = rsG["generated"].ToString();
                //            }

                //            arrClaim.Clear();
                //            arrClaim.Add(new cArrayList("@claim_no", sCLNO));
                //            arrClaim.Add(new cArrayList("@claim_dt", DateTime.Now.ToString()));
                //            arrClaim.Add(new cArrayList("@prop_no", rsCashOut["prop_no"].ToString()));
                //            arrClaim.Add(new cArrayList("@ccnr_no", null));
                //            if ((bll.vLookUp("select rdcost from tmst_proposal where prop_no='" + rsCashOut["prop_no"].ToString() + "'")) == "branch")
                //            {
                //                arrClaim.Add(new cArrayList("@claim_sta_id", "L"));
                //            }
                //            else
                //            {
                //                arrClaim.Add(new cArrayList("@claim_sta_id", "N"));
                //            }
                //            arrClaim.Add(new cArrayList("@amount", Convert.ToDouble(rsBA["total"].ToString())));
                //            arrClaim.Add(new cArrayList("@ordervalue", rsCashOut["total"].ToString()));
                //            arrClaim.Add(new cArrayList("@orderqty", 0.00));
                //            arrClaim.Add(new cArrayList("@freevalue", 0.00));
                //            arrClaim.Add(new cArrayList("@freeqty", 0.00));
                //            arrClaim.Add(new cArrayList("@price_avg", 0.00));
                //            arrClaim.Add(new cArrayList("@sendtoho_dt", DateTime.Now.ToString()));
                //            arrClaim.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                //            arrClaim.Add(new cArrayList("@cby", Request.Cookies["usr_id"].Value.ToString()));
                //            arrClaim.Add(new cArrayList("@txMonth", cbMonth.SelectedValue.ToString()));
                //            arrClaim.Add(new cArrayList("@txYear", cbYear.SelectedValue.ToString()));
                //            arrClaim.Add(new cArrayList("@disc_cd", ""));
                //            arrClaim.Add(new cArrayList("@discount_mec", "CSHO"));
                //            bll.vInsertClaimHO(arrClaim);

                //            arrClaim.Clear();
                //            arrClaim.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                //            arrClaim.Add(new cArrayList("@prop_no", rsCashOut["prop_no"].ToString()));
                //            arrClaim.Add(new cArrayList("@claim_no", sCLNO));
                //            arrClaim.Add(new cArrayList("@Month", cbMonth.SelectedValue.ToString()));
                //            arrClaim.Add(new cArrayList("@Year", cbYear.SelectedValue.ToString()));
                //            bll.vInsertClaimdtlcashoutHO(arrClaim);
                //            arrClaim.Clear();

                //            sPdfName = Request.Cookies["sp"].Value.ToString() + "-" + sCLNO + "-" + "P09" + ".pdf";
                //            sPdfName1 = Request.Cookies["sp"].Value.ToString() + "-" + sCLNO + "-" + "P04" + ".xls";
                //            sPdfName2 = Request.Cookies["sp"].Value.ToString() + "-" + sCLNO + "-" + "P02" + ".pdf";

                //            arrClaim.Clear();
                //            arrClaim.Add(new cArrayList("@claim_no", sCLNO));
                //            arrClaim.Add(new cArrayList("@doc_cd", "P04"));
                //            arrClaim.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                //            arrClaim.Add(new cArrayList("@fileloc", Request.Cookies["sp"].Value.ToString() + "-" + sCLNO + "-" + "P04" + ".xls"));
                //            arrClaim.Add(new cArrayList("@doc_nm", "Summary Claim"));
                //            bll.vInsClaimDoc(arrClaim);
                //            arrClaim.Clear();
                //            arrClaim.Add(new cArrayList("@claim_no", sCLNO));
                //            arrClaim.Add(new cArrayList("@doc_cd", "P02"));
                //            arrClaim.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                //            arrClaim.Add(new cArrayList("@fileloc", Request.Cookies["sp"].Value.ToString() + "-" + sCLNO + "-" + "P02" + ".pdf"));
                //            arrClaim.Add(new cArrayList("@doc_nm", "Proposal Approved"));
                //            bll.vInsClaimDoc(arrClaim);
                //            arrClaim.Clear();

                //            //arrClaim.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                //            //arrClaim.Add(new cArrayList("@claim_no", sCLNO));
                //            //bll.vUpdateTmstclaim(arrClaim);
                //            //arrClaim.Clear();
                //            //arrClaim.Clear();
                //            //arrClaim.Add(new cArrayList("@claim_no", sCLNO));
                //            //arrClaim.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                //            //arrClaim.Add(new cArrayList("@customer", null));
                //            //rep.vShowReportToEXCEL("rp_claimCashOut.rpt", arrClaim, sPath + sPdfName1);
                //            //arrClaim.Clear();

                //        }
                //        //rsCashOut.Close();
                //    }
                //    catch (Exception ex)
                //    {
                //        bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : GENERATE CLAIM (CASHOUT HO)");
                //        btncheck.Text = "Check";
                //    }
                //}
                if (lbchcontract.Text == "OK")
                {
                    //Genereate Business Agreement 
                    try {
                        
                        bll.vGetContractKAList(arr, ref rsBA);
                        while (rsBA.Read())
                        {
                            string sCLNO = "", claim_typ = "";
                            System.Data.SqlClient.SqlDataReader rsG = null;
                            List<cArrayList> arrG = new List<cArrayList>();
                            arrG.Add(new cArrayList("@sys", "claimho"));
                            arrG.Add(new cArrayList("@sysno", ""));
                            bll.vGetDiscountNo(arrG, ref rsG);
                            while (rsG.Read())
                            {
                                sCLNO = rsG["generated"].ToString();
                            }

                            arrClaim.Clear();

                            arrClaim.Add(new cArrayList("@claim_no", sCLNO));
                            arrClaim.Add(new cArrayList("@claim_dt", DateTime.Now.ToString()));
                            arrClaim.Add(new cArrayList("@prop_no", rsBA["prop_no"].ToString()));
                            arrClaim.Add(new cArrayList("@ccnr_no", null));
                            if ((bll.vLookUp("select rdcost from tmst_proposal where prop_no='" + rsBA["prop_no"].ToString() + "'")) == "branch")
                            {
                                arrClaim.Add(new cArrayList("@claim_sta_id", "L"));
                            }
                            else
                            {
                                arrClaim.Add(new cArrayList("@claim_sta_id", "N"));
                            }
                            arrClaim.Add(new cArrayList("@amount", Convert.ToDouble(rsBA["amount"].ToString())));
                            arrClaim.Add(new cArrayList("@ordervalue", 0.00));
                            arrClaim.Add(new cArrayList("@orderqty", 0.00));
                            arrClaim.Add(new cArrayList("@freevalue", Convert.ToDouble(rsBA["amount"].ToString())));
                            arrClaim.Add(new cArrayList("@freeqty", 0.00));
                            arrClaim.Add(new cArrayList("@price_avg", 0.00));
                            arrClaim.Add(new cArrayList("@sendtoho_dt", DateTime.Now.ToString()));
                            arrClaim.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                            arrClaim.Add(new cArrayList("@cby", Request.Cookies["usr_id"].Value.ToString()));
                            arrClaim.Add(new cArrayList("@txmonth", cbMonth.SelectedValue.ToString()));
                            arrClaim.Add(new cArrayList("@txyear", cbYear.SelectedValue.ToString()));
                            arrClaim.Add(new cArrayList("@disc_cd", ""));
                            arrClaim.Add(new cArrayList("@discount_mec", "CMHO"));
                            bll.vInsertClaimHO(arrClaim);

                            arrClaim.Clear();
                            arrClaim.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                            arrClaim.Add(new cArrayList("@prop_no", rsBA["prop_no"].ToString()));
                            arrClaim.Add(new cArrayList("@claim_no", sCLNO));
                            arrClaim.Add(new cArrayList("@Month", cbMonth.SelectedValue.ToString()));
                            arrClaim.Add(new cArrayList("@Year", cbYear.SelectedValue.ToString()));
                            bll.vInsertClaimdtlcontractKA(arrClaim);
                            arrClaim.Clear();

                            int countContractKA = 0, countCMKA = 0;
                            string sPdfName3 = ""; string sPdfName4 = ""; string sPdfName5 = ""; string sPdfName6 = ""; 
                            string sPdfName7 = ""; string sPdfName3ext = ""; 
                            string[] sPdfName3exta;
                            countContractKA = int.Parse(bll.vLookUp("select count(distinct inv_no) from tclaim_dtl where claim_no='"+sCLNO+"'").ToString());
                            countCMKA = int.Parse(bll.vLookUp("select count(distinct so_cd) from tclaim_dtl where claim_no='" + sCLNO + "'").ToString());

                            if (countContractKA <= 0) 
                            {
                                sPdfName3ext = bll.vLookUp("select fileloc from tcontract_ka_doc where contract_no=(select distinct inv_no from tclaim_dtl where claim_no='"+sCLNO+"')");
                                sPdfName3exta = sPdfName3ext.ToString().Split('.');
                                sPdfName3 = Request.Cookies["sp"].Value.ToString() + "-" + sCLNO + "-" + "P03" + "." + sPdfName3exta[1];
                            }
                            else
                            {
                                sPdfName3 = Request.Cookies["sp"].Value.ToString() + "-" + sCLNO + "-" + "P03" + ".zip";
                            }

                            sPdfName4 = Request.Cookies["sp"].Value.ToString() + "-" + sCLNO + "-" + "P35" + ".zip";
                            sPdfName5 = Request.Cookies["sp"].Value.ToString() + "-" + sCLNO + "-" + "P05" + ".zip";
                            sPdfName6 = Request.Cookies["sp"].Value.ToString() + "-" + sCLNO + "-" + "P06" + ".zip";
                            sPdfName7 = Request.Cookies["sp"].Value.ToString() + "-" + sCLNO + "-" + "P07" + ".zip";

                            sPdfName = Request.Cookies["sp"].Value.ToString() + "-" + sCLNO + "-" + "P09" + ".pdf";
                            sPdfName1 = Request.Cookies["sp"].Value.ToString() + "-" + sCLNO + "-" + "P04" + ".xls";
                            sPdfName2 = Request.Cookies["sp"].Value.ToString() + "-" + sCLNO + "-" + "P02" + ".pdf";


                            arrClaim.Clear();
                            arrClaim.Add(new cArrayList("@claim_no", sCLNO));
                            arrClaim.Add(new cArrayList("@doc_cd", "P04"));
                            arrClaim.Add(new cArrayList("@salespoint", Request.Cookies["sp"].Value.ToString()));
                            arrClaim.Add(new cArrayList("@fileloc", Request.Cookies["sp"].Value.ToString() + "-" + sCLNO + "-" + "P04" + ".xls"));
                            arrClaim.Add(new cArrayList("@doc_nm", "Summary Claim"));
                            bll.vInsClaimDoc(arrClaim);
                            arrClaim.Clear();
                            arrClaim.Add(new cArrayList("@claim_no", sCLNO));
                            arrClaim.Add(new cArrayList("@doc_cd", "P02"));
                            arrClaim.Add(new cArrayList("@salespoint", Request.Cookies["sp"].Value.ToString()));
                            arrClaim.Add(new cArrayList("@fileloc", Request.Cookies["sp"].Value.ToString() + "-" + sCLNO + "-" + "P02" + ".pdf"));
                            arrClaim.Add(new cArrayList("@doc_nm", "Proposal Approved"));
                            bll.vInsClaimDoc(arrClaim);
                            arrClaim.Clear();

                            // Generate Summary Claim 
                            arrClaim.Clear();
                            arrClaim.Add(new cArrayList("@claim_no", sCLNO));
                            arrClaim.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                            rep.vShowReportToEXCEL("rp_claimCM.rpt", arrClaim, sPath + sPdfName1);
                            arrClaim.Clear();

                            // <Start> Link Document with AGHO and CMHO 
                            arrClaim.Clear();
                            arrClaim.Add(new cArrayList("@claim_no", sCLNO));
                            arrClaim.Add(new cArrayList("@doc_cd", "P03"));
                            arrClaim.Add(new cArrayList("@salespoint", Request.Cookies["sp"].Value.ToString()));
                            arrClaim.Add(new cArrayList("@fileloc", sPdfName3));
                            arrClaim.Add(new cArrayList("@doc_nm", "Agreement"));
                            bll.vInsClaimDoc(arrClaim);
                            arrClaim.Clear();
                            string statusCM1 = "";
                            statusCM1 = bll.vLookUp("select distinct fileloc from tcreditmemo_doc a inner join tclaim_dtl b on a.cm_no=b.so_cd where a.doc_cd='CM01' and b.claim_no='"+sCLNO+"'");
                            if (statusCM1 != "")
                            {
                                arrClaim.Add(new cArrayList("@claim_no", sCLNO));
                                arrClaim.Add(new cArrayList("@doc_cd", "P35"));
                                arrClaim.Add(new cArrayList("@salespoint", Request.Cookies["sp"].Value.ToString()));
                                arrClaim.Add(new cArrayList("@fileloc", sPdfName4));
                                arrClaim.Add(new cArrayList("@doc_nm", "CCNR/Credit Memo"));
                                bll.vInsClaimDoc(arrClaim);
                            }
                            statusCM1 = "";
                            statusCM1 = bll.vLookUp("select distinct fileloc from tcreditmemo_doc a inner join tclaim_dtl b on a.cm_no=b.so_cd where a.doc_cd='CM02' and b.claim_no='"+sCLNO+"'");
                            if (statusCM1 != "")
                            {
                                arrClaim.Clear();
                                arrClaim.Add(new cArrayList("@claim_no", sCLNO));
                                arrClaim.Add(new cArrayList("@doc_cd", "P05"));
                                arrClaim.Add(new cArrayList("@salespoint", Request.Cookies["sp"].Value.ToString()));
                                arrClaim.Add(new cArrayList("@fileloc", sPdfName5));
                                arrClaim.Add(new cArrayList("@doc_nm", "Customer Statement of Acc. (Only for KA/Account Deduction)"));
                                bll.vInsClaimDoc(arrClaim);
                            }
                            statusCM1 = "";
                            statusCM1 = bll.vLookUp("select distinct fileloc from tcreditmemo_doc a inner join tclaim_dtl b on a.cm_no=b.so_cd where a.doc_cd='CM04' and b.claim_no='"+sCLNO+"'");
                            if (statusCM1 != "")
                            {
                                arrClaim.Clear();
                                arrClaim.Add(new cArrayList("@claim_no", sCLNO));
                                arrClaim.Add(new cArrayList("@doc_cd", "P06"));
                                arrClaim.Add(new cArrayList("@salespoint", Request.Cookies["sp"].Value.ToString()));
                                arrClaim.Add(new cArrayList("@fileloc", sPdfName6));
                                arrClaim.Add(new cArrayList("@doc_nm", "Copy of Cheque"));
                                bll.vInsClaimDoc(arrClaim);
                            }
                            statusCM1 = "";
                            statusCM1 = bll.vLookUp("select distinct fileloc from tcreditmemo_doc a inner join tclaim_dtl b on a.cm_no=b.so_cd where a.doc_cd='CM03' and b.claim_no='"+sCLNO+"'");
                            if (statusCM1 != "")
                            {
                                arrClaim.Clear();
                                arrClaim.Add(new cArrayList("@claim_no", sCLNO));
                                arrClaim.Add(new cArrayList("@doc_cd", "P07"));
                                arrClaim.Add(new cArrayList("@salespoint", Request.Cookies["sp"].Value.ToString()));
                                arrClaim.Add(new cArrayList("@fileloc", sPdfName7));
                                arrClaim.Add(new cArrayList("@doc_nm", "Copy of Receipt Voucher"));
                                bll.vInsClaimDoc(arrClaim);
                            }                            

                            // Agreement Zip File
                            if (countContractKA > 0)
                            {
                                System.Data.SqlClient.SqlDataReader rsAgreementClaim = null;
                                List<cArrayList> arrAgClaim = new List<cArrayList>();
                                arrAgClaim.Add(new cArrayList("@claim_no", sCLNO.ToString()));
                                bll.vGetAgreementCMByClaim(ref rsAgreementClaim, arrAgClaim);
                                using (ZipFile zip = new ZipFile())
                                {
                                    zip.AlternateEncodingUsage = ZipOption.AsNecessary;
                                    zip.AddDirectoryByName("Files");
                                    while (rsAgreementClaim.Read())
                                    {
                                        string filePath = bll.sGetControlParameter("image_path") + "/contractka_doc/" + rsAgreementClaim["fileloc"].ToString();
                                        zip.AddFile(filePath, "Files");
                                    }
                                    rsAgreementClaim.Close();
                                    arrAgClaim.Clear();
                                    zip.Save(bll.sGetControlParameter("image_path") + "/claim_doc/" + sPdfName3);
                                    //Response.Clear();
                                    //Response.BufferOutput = false;
                                    //string zipName = sPdfName3;
                                    //Response.ContentType = "application/zip";
                                    //Response.AddHeader("content-disposition", "attachment; filename=" + zipName);
                                    //zip.Save(Response.OutputStream);
                                    //Response.End();
                                }
                            }

                            System.Data.SqlClient.SqlDataReader rsCMClaim = null;
                            List<cArrayList> arrCMClaim = new List<cArrayList>();
                            arrCMClaim.Add(new cArrayList("@claim_no", sCLNO.ToString()));
                            arrCMClaim.Add(new cArrayList("@type", "CM01"));
                            bll.vGetCMByClaim(ref rsCMClaim, arrCMClaim);
                            using (ZipFile zip2 = new ZipFile())
                            {
                                zip2.AlternateEncodingUsage = ZipOption.AsNecessary;
                                zip2.AddDirectoryByName("Files");
                                while (rsCMClaim.Read())
                                {
                                    string filePath = bll.sGetControlParameter("image_path") + "/creditmemo_doc/" + rsCMClaim["fileloc"].ToString();
                                    zip2.AddFile(filePath, "Files");
                                }
                                rsCMClaim.Close();
                                arrCMClaim.Clear();
                                zip2.Save(bll.sGetControlParameter("image_path") + "/claim_doc/" + sPdfName4);
                            }
                            arrCMClaim.Clear();
                            arrCMClaim.Add(new cArrayList("@claim_no", sCLNO.ToString()));
                            arrCMClaim.Add(new cArrayList("@type", "CM02"));
                            bll.vGetCMByClaim(ref rsCMClaim, arrCMClaim);
                            using (ZipFile zip3 = new ZipFile())
                            {
                                zip3.AlternateEncodingUsage = ZipOption.AsNecessary;
                                zip3.AddDirectoryByName("Files");
                                while (rsCMClaim.Read())
                                {
                                    string filePath = bll.sGetControlParameter("image_path") + "/creditmemo_doc/" + rsCMClaim["fileloc"].ToString();
                                    zip3.AddFile(filePath, "Files");
                                }
                                rsCMClaim.Close();
                                arrCMClaim.Clear();
                                zip3.Save(bll.sGetControlParameter("image_path") + "/claim_doc/" + sPdfName5);
                            }
                            arrCMClaim.Clear();
                            arrCMClaim.Add(new cArrayList("@claim_no", sCLNO.ToString()));
                            arrCMClaim.Add(new cArrayList("@type", "CM04"));
                            bll.vGetCMByClaim(ref rsCMClaim, arrCMClaim);
                            using (ZipFile zip4 = new ZipFile())
                            {
                                zip4.AlternateEncodingUsage = ZipOption.AsNecessary;
                                zip4.AddDirectoryByName("Files");
                                while (rsCMClaim.Read())
                                {
                                    string filePath = bll.sGetControlParameter("image_path") + "/creditmemo_doc/" + rsCMClaim["fileloc"].ToString();
                                    zip4.AddFile(filePath, "Files");
                                }
                                rsCMClaim.Close();
                                arrCMClaim.Clear();
                                zip4.Save(bll.sGetControlParameter("image_path") + "/claim_doc/" + sPdfName6);
                            } 
                            arrCMClaim.Clear();
                            arrCMClaim.Add(new cArrayList("@claim_no", sCLNO.ToString()));
                            arrCMClaim.Add(new cArrayList("@type", "CM03"));
                            bll.vGetCMByClaim(ref rsCMClaim, arrCMClaim);
                            using (ZipFile zip5 = new ZipFile())
                            {
                                zip5.AlternateEncodingUsage = ZipOption.AsNecessary;
                                zip5.AddDirectoryByName("Files");
                                while (rsCMClaim.Read())
                                {
                                    string filePath = bll.sGetControlParameter("image_path") + "/creditmemo_doc/" + rsCMClaim["fileloc"].ToString();
                                    zip5.AddFile(filePath, "Files");
                                }
                                rsCMClaim.Close();
                                arrCMClaim.Clear();
                                zip5.Save(bll.sGetControlParameter("image_path") + "/claim_doc/" + sPdfName7);
                            }

                            // <End> Link Document with AGHO and CMHO 


                            //arrClaim.Add(new cArrayList("@claim_no", sCLNO));
                            //arrClaim.Add(new cArrayList("@doc_cd", "P09"));
                            //arrClaim.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                            //arrClaim.Add(new cArrayList("@fileloc", Request.Cookies["sp"].Value.ToString() + "-" + sCLNO + "-" + "P09" + ".pdf"));
                            //arrClaim.Add(new cArrayList("@doc_nm", "Claim Invoices (if any)"));
                            //bll.vInsertTclaimDoc(arrClaim);
                            

                        }
                        //rsBA.Close();
                    
                    }
                    catch (Exception ex)
                    {
                        bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : GENERATE CLAIM (CMHO)");
                        btncheck.Text = "Check";
                    }
                }


                btncheck.Text = (btncheck.Text == "Check") ? "Generate" : "Check";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Claim has been generated.',' Claim List to completed CCNR & Other Documents.','success');", true);
                return;

            }
            catch (Exception ex)
            {
                bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : GENERATE CLAIM");
                btncheck.Text = "Check";
            }
        }
    }


    protected void cbYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        int iYear = int.Parse(cbYear.SelectedValue.ToString());
        if (iYear == 2017)
        {
            cbMonth.Items.Clear();
            cbMonth.Items.Insert(0, new ListItem("January", "01"));
            cbMonth.Items.Insert(1, new ListItem("February", "02"));
            cbMonth.Items.Insert(2, new ListItem("March", "03"));
            cbMonth.Items.Insert(3, new ListItem("April", "04"));
            cbMonth.Items.Insert(4, new ListItem("May", "05"));
            cbMonth.Items.Insert(5, new ListItem("June", "06"));
            cbMonth.Items.Insert(6, new ListItem("July", "07"));
            cbMonth.Items.Insert(7, new ListItem("August", "08"));
            cbMonth.Items.Insert(8, new ListItem("September", "09"));
            cbMonth.Items.Insert(9, new ListItem("October", "10"));
            cbMonth.Items.Insert(10, new ListItem("November", "11"));
            cbMonth.Items.Insert(11, new ListItem("December", "12"));
        }
        else
        {
            cbMonth.Items.Clear();
            cbMonth.Items.Insert(0, new ListItem("January", "01"));
            cbMonth.Items.Insert(1, new ListItem("February", "02"));
            cbMonth.Items.Insert(2, new ListItem("March", "03"));
            cbMonth.Items.Insert(3, new ListItem("April", "04"));
            cbMonth.Items.Insert(4, new ListItem("May", "05"));
            cbMonth.Items.Insert(5, new ListItem("June", "06"));
            cbMonth.Items.Insert(6, new ListItem("July", "07"));
            cbMonth.Items.Insert(7, new ListItem("August", "08"));
            cbMonth.Items.Insert(8, new ListItem("September", "09"));
            cbMonth.Items.Insert(9, new ListItem("October", "10"));
            cbMonth.Items.Insert(10, new ListItem("November", "11"));
            cbMonth.Items.Insert(11, new ListItem("December", "12"));
        }
    }
}