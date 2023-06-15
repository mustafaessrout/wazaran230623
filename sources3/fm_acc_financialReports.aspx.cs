using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_acc_financialReports : System.Web.UI.Page
{
    cbll bll = new cbll();
    //Boolean isWorksheetReport;
    public string layout;
    List<cArrayList> arr = new List<cArrayList>();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingComboToSp(ref cbsalespoint2, "sp_tacc_mst_salespoint_get", "salespointcd", "salespoint_desc");
            cbsalespoint2.SelectedValue = Request.Cookies["sp"].Value.ToString();

            bll.vBindingComboToSp(ref ddReport, "sp_tacc_fin_report_dd_get", "id", "report_name");

            arr.Clear();
            arr.Add(new cArrayList("@sp", cbsalespoint2.SelectedValue.ToString()));
            bll.vBindingComboToSp(ref ddLayout, "sp_tacc_fin_report_layout_dd_get", "layout_cd", "layout_nm",arr);

            //if (Request.Cookies["sp"].Value.ToString() == "0")
            //{
            //    cbsalespoint.Enabled = true;
            //}
            //else
            //{
            //    cbsalespoint.Enabled = false;
            //}
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabErr", "tabErr();", true);
    }

    protected void btprintfinreport_Click(object sender, EventArgs e)
    {
        arr.Clear();

        arr.Add(new cArrayList("@period", txPeriod.Text.ToString()));
        arr.Add(new cArrayList("@salespointcd", cbsalespoint2.SelectedValue.ToString()));

        //arr.Add(new cArrayList("@report", ddReport.SelectedValue.ToString()));
        string reportType = bll.vLookUp("SELECT report_type FROM tacc_fin_report WHERE id ='" + ddReport.SelectedValue.ToString() + "'");
        arr.Add(new cArrayList("@report", reportType));

        layout = ddLayout.SelectedValue.ToString();
        arr.Add(new cArrayList("@layout", layout));

        arr.Add(new cArrayList("@rpid", ddReport.SelectedValue.ToString()));
        arr.Add(new cArrayList("p_user", bll.vLookUp("select emp_nm from tmst_employee where emp_cd=cast('" + Request.Cookies["usr_id"].Value.ToString() + "' as varchar)")));
        arr.Add(new cArrayList("p_salesPointName", Request.Cookies["spn"].Value.ToString()));


        string my = bll.vLookUp("SELECT my FROM tacc_fin_report WHERE id ='" + ddReport.SelectedValue.ToString() + "'");
        if (my == "Y") { 

            if (layout == "SME")
            {
                Session["lParamfinancialReportSME"] = arr;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opf", "openreport('fm_report2.aspx?src=acc_financialReportSME');", true);
            }
            else if (layout == "TSM")
            {
                Session["lParamfinancialReportTSM"] = arr;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opf", "openreport('fm_report2.aspx?src=acc_financialReportTSM');", true);
            }
            else if (layout == "SPR")
            {
                Session["lParamfinancialReportSPR"] = arr;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opf", "openreport('fm_report2.aspx?src=acc_financialReportSPR');", true);
            }
        }
        else if (my == "M")
        {

            if (layout == "SME")
            {
                Session["lParamfinancialReportSME_MTD"] = arr;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opf", "openreport('fm_report2.aspx?src=acc_financialReportSME_MTD');", true);
            }
            else if (layout == "TSM")
            {
                Session["lParamfinancialReportTSM_MTD"] = arr;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opf", "openreport('fm_report2.aspx?src=acc_financialReportTSM_MTD');", true);
            }
            else if (layout == "SPR")
            {
                Session["lParamfinancialReportSPR_MTD"] = arr;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opf", "openreport('fm_report2.aspx?src=acc_financialReportSPR_MTD');", true);
            }
        }

    }
    protected void cbsalespoint_SelectedIndexChanged(object sender, EventArgs e)
    {
        arr.Clear();
        arr.Add(new cArrayList("@sp", cbsalespoint2.SelectedValue.ToString()));
        bll.vBindingComboToSp(ref ddLayout, "sp_tacc_fin_report_layout_dd_get", "layout_cd", "layout_nm", arr);
    }
}