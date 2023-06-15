using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class gclmdoc_ho : System.Web.UI.Page
{
    cbll bll = new cbll();
    creport rep = new creport();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //bll.vBindingComboToSp(ref rdyear, "sp_tmst_period_getbyyear", "yearvalue", "yearvalue");
        }
    }
    protected void btgn_Click(object sender, EventArgs e)
    {
        string sPath = @"d:/images/claim_doc/";
        string CL = txclaim.Text;
        string discountMec = "";
        try
        {
            discountMec = bll.vLookUp("select discount_mec from tmst_claim where claim_no='"+txclaim.Text+"'");
            if (discountMec != "CMHO")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('This Claim not CMHO','Please insert another claim no !','warning');", true);
                return;
            }
            List<cArrayList> arrClaim = new List<cArrayList>();
            string sPdfName = "", sPdfName1 = "", sPdfName2 = "";
            sPath = bll.sGetControlParameter("image_path") + @"\claim_doc\";
            int countContractKA = 0, countCMKA = 0;
            string sPdfName3 = ""; string sPdfName4 = ""; string sPdfName5 = ""; string sPdfName6 = "";
            string sPdfName7 = ""; string sPdfName3ext = "";
            string[] sPdfName3exta;
            countContractKA = int.Parse(bll.vLookUp("select count(distinct inv_no) from tclaim_dtl where claim_no='" + txclaim.Text + "'").ToString());
            countCMKA = int.Parse(bll.vLookUp("select count(distinct so_cd) from tclaim_dtl where claim_no='" + txclaim.Text + "'").ToString());

            if (countContractKA <= 0)
            {
                sPdfName3ext = bll.vLookUp("select fileloc from tcontract_ka_doc where contract_no=(select distinct inv_no from tclaim_dtl where claim_no='" + txclaim.Text + "')");
                sPdfName3exta = sPdfName3ext.ToString().Split('.');
                sPdfName3 = Request.Cookies["sp"].Value.ToString() + "-" + txclaim.Text + "-" + "P03" + "." + sPdfName3exta[1];
            }
            else
            {
                sPdfName3 = Request.Cookies["sp"].Value.ToString() + "-" + txclaim.Text + "-" + "P03" + ".zip";
            }

            sPdfName4 = Request.Cookies["sp"].Value.ToString() + "-" + txclaim.Text + "-" + "P35" + ".zip";
            sPdfName5 = Request.Cookies["sp"].Value.ToString() + "-" + txclaim.Text + "-" + "P05" + ".zip";
            sPdfName6 = Request.Cookies["sp"].Value.ToString() + "-" + txclaim.Text + "-" + "P06" + ".zip";
            sPdfName7 = Request.Cookies["sp"].Value.ToString() + "-" + txclaim.Text + "-" + "P07" + ".zip";

            sPdfName = Request.Cookies["sp"].Value.ToString() + "-" + txclaim.Text + "-" + "P09" + ".pdf";
            sPdfName1 = Request.Cookies["sp"].Value.ToString() + "-" + txclaim.Text + "-" + "P04" + ".xls";
            sPdfName2 = Request.Cookies["sp"].Value.ToString() + "-" + txclaim.Text + "-" + "P02" + ".pdf";

            arrClaim.Clear();
            arrClaim.Add(new cArrayList("@claim_no", txclaim.Text));
            arrClaim.Add(new cArrayList("@doc_cd", "P04"));
            arrClaim.Add(new cArrayList("@salespoint", Request.Cookies["sp"].Value.ToString()));
            arrClaim.Add(new cArrayList("@fileloc", Request.Cookies["sp"].Value.ToString() + "-" + txclaim.Text + "-" + "P04" + ".xls"));
            arrClaim.Add(new cArrayList("@doc_nm", "Summary Claim"));
            bll.vInsClaimDoc(arrClaim);
            arrClaim.Clear();
            arrClaim.Add(new cArrayList("@claim_no", txclaim.Text));
            arrClaim.Add(new cArrayList("@doc_cd", "P02"));
            arrClaim.Add(new cArrayList("@salespoint", Request.Cookies["sp"].Value.ToString()));
            arrClaim.Add(new cArrayList("@fileloc", Request.Cookies["sp"].Value.ToString() + "-" + txclaim.Text + "-" + "P02" + ".pdf"));
            arrClaim.Add(new cArrayList("@doc_nm", "Proposal Approved"));
            bll.vInsClaimDoc(arrClaim);
            arrClaim.Clear();

            // Generate Summary Claim 
            arrClaim.Clear();
            arrClaim.Add(new cArrayList("@claim_no", txclaim.Text));
            arrClaim.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            rep.vShowReportToEXCEL("rp_claimCM.rpt", arrClaim, sPath + sPdfName1);
            arrClaim.Clear();

            // <Start> Link Document with AGHO and CMHO 
            arrClaim.Clear();
            arrClaim.Add(new cArrayList("@claim_no", txclaim.Text));
            arrClaim.Add(new cArrayList("@doc_cd", "P03"));
            arrClaim.Add(new cArrayList("@salespoint", Request.Cookies["sp"].Value.ToString()));
            arrClaim.Add(new cArrayList("@fileloc", sPdfName3));
            arrClaim.Add(new cArrayList("@doc_nm", "Agreement"));
            bll.vInsClaimDoc(arrClaim);
            arrClaim.Clear();
            string statusCM1 = "";
            statusCM1 = bll.vLookUp("select distinct fileloc from tcreditmemo_doc a inner join tclaim_dtl b on a.cm_no=b.so_cd where a.doc_cd='CM01' and b.claim_no='" + txclaim.Text + "'");
            if (statusCM1 != "")
            {
                arrClaim.Add(new cArrayList("@claim_no", txclaim.Text));
                arrClaim.Add(new cArrayList("@doc_cd", "P35"));
                arrClaim.Add(new cArrayList("@salespoint", Request.Cookies["sp"].Value.ToString()));
                arrClaim.Add(new cArrayList("@fileloc", sPdfName4));
                arrClaim.Add(new cArrayList("@doc_nm", "CCNR/Credit Memo"));
                bll.vInsClaimDoc(arrClaim);
            }
            statusCM1 = "";
            statusCM1 = bll.vLookUp("select distinct fileloc from tcreditmemo_doc a inner join tclaim_dtl b on a.cm_no=b.so_cd where a.doc_cd='CM02' and b.claim_no='" + txclaim.Text + "'");
            if (statusCM1 != "")
            {
                arrClaim.Clear();
                arrClaim.Add(new cArrayList("@claim_no", txclaim.Text));
                arrClaim.Add(new cArrayList("@doc_cd", "P05"));
                arrClaim.Add(new cArrayList("@salespoint", Request.Cookies["sp"].Value.ToString()));
                arrClaim.Add(new cArrayList("@fileloc", sPdfName5));
                arrClaim.Add(new cArrayList("@doc_nm", "Customer Statement of Acc. (Only for KA/Account Deduction)"));
                bll.vInsClaimDoc(arrClaim);
            }
            statusCM1 = "";
            statusCM1 = bll.vLookUp("select distinct fileloc from tcreditmemo_doc a inner join tclaim_dtl b on a.cm_no=b.so_cd where a.doc_cd='CM04' and b.claim_no='" + txclaim.Text + "'");
            if (statusCM1 != "")
            {
                arrClaim.Clear();
                arrClaim.Add(new cArrayList("@claim_no", txclaim.Text));
                arrClaim.Add(new cArrayList("@doc_cd", "P06"));
                arrClaim.Add(new cArrayList("@salespoint", Request.Cookies["sp"].Value.ToString()));
                arrClaim.Add(new cArrayList("@fileloc", sPdfName6));
                arrClaim.Add(new cArrayList("@doc_nm", "Copy of Cheque"));
                bll.vInsClaimDoc(arrClaim);
            }
            statusCM1 = "";
            statusCM1 = bll.vLookUp("select distinct fileloc from tcreditmemo_doc a inner join tclaim_dtl b on a.cm_no=b.so_cd where a.doc_cd='CM03' and b.claim_no='" + txclaim.Text + "'");
            if (statusCM1 != "")
            {
                arrClaim.Clear();
                arrClaim.Add(new cArrayList("@claim_no", txclaim.Text));
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
                arrAgClaim.Add(new cArrayList("@claim_no", txclaim.Text.ToString()));
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
            arrCMClaim.Add(new cArrayList("@claim_no", txclaim.Text.ToString()));
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
            arrCMClaim.Add(new cArrayList("@claim_no", txclaim.Text.ToString()));
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
            arrCMClaim.Add(new cArrayList("@claim_no", txclaim.Text.ToString()));
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
            arrCMClaim.Add(new cArrayList("@claim_no", txclaim.Text.ToString()));
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
            
            Response.Write("<script> window.open('fm_invByProposal_ho.aspx?number="+txclaim.Text+"','_blank'); </script>");
        }
        catch (Exception ex)
        {
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : REGENERATE CLAIM (CMHO)");
        }
    }

}