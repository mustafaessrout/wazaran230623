using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_acc_trialBalance : System.Web.UI.Page
{
    cbll bll = new cbll();
    //Boolean isWorksheetReport;
    public bool isWorksheetReport { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingComboToSp(ref cbsalespoint1, "sp_tacc_mst_salespoint_get", "salespointcd", "salespoint_desc");
            cbsalespoint1.SelectedValue = Request.Cookies["sp"].Value.ToString();

            bll.vBindingComboToSp(ref cbsalespoint2, "sp_tacc_mst_salespoint_get", "salespointcd", "salespoint_desc");
            cbsalespoint2.SelectedValue = Request.Cookies["sp"].Value.ToString();

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

    protected void btprinttrialbalance_Click(object sender, EventArgs e)
    {
        DateTime dtfromP = DateTime.ParseExact(dtfrom0.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        DateTime dttoP = DateTime.ParseExact(dtto0.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        string dtfromStr = dtfromP.Year.ToString() + "-" + dtfromP.Month.ToString("00") + "-" + dtfromP.Day.ToString("00");
        string dttoStr = dttoP.Year.ToString() + "-" + dttoP.Month.ToString("00") + "-" + dttoP.Day.ToString("00");

        if (dtfromStr.ToString().Substring(0, 4) != dttoStr.ToString().Substring(0, 4))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Please change both Start Date and End Date to be in the same year!','','warning');", true);
        }
        else
        {


            int isMonthlyYes = 1;

            List<cArrayList> arr = new List<cArrayList>();
            arr.Clear();
            arr.Add(new cArrayList("@salespointcd", cbsalespoint1.SelectedValue.ToString()));
            //arr.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("p_user", bll.vLookUp("select emp_nm from tmst_employee where emp_cd='" + Request.Cookies["usr_id"].Value.ToString() + "'")));
            //arr.Add(new cArrayList("p_salesPointName", Request.Cookies["spn"].Value.ToString()));
            arr.Add(new cArrayList("p_salesPointName", bll.vLookUp("select salespoint_nm from tmst_salespoint where salespointcd='" + cbsalespoint1.SelectedValue.ToString() + "'")));

            arr.Add(new cArrayList("@startdate", dtfromStr));
            arr.Add(new cArrayList("@enddate", dttoStr));
            arr.Add(new cArrayList("@year", dtfromP.Year.ToString()));
            arr.Add(new cArrayList("@isMonthly", isMonthlyYes));

            if (rdIsWorksheet.SelectedValue == "T" | rdIsWorksheet2.SelectedValue == "T")
            {
                isWorksheetReport = false;
            }
            else if (rdIsWorksheet.SelectedValue == "W" | rdIsWorksheet2.SelectedValue == "W")
            {
                isWorksheetReport = true;
            }


            if (isWorksheetReport)
            {
                Session["lParamtrialbalanceWS"] = arr;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opf", "openreport('fm_report2.aspx?src=acc_trialBalanceWorkSheet');", true);
            }
            else
            {
                Session["lParamtrialbalance"] = arr;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opf", "openreport('fm_report2.aspx?src=acc_trialBalance');", true);
            }
        }
    }
    protected void btprinttrialbalanceyr_Click(object sender, EventArgs e)
    {
        //DateTime dtfromP = DateTime.ParseExact(dtfrom0.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        //DateTime dttoP = DateTime.ParseExact(dtto0.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        //string dtfromStr = dtfromP.Year.ToString() + "-" + dtfromP.Month.ToString("00") + "-" + dtfromP.Day.ToString("00");
        //string dttoStr = dttoP.Year.ToString() + "-" + dttoP.Month.ToString("00") + "-" + dttoP.Day.ToString("00");

        int isMonthlyNo = 0;

        List<cArrayList> arr = new List<cArrayList>();
        arr.Clear();
        arr.Add(new cArrayList("@salespointcd", cbsalespoint2.SelectedValue.ToString()));
        //arr.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("p_user", bll.vLookUp("select emp_nm from tmst_employee where emp_cd='" + Request.Cookies["usr_id"].Value.ToString() + "'")));
        arr.Add(new cArrayList("p_salesPointName", Request.Cookies["spn"].Value.ToString()));

        arr.Add(new cArrayList("@startdate", null));
        arr.Add(new cArrayList("@enddate", null));
        arr.Add(new cArrayList("@year", txYear.Text.ToString()));
        arr.Add(new cArrayList("@isMonthly", isMonthlyNo));

        if (rdIsWorksheet.SelectedValue == "T" | rdIsWorksheet2.SelectedValue == "T")
        {
            isWorksheetReport = false;
        }
        else if (rdIsWorksheet.SelectedValue == "W" | rdIsWorksheet2.SelectedValue == "W")
        {
            isWorksheetReport = true;
        }

        if (isWorksheetReport)
        {
            Session["lParamtrialbalanceWS"] = arr;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opf", "openreport('fm_report2.aspx?src=acc_trialBalanceWorkSheet');", true);
        }
        else
        {
            Session["lParamtrialbalance"] = arr;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opf", "openreport('fm_report2.aspx?src=acc_trialBalance');", true);
        }

    }
    //public void rdbtworksheet_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (rdIsWorksheet.SelectedValue == "T" | rdIsWorksheet2.SelectedValue == "T")
    //    {
    //        isWorksheetReport = false;
    //    }
    //    else if (rdIsWorksheet.SelectedValue == "W" | rdIsWorksheet2.SelectedValue == "W")
    //    {
    //        isWorksheetReport = true;
    //    }
    //}
}