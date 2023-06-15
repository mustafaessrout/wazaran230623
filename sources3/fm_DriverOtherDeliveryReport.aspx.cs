using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_DriverOtherDeliveryReport : System.Web.UI.Page
{
    cbll bll = new cbll();
    string tdriver_cd = "";
    DateTime dtdate_from =  DateTime.Now;
    DateTime dtdate_to =  DateTime.Now;
    List<cArrayList> arr = new List<cArrayList>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            dtDateFrom_CalendarExtender.SelectedDate = System.DateTime.ParseExact(bll.vLookUp("select dbo.fn_getLatestDate()"), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            dtDateFrom.Text = bll.vLookUp("select dbo.fn_getLatestDate()");
            dtDateTo_CalendarExtender.SelectedDate = System.DateTime.ParseExact(bll.vLookUp("select dbo.fn_getLatestDate()"), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            dtDateTo.Text = bll.vLookUp("select dbo.fn_getLatestDate()");
            arr.Add(new cArrayList("@job_title_cd", Convert.ToString("5")));
            arr.Add(new cArrayList("@level_cd", Convert.ToString("1")));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingComboToSp(ref ddlDriver, "sp_tmst_employee_getbyjobtitle", "emp_cd", "emp_desc", arr);

        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        arr.Clear();
        if (ddlDriver.SelectedValue.ToString() == "Select")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Driver is not Selected ','Please Select Driver!','warning');", true);
            return;
        }
        else
        { tdriver_cd = ddlDriver.SelectedValue.ToString(); }

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opf", "openreport('fm_report2.aspx?src=DrOtherDelSelect&date_from=" + dtDateFrom.Text + "&date_to=" + dtDateTo.Text + "&driver_cd=" + tdriver_cd + "');", true);
        ddlDriver.SelectedIndex = 0;
    }
    protected void btnShowAll_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opf", "openreport('fm_report2.aspx?src=DrOtherDelAll');", true);
        ddlDriver.SelectedIndex = 0;
    }
}