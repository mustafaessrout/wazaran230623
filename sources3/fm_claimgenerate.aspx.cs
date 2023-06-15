using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_claimgenerate : System.Web.UI.Page
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
            lbchCndn.Text = "Not OK";
            lbchcontract.Text = "Not OK";
            lbchPrice.Text = lbchInvoice.Text;
            btncheck.Text = "Check";
            bll.vBindingComboToSp(ref cbYear, "sp_tmst_period_getbyyear", "yearvalue", "yearvalue");
            cbYear_SelectedIndexChanged(sender, e);
        }
    }
    protected void btncheck_Click(object sender, EventArgs e)
    {
        bool stInv = false, stCO = false, stCNDN = false, stBA = false;
        string stInvoice = "OK", stCashout = "OK", stCredit = "OK", stAgreement = "OK";
        int qryInv = 0, qryCO = 0, qryCN = 0, qryBA = 0;
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
            // Check Invoice 
            arrDiscount.Add(new cArrayList("@month", cbMonth.SelectedValue.ToString()));
            arrDiscount.Add(new cArrayList("@year", cbYear.SelectedValue.ToString()));
            bll.vGetDiscountList(arrDiscount, ref rsDisc);
            while (rsDisc.Read())
            {
                arrInvoice.Clear();
                arrInvoice.Add(new cArrayList("@month", cbMonth.SelectedValue.ToString()));
                arrInvoice.Add(new cArrayList("@year", cbYear.SelectedValue.ToString()));
                arrInvoice.Add(new cArrayList("@disc_cd", rsDisc["disc_cd"].ToString()));
                arrInvoice.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vGetInvoiceList(arrInvoice, ref rsInv);
                while (rsInv.Read())
                {
                    qryInv = int.Parse(bll.vLookUp("select count(*) from tclaim_confirm where inv_no = '" + rsInv["inv_no"].ToString() + "'"));
                    if (qryInv > 0) { stInvoice = "OK"; } else { stInvoice = "Not OK"; break; }
                } rsInv.Close();
            } rsDisc.Close();
            // Check CashOut
            qryCO = int.Parse(bll.vLookUp("select count(*) from tproposal_paid where month(schedule_dt)='" + cbMonth.SelectedValue.ToString() + "' and YEAR(schedule_dt)='" + cbYear.SelectedValue.ToString() + "' and claimco_sta_id not in ('A','E','L') "));
            if (qryCO > 0) { stCashout = "Not OK"; } else { stCashout = "OK"; }
            // Check CNDN 
            qryCN = int.Parse(bll.vLookUp("select count(*) from tclaim_reqcndn where month(cndn_dt)='" + cbMonth.SelectedValue.ToString() + "' and YEAR(cndn_dt)='" + cbYear.SelectedValue.ToString() + "' and cndn_sta_id not in ('A','E' )"));
            if (qryCN > 0) { stCredit = "Not OK"; } else { stCredit = "OK"; }
            // CHeck Business Agreement
            qryBA = int.Parse(bll.vLookUp("select count(*) from tmst_contract a inner join tcontract_payment b on a.contract_no=b.contract_no where month(b.payment_dt)='" + cbMonth.SelectedValue.ToString() + "' and YEAR(b.payment_dt)='" + cbYear.SelectedValue.ToString() + "' and a.approval not in ('C','R','A','N','E') "));
            if (qryBA > 0) { stAgreement = "Not OK"; } else { stAgreement = "OK"; }

            //stInvoice = "Not OK"; lbchInvoice.Text = "Not OK"; stCashout = "Not OK"; stCredit = "Not OK";
            
            lbchInvoice.Text = stInvoice;//stInvoice;
            lbchPrice.Text = lbchInvoice.Text;
            lbchCashout.Text = stCashout;
            lbchCndn.Text = stCredit;
            lbchcontract.Text = stAgreement;

            btncheck.Text = (lbchInvoice.Text == "OK" && lbchCashout.Text == "OK" && lbchCndn.Text == "OK" && lbchcontract.Text == "OK") ? "Generate" : "Generate";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "cl", "vDisableShow();", true);
            //return;          

            //lbchInvoice.Text = "Not OK";//stInvoice;
            //lbchPrice.Text = "Not OK";
            //lbchCashout.Text = "Not OK";
            //lbchCndn.Text = "Not OK";



        }
        else
        {
            //if (lbchInvoice.Text == "OK" && lbchCashout.Text == "OK" && lbchCndn.Text == "OK" && lbchcontract.Text == "OK")
            //{
                try 
                {
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
                    if (lbchInvoice.Text == "OK")
                    {
                        // Generate TO & CO 
                        try { 
                        bll.vGetDiscountList(arrDiscount, ref rsDisc);
                        while (rsDisc.Read())
                        {
                            arrClaim.Clear();
                            string sCLNO = "", claim_typ = "";
                            arrClaim.Add(new cArrayList("@claim_dt", DateTime.ParseExact(bll.vLookUp("select parm_valu from tcontrol_parameter where parm_nm='wazaran_dt'"), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                            arrClaim.Add(new cArrayList("@prop_no", rsDisc["proposal"].ToString()));
                            arrClaim.Add(new cArrayList("@ccnr_no", null));
                            if ((bll.vLookUp("select rdcost from tmst_proposal where prop_no='" + rsDisc["proposal"].ToString() + "'")) == "branch")
                            {
                                arrClaim.Add(new cArrayList("@claim_sta_id", "L"));
                            }
                            else
                            {
                                arrClaim.Add(new cArrayList("@claim_sta_id", "N"));
                            }

                            if (rsDisc["discount_mec"].ToString() == "FG")
                            {
                                arrClaim.Add(new cArrayList("@ordervalue", Convert.ToDouble("0")));
                                arrClaim.Add(new cArrayList("@orderqty", 1 * Convert.ToDouble(bll.vLookUp("exec sp_tot_claim_fg '" + rsDisc["disc_cd"].ToString() + "','" + cbMonth.SelectedValue.ToString() + "','" + cbYear.SelectedValue.ToString() + "','" + Request.Cookies["sp"].Value.ToString() + "','total'"))));
                                arrClaim.Add(new cArrayList("@freevalue", Convert.ToDouble("0")));
                                arrClaim.Add(new cArrayList("@freeqty", 1 * Convert.ToDouble(bll.vLookUp("exec sp_tot_claim_fg '" + rsDisc["disc_cd"].ToString() + "','" + cbMonth.SelectedValue.ToString() + "','" + cbYear.SelectedValue.ToString() + "','" + Request.Cookies["sp"].Value.ToString() + "','qty'"))));
                                arrClaim.Add(new cArrayList("@price_avg", Convert.ToDouble(bll.vLookUp("exec sp_tot_claim_fg '" + rsDisc["disc_cd"].ToString() + "','" + cbMonth.SelectedValue.ToString() + "','" + cbYear.SelectedValue.ToString() + "','" + Request.Cookies["sp"].Value.ToString() + "','avg'"))));
                            }
                            else
                            {
                                arrClaim.Add(new cArrayList("@ordervalue", 1 * Convert.ToDouble(bll.vLookUp("exec sp_tot_claim_ch '" + rsDisc["disc_cd"].ToString() + "','" + cbMonth.SelectedValue.ToString() + "','" + cbYear.SelectedValue.ToString() + "','" + Request.Cookies["sp"].Value.ToString() + "','total'"))));
                                arrClaim.Add(new cArrayList("@orderqty", Convert.ToDouble("0")));
                                arrClaim.Add(new cArrayList("@freevalue", 1 * Convert.ToDouble(bll.vLookUp("exec sp_tot_claim_ch '" + rsDisc["disc_cd"].ToString() + "','" + cbMonth.SelectedValue.ToString() + "','" + cbYear.SelectedValue.ToString() + "','" + Request.Cookies["sp"].Value.ToString() + "','qty'"))));
                                arrClaim.Add(new cArrayList("@freeqty", Convert.ToDouble("0")));
                                arrClaim.Add(new cArrayList("@price_avg", Convert.ToDouble("0")));
                            }
                            arrClaim.Add(new cArrayList("@sendtoho_dt", DateTime.ParseExact(bll.vLookUp("select parm_valu from tcontrol_parameter where parm_nm='wazaran_dt'"), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                            arrClaim.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                            arrClaim.Add(new cArrayList("@cby", Request.Cookies["usr_id"].Value.ToString()));
                            arrClaim.Add(new cArrayList("@txMonth", cbMonth.SelectedValue.ToString()));
                            arrClaim.Add(new cArrayList("@txYear", cbYear.SelectedValue.ToString()));
                            arrClaim.Add(new cArrayList("@disc_cd", rsDisc["disc_cd"].ToString()));
                            arrClaim.Add(new cArrayList("@discount_mec", rsDisc["discount_mec"].ToString()));
                            bll.vInsertClaim(arrClaim, ref sCLNO);
                            arrClaim.Clear();
                            arrClaim.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                            arrClaim.Add(new cArrayList("@disc_cd", rsDisc["disc_cd"].ToString()));
                            arrClaim.Add(new cArrayList("@claim_no", sCLNO));
                            arrClaim.Add(new cArrayList("@Month", cbMonth.SelectedValue.ToString()));
                            arrClaim.Add(new cArrayList("@Year", cbYear.SelectedValue.ToString()));
                            bll.vInsertClaimdtlfreeso(arrClaim);
                            bll.vInsertClaimdtlfreeco(arrClaim);
                            bll.vInsertClaimdtlcashso(arrClaim);
                            bll.vInsertClaimdtlcashco(arrClaim);
                            arrClaim.Clear();

                            sPdfName = Request.Cookies["sp"].Value.ToString() + "-" + sCLNO + "-" + "P09" + ".pdf";
                            sPdfName1 = Request.Cookies["sp"].Value.ToString() + "-" + sCLNO + "-" + "P04" + ".xls";
                            sPdfName2 = Request.Cookies["sp"].Value.ToString() + "-" + sCLNO + "-" + "P02" + ".pdf";

                            arrClaim.Add(new cArrayList("@claim_no", sCLNO));
                            arrClaim.Add(new cArrayList("@doc_cd", "P09"));
                            arrClaim.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                            arrClaim.Add(new cArrayList("@fileloc", Request.Cookies["sp"].Value.ToString() + "-" + sCLNO + "-" + "P09" + ".pdf"));
                            arrClaim.Add(new cArrayList("@doc_nm", "Claim Invoices (if any)"));
                            bll.vInsertTclaimDoc(arrClaim);
                            arrClaim.Clear();
                            arrClaim.Add(new cArrayList("@claim_no", sCLNO));
                            arrClaim.Add(new cArrayList("@doc_cd", "P04"));
                            arrClaim.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                            arrClaim.Add(new cArrayList("@fileloc", Request.Cookies["sp"].Value.ToString() + "-" + sCLNO + "-" + "P04" + ".xls"));
                            arrClaim.Add(new cArrayList("@doc_nm", "Summary Claim"));
                            bll.vInsertTclaimDoc(arrClaim);
                            arrClaim.Clear();
                            arrClaim.Add(new cArrayList("@claim_no", sCLNO));
                            arrClaim.Add(new cArrayList("@doc_cd", "P02"));
                            arrClaim.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                            arrClaim.Add(new cArrayList("@fileloc", Request.Cookies["sp"].Value.ToString() + "-" + sCLNO + "-" + "P02" + ".pdf"));
                            arrClaim.Add(new cArrayList("@doc_nm", "Proposal Approved"));
                            bll.vInsertTclaimDoc(arrClaim);
                            arrClaim.Clear();
                            arrClaim.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                            arrClaim.Add(new cArrayList("@claim_no", sCLNO));
                            bll.vUpdateTmstclaim(arrClaim);
                            arrClaim.Clear();
                            arrClaim.Add(new cArrayList("@claim_no", sCLNO));
                            arrClaim.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                            arrClaim.Add(new cArrayList("@rp_typ", null));
                            rep.vShowReportToPDF("rp_claim_invoice.rpt", arrClaim, sPath + sPdfName);
                            arrClaim.Clear();
                            arrClaim.Add(new cArrayList("@claim_no", sCLNO));
                            arrClaim.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                            claim_typ = bll.vLookUp("select distinct disc_typ from tclaim_dtl where claim_no='" + sCLNO + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
                            if (claim_typ == "FG")
                            {
                                rep.vShowReportToEXCEL("rp_claimFG.rpt", arrClaim, sPath + sPdfName1);
                            }
                            else if (claim_typ == "CH")
                            {
                                rep.vShowReportToEXCEL("rp_claimDC.rpt", arrClaim, sPath + sPdfName1);
                            }
                            arrClaim.Clear();

                            //string prop_get = bll.vLookUp("select prop_no from tmst_claim where claim_no='" + sCLNO + "'");
                            //string prop_cust = bll.vLookUp("select rdcust from tmst_proposal where prop_no = '" + prop_get + "'");
                            //string prop_item = bll.vLookUp("select rditem from tmst_proposal where prop_no = '" + prop_get + "'");

                            //arrClaim.Add(new cArrayList("@prop_no", prop_get));
                            //arrClaim.Add(new cArrayList("@cust", prop_cust));
                            //arrClaim.Add(new cArrayList("@product", prop_item));
                            //arrClaim.Add(new cArrayList("@salespoint", null));
                            //arrClaim.Add(new cArrayList("@dbp", "no"));
                            //arrClaim.Add(new cArrayList("@vendor", null));
                            //arrClaim.Add(new cArrayList("@cost", null));
                            //arrClaim.Add(new cArrayList("@promo", null));
                            //arrClaim.Add(new cArrayList("@year", null));
                            //arrClaim.Add(new cArrayList("@month", null));
                            //rep.vShowReportToPDF("rp_proposal.rpt", arrClaim, sPath + sPdfName2);
                            //arrClaim.Clear();
                        }
                        //rsDisc.Close();
                        }
                        catch (Exception ex)
                        {
                            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : GENERATE CLAIM (INVOICE)");
                            btncheck.Text = "Check";
                        }
                    }
                    if (lbchPrice.Text == "OK")
                    {
                        // Generate Different Price Adjustment 
                        try
                        {
                            string periode = "";

                            periode = bll.vLookUp("select 'true' from tcontrol_parameter where parm_nm='wazaran_dt' and  CONVERT(char(2), cast((convert(date,parm_valu,103)) as datetime), 101) = '"+cbMonth.SelectedValue.ToString()+"' and year(convert(date,parm_valu,103))='"+cbYear.SelectedValue.ToString()+"'");

                            if (periode == "")
                            {
                                bll.vGetDifferentPriceAdjList(arr, ref rsPrice);
                                while (rsPrice.Read())
                                {
                                    arrClaim.Clear();
                                    string sCLNO = "", claim_typ = "";
                                    arrClaim.Add(new cArrayList("@claim_dt", DateTime.ParseExact(bll.vLookUp("select parm_valu from tcontrol_parameter where parm_nm='wazaran_dt'"), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                                    arrClaim.Add(new cArrayList("@prop_no", rsPrice["prop_no"].ToString()));
                                    arrClaim.Add(new cArrayList("@ccnr_no", null));
                                    if ((bll.vLookUp("select rdcost from tmst_proposal where prop_no='" + rsPrice["prop_no"].ToString() + "'")) == "branch")
                                    {
                                        arrClaim.Add(new cArrayList("@claim_sta_id", "L"));
                                    }
                                    else
                                    {
                                        arrClaim.Add(new cArrayList("@claim_sta_id", "N"));
                                    }

                                    arrClaim.Add(new cArrayList("@ordervalue", Convert.ToDouble("0")));
                                    arrClaim.Add(new cArrayList("@orderqty", Convert.ToDouble("0")));
                                    arrClaim.Add(new cArrayList("@freevalue", Convert.ToDouble("0")));
                                    arrClaim.Add(new cArrayList("@freeqty", Convert.ToDouble("0")));
                                    arrClaim.Add(new cArrayList("@price_avg", Convert.ToDouble("0")));
                                    
                                    arrClaim.Add(new cArrayList("@sendtoho_dt", DateTime.ParseExact(bll.vLookUp("select parm_valu from tcontrol_parameter where parm_nm='wazaran_dt'"), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                                    arrClaim.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                                    arrClaim.Add(new cArrayList("@cby", Request.Cookies["usr_id"].Value.ToString()));
                                    arrClaim.Add(new cArrayList("@txMonth", cbMonth.SelectedValue.ToString()));
                                    arrClaim.Add(new cArrayList("@txYear", cbYear.SelectedValue.ToString()));
                                    arrClaim.Add(new cArrayList("@disc_cd", ""));
                                    arrClaim.Add(new cArrayList("@discount_mec", "CH"));
                                    bll.vInsertClaim(arrClaim, ref sCLNO);
                                    arrClaim.Clear();
                                    arrClaim.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                                    arrClaim.Add(new cArrayList("@prop_no", rsPrice["prop_no"].ToString()));
                                    arrClaim.Add(new cArrayList("@claim_no", sCLNO));
                                    arrClaim.Add(new cArrayList("@Month", cbMonth.SelectedValue.ToString()));
                                    arrClaim.Add(new cArrayList("@Year", cbYear.SelectedValue.ToString()));
                                    bll.vInsertClaimdtlprice(arrClaim);
                                    arrClaim.Clear();

                                    sPdfName = Request.Cookies["sp"].Value.ToString() + "-" + sCLNO + "-" + "P09" + ".pdf";
                                    sPdfName1 = Request.Cookies["sp"].Value.ToString() + "-" + sCLNO + "-" + "P04" + ".xls";
                                    sPdfName2 = Request.Cookies["sp"].Value.ToString() + "-" + sCLNO + "-" + "P02" + ".pdf";

                                    arrClaim.Add(new cArrayList("@claim_no", sCLNO));
                                    arrClaim.Add(new cArrayList("@doc_cd", "P09"));
                                    arrClaim.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                                    arrClaim.Add(new cArrayList("@fileloc", Request.Cookies["sp"].Value.ToString() + "-" + sCLNO + "-" + "P09" + ".pdf"));
                                    arrClaim.Add(new cArrayList("@doc_nm", "Claim Invoices (if any)"));
                                    bll.vInsertTclaimDoc(arrClaim);
                                    arrClaim.Clear();
                                    arrClaim.Add(new cArrayList("@claim_no", sCLNO));
                                    arrClaim.Add(new cArrayList("@doc_cd", "P04"));
                                    arrClaim.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                                    arrClaim.Add(new cArrayList("@fileloc", Request.Cookies["sp"].Value.ToString() + "-" + sCLNO + "-" + "P04" + ".xls"));
                                    arrClaim.Add(new cArrayList("@doc_nm", "Summary Claim"));
                                    bll.vInsertTclaimDoc(arrClaim);
                                    arrClaim.Clear();
                                    arrClaim.Add(new cArrayList("@claim_no", sCLNO));
                                    arrClaim.Add(new cArrayList("@doc_cd", "P02"));
                                    arrClaim.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                                    arrClaim.Add(new cArrayList("@fileloc", Request.Cookies["sp"].Value.ToString() + "-" + sCLNO + "-" + "P02" + ".pdf"));
                                    arrClaim.Add(new cArrayList("@doc_nm", "Proposal Approved"));
                                    bll.vInsertTclaimDoc(arrClaim);
                                    arrClaim.Clear();
                                    arrClaim.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                                    arrClaim.Add(new cArrayList("@claim_no", sCLNO));
                                    bll.vUpdateTmstclaim(arrClaim);
                                    arrClaim.Clear();
                                    arrClaim.Add(new cArrayList("@claim_no", sCLNO));
                                    arrClaim.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                                    arrClaim.Add(new cArrayList("@rp_typ", null));
                                    rep.vShowReportToPDF("rp_claim_invoice.rpt", arrClaim, sPath + sPdfName);
                                    arrClaim.Clear();
                                    arrClaim.Add(new cArrayList("@claim_no", sCLNO));
                                    arrClaim.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                                    claim_typ = bll.vLookUp("select distinct disc_typ from tclaim_dtl where claim_no='" + sCLNO + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
                                    if (claim_typ == "FG")
                                    {
                                        rep.vShowReportToEXCEL("rp_claimFG.rpt", arrClaim, sPath + sPdfName1);
                                    }
                                    else if (claim_typ == "CH")
                                    {
                                        rep.vShowReportToEXCEL("rp_claimDC.rpt", arrClaim, sPath + sPdfName1);
                                    }
                                    arrClaim.Clear();
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : GENERATE CLAIM (PRICE)");
                            btncheck.Text = "Check";
                        }
                    }
                    if (lbchCashout.Text == "OK")
                    {
                        // Generate Cashout 
                        try { 
                            string periode = "";

                            periode = bll.vLookUp("select 'true' from tcontrol_parameter where parm_nm='wazaran_dt' and  CONVERT(char(2), cast((convert(date,parm_valu,103)) as datetime), 101) = '"+cbMonth.SelectedValue.ToString()+"' and year(convert(date,parm_valu,103))='"+cbYear.SelectedValue.ToString()+"'");

                            if (periode == "")
                            {
                                bll.vGetCashoutList(arr, ref rsCashOut);
                                while (rsCashOut.Read())
                                {
                                    arrClaim.Clear();
                                    string sCLNO = "", claim_typ = "";
                                    arrClaim.Add(new cArrayList("@claim_dt", DateTime.ParseExact(bll.vLookUp("select parm_valu from tcontrol_parameter where parm_nm='wazaran_dt'"), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                                    arrClaim.Add(new cArrayList("@prop_no", rsCashOut["prop_no"].ToString()));
                                    arrClaim.Add(new cArrayList("@ccnr_no", null));
                                    if ((bll.vLookUp("select rdcost from tmst_proposal where prop_no='" + rsCashOut["prop_no"].ToString() + "'")) == "branch")
                                    {
                                        arrClaim.Add(new cArrayList("@claim_sta_id", "L"));
                                    }
                                    else
                                    {
                                        arrClaim.Add(new cArrayList("@claim_sta_id", "N"));
                                    }
                                    arrClaim.Add(new cArrayList("@ordervalue", rsCashOut["amount"].ToString()));
                                    arrClaim.Add(new cArrayList("@orderqty", 0.00));
                                    arrClaim.Add(new cArrayList("@freevalue", 0.00));
                                    arrClaim.Add(new cArrayList("@freeqty", 0.00));
                                    arrClaim.Add(new cArrayList("@price_avg", 0.00));
                                    arrClaim.Add(new cArrayList("@sendtoho_dt", DateTime.ParseExact(bll.vLookUp("select parm_valu from tcontrol_parameter where parm_nm='wazaran_dt'"), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                                    arrClaim.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                                    arrClaim.Add(new cArrayList("@cby", Request.Cookies["usr_id"].Value.ToString()));
                                    arrClaim.Add(new cArrayList("@txMonth", cbMonth.SelectedValue.ToString()));
                                    arrClaim.Add(new cArrayList("@txYear", cbYear.SelectedValue.ToString()));
                                    arrClaim.Add(new cArrayList("@disc_cd", ""));
                                    arrClaim.Add(new cArrayList("@discount_mec", "CSH"));
                                    bll.vInsertClaim(arrClaim, ref sCLNO);
                                    arrClaim.Clear();
                                    arrClaim.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                                    arrClaim.Add(new cArrayList("@prop_no", rsCashOut["prop_no"].ToString()));
                                    arrClaim.Add(new cArrayList("@claim_no", sCLNO));
                                    arrClaim.Add(new cArrayList("@Month", cbMonth.SelectedValue.ToString()));
                                    arrClaim.Add(new cArrayList("@Year", cbYear.SelectedValue.ToString()));
                                    bll.vInsertClaimdtlcashout(arrClaim);
                                    arrClaim.Clear();

                                    sPdfName = Request.Cookies["sp"].Value.ToString() + "-" + sCLNO + "-" + "P09" + ".pdf";
                                    sPdfName1 = Request.Cookies["sp"].Value.ToString() + "-" + sCLNO + "-" + "P04" + ".xls";
                                    sPdfName2 = Request.Cookies["sp"].Value.ToString() + "-" + sCLNO + "-" + "P02" + ".pdf";

                                    arrClaim.Clear();
                                    arrClaim.Add(new cArrayList("@claim_no", sCLNO));
                                    arrClaim.Add(new cArrayList("@doc_cd", "P04"));
                                    arrClaim.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                                    arrClaim.Add(new cArrayList("@fileloc", Request.Cookies["sp"].Value.ToString() + "-" + sCLNO + "-" + "P04" + ".xls"));
                                    arrClaim.Add(new cArrayList("@doc_nm", "Summary Claim"));
                                    bll.vInsertTclaimDoc(arrClaim);
                                    arrClaim.Clear();
                                    arrClaim.Add(new cArrayList("@claim_no", sCLNO));
                                    arrClaim.Add(new cArrayList("@doc_cd", "P02"));
                                    arrClaim.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                                    arrClaim.Add(new cArrayList("@fileloc", Request.Cookies["sp"].Value.ToString() + "-" + sCLNO + "-" + "P02" + ".pdf"));
                                    arrClaim.Add(new cArrayList("@doc_nm", "Proposal Approved"));
                                    bll.vInsertTclaimDoc(arrClaim);
                                    arrClaim.Clear();
                                    arrClaim.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                                    arrClaim.Add(new cArrayList("@claim_no", sCLNO));
                                    bll.vUpdateTmstclaim(arrClaim);
                                    arrClaim.Clear();
                                    arrClaim.Clear();
                                    arrClaim.Add(new cArrayList("@claim_no", sCLNO));
                                    arrClaim.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                                    arrClaim.Add(new cArrayList("@customer", null));
                                    rep.vShowReportToEXCEL("rp_claimCashOut.rpt", arrClaim, sPath + sPdfName1);
                                    arrClaim.Clear();

                                    //string prop_get = bll.vLookUp("select prop_no from tmst_claim where claim_no='" + sCLNO + "'");
                                    //string prop_cust = bll.vLookUp("select rdcust from tmst_proposal where prop_no = '" + prop_get + "'");
                                    //string prop_item = bll.vLookUp("select rditem from tmst_proposal where prop_no = '" + prop_get + "'");

                                    //arrClaim.Add(new cArrayList("@prop_no", prop_get));
                                    //arrClaim.Add(new cArrayList("@cust", prop_cust));
                                    //arrClaim.Add(new cArrayList("@product", prop_item));
                                    //arrClaim.Add(new cArrayList("@salespoint", null));
                                    //arrClaim.Add(new cArrayList("@dbp", "no"));
                                    //arrClaim.Add(new cArrayList("@vendor", null));
                                    //arrClaim.Add(new cArrayList("@cost", null));
                                    //arrClaim.Add(new cArrayList("@promo", null));
                                    //arrClaim.Add(new cArrayList("@year", null));
                                    //arrClaim.Add(new cArrayList("@month", null));
                                    //rep.vShowReportToPDF("rp_proposal.rpt", arrClaim, sPath + sPdfName2);
                                    //arrClaim.Clear();
                                }
                                //rsCashOut.Close();
                            }
                        }
                        catch (Exception ex)
                        {
                            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : GENERATE CLAIM (CASHOUT)");
                            btncheck.Text = "Check";
                        }
                    }
                    if (lbchCndn.Text == "OK")
                    {
                        // Generate CNDN 
                        try
                        {
                            string periode = "";

                            periode = bll.vLookUp("select 'true' from tcontrol_parameter where parm_nm='wazaran_dt' and  CONVERT(char(2), cast((convert(date,parm_valu,103)) as datetime), 101) = '" + cbMonth.SelectedValue.ToString() + "' and year(convert(date,parm_valu,103))='" + cbYear.SelectedValue.ToString() + "'");

                            if (periode == "")
                            {

                                bll.vGetCNDNList(arr, ref rsCNDN);
                                while (rsCNDN.Read())
                                {
                                    arrClaim.Clear();
                                    string sCLNO = "", claim_typ = "";
                                    arrClaim.Add(new cArrayList("@claim_dt", DateTime.ParseExact(bll.vLookUp("select parm_valu from tcontrol_parameter where parm_nm='wazaran_dt'"), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                                    arrClaim.Add(new cArrayList("@prop_no", rsCNDN["prop_no"].ToString()));
                                    arrClaim.Add(new cArrayList("@ccnr_no", null));
                                    if ((bll.vLookUp("select rdcost from tmst_proposal where prop_no='" + rsCNDN["prop_no"].ToString() + "'")) == "branch")
                                    {
                                        arrClaim.Add(new cArrayList("@claim_sta_id", "L"));
                                    }
                                    else
                                    {
                                        arrClaim.Add(new cArrayList("@claim_sta_id", "N"));
                                    }
                                    arrClaim.Add(new cArrayList("@ordervalue", rsCNDN["amount"].ToString()));
                                    arrClaim.Add(new cArrayList("@orderqty", 0.00));
                                    arrClaim.Add(new cArrayList("@freevalue", 0.00));
                                    arrClaim.Add(new cArrayList("@freeqty", 0.00));
                                    arrClaim.Add(new cArrayList("@price_avg", 0.00));
                                    arrClaim.Add(new cArrayList("@sendtoho_dt", DateTime.ParseExact(bll.vLookUp("select parm_valu from tcontrol_parameter where parm_nm='wazaran_dt'"), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                                    arrClaim.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                                    arrClaim.Add(new cArrayList("@cby", Request.Cookies["usr_id"].Value.ToString()));
                                    arrClaim.Add(new cArrayList("@txMonth", cbMonth.SelectedValue.ToString()));
                                    arrClaim.Add(new cArrayList("@txYear", cbYear.SelectedValue.ToString()));
                                    arrClaim.Add(new cArrayList("@disc_cd", ""));
                                    arrClaim.Add(new cArrayList("@discount_mec", "CNDN"));
                                    bll.vInsertClaim(arrClaim, ref sCLNO);
                                    arrClaim.Clear();
                                    arrClaim.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                                    arrClaim.Add(new cArrayList("@prop_no", rsCNDN["prop_no"].ToString()));
                                    arrClaim.Add(new cArrayList("@claim_no", sCLNO));
                                    arrClaim.Add(new cArrayList("@month", cbMonth.SelectedValue.ToString()));
                                    arrClaim.Add(new cArrayList("@year", cbYear.SelectedValue.ToString()));
                                    bll.vInsertClaimdtlcndn(arrClaim);
                                    arrClaim.Clear();

                                    sPdfName = Request.Cookies["sp"].Value.ToString() + "-" + sCLNO + "-" + "P09" + ".pdf";
                                    sPdfName1 = Request.Cookies["sp"].Value.ToString() + "-" + sCLNO + "-" + "P04" + ".xls";
                                    sPdfName2 = Request.Cookies["sp"].Value.ToString() + "-" + sCLNO + "-" + "P02" + ".pdf";

                                    arrClaim.Clear();
                                    arrClaim.Add(new cArrayList("@claim_no", sCLNO));
                                    arrClaim.Add(new cArrayList("@doc_cd", "P04"));
                                    arrClaim.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                                    arrClaim.Add(new cArrayList("@fileloc", Request.Cookies["sp"].Value.ToString() + "-" + sCLNO + "-" + "P04" + ".xls"));
                                    arrClaim.Add(new cArrayList("@doc_nm", "Summary Claim"));
                                    bll.vInsertTclaimDoc(arrClaim);
                                    arrClaim.Clear();
                                    arrClaim.Add(new cArrayList("@claim_no", sCLNO));
                                    arrClaim.Add(new cArrayList("@doc_cd", "P02"));
                                    arrClaim.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                                    arrClaim.Add(new cArrayList("@fileloc", Request.Cookies["sp"].Value.ToString() + "-" + sCLNO + "-" + "P02" + ".pdf"));
                                    arrClaim.Add(new cArrayList("@doc_nm", "Proposal Approved"));
                                    bll.vInsertTclaimDoc(arrClaim);
                                    arrClaim.Clear();
                                    arrClaim.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                                    arrClaim.Add(new cArrayList("@claim_no", sCLNO));
                                    bll.vUpdateTmstclaim(arrClaim);
                                    arrClaim.Clear();
                                    arrClaim.Add(new cArrayList("@claim_no", sCLNO));
                                    arrClaim.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                                    rep.vShowReportToPDF("rp_claimsumcndn.rpt", arrClaim, sPath + sPdfName1);
                                    arrClaim.Clear();

                                    //string prop_get = bll.vLookUp("select prop_no from tmst_claim where claim_no='" + sCLNO + "'");
                                    //string prop_cust = bll.vLookUp("select rdcust from tmst_proposal where prop_no = '" + prop_get + "'");
                                    //string prop_item = bll.vLookUp("select rditem from tmst_proposal where prop_no = '" + prop_get + "'");

                                    //arrClaim.Add(new cArrayList("@prop_no", prop_get));
                                    //arrClaim.Add(new cArrayList("@cust", prop_cust));
                                    //arrClaim.Add(new cArrayList("@product", prop_item));
                                    //arrClaim.Add(new cArrayList("@salespoint", null));
                                    //arrClaim.Add(new cArrayList("@dbp", "no"));
                                    //arrClaim.Add(new cArrayList("@vendor", null));
                                    //arrClaim.Add(new cArrayList("@cost", null));
                                    //arrClaim.Add(new cArrayList("@promo", null));
                                    //arrClaim.Add(new cArrayList("@year", null));
                                    //arrClaim.Add(new cArrayList("@month", null));
                                    //rep.vShowReportToPDF("rp_proposal.rpt", arrClaim, sPath + sPdfName2);
                                    //arrClaim.Clear();
                                }
                                //rsCNDN.Close();
                            }
                        }
                        catch (Exception ex)
                        {
                            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : GENERATE CLAIM (CNDN)");
                            btncheck.Text = "Check";
                        }
                    }
                    if (lbchcontract.Text == "OK")
                    {
                        //Genereate Business Agreement 
                        try { 
                        string periode = "";

                        periode = bll.vLookUp("select 'true' from tcontrol_parameter where parm_nm='wazaran_dt' and  CONVERT(char(2), cast((convert(date,parm_valu,103)) as datetime), 101) = '"+cbMonth.SelectedValue.ToString()+"' and year(convert(date,parm_valu,103))='"+cbYear.SelectedValue.ToString()+"'");

                        //periode = "";

                        if (periode == "")
                        {
                            bll.vGetBAList(arr, ref rsBA);
                            while (rsBA.Read())
                            {
                                arrClaim.Clear();
                                string sCLNO = "", claim_typ = "";
                                arrClaim.Add(new cArrayList("@claim_dt", DateTime.ParseExact(bll.vLookUp("select parm_valu from tcontrol_parameter where parm_nm='wazaran_dt'"), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
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
                                arrClaim.Add(new cArrayList("@ordervalue", 0.00));
                                arrClaim.Add(new cArrayList("@orderqty", 0.00));
                                arrClaim.Add(new cArrayList("@freevalue", 0.00));
                                arrClaim.Add(new cArrayList("@freeqty", 1 * Convert.ToDouble(bll.vLookUp("exec sp_tot_contract_fg '" + rsBA["prop_no"].ToString() + "','" + cbMonth.SelectedValue.ToString() + "','" + cbYear.SelectedValue.ToString() + "','" + Request.Cookies["sp"].Value.ToString() + "','qty'"))));
                                arrClaim.Add(new cArrayList("@price_avg", 0.00));
                                arrClaim.Add(new cArrayList("@sendtoho_dt", DateTime.ParseExact(bll.vLookUp("select parm_valu from tcontrol_parameter where parm_nm='wazaran_dt'"), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                                arrClaim.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                                arrClaim.Add(new cArrayList("@cby", Request.Cookies["usr_id"].Value.ToString()));
                                arrClaim.Add(new cArrayList("@txMonth", cbMonth.SelectedValue.ToString()));
                                arrClaim.Add(new cArrayList("@txYear", cbYear.SelectedValue.ToString()));
                                arrClaim.Add(new cArrayList("@disc_cd", ""));
                                arrClaim.Add(new cArrayList("@discount_mec", "BA"));
                                bll.vInsertClaim(arrClaim, ref sCLNO);
                                arrClaim.Clear();
                                arrClaim.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                                arrClaim.Add(new cArrayList("@prop_no", rsBA["prop_no"].ToString()));
                                arrClaim.Add(new cArrayList("@claim_no", sCLNO));
                                arrClaim.Add(new cArrayList("@Month", cbMonth.SelectedValue.ToString()));
                                arrClaim.Add(new cArrayList("@Year", cbYear.SelectedValue.ToString()));
                                bll.vInsertClaimdtlcontract(arrClaim);
                                arrClaim.Clear();

                                sPdfName = Request.Cookies["sp"].Value.ToString() + "-" + sCLNO + "-" + "P09" + ".pdf";
                                sPdfName1 = Request.Cookies["sp"].Value.ToString() + "-" + sCLNO + "-" + "P04" + ".xls";
                                sPdfName2 = Request.Cookies["sp"].Value.ToString() + "-" + sCLNO + "-" + "P02" + ".pdf";

                                arrClaim.Add(new cArrayList("@claim_no", sCLNO));
                                arrClaim.Add(new cArrayList("@doc_cd", "P09"));
                                arrClaim.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                                arrClaim.Add(new cArrayList("@fileloc", Request.Cookies["sp"].Value.ToString() + "-" + sCLNO + "-" + "P09" + ".pdf"));
                                arrClaim.Add(new cArrayList("@doc_nm", "Claim Invoices (if any)"));
                                bll.vInsertTclaimDoc(arrClaim);
                                arrClaim.Clear();
                                arrClaim.Add(new cArrayList("@claim_no", sCLNO));
                                arrClaim.Add(new cArrayList("@doc_cd", "P04"));
                                arrClaim.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                                arrClaim.Add(new cArrayList("@fileloc", Request.Cookies["sp"].Value.ToString() + "-" + sCLNO + "-" + "P04" + ".xls"));
                                arrClaim.Add(new cArrayList("@doc_nm", "Summary Claim"));
                                bll.vInsertTclaimDoc(arrClaim);
                                arrClaim.Clear();
                                arrClaim.Add(new cArrayList("@claim_no", sCLNO));
                                arrClaim.Add(new cArrayList("@doc_cd", "P02"));
                                arrClaim.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                                arrClaim.Add(new cArrayList("@fileloc", Request.Cookies["sp"].Value.ToString() + "-" + sCLNO + "-" + "P02" + ".pdf"));
                                arrClaim.Add(new cArrayList("@doc_nm", "Proposal Approved"));
                                bll.vInsertTclaimDoc(arrClaim);
                                arrClaim.Clear();
                                arrClaim.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                                arrClaim.Add(new cArrayList("@claim_no", sCLNO));
                                bll.vUpdateTmstclaim(arrClaim);
                                arrClaim.Clear();
                                arrClaim.Add(new cArrayList("@claim_no", sCLNO));
                                arrClaim.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                                arrClaim.Add(new cArrayList("@rp_typ", null));
                                rep.vShowReportToPDF("rp_claim_invoice.rpt", arrClaim, sPath + sPdfName);
                                arrClaim.Clear();
                                arrClaim.Clear();
                                arrClaim.Add(new cArrayList("@claim_no", sCLNO));
                                arrClaim.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                                rep.vShowReportToEXCEL("rp_claimFG.rpt", arrClaim, sPath + sPdfName1);
                                arrClaim.Clear();

                                //string prop_get = bll.vLookUp("select prop_no from tmst_claim where claim_no='" + sCLNO + "'");
                                //string prop_cust = bll.vLookUp("select rdcust from tmst_proposal where prop_no = '" + prop_get + "'");
                                //string prop_item = bll.vLookUp("select rditem from tmst_proposal where prop_no = '" + prop_get + "'");

                                //arrClaim.Add(new cArrayList("@prop_no", prop_get));
                                //arrClaim.Add(new cArrayList("@cust", prop_cust));
                                //arrClaim.Add(new cArrayList("@product", prop_item));
                                //arrClaim.Add(new cArrayList("@salespoint", null));
                                //arrClaim.Add(new cArrayList("@dbp", "no"));
                                //arrClaim.Add(new cArrayList("@vendor", null));
                                //arrClaim.Add(new cArrayList("@cost", null));
                                //arrClaim.Add(new cArrayList("@promo", null));
                                //arrClaim.Add(new cArrayList("@year", null));
                                //arrClaim.Add(new cArrayList("@month", null));
                                //rep.vShowReportToPDF("rp_proposal.rpt", arrClaim, sPath + sPdfName2);
                                //arrClaim.Clear();
                            }
                            //rsBA.Close();
                        }
                        }
                        catch (Exception ex)
                        {
                            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : GENERATE CLAIM (BA)");
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
            //}
            //else
            //{
            //    btncheck.Text = (btncheck.Text == "Check") ? "Generate" : "Check";
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('All Status Must be OK.','Check Daily Confirm.','error');", true);
            //    return;
            //}
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