using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_claimreport_ho : System.Web.UI.Page
{

    cbll bll = new cbll();
    creport rep = new creport();


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            cbreport.Items.Insert(0, new ListItem("<< Select Report >>", "select"));
            cbreport.Items.Insert(1, new ListItem("1. Detail Claim Logbook", "logbook"));
            cbreport.Items.Insert(2, new ListItem("2. Summary Claim Logbook", "sumlogbook"));
            cbreport.Items.Insert(3, new ListItem("3. Detail (Proposal vs Claim)", "propvsclaimdtl"));
            cbreport.Items.Insert(4, new ListItem("4. Summary (Proposal vs Claim)", "propvsclaimsum"));
            cbreport.Items.Insert(5, new ListItem("5. Detail (Claim vs Payment)", "payvsclaimdtl"));
            cbreport.Items.Insert(6, new ListItem("6. Summary (Claim vs Payment)", "payvsclaimsum"));
            cbreport.Items.Insert(7, new ListItem("7. Detail (Claim vs Claim)", "claimvsclaimdtl"));
            cbreport.Items.Insert(8, new ListItem("8. Summary (Claim vs Claim)", "claimvsclaimsum"));
            cbreport.Items.Insert(9, new ListItem("9. Claim List Statement", "claimstatement"));
            cbreport.Items.Insert(10, new ListItem("10. Sales VS Claim Detail", "salesvsclaimdetail"));
            cbreport.Items.Insert(11, new ListItem("11. Payment Logbook Detail", "paylogbook"));
            cbreport.Items.Insert(12, new ListItem("12. Vat Invoice Logbook", "vatinvlogbook"));
            bll.vBindingComboToSp(ref cblogstatus, "sp_tfield_value_get4", "fld_valu", "fld_note");
            bll.vBindingComboToSp(ref cblogbranch, "sp_tmst_salespoint_get", "salespointcd", "salespoint_nm");
            bll.vBindingComboToSp(ref cblogvendor, "sp_tmst_vendor_get", "vendor_cd", "vendor_nm");
            bll.vBindingComboToSp(ref cbpvscdtlvendor, "sp_tmst_vendor_get", "vendor_cd", "vendor_nm");
            bll.vBindingComboToSp(ref cbstatementbr, "sp_tmst_salespoint_get", "salespointcd", "salespoint_nm");
            bll.vBindingComboToSp(ref cbsalesdetailbranch, "sp_tmst_salespoint_get", "salespointcd", "salespoint_nm");
            bll.vBindingComboToSp(ref cbsalesdetailproduct, "sp_tmst_product_get4", "ID", "ProdName");
            bll.vBindingComboToSp(ref cbpvscdtltypepay, "sp_get_docpayment_typ", "fld_valu", "fld_desc");

            // Year Claim 
            bll.vBindingComboToSp(ref cblogYear, "sp_tmst_year_getbyclaim", "fld_valu", "fld_valu");
            bll.vBindingComboToSp(ref cbsumyear, "sp_tmst_year_getbyclaim", "fld_valu", "fld_valu");
            bll.vBindingComboToSp(ref cbpvscdtlyear, "sp_tmst_year_getbyclaim", "fld_valu", "fld_valu");
            bll.vBindingComboToSp(ref cbstatementyear, "sp_tmst_year_getbyclaim", "fld_valu", "fld_valu");


            cblogstatus.Items.Insert(0, new ListItem("<< ALL Status >>", "ALL"));
            cblogbranch.Items.Insert(0, new ListItem("<< ALL Branch >>", "ALL"));
            cblogMonth.Items.Insert(0, new ListItem("<< ALL Month >>", "ALL"));
            cblogvendor.Items.Insert(0, new ListItem("<< ALL Vendor/Principal >>", "ALL"));
            cbpvscdtlvendor.Items.Insert(0, new ListItem("<< ALL Vendor/Principal >>", "ALL"));
            cbstatementbr.Items.Insert(0, new ListItem("<< ALL Branch >>", "ALL"));
            cbstatementmonth.Items.Insert(0, new ListItem("<< ALL Month >>", "ALL"));
            cbpvscdtltype.Items.Insert(0, new ListItem("<< ALL Type >>", "ALL"));
            cbpvscdtlcost.Items.Insert(0, new ListItem("<< ALL Cost >>", "ALL"));
            cbsalesdetailbranch.Items.Insert(0, new ListItem("<< ALL Branch >>", "ALL"));
            cbsumyear.Items.Insert(0, new ListItem("<< ALL Year >>", "ALL"));
            cbpvscdtltypepay.Items.Insert(0, new ListItem("<< ALL Payment >>", "ALL"));
            cbpvscdtlyear.Items.Insert(0, new ListItem("<< ALL Year >>", "ALL"));
            cbpvscdtlmonth.Items.Insert(0, new ListItem("<< ALL Month >>", "ALL"));
            cblogYear.Items.Insert(0, new ListItem("<< ALL Year >>", "ALL"));
            //clmonth.Items.Insert(0, new ListItem("<< ALL Month >>", "ALL"));
            //clyear.Items.Insert(0, new ListItem("<< ALL Year >>", "ALL"));            
            cbreport_SelectedIndexChanged(sender, e);
        }
    }

    protected void cbreport_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cbreport.SelectedValue.ToString() == "logbook")
        {
            logbook.Attributes.Remove("style");
            summarylog.Attributes.Add("style", "display:none");
            propvsclaimdtl.Attributes.Add("style", "display:none");
            claimstatement.Attributes.Add("style", "display:none");
            salesvsclaimdetail.Attributes.Add("style", "display:none");
        }
        else if (cbreport.SelectedValue.ToString() == "sumlogbook")
        {
            summarylog.Attributes.Remove("style");
            logbook.Attributes.Add("style", "display:none");
            propvsclaimdtl.Attributes.Add("style", "display:none");
            claimstatement.Attributes.Add("style", "display:none");
            salesvsclaimdetail.Attributes.Add("style", "display:none");
        }
        else if (cbreport.SelectedValue.ToString() == "propvsclaimdtl" || cbreport.SelectedValue.ToString() == "propvsclaimsum" || cbreport.SelectedValue.ToString() == "claimvsclaimdtl" || cbreport.SelectedValue.ToString() == "claimvsclaimsum")
        {
            propvsclaimdtl.Attributes.Remove("style");
            logbook.Attributes.Add("style", "display:none");
            summarylog.Attributes.Add("style", "display:none");
            claimstatement.Attributes.Add("style", "display:none");
            salesvsclaimdetail.Attributes.Add("style", "display:none");
        }
        else if (cbreport.SelectedValue.ToString() == "payvsclaimdtl")
        {
            propvsclaimdtl.Attributes.Remove("style");
            logbook.Attributes.Add("style", "display:none");
            summarylog.Attributes.Add("style", "display:none");
            claimstatement.Attributes.Add("style", "display:none");
            salesvsclaimdetail.Attributes.Add("style", "display:none");
            pvscdtltypepay.Attributes.Remove("style");
            cbpvscdtltypepay.Items.Remove("ALL");
            cbpvscdtlyear.Items.Remove("ALL");
        }
        else if (cbreport.SelectedValue.ToString() == "payvsclaimsum")
        {
            propvsclaimdtl.Attributes.Remove("style");
            logbook.Attributes.Add("style", "display:none");
            summarylog.Attributes.Add("style", "display:none");
            claimstatement.Attributes.Add("style", "display:none");
            salesvsclaimdetail.Attributes.Add("style", "display:none");
            pvscdtltypepay.Attributes.Add("style", "display:none");
            cbpvscdtltypepay.Items.Remove("<< ALL Type >>");
            cbpvscdtlyear.Items.Remove("<< ALL Year >>");
        }
        else if (cbreport.SelectedValue.ToString() == "claimstatement" || cbreport.SelectedValue.ToString() == "vatinvlogbook")
        {
            claimstatement.Attributes.Remove("style");
            logbook.Attributes.Add("style", "display:none");
            summarylog.Attributes.Add("style", "display:none");
            propvsclaimdtl.Attributes.Add("style", "display:none");
            salesvsclaimdetail.Attributes.Add("style", "display:none");
        }
        else if (cbreport.SelectedValue.ToString() == "salesvsclaimdetail")
        {
            logbook.Attributes.Add("style", "display:none");
            summarylog.Attributes.Add("style", "display:none");
            propvsclaimdtl.Attributes.Add("style", "display:none");
            claimstatement.Attributes.Add("style", "display:none");
            salesvsclaimdetail.Attributes.Remove("style");
        }
        else if (cbreport.SelectedValue.ToString() == "paylogbook")
        {
            propvsclaimdtl.Attributes.Remove("style");
            logbook.Attributes.Add("style", "display:none");
            summarylog.Attributes.Add("style", "display:none");
            claimstatement.Attributes.Add("style", "display:none");
            salesvsclaimdetail.Attributes.Add("style", "display:none");
            pvscdtltypepay.Attributes.Remove("style");            
        }
        else{
            logbook.Attributes.Add("style", "display:none");
            summarylog.Attributes.Add("style", "display:none");
            propvsclaimdtl.Attributes.Add("style", "display:none");
            claimstatement.Attributes.Add("style", "display:none");
            salesvsclaimdetail.Attributes.Add("style", "display:none");
        }
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (cbreport.SelectedValue.ToString() == "logbook")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport('fm_report2.aspx?src=logclaim&st=" + cblogstatus.SelectedValue.ToString() + "&sp=" + cblogbranch.SelectedValue.ToString() + "&vd=" + cblogvendor.SelectedValue.ToString() + "&mt=" + cblogMonth.SelectedValue.ToString() + "&yr=" + cblogYear.SelectedValue.ToString() + "');", true);
        }
        else if (cbreport.SelectedValue.ToString() == "sumlogbook")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport('fm_report2.aspx?src=sumlogclaim&yr=" + cbsumyear.SelectedValue.ToString() + "');", true);
        }
        else if (cbreport.SelectedValue.ToString() == "propvsclaimdtl")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport('fm_report2.aspx?src=propvsclaimdtl&vd=" + cbpvscdtlvendor.SelectedValue.ToString() + "&yr=" + cbpvscdtlyear.SelectedValue.ToString() + "&cs="+cbpvscdtlcost.SelectedValue.ToString()+"&tp="+cbpvscdtltype.SelectedValue.ToString()+"');", true);
        }
        else if (cbreport.SelectedValue.ToString() == "propvsclaimsum")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport('fm_report2.aspx?src=propvsclaimsum&vd=" + cbpvscdtlvendor.SelectedValue.ToString() + "&yr=" + cbpvscdtlyear.SelectedValue.ToString() + "&cs=" + cbpvscdtlcost.SelectedValue.ToString() + "&tp=" + cbpvscdtltype.SelectedValue.ToString() + "');", true);
        }
        else if (cbreport.SelectedValue.ToString() == "payvsclaimdtl")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport('fm_report2.aspx?src=payvsclaimdtl&vd=" + cbpvscdtlvendor.SelectedValue.ToString() + "&yr=" + cbpvscdtlyear.SelectedValue.ToString() + "&cs=" + cbpvscdtlcost.SelectedValue.ToString() + "&tp=" + cbpvscdtltype.SelectedValue.ToString() + "&py="+cbpvscdtltypepay.SelectedValue.ToString()+"');", true);
        }
        else if (cbreport.SelectedValue.ToString() == "payvsclaimsum")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport('fm_report2.aspx?src=payvsclaimsum&vd=" + cbpvscdtlvendor.SelectedValue.ToString() + "&yr=" + cbpvscdtlyear.SelectedValue.ToString() + "&cs=" + cbpvscdtlcost.SelectedValue.ToString() + "&tp=" + cbpvscdtltype.SelectedValue.ToString() + "');", true);
        }
        else if (cbreport.SelectedValue.ToString() == "claimvsclaimdtl")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport('fm_report2.aspx?src=claimvsclaimdtl&vd=" + cbpvscdtlvendor.SelectedValue.ToString() + "&yr=" + cbpvscdtlyear.SelectedValue.ToString() + "&cs=" + cbpvscdtlcost.SelectedValue.ToString() + "&tp=" + cbpvscdtltype.SelectedValue.ToString() + "&py=" + cbpvscdtltypepay.SelectedValue.ToString() + "');", true);
        }
        else if (cbreport.SelectedValue.ToString() == "claimvsclaimsum")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport('fm_report2.aspx?src=claimvsclaimsum&vd=" + cbpvscdtlvendor.SelectedValue.ToString() + "&yr=" + cbpvscdtlyear.SelectedValue.ToString() + "&cs=" + cbpvscdtlcost.SelectedValue.ToString() + "&tp=" + cbpvscdtltype.SelectedValue.ToString() + "');", true);
        }
        else if (cbreport.SelectedValue.ToString() == "claimstatement")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport('fm_report2.aspx?src=claimstatement&sp=" + cbstatementbr.SelectedValue.ToString() + "&yr=" + cbstatementyear.SelectedValue.ToString() + "&mt=" + cbstatementmonth.SelectedValue.ToString() + "');", true);
        }
        else if (cbreport.SelectedValue.ToString() == "salesvsclaimdetail")
        {
            DateTime dtfrom = Convert.ToDateTime(DateTime.ParseExact(dtsalesdetailbranchstart.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture));
            DateTime dtto = Convert.ToDateTime(DateTime.ParseExact(dtsalesdetailbranchend.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture));
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport('fm_report2.aspx?src=salesvsclaimdetail&sp=" + cbsalesdetailbranch.SelectedValue.ToString() + "&start=" + dtfrom + "&end=" + dtto + "');", true);
        }
        else if (cbreport.SelectedValue.ToString() == "paylogbook")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport('fm_report2.aspx?src=paylogbook&vd=" + cbpvscdtlvendor.SelectedValue.ToString() + "&mt="+ cbpvscdtlmonth.SelectedValue.ToString() +"&yr=" + cbpvscdtlyear.SelectedValue.ToString() + "&cs=" + cbpvscdtlcost.SelectedValue.ToString() + "&tp=" + cbpvscdtltype.SelectedValue.ToString() + "&py=" + cbpvscdtltypepay.SelectedValue.ToString() + "');", true);
        }
        else if (cbreport.SelectedValue.ToString() == "vatinvlogbook")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport('fm_report2.aspx?src=vatinvlogbook&sp=" + cbstatementbr.SelectedValue.ToString() + "&yr=" + cbstatementyear.SelectedValue.ToString() + "&mt=" + cbstatementmonth.SelectedValue.ToString() + "');", true);
        }
    }

    protected void btngenerate_Click(object sender, EventArgs e)
    {
        string sPath = @"d:/images/claim_doc/ho/";
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            string sPdfName1 = "Detail_Logbook.xls";
            string sPdfName2 = "Summary_Logbook.xls";
            string sPdfName3 = "Detail_Proposal.xls";
            string sPdfName4 = "Summary_Proposal.xls";
            arr.Clear();
            arr.Add(new cArrayList("@status", "ALL"));
            arr.Add(new cArrayList("@salespointcd", "ALL"));
            arr.Add(new cArrayList("@vendor", "ALL"));
            arr.Add(new cArrayList("@month", "ALL"));
            arr.Add(new cArrayList("@year", "ALL"));
            rep.vShowReportToEXCEL("rp_claimdata.rpt", arr, sPath + sPdfName1);
            arr.Clear();
            arr.Add(new cArrayList("@year", "ALL"));
            rep.vShowReportToEXCEL("rp_claimdatasum.rpt", arr, sPath + sPdfName2);
            arr.Clear();
            arr.Add(new cArrayList("@year", "ALL"));
            arr.Add(new cArrayList("@vendor", "ALL"));
            arr.Add(new cArrayList("@cost", "ALL"));
            arr.Add(new cArrayList("@type", "ALL"));
            rep.vShowReportToEXCEL("rp_claimprop_dtl.rpt", arr, sPath + sPdfName3);
            arr.Clear();
            arr.Add(new cArrayList("@year", "ALL"));
            arr.Add(new cArrayList("@vendor", "ALL"));
            arr.Add(new cArrayList("@cost", "ALL"));
            arr.Add(new cArrayList("@type", "ALL"));
            rep.vShowReportToEXCEL("rp_claimprop_sum.rpt", arr, sPath + sPdfName4);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "cl", "vDisableShow();", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Claim Monthly has been generated.',' Claim Report has been send to E-Mail.','success');", true);
            return;
        }
        catch (Exception ex)
        {
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : GENERATE CLAIM MONTHLY");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Claim Monthly has not been generated.',' Claim Report failed.','warning');", true);
            return;
        }
    }
}