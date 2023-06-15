using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.IO;
using System.Data.SqlClient;


public partial class fm_claimEntry : System.Web.UI.Page
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

    double dubtotamtba = 0;
    double dubtotqtyba = 0;

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
            lbltotcashoutdesc.Visible = false;
            lbltotcashout.Visible = false;
            bll.vBindingComboToSp(ref ddYear, "sp_tmst_period_getbyyear", "yearvalue", "yearvalue");
            txtDate.Text = DateTime.Now.ToString("d/M/yyyy");
            List<cArrayList> arr = new List<cArrayList>();
            string waz_dt = Request.Cookies["waz_dt"].Value.ToString();
            DateTime dtwaz_dt = DateTime.ParseExact(waz_dt, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            ddYear_SelectedIndexChanged(sender, e);
            ddYear.SelectedValue = dtwaz_dt.ToString("yyyy");
        }
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {

    }
    protected void btSave_Click(object sender, EventArgs e)
    {
        if (rdbtrxn.SelectedValue.ToString() == "IV")
        {


            string ccman = bll.vLookUp("select salespoint_sn from tmsT_salespoint where salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
            ccman = ccman + txtCCNR.Text;

            string claimNo = "";

            //if (ccman == bll.vLookUp("select manual_no from tmst_ccnr where manual_no='" + ccman + "'"))
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('CCNR Manual no already in the system','Please Enter correct CCNR No or contact WAZARAN ADMIN','warning');", true);
            //    return;
            //}


            if (lblTFreeCash.Text == "0.00" && lblTFreeItem.Text == "0.00")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('No Free Item and Free Cash for this proposal','This proposal no need to send to HO for claim.','warning');", true);
                return;
            }

            if (txtProposal.Text.ToString() == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Proposal Number must selected!','Please choose proposal number to claim.','warning');", true);
                return;
            }

            //if (txtCCNR.Text.ToString() == "")
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('CCNR Manual Number must Inserted!','Please Insert CCNR.','error');", true);
            //    return;
            //}

            string docStatus = "";
            string docSystem = "";
            string[] promotype = txtProposal.Text.Split('/');

            foreach (GridViewRow row1 in grdcate.Rows)
            {
                Label lbdocname1 = (Label)row1.FindControl("lbdocname");
                Label lbdoccode = (Label)row1.FindControl("lbdoccode");
                FileUpload upl1 = (FileUpload)row1.FindControl("upl");
                if (lbdocname1.Text != "Summary Claim" && lbdocname1.Text != "Claim Invoices (if any)")
                {
                    docStatus = bll.vLookUp("select doc_status from tpromotion_doc where doc_cd='" + lbdoccode.Text + "' and promo_typ='" + promotype[1].ToString() + "'");
                    docSystem = bll.vLookUp("select doc_system from tpromotion_doc where doc_cd='" + lbdoccode.Text + "' and promo_typ='" + promotype[1].ToString() + "'");

                    if (!upl1.HasFile)
                    {
                        if (docStatus == "Y")
                        {
                            if (docSystem == "N")
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('" + lbdocname1.Text + " Must be Uploaded','" + lbdocname1.Text + " Image Missing');", true);
                                return;
                            }
                        }
                    }
                }
            }

            try
            {
                List<cArrayList> arr = new List<cArrayList>();
                //arr.Add(new cArrayList("@claim_no", txtClaimNo.Text));
                string sCLNO = "";
                arr.Add(new cArrayList("@claim_dt", DateTime.ParseExact(txtDate.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                arr.Add(new cArrayList("@prop_no", txtProposal.Text));
                arr.Add(new cArrayList("@ccnr_no", txtCCNR.Text));
                arr.Add(new cArrayList("@ordervalue", Convert.ToDouble(lblTValue.Text)));
                arr.Add(new cArrayList("@orderqty", Convert.ToDouble(lblTQtyOrder.Text)));
                arr.Add(new cArrayList("@freevalue", Convert.ToDouble(lblTFreeCash.Text)));
                arr.Add(new cArrayList("@freeqty", Convert.ToDouble(lblTFreeItem.Text)));
                arr.Add(new cArrayList("@sendtoho_dt", DateTime.ParseExact(txtDate.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                arr.Add(new cArrayList("@claim_sta_id", "N"));
                arr.Add(new cArrayList("@cby", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@txMonth", ddMonth.SelectedValue.ToString()));
                arr.Add(new cArrayList("@txYear", ddYear.SelectedValue.ToString()));
                arr.Add(new cArrayList("@price_avg", lbAvgPrice.Text.ToString()));
                arr.Add(new cArrayList("@disc_cd", lhDiscCd.Value.ToString()));
                arr.Add(new cArrayList("@discount_mec", lhDiscountMec.Value.ToString()));
                bll.vInsertClaim(arr, ref sCLNO);
                txtClaimNo.Text = sCLNO;
                //insert claim_dtl
                arr.Clear();
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                arr.Add(new cArrayList("@disc_cd", lhDiscCd.Value.ToString()));
                arr.Add(new cArrayList("@claim_no", sCLNO));
                arr.Add(new cArrayList("@Month", ddMonth.SelectedValue.ToString()));
                arr.Add(new cArrayList("@Year", ddYear.SelectedValue.ToString()));
                bll.vInsertClaimdtlfreeso(arr);
                bll.vInsertClaimdtlfreeco(arr);
                bll.vInsertClaimdtlcashso(arr);
                bll.vInsertClaimdtlcashco(arr);

                //
                string sCCNRNO = "";
                arr.Clear();
                string sbrn = bll.vLookUp("select salespoint_sn from tmst_salespoint where salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'") + txtCCNR.Text;

                arr.Add(new cArrayList("@ccnr_dt", DateTime.ParseExact(txtDate.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                arr.Add(new cArrayList("@manual_no", sbrn));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                arr.Add(new cArrayList("@claim_no", sCLNO));
                arr.Add(new cArrayList("@app_by", null));

                bll.vInsertCcnr(arr, ref sCCNRNO);

                txtClaimNo.Text = sCLNO;

                arr.Clear();
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
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('Image size too big','Image size must be less than 50KB');", true);
                            return;
                        }


                    }
                    //else
                    //{
                    //    // statusUpload = "0";
                    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('Please attach all the documents required','Proposal, Summary claim and Invoice (zip files)');", true);
                    //    return;
                    //}
                }


                arr.Clear();
                string sPath = bll.sGetControlParameter("image_path") + @"\claim_doc\";
                string sPdfName = Request.Cookies["sp"].Value.ToString() + "-" + txtClaimNo.Text + "-" + "P09" + ".pdf";
                string sPdfName1 = Request.Cookies["sp"].Value.ToString() + "-" + txtClaimNo.Text + "-" + "P04" + ".xls";
                //string sPdfName3 = Request.Cookies["sp"].Value.ToString() + "-" + sCCNRNO + "-" + "P35" + ".pdf";
                creport rep = new creport();
                arr.Add(new cArrayList("@claim_no", txtClaimNo.Text));
                arr.Add(new cArrayList("@doc_cd", "P09"));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                arr.Add(new cArrayList("@fileloc", Request.Cookies["sp"].Value.ToString() + "-" + txtClaimNo.Text + "-" + "P09" + ".pdf"));
                arr.Add(new cArrayList("@doc_nm", " Claim Invoices (if any) "));
                bll.vInsertTclaimDoc(arr);
                //arr.Clear();//ccnrdoc
                //arr.Add(new cArrayList("@claim_no", sCCNRNO));
                //arr.Add(new cArrayList("@doc_cd", "P33"));
                //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                //arr.Add(new cArrayList("@fileloc", Request.Cookies["sp"].Value.ToString() + "-" + sCCNRNO + "-" + "P35" + ".pdf"));
                //arr.Add(new cArrayList("@doc_nm", " CCNR "));
                //bll.vInsertTclaimDoc(arr);
                arr.Clear();
                arr.Add(new cArrayList("@claim_no", txtClaimNo.Text));
                arr.Add(new cArrayList("@doc_cd", "P04"));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                arr.Add(new cArrayList("@fileloc", Request.Cookies["sp"].Value.ToString() + "-" + txtClaimNo.Text + "-" + "P04" + ".xls"));
                arr.Add(new cArrayList("@doc_nm", " Summary Claim "));
                bll.vInsertTclaimDoc(arr);
                arr.Clear();
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                arr.Add(new cArrayList("@claim_no", txtClaimNo.Text));
                bll.vUpdateTmstclaim(arr);
                arr.Clear();

                arr.Add(new cArrayList("@claim_no", txtClaimNo.Text));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                rep.vShowReportToPDF("rp_claim_invoice.rpt", arr, sPath + sPdfName);
                string claim_typ = bll.vLookUp("select distinct disc_typ from tclaim_dtl where claim_no='" + txtClaimNo.Text + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
                if (claim_typ == "FG")
                {
                    rep.vShowReportToEXCEL("rp_claimFG.rpt", arr, sPath + sPdfName1);
                }
                else if (claim_typ == "CH")
                {
                    rep.vShowReportToEXCEL("rp_claimDC.rpt", arr, sPath + sPdfName1);
                }

                //arr.Clear();

                //arr.Add(new cArrayList("@ccnr_no", bll.vLookUp("select ccnr_no from tmst_ccnr where claim_no='" + txtClaimNo.Text + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'")));
                //arr.Add(new cArrayList("@salespoint_cd", Request.Cookies["sp"].Value.ToString()));
                //arr.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));

                //rep.vShowReportToPDF("rp_ccnr.rpt", arr, sPath + sPdfName3);                

                arr.Clear();
                btnNew.Visible = true;
                btnCancel.Visible = true;
                btprint.Visible = false;
                /// SEnd Email
                /// 
                List<string> lapproval = bll.lGetApproval("branchspv", 1);
                Random nRdm = new Random();
                int token = nRdm.Next(1000, 9999);
                double tokenmail = nRdm.Next();
                Uri urlnya = Request.Url;
                string host = urlnya.GetLeftPart(UriPartial.Authority);
                if (hdto.Value.ToString() == "")
                {
                    List<string> lccedp = bll.lGetApproval("edp", 1);
                    vSendEmail(txtClaimNo.Text, tokenmail.ToString(), lapproval[1], lccedp[1], sCCNRNO);
                    arr.Clear();
                    arr.Add(new cArrayList("@trxcd", "Claim"));
                    arr.Add(new cArrayList("@token", tokenmail.ToString()));
                    arr.Add(new cArrayList("@doc_no", sCCNRNO));
                    //bll.vInsertEmailSent(arr);
                }


                //  ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('Proposal Has been saved for claim.','Claim Entry.','sucess');", true);
                //Response.Redirect("fm_claimEntry.aspx");
                Response.Write("<script language='javascript'>window.alert('Claim: " + txtClaimNo.Text + " Has been Created');window.location='fm_claimEntry.aspx';</script>");
            }
            catch (Exception ex)
            {
                bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : INVOICE CLAIM");
            }
        }
        else if (rdbtrxn.SelectedValue.ToString() == "CSH")
        {
            string ccman = bll.vLookUp("select salespoint_sn from tmst_salespoint where salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
            ccman = ccman + txtCCNR.Text;
            //string claimNo = "";
            //if (ccman == bll.vLookUp("select manual_no from tmst_ccnr where manual_no='" + ccman + "'"))
            //{
            //    ScriptManager.RegisterSta
            //        rtupScript(Page, Page.GetType(), "al", "sweetAlert('CCNR Manual no already in the system','Please Enter correct CCNR No or contact WAZARAN ADMIN','warning');", true);
            //    return;
            //}
            if (lblTFreeCash.Text == "0.00" && lblTFreeItem.Text == "0.00")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('No Free Item and Free Cash for this proposal','This proposal no need to send to HO for claim.','warning');", true);
                return;
            }

            if (txtProposal.Text.ToString() == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Proposal Number must selected!','Please choose proposal number to claim.','warning');", true);
                return;
            }
            if (txtCCNR.Text.ToString() == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('CCNR Manual Number must Inserted!','Please Insert CCNR.','error');", true);
                return;
            }
            string docStatus = "";
            string docSystem = "";
            string[] promotype = txtProposal.Text.Split('/');

            foreach (GridViewRow row1 in grdcate.Rows)
            {
                Label lbdocname1 = (Label)row1.FindControl("lbdocname");
                Label lbdoccode = (Label)row1.FindControl("lbdoccode");
                FileUpload upl1 = (FileUpload)row1.FindControl("upl");
                if (lbdocname1.Text != "Summary Claim" && lbdocname1.Text != "Claim Invoices (if any)")
                {
                    docStatus = bll.vLookUp("select doc_status from tpromotion_doc where doc_cd='" + lbdoccode.Text + "' and promo_typ='" + promotype[1].ToString() + "'");
                    docSystem = bll.vLookUp("select doc_system from tpromotion_doc where doc_cd='" + lbdoccode.Text + "' and promo_typ='" + promotype[1].ToString() + "'");

                    if (!upl1.HasFile)
                    {
                        if (docStatus == "Y")
                        {
                            if (docSystem == "N")
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('" + lbdocname1.Text + " Must be Uploaded','" + lbdocname1.Text + " Image Missing');", true);
                                return;
                            }
                        }
                    }

                }
            }

            try
            {
                List<cArrayList> arr = new List<cArrayList>();
                //arr.Add(new cArrayList("@claim_no", txtClaimNo.Text));
                string sCLNO = "";
                arr.Add(new cArrayList("@claim_dt", DateTime.ParseExact(txtDate.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                arr.Add(new cArrayList("@prop_no", lhDiscCd.Value.ToString()));
                arr.Add(new cArrayList("@ccnr_no", txtCCNR.Text));
                arr.Add(new cArrayList("@ordervalue", Convert.ToDouble(lbltotcashout.Text) + Convert.ToDouble(lbltotcashoutex.Text)));
                arr.Add(new cArrayList("@orderqty", 0.00));
                arr.Add(new cArrayList("@freevalue", 0.00));
                arr.Add(new cArrayList("@freeqty", 0.00));
                arr.Add(new cArrayList("@sendtoho_dt", DateTime.ParseExact(txtDate.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                arr.Add(new cArrayList("@claim_sta_id", "N"));
                arr.Add(new cArrayList("@cby", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@txMonth", ddMonth.SelectedValue.ToString()));
                arr.Add(new cArrayList("@txYear", ddYear.SelectedValue.ToString()));
                arr.Add(new cArrayList("@price_avg", 0.00));
                arr.Add(new cArrayList("@disc_cd", ""));
                arr.Add(new cArrayList("@discount_mec", "CSH"));
                bll.vInsertClaim(arr, ref sCLNO);
                txtClaimNo.Text = sCLNO;
                //insert claim_dtl
                arr.Clear();
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                arr.Add(new cArrayList("@prop_no", lhDiscCd.Value.ToString()));
                arr.Add(new cArrayList("@claim_no", sCLNO));
                arr.Add(new cArrayList("@Month", ddMonth.SelectedValue.ToString()));
                arr.Add(new cArrayList("@Year", ddYear.SelectedValue.ToString()));
                bll.vInsertClaimdtlcashout(arr);
                //
                arr.Clear();
                //arr.Add(new cArrayList("@prop_no", lhDiscCd.Value));
                //arr.Add(new cArrayList("@month", ddMonth.Text));
                //arr.Add(new cArrayList("@year", ddYear.Text));
                //bll.vBindingGridToSp(ref grdcst, "sp_getProposacashoutbycust_get", arr);
                //bll.vBindingGridToSp(ref grdgr, "sp_getProposacashoutbycusgrcd_get", arr);
                //
                string sCCNRNO = "";
                arr.Clear();
                string sbrn = bll.vLookUp("select salespoint_sn from tmst_salespoint where salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'") + txtCCNR.Text;
                arr.Add(new cArrayList("@ccnr_dt", DateTime.ParseExact(txtDate.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                arr.Add(new cArrayList("@manual_no", sbrn));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                arr.Add(new cArrayList("@claim_no", sCLNO));
                arr.Add(new cArrayList("@app_by", null));
                bll.vInsertCcnr(arr, ref sCCNRNO);
                //txtClaimNo.Text = sCLNO;
                arr.Clear();
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
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('Image size too big','Image size must be less than 50KB');", true);
                            return;
                        }


                    }
                    //else
                    //{
                    //    // statusUpload = "0";
                    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('Please attach all the documents required','Proposal, Summary claim and Invoice (zip files)');", true);
                    //    return;
                    //}
                }
                /// Genereate Document 
                arr.Clear();
                string sPath = bll.sGetControlParameter("image_path") + @"\claim_doc\";
                string sPdfName1 = Request.Cookies["sp"].Value.ToString() + "-" + txtClaimNo.Text + "-" + "P04" + ".xls";
                creport rep = new creport();
                arr.Add(new cArrayList("@claim_no", txtClaimNo.Text));
                arr.Add(new cArrayList("@doc_cd", "P04"));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                arr.Add(new cArrayList("@fileloc", Request.Cookies["sp"].Value.ToString() + "-" + txtClaimNo.Text + "-" + "P04" + ".xls"));
                arr.Add(new cArrayList("@doc_nm", " Summary Claim "));
                bll.vInsertTclaimDoc(arr);
                arr.Clear();
                arr.Add(new cArrayList("@claim_no", txtClaimNo.Text));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                arr.Add(new cArrayList("@customer", null));
                rep.vShowReportToEXCEL("rp_claimCashOut.rpt", arr, sPath + sPdfName1);
                ////
                arr.Clear();
                btnNew.Visible = true;
                btnCancel.Visible = true;
                btprint.Visible = false;
                /// SEnd Email
                /// 
                List<string> lapproval = bll.lGetApproval("branchspv", 1);
                Random nRdm = new Random();
                int token = nRdm.Next(1000, 9999);
                double tokenmail = nRdm.Next();
                Uri urlnya = Request.Url;
                string host = urlnya.GetLeftPart(UriPartial.Authority);
                if (hdto.Value.ToString() == "")
                {
                    List<string> lccedp = bll.lGetApproval("edp", 1);
                    vSendEmail(txtClaimNo.Text, tokenmail.ToString(), lapproval[1], lccedp[1], sCCNRNO);
                    arr.Clear();
                    arr.Add(new cArrayList("@trxcd", "Claim"));
                    arr.Add(new cArrayList("@token", tokenmail.ToString()));
                    arr.Add(new cArrayList("@doc_no", sCCNRNO));
                    bll.vInsertEmailSent(arr);
                }


                //  ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('Proposal Has been saved for claim.','Claim Entry.','sucess');", true);
                //Response.Redirect("fm_claimEntry.aspx");
                Response.Write("<script language='javascript'>window.alert('Claim: " + txtClaimNo.Text + " Has been Created');window.location='fm_claimEntry.aspx';</script>");
            }
            catch (Exception ex)
            {
                bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : CASHOUT CLAIM");
            }
        }
        else if (rdbtrxn.SelectedValue.ToString() == "CNDN")
        {
            string ccman = bll.vLookUp("select salespoint_sn from tmst_salespoint where salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
            ccman = ccman + txtCCNR.Text;
            //string claimNo = "";
            //if (ccman == bll.vLookUp("select manual_no from tmst_ccnr where manual_no='" + ccman + "'"))
            //{
            //    ScriptManager.RegisterSta
            //        rtupScript(Page, Page.GetType(), "al", "sweetAlert('CCNR Manual no already in the system','Please Enter correct CCNR No or contact WAZARAN ADMIN','warning');", true);
            //    return;
            //}
            //if (lblTFreeCash.Text == "0.00" && lblTFreeItem.Text == "0.00")
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('No Free Item and Free Cash for this proposal','This proposal no need to send to HO for claim.','warning');", true);
            //    return;
            //}

            if (txtProposal.Text.ToString() == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Proposal Number must selected!','Please choose proposal number to claim.','warning');", true);
                return;
            }
            if (txtCCNR.Text.ToString() == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('CCNR Manual Number must Inserted!','Please Insert CCNR.','error');", true);
                return;
            }
            string docStatus = "";
            string docSystem = "";
            string[] promotype = txtProposal.Text.Split('/');

            foreach (GridViewRow row1 in grdcate.Rows)
            {
                Label lbdocname1 = (Label)row1.FindControl("lbdocname");
                Label lbdoccode = (Label)row1.FindControl("lbdoccode");
                FileUpload upl1 = (FileUpload)row1.FindControl("upl");
                if (lbdocname1.Text != "Summary Claim" && lbdocname1.Text != "Claim Invoices (if any)")
                {
                    docStatus = bll.vLookUp("select doc_status from tpromotion_doc where doc_cd='" + lbdoccode.Text + "' and promo_typ='" + promotype[1].ToString() + "'");
                    docSystem = bll.vLookUp("select doc_system from tpromotion_doc where doc_cd='" + lbdoccode.Text + "' and promo_typ='" + promotype[1].ToString() + "'");

                    if (!upl1.HasFile)
                    {
                        if (docStatus == "Y")
                        {
                            if (docSystem == "N")
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('" + lbdocname1.Text + " Must be Uploaded','" + lbdocname1.Text + " Image Missing');", true);
                                return;
                            }
                        }
                    }

                }
            }

            try
            {
                List<cArrayList> arr = new List<cArrayList>();
                //arr.Add(new cArrayList("@claim_no", txtClaimNo.Text));
                string sCLNO = "";
                arr.Add(new cArrayList("@claim_dt", DateTime.ParseExact(txtDate.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                arr.Add(new cArrayList("@prop_no", lhDiscCd.Value.ToString()));
                arr.Add(new cArrayList("@ccnr_no", txtCCNR.Text));
                arr.Add(new cArrayList("@ordervalue", Convert.ToDouble(lbltotcndn.Text)));
                arr.Add(new cArrayList("@orderqty", 0.00));
                arr.Add(new cArrayList("@freevalue", 0.00));
                arr.Add(new cArrayList("@freeqty", 0.00));
                arr.Add(new cArrayList("@sendtoho_dt", DateTime.ParseExact(txtDate.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                arr.Add(new cArrayList("@claim_sta_id", "N"));
                arr.Add(new cArrayList("@cby", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@txMonth", ddMonth.SelectedValue.ToString()));
                arr.Add(new cArrayList("@txYear", ddYear.SelectedValue.ToString()));
                arr.Add(new cArrayList("@price_avg", 0.00));
                arr.Add(new cArrayList("@disc_cd", ""));
                arr.Add(new cArrayList("@discount_mec", "CNDN"));
                bll.vInsertClaim(arr, ref sCLNO);
                txtClaimNo.Text = sCLNO;
                //insert claim_dtl
                arr.Clear();
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                arr.Add(new cArrayList("@prop_no", lhDiscCd.Value.ToString()));
                arr.Add(new cArrayList("@claim_no", sCLNO));
                arr.Add(new cArrayList("@Month", ddMonth.SelectedValue.ToString()));
                arr.Add(new cArrayList("@Year", ddYear.SelectedValue.ToString()));
                bll.vInsertClaimdtlcndn(arr);
                //
                arr.Clear();
                //arr.Add(new cArrayList("@prop_no", lhDiscCd.Value));
                //arr.Add(new cArrayList("@month", ddMonth.Text));
                //arr.Add(new cArrayList("@year", ddYear.Text));
                //bll.vBindingGridToSp(ref grdcst, "sp_getProposacashoutbycust_get", arr);
                //bll.vBindingGridToSp(ref grdgr, "sp_getProposacashoutbycusgrcd_get", arr);
                //
                string sCCNRNO = "";
                arr.Clear();
                string sbrn = bll.vLookUp("select salespoint_sn from tmst_salespoint where salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'") + txtCCNR.Text;
                arr.Add(new cArrayList("@ccnr_dt", DateTime.ParseExact(txtDate.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                arr.Add(new cArrayList("@manual_no", sbrn));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                arr.Add(new cArrayList("@claim_no", sCLNO));
                arr.Add(new cArrayList("@app_by", null));
                bll.vInsertCcnr(arr, ref sCCNRNO);
                //txtClaimNo.Text = sCLNO;
                arr.Clear();
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
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('Image size too big','Image size must be less than 50KB');", true);
                            return;
                        }


                    }
                    //else
                    //{
                    //    // statusUpload = "0";
                    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('Please attach all the documents required','Proposal, Summary claim and Invoice (zip files)');", true);
                    //    return;
                    //}
                }
                /// Genereate Document 
                arr.Clear();
                string sPath = bll.sGetControlParameter("image_path") + @"\claim_doc\";
                string sPdfName1 = Request.Cookies["sp"].Value.ToString() + "-" + txtClaimNo.Text + "-" + "P04" + ".pdf";
                creport rep = new creport();
                arr.Add(new cArrayList("@claim_no", txtClaimNo.Text));
                arr.Add(new cArrayList("@doc_cd", "P04"));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                arr.Add(new cArrayList("@fileloc", Request.Cookies["sp"].Value.ToString() + "-" + txtClaimNo.Text + "-" + "P04" + ".pdf"));
                arr.Add(new cArrayList("@doc_nm", " Summary Claim "));
                bll.vInsertTclaimDoc(arr);
                arr.Clear();
                arr.Add(new cArrayList("@claim_no", txtClaimNo.Text));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                rep.vShowReportToPDF("rp_claimsumcndn.rpt", arr, sPath + sPdfName1);
                ////
                arr.Clear();
                btnNew.Visible = true;
                btnCancel.Visible = true;
                btprint.Visible = false;
                /// SEnd Email
                /// 
                List<string> lapproval = bll.lGetApproval("branchspv", 1);
                Random nRdm = new Random();
                int token = nRdm.Next(1000, 9999);
                double tokenmail = nRdm.Next();
                Uri urlnya = Request.Url;
                string host = urlnya.GetLeftPart(UriPartial.Authority);
                if (hdto.Value.ToString() == "")
                {
                    List<string> lccedp = bll.lGetApproval("edp", 1);
                    vSendEmail(txtClaimNo.Text, tokenmail.ToString(), lapproval[1], lccedp[1], sCCNRNO);
                    arr.Clear();
                    arr.Add(new cArrayList("@trxcd", "Claim"));
                    arr.Add(new cArrayList("@token", tokenmail.ToString()));
                    arr.Add(new cArrayList("@doc_no", sCCNRNO));
                    bll.vInsertEmailSent(arr);
                }


                //  ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('Proposal Has been saved for claim.','Claim Entry.','sucess');", true);
                //Response.Redirect("fm_claimEntry.aspx");
                Response.Write("<script language='javascript'>window.alert('Claim: " + txtClaimNo.Text + " Has been Created');window.location='fm_claimEntry.aspx';</script>");
            }
            catch (Exception ex)
            {
                bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : CN/DN CLAIM");
            }
        }
        else if (rdbtrxn.SelectedValue.ToString() == "BA")
        {


            string ccman = bll.vLookUp("select salespoint_sn from tmst_salespoint where salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
            ccman = ccman + txtCCNR.Text;
            string claimNo = "";
            if (ccman == bll.vLookUp("select manual_no from tmst_ccnr where manual_no='" + ccman + "'"))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('CCNR Manual no already in the system','Please Enter correct CCNR No or contact WAZARAN ADMIN','warning');", true);
                return;
            }
            if (lblTFreeItem.Text == "0.00")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('No Free Item for this proposal','This proposal no need to send to HO for claim.','warning');", true);
                return;
            }

            if (txtProposal.Text.ToString() == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Proposal Number must selected!','Please choose proposal number to claim.','warning');", true);
                return;
            }
            if (txtCCNR.Text.ToString() == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('CCNR Manual Number must Inserted!','Please Insert CCNR.','error');", true);
                return;
            }
            string docStatus = "";
            string docSystem = "";
            string[] promotype = txtProposal.Text.Split('/');

            foreach (GridViewRow row1 in grdcate.Rows)
            {
                Label lbdocname1 = (Label)row1.FindControl("lbdocname");
                Label lbdoccode = (Label)row1.FindControl("lbdoccode");
                FileUpload upl1 = (FileUpload)row1.FindControl("upl");
                if (lbdocname1.Text != "Summary Claim" && lbdocname1.Text != "Claim Invoices (if any)")
                {
                    docStatus = bll.vLookUp("select doc_status from tpromotion_doc where doc_cd='" + lbdoccode.Text + "' and promo_typ='" + promotype[1].ToString() + "'");
                    docSystem = bll.vLookUp("select doc_system from tpromotion_doc where doc_cd='" + lbdoccode.Text + "' and promo_typ='" + promotype[1].ToString() + "'");

                    if (!upl1.HasFile)
                    {
                        if (docStatus == "Y")
                        {
                            if (docSystem == "N")
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('" + lbdocname1.Text + " Must be Uploaded','" + lbdocname1.Text + " Image Missing');", true);
                                return;
                            }
                        }
                    }

                }
            }

            try
            {
                List<cArrayList> arr = new List<cArrayList>();
                //arr.Add(new cArrayList("@claim_no", txtClaimNo.Text));
                string sCLNO = "";
                arr.Add(new cArrayList("@claim_dt", DateTime.ParseExact(txtDate.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                arr.Add(new cArrayList("@prop_no", txtProposal.Text));
                arr.Add(new cArrayList("@ccnr_no", txtCCNR.Text));
                arr.Add(new cArrayList("@ordervalue", 0.00));
                arr.Add(new cArrayList("@orderqty", 0.00));
                arr.Add(new cArrayList("@freevalue", 0.00));
                arr.Add(new cArrayList("@freeqty", Convert.ToDouble(lblTFreeItem.Text)));
                arr.Add(new cArrayList("@sendtoho_dt", DateTime.ParseExact(txtDate.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                arr.Add(new cArrayList("@claim_sta_id", "N"));
                arr.Add(new cArrayList("@cby", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@txMonth", ddMonth.SelectedValue.ToString()));
                arr.Add(new cArrayList("@txYear", ddYear.SelectedValue.ToString()));
                arr.Add(new cArrayList("@price_avg", 0.00));
                arr.Add(new cArrayList("@disc_cd", ""));
                arr.Add(new cArrayList("@discount_mec", "BA"));
                bll.vInsertClaim(arr, ref sCLNO);
                txtClaimNo.Text = sCLNO;
                //insert claim_dtl
                arr.Clear();
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                arr.Add(new cArrayList("@prop_no", lhDiscCd.Value.ToString()));
                arr.Add(new cArrayList("@claim_no", sCLNO));
                arr.Add(new cArrayList("@Month", ddMonth.SelectedValue.ToString()));
                arr.Add(new cArrayList("@Year", ddYear.SelectedValue.ToString()));
                bll.vInsertClaimdtlcontract(arr);

                //
                string sCCNRNO = "";
                arr.Clear();
                string sbrn = bll.vLookUp("select salespoint_sn from tmst_salespoint where salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'") + txtCCNR.Text;
                arr.Add(new cArrayList("@ccnr_dt", DateTime.ParseExact(txtDate.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                arr.Add(new cArrayList("@manual_no", sbrn));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                arr.Add(new cArrayList("@claim_no", sCLNO));
                arr.Add(new cArrayList("@app_by", null));
                bll.vInsertCcnr(arr, ref sCCNRNO);
                //txtClaimNo.Text = sCLNO;
                arr.Clear();
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
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('Image size too big','Image size must be less than 50KB');", true);
                            return;
                        }


                    }
                    //else
                    //{
                    //    // statusUpload = "0";
                    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('Please attach all the documents required','Proposal, Summary claim and Invoice (zip files)');", true);
                    //    return;
                    //}
                }


                arr.Clear();
                string sPath = bll.sGetControlParameter("image_path") + @"\claim_doc\";
                string sPdfName = Request.Cookies["sp"].Value.ToString() + "-" + txtClaimNo.Text + "-" + "P09" + ".pdf";
                string sPdfName1 = Request.Cookies["sp"].Value.ToString() + "-" + txtClaimNo.Text + "-" + "P04" + ".xls";
                string sPdfName3 = Request.Cookies["sp"].Value.ToString() + "-" + sCCNRNO + "-" + "P35" + ".pdf";
                creport rep = new creport();
                arr.Add(new cArrayList("@claim_no", txtClaimNo.Text));
                arr.Add(new cArrayList("@doc_cd", "P09"));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                arr.Add(new cArrayList("@fileloc", Request.Cookies["sp"].Value.ToString() + "-" + txtClaimNo.Text + "-" + "P09" + ".pdf"));
                arr.Add(new cArrayList("@doc_nm", " Claim Invoices (if any) "));
                bll.vInsertTclaimDoc(arr);
                arr.Clear();//ccnrdoc
                arr.Add(new cArrayList("@claim_no", sCCNRNO));
                arr.Add(new cArrayList("@doc_cd", "P33"));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                arr.Add(new cArrayList("@fileloc", Request.Cookies["sp"].Value.ToString() + "-" + sCCNRNO + "-" + "P35" + ".pdf"));
                arr.Add(new cArrayList("@doc_nm", " CCNR "));
                bll.vInsertTclaimDoc(arr);
                arr.Clear();
                arr.Add(new cArrayList("@claim_no", txtClaimNo.Text));
                arr.Add(new cArrayList("@doc_cd", "P04"));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                arr.Add(new cArrayList("@fileloc", Request.Cookies["sp"].Value.ToString() + "-" + txtClaimNo.Text + "-" + "P04" + ".xls"));
                arr.Add(new cArrayList("@doc_nm", " Summary Claim "));
                bll.vInsertTclaimDoc(arr);
                arr.Clear();
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                arr.Add(new cArrayList("@claim_no", txtClaimNo.Text));
                bll.vUpdateTmstclaim(arr);
                arr.Clear();

                arr.Add(new cArrayList("@claim_no", txtClaimNo.Text));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                rep.vShowReportToPDF("rp_claim_invoice.rpt", arr, sPath + sPdfName);
                //string claim_typ = bll.vLookUp("select distinct disc_typ from tclaim_dtl where claim_no='" + txtClaimNo.Text + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
                rep.vShowReportToEXCEL("rp_claimFG.rpt", arr, sPath + sPdfName1);
                arr.Clear();
                arr.Add(new cArrayList("@ccnr_no", bll.vLookUp("select ccnr_no from tmst_ccnr where claim_no='" + txtClaimNo.Text + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'")));
                arr.Add(new cArrayList("@salespoint_cd", Request.Cookies["sp"].Value.ToString()));
                arr.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
                rep.vShowReportToPDF("rp_ccnr.rpt", arr, sPath + sPdfName3);
                arr.Clear();
                btnNew.Visible = true;
                btnCancel.Visible = true;
                btprint.Visible = false;
                /// SEnd Email
                /// 

                List<string> lapproval = bll.lGetApproval("branchspv", 1);
                Random nRdm = new Random();
                int token = nRdm.Next(1000, 9999);
                double tokenmail = nRdm.Next();
                Uri urlnya = Request.Url;
                string host = urlnya.GetLeftPart(UriPartial.Authority);
                if (hdto.Value.ToString() == "")
                {
                    List<string> lccedp = bll.lGetApproval("edp", 1);
                    vSendEmail(txtClaimNo.Text, tokenmail.ToString(), lapproval[1], lccedp[1], sCCNRNO);
                    arr.Clear();
                    arr.Add(new cArrayList("@trxcd", "Claim"));
                    arr.Add(new cArrayList("@token", tokenmail.ToString()));
                    arr.Add(new cArrayList("@doc_no", sCCNRNO));
                    bll.vInsertEmailSent(arr);
                }


                //  ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('Proposal Has been saved for claim.','Claim Entry.','sucess');", true);
                //Response.Redirect("fm_claimEntry.aspx");
                Response.Write("<script language='javascript'>window.alert('Claim: " + txtClaimNo.Text + " Has been Created');window.location='fm_claimEntry.aspx';</script>");
            }
            catch (Exception ex)
            {
                bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : BUSINESS AGREEMENT CLAIM");
            }
        }

    }
    //othman
    private void vSendEmail(string sCustCode, string sToken, string sReceived, string sCCReceived, string sCCNRNO)
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
        string sBody = "Dear Branch Spv, \n\r New Claim Needs Approval " + txtClaimNo.Text +
                  "<table style='width:100%'><tr style='background-color:silver'><td class='auto-style1'>Salespoint CD.</td><td>&nbsp;</td><td>" + Request.Cookies["sp"].Value.ToString() + "</td><td>Salespoint NM.</td><td>&nbsp;</td><td>" + bll.vLookUp("select salespoint_nm from tmst_salespoint where salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'") + "</td></tr><tr style='background-color:silver'><td class='auto-style1'>Claim No</td><td>:</td><td>" + txtClaimNo.Text + "</td><td>CCNR NO</td><td>:</td><td>" + sCCNRNO + "</td></tr>" +
                  "<tr><td class='auto-style1'>Proposal</td><td>:</td><td>" + txtProposal.Text + "</td><td>Created Date</td><td>:</td><td>" + txtDate.Text + "</td></tr>" +
                  "<tr style='background-color:silver'><td class='auto-style1'>Total Item Claim</td><td>:</td><td>" + Convert.ToDouble(lblTQtyOrder.Text) + "</td><td>Total Free Item</td><td>:</td><td>" + Convert.ToDouble(lblTFreeItem.Text) + "</td></tr>" +
                  "<tr><td class='auto-style1'>Total Quantity</td><td>:</td><td>" + Convert.ToDouble(lblTValue.Text) + "</td><td>Total Discount</td><td>:</td><td>" + Convert.ToDouble(lblTFreeCash.Text) + "</td></tr>" +
                  "</table>";
        //  sBody += "Please click <a href='landingpage.aspx?sta=A&trnname=salesorder&appcode=" + sToken + "'>Approve</a> for approved OR <a href='landingpage.aspx?sta=R&trnname=salesorder&appcode=" + sToken + "'>Reject</a> for rejected !";
        bll.vSendMail(sCCReceived, "cc:Claim Request Needs Approval", sBody);
        sBody += "Please click <a href='" + sHttp + "'>Login to Approve it</a>!";
        //sBody += "Please click <a href='" + sHttp + @"/landingclaimpage.aspx?sta=N&trnname=claim&appcode=" + sToken + "&salespointcd=" + Request.Cookies["sp"].Value.ToString() + "'>Approve</a> for approved OR <a href='" + sHttp + @"/landingclaimpage.aspx?sta=R&trnname=claim&appcode=" + sToken + "&salespointcd=" + Request.Cookies["sp"].Value.ToString() + "'>Reject</a> for rejected !";
        bll.vSendMail(sReceived, "Claim Request Needs Approval", sBody);


    }
    protected void btsearchso_Click(object sender, EventArgs e)
    {
        //if (rdbtrxn.SelectedValue.ToString() != "IV" || rdbtrxn.SelectedValue.ToString() != "CSH")
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Please choose type of Claim','Invoices OR Cashout','warning');", true);
        //    return;
        //}
        if (rdbtrxn.SelectedValue.ToString() == "IV")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "lp", "window.open('lookup_proposal1.aspx?month=" + ddMonth.SelectedValue + "&year=" + ddYear.SelectedValue + "&ven=-1','mywindow','toolbar=n,scrollbars=y,width=800,height=800,top=75,left=300',true);", true);
        }
        else if (rdbtrxn.SelectedValue.ToString() == "CSH")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "lp", "window.open('lookup_proposal2.aspx?month=" + ddMonth.SelectedValue + "&year=" + ddYear.SelectedValue + "&ven=-1','mywindow','toolbar=n,scrollbars=y,width=800,height=800,top=75,left=300',true);", true);
        }
        else if (rdbtrxn.SelectedValue.ToString() == "CNDN")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "lp", "window.open('lookup_proposal3.aspx?month=" + ddMonth.SelectedValue + "&year=" + ddYear.SelectedValue + "&ven=-1','mywindow','toolbar=n,scrollbars=y,width=800,height=800,top=75,left=300',true);", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "lp", "window.open('lookup_proposal4.aspx?month=" + ddMonth.SelectedValue + "&year=" + ddYear.SelectedValue + "&ven=-1','mywindow','toolbar=n,scrollbars=y,width=800,height=800,top=75,left=300',true);", true);
        }
    }


    double totalFreeItem = 0;
    double totalFreeCash = 0;
    double totalQtyOrder = 0;
    double totalQtyCash = 0;
    protected void btrefresh_Click(object sender, EventArgs e)
    {
        refreshGrid();
    }

    void refreshGrid()
    {
        if (rdbtrxn.SelectedValue.ToString() == "IV")
        {
            List<cArrayList> arr = new List<cArrayList>();
            HttpCookie cook;
            cook = HttpContext.Current.Request.Cookies["sp"];
            //txtProposal.ReadOnly = false;
            if (txtProposal.Text == string.Empty) return;
            arr.Add(new cArrayList("@disc_cd", lhDiscCd.Value));
            arr.Add(new cArrayList("@month", ddMonth.Text));
            arr.Add(new cArrayList("@year", ddYear.Text));
            arr.Add(new cArrayList("@salespointcd", cook.Value.ToString()));

            string remark = bll.vLookUp("SELECT remark FROM tmst_discount where disc_cd='" + lhDiscCd.Value + "'");
            string discount_mec = bll.vLookUp("SELECT discount_mec FROM tmst_discount where disc_cd='" + lhDiscCd.Value + "'");

            lhDiscountMec.Value = discount_mec;
            lblRemark.Text = remark;
            //Label3.Text = "Canvas Order Free Cash";
            //Label4.Text = "Canvas Order Free Item";
            lblTFreeItem.Text = "";
            lblTFreeCash.Text = "";

            //if (lhDiscountMec.Value.ToString() == "CH")
            //{
            bll.vBindingGridToSp(ref gridClaimSO, "sp_getSOByPropNo", arr);
            bll.vBindingGridToSp(ref gridClaimCO, "sp_getCOByPropNo", arr);
            lblTValue.Visible = true;
            lblTFreeCash.Visible = true;
            //}

            //else if (lhDiscountMec.Value.ToString() == "FG")
            //{
            bll.vBindingGridToSp(ref gridClaimSOItem, "sp_getSOItemByPropNo", arr);
            bll.vBindingGridToSp(ref gridClaimCOItem, "sp_getCOItemByPropNo", arr);
            lblTQtyOrder.Visible = true;
            lblTFreeItem.Visible = true;
            //}
            //txtProposal.ReadOnly = true;
            //lblTFreeItem = totalFreeItem;


            totalFreeItem = dSubTotal + dSubTotalCOItem;
            totalFreeCash = dSubTotalSO + dSubTotalCO;

            totalQtyOrder = dSubTotalQtyCO + dSubTotalQtySO;
            totalQtyCash = dSubTotalQtyCashCO + dSubTotalQtyCashSO;

            double priceAVG = (priceAVGSO + priceAVGCO) / 2;
            lbAvgPrice.Text = priceAVG.ToString();

            lblTValue.Text = totalQtyCash.ToString("N2");
            lblTQtyOrder.Text = totalQtyOrder.ToString("N2");
            lblTFreeItem.Text = totalFreeItem.ToString("N2");
            lblTFreeCash.Text = totalFreeCash.ToString("N2");

            /*System.Data.SqlClient.SqlDataReader rs = null;
            arr.Clear();
            arr.Add(new cArrayList("@disc_typ", "CD"));
            bll.vLookUp("sp_tdisctype_document_get", ref rs);
            while (rs.Read())
            {
            
            }
            rs.Close();
            */
            arr.Clear();
            string disc_typ = bll.vLookUp("select disc_typ from tmst_discount where disc_cd='" + lhDiscCd.Value + "'");
            arr.Add(new cArrayList("@promo_cd", disc_typ));
            arr.Add(new cArrayList("@dic", "BR"));
            bll.vBindingGridToSp(ref grdcate, "sp_tpromotion_doc_get", arr);
            ddMonth.Enabled = false;
            ddYear.Enabled = false;
            txtDate.Enabled = false;
            rdbtrxn.Enabled = false;


            string cost = bll.vLookUp("SELECT rdcost FROM tmst_proposal where prop_no='" + txtProposal.Text + "'");
            string sbtccost = bll.vLookUp("SELECT sbtccost FROM tmst_proposal where prop_no='" + txtProposal.Text + "'");
            string principalcost = bll.vLookUp("SELECT principalcost FROM tmst_proposal where prop_no='" + txtProposal.Text + "'");


            txBudget.Text = (cost == "percentage") ? "Sbtc " + sbtccost + "%, Principal " + principalcost : (cost == "sbtc") ? "Sbtc " + sbtccost + "%" : "Principal " + principalcost + "%";
        }
        else if (rdbtrxn.SelectedValue.ToString() == "CSH")
        {
            List<cArrayList> arr = new List<cArrayList>();
            if (txtProposal.Text == string.Empty) return;
            arr.Add(new cArrayList("@prop_no", lhDiscCd.Value));
            arr.Add(new cArrayList("@month", ddMonth.Text));
            arr.Add(new cArrayList("@year", ddYear.Text));
            bll.vBindingGridToSp(ref grdcst, "sp_getProposacashoutbycust_get", arr);
            bll.vBindingGridToSp(ref grdgr, "sp_getProposacashoutbycusgrcd_get", arr);
            lbltotcashout.Text = dubtotamt.ToString();
            lbltotcashoutex.Text = dubtotamtex.ToString();
            arr.Clear();
            string disc_typ = bll.vLookUp("select promotype from tmst_proposal where prop_no='" + lhDiscCd.Value + "'");
            arr.Add(new cArrayList("@promo_cd", disc_typ));
            arr.Add(new cArrayList("@dic", "BR"));
            bll.vBindingGridToSp(ref grdcate, "sp_tpromotion_doc_get", arr);
            ddMonth.Enabled = false;
            ddYear.Enabled = false;
            txtDate.Enabled = false;
            rdbtrxn.Enabled = false;
        }
        else if (rdbtrxn.SelectedValue.ToString() == "CNDN")
        {
            List<cArrayList> arr = new List<cArrayList>();
            if (txtProposal.Text == string.Empty) return;
            arr.Add(new cArrayList("@prop_no", lhDiscCd.Value));
            arr.Add(new cArrayList("@month", ddMonth.Text));
            arr.Add(new cArrayList("@year", ddYear.Text));
            bll.vBindingGridToSp(ref grdcndn, "sp_getProposacndn_get", arr);
            lbltotcndn.Text = dubtotamtex.ToString();
            //lbltotcndnex.Text = dubtotamtex.ToString();
            arr.Clear();
            string disc_typ = bll.vLookUp("select promotype from tmst_proposal where prop_no='" + lhDiscCd.Value + "'");
            arr.Add(new cArrayList("@promo_cd", disc_typ));
            arr.Add(new cArrayList("@dic", "BR"));
            bll.vBindingGridToSp(ref grdcate, "sp_tpromotion_doc_get", arr);
            ddMonth.Enabled = false;
            ddYear.Enabled = false;
            txtDate.Enabled = false;
            rdbtrxn.Enabled = false;
            lbltotitem.Visible = false;
            lbltotcashoutdesc.Visible = false;
            lbtotfree.Visible = false;
            lbtotqtyorder.Visible = false;
            lbltotcashoutdescex.Visible = false;
            lbtotdiscount.Visible = false;
            lbltotcndndescex.Visible = false;
        }
        else
        {
            List<cArrayList> arr = new List<cArrayList>();
            if (txtProposal.Text == string.Empty) return;
            arr.Add(new cArrayList("@prop_no", lhDiscCd.Value));
            arr.Add(new cArrayList("@month", ddMonth.Text));
            arr.Add(new cArrayList("@year", ddYear.Text));
            bll.vBindingGridToSp(ref grdBA, "sp_getProposalContract_get", arr);

            ddMonth.Enabled = false;
            ddYear.Enabled = false;
            txtDate.Enabled = false;
            rdbtrxn.Enabled = false;
            lbltotitem.Visible = true;
            lbltotcashoutdesc.Visible = false;
            lbtotfree.Visible = true;
            lbtotqtyorder.Visible = false;
            lbltotcashoutdescex.Visible = false;
            lbtotdiscount.Visible = false;
            lbltotcndndescex.Visible = false;
            lbltotcndn.Visible = false;
            lbltotcndndesc.Visible = false;
            lblTFreeItem.Visible = true;
            lblTQtyOrder.Visible = true;
            lblTFreeItem.Text = "0.0";
            lbltotitem.Text = "Total Claim";

            arr.Clear();
            string disc_typ = bll.vLookUp("select promotype from tmst_proposal where prop_no='" + lhDiscCd.Value + "'");
            arr.Add(new cArrayList("@promo_cd", disc_typ));
            arr.Add(new cArrayList("@dic", "BR"));
            bll.vBindingGridToSp(ref grdcate, "sp_tpromotion_doc_get", arr);

            string cost = bll.vLookUp("SELECT rdcost FROM tmst_proposal where prop_no='" + txtProposal.Text + "'");
            string sbtccost = bll.vLookUp("SELECT sbtccost FROM tmst_proposal where prop_no='" + txtProposal.Text + "'");
            string principalcost = bll.vLookUp("SELECT principalcost FROM tmst_proposal where prop_no='" + txtProposal.Text + "'");
            txBudget.Text = (cost == "percentage") ? "Sbtc " + sbtccost + "%, Principal " + principalcost : (cost == "sbtc") ? "Sbtc " + sbtccost + "%" : "Principal " + principalcost + "%";

            lblTQtyOrder.Text = dubtotamtba.ToString("N2");
            lblTFreeItem.Text = dubtotqtyba.ToString("N2");
        }
    }

    protected void gridClaimSOItem_RowDataBound(Object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            i++;
            Label lbsubtotal = (Label)e.Row.FindControl("qty");
            dSubTotal += Convert.ToDouble(lbsubtotal.Text);

            Label lbsubtotalQty = (Label)e.Row.FindControl("subtotal");
            if (lbsubtotalQty.Text == string.Empty) lbsubtotalQty.Text = "0";
            dSubTotalQtySO += Convert.ToDouble(lbsubtotalQty.Text);

            Label price = (Label)e.Row.FindControl("priceBuySOItem");
            totalPrice += Convert.ToDouble(price.Text);
        }


        double priceAvg = totalPrice / i;
        priceAVGSO = priceAvg;

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

    protected void gridClaimCOItem_RowDataBound(Object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            i++;
            Label lbsubtotalCO = (Label)e.Row.FindControl("qtyCO");
            dSubTotalCOItem += Convert.ToDouble(lbsubtotalCO.Text);

            Label lbsubtotalQtyCO = (Label)e.Row.FindControl("qtyOrderCO");
            if (lbsubtotalQtyCO.Text == string.Empty) lbsubtotalQtyCO.Text = "0";
            dSubTotalQtyCO += Convert.ToDouble(lbsubtotalQtyCO.Text);

            Label price = (Label)e.Row.FindControl("priceBuyCOItem");
            totalPrice += Convert.ToDouble(price.Text);
        }


        double priceAvg = totalPrice / i;
        priceAVGCO = priceAvg;

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

            Label lbQtyCashSO = (Label)e.Row.FindControl("qtyCashSO");
            dSubTotalQtyCashSO += Convert.ToDouble(lbQtyCashSO.Text);
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lbtotsubtotalSO = (Label)e.Row.FindControl("lbtotsubtotalSO");
            lbtotsubtotalSO.Text = dSubTotalSO.ToString("N2");
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Right;

            Label QtyOrderSO = (Label)e.Row.FindControl("lbTotQtyOrderSO");
            QtyOrderSO.Text = dSubTotalQtyCashSO.ToString("N2");
        }
    }

    protected void gridClaimCO_RowDataBound(Object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbsubtotalCO = (Label)e.Row.FindControl("amtCO");
            dSubTotalCO += Convert.ToDouble(lbsubtotalCO.Text);

            Label lbQtyCashCO = (Label)e.Row.FindControl("QtyCashCO");
            dSubTotalQtyCashCO += Convert.ToDouble(lbQtyCashCO.Text);

        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lbtotsubtotalCO = (Label)e.Row.FindControl("lbtotsubtotalCO");
            lbtotsubtotalCO.Text = dSubTotalCO.ToString("N2");
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Right;

            Label QtyOrderCO = (Label)e.Row.FindControl("lbTotQtyOrderCO");
            QtyOrderCO.Text = dSubTotalQtyCashCO.ToString("N2");
        }
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_claimEntry.aspx", true);
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Sorry!','You cannot cancel the claim for now.','warning');", true);
    }
    protected void ddMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        refreshGrid();
    }
    protected void ddYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        int iYear = int.Parse(ddYear.SelectedValue.ToString());
        if (iYear == 2017)
        {
            ddMonth.Items.Clear();
            ddMonth.Items.Insert(0, new ListItem("January", "01"));
            ddMonth.Items.Insert(1, new ListItem("February", "02"));
            ddMonth.Items.Insert(2, new ListItem("March", "03"));
            ddMonth.Items.Insert(3, new ListItem("April", "04"));
            ddMonth.Items.Insert(4, new ListItem("May", "05"));
            ddMonth.Items.Insert(5, new ListItem("June", "06"));
        }
        else
        {
            ddMonth.Items.Clear();
            ddMonth.Items.Insert(0, new ListItem("January", "01"));
            ddMonth.Items.Insert(1, new ListItem("February", "02"));
            ddMonth.Items.Insert(2, new ListItem("March", "03"));
            ddMonth.Items.Insert(3, new ListItem("April", "04"));
            ddMonth.Items.Insert(4, new ListItem("May", "05"));
            ddMonth.Items.Insert(5, new ListItem("June", "06"));
            ddMonth.Items.Insert(6, new ListItem("July", "07"));
            ddMonth.Items.Insert(7, new ListItem("August", "08"));
            ddMonth.Items.Insert(8, new ListItem("September", "09"));
            ddMonth.Items.Insert(9, new ListItem("October", "10"));
            ddMonth.Items.Insert(10, new ListItem("November", "11"));
            ddMonth.Items.Insert(11, new ListItem("December", "12"));
        }
        refreshGrid();
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
    protected void grdgr_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbamt = (Label)e.Row.FindControl("lbamt");
            dubtotamtex += Convert.ToDouble(lbamt.Text);

        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lbsubamt = (Label)e.Row.FindControl("lbsubamt");
            lbsubamt.Text = dubtotamtex.ToString("N2");
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Right;
        }
    }
    protected void grdcst_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbamt = (Label)e.Row.FindControl("lbamt");
            dubtotamt += Convert.ToDouble(lbamt.Text);

        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lbsubamt = (Label)e.Row.FindControl("lbsubamt");
            lbsubamt.Text = dubtotamt.ToString("N2");
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Right;
        }
    }
    protected void grdcndn_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbamt = (Label)e.Row.FindControl("lbamt");
            dubtotamtex += Convert.ToDouble(lbamt.Text);

        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lbsubamt = (Label)e.Row.FindControl("lbsubamt");
            lbsubamt.Text = dubtotamtex.ToString("N2");
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Right;
        }
    }
    protected void rdbtrxn_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdbtrxn.SelectedValue == "IV")
        {
            lbltotitem.Visible = true;
            lblTQtyOrder.Visible = true;
            lbAvgPrice.Visible = true;
            lbtotqtyorder.Visible = true;
            lblTValue.Visible = true;
            lbtotfree.Visible = true;
            lblTFreeItem.Visible = true;
            lbtotdiscount.Visible = true;
            lblTFreeCash.Visible = true;
            lbltotcashoutdesc.Visible = false;
            lbltotcashout.Visible = false;
            lbltotcashoutdescex.Visible = false;
            lbltotcashoutex.Visible = false;
        }
        else if (rdbtrxn.SelectedValue == "CSH")
        {
            lbltotcashoutdesc.Visible = true;
            lbltotcashout.Visible = true;
            lbltotcashoutdescex.Visible = true;
            lbltotcashoutex.Visible = true;
            lbltotitem.Visible = false;
            lblTQtyOrder.Visible = false;
            lbAvgPrice.Visible = false;
            lbtotqtyorder.Visible = false;
            lblTValue.Visible = false;
            lbtotfree.Visible = false;
            lblTFreeItem.Visible = false;
            lbtotdiscount.Visible = false;
            lblTFreeCash.Visible = false;
        }
    }
    protected void grdBA_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbamt = (Label)e.Row.FindControl("lbamt");
            Label lbqty = (Label)e.Row.FindControl("lbqty");
            dubtotamtba += Convert.ToDouble(lbamt.Text);
            dubtotqtyba += Convert.ToDouble(lbqty.Text);
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lbsubamt = (Label)e.Row.FindControl("lbsubamt");
            Label lbsubqty = (Label)e.Row.FindControl("lbsubqty");
            lbsubamt.Text = dubtotamtba.ToString("N2");
            lbsubqty.Text = dubtotqtyba.ToString("N2");
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Right;
        }
    }

}