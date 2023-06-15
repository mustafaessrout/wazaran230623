using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

using System.IO;
public partial class fm_DriverOtherDelivery : System.Web.UI.Page
{
    cbll bll = new cbll();
 
    List<cArrayList> arr = new List<cArrayList>();

    string month_now = "", month_last = "", lastday_month_now = "", lastday_month_last = "", date_month_now = "", date_month_last = "";
    DateTime now_dt = DateTime.Now;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            arr.Add(new cArrayList("@job_title_cd", Convert.ToString("5")));
            arr.Add(new cArrayList("@level_cd", Convert.ToString("1")));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingComboToSp(ref ddlDriver, "sp_tmst_employee_getbyjobtitle", "emp_cd", "emp_desc", arr);
            bll.vBindingComboToSp(ref ddlPeriod, "sp_tmst_period_get", "period_cd", "period_nm");
            //ddlPeriod.SelectedValue = bll.sGetControlParameter("period");
            arr.Clear();
            arr.Add(new cArrayList("@reasn_typ", Convert.ToString("DOD")));
            bll.vBindingComboToSp(ref ddlReason, "sp_tmst_reason_get", "reasn_cd", "reasn_nm", arr);

            bll.vBindingFieldValueToCombo(ref ddllocFrom, "otherdelivery_location");
            bll.vBindingFieldValueToCombo(ref ddllocTo, "otherdelivery_location");

            dtDrIOtherDelDate_CalendarExtender.SelectedDate = System.DateTime.ParseExact(bll.vLookUp("select dbo.fn_getLatestDate()"), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            dtDrIOtherDelDate.Text = bll.vLookUp("select dbo.fn_getLatestDate()");
            dtDrIOtherDelDate.Enabled = false;
            ddllocFrom.SelectedIndex = 0;
            ddllocTo.SelectedIndex = 0;
            ddllocTo_SelectedIndexChanged(sender, e);
            ddllocFrom_SelectedIndexChanged(sender, e);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabErr", "tabErr();", true);

            now_dt = System.DateTime.ParseExact(bll.vLookUp("select dbo.fn_getLatestDate()"), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);

        }
    }

     protected void ddlPeriod_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (now_dt.Month < 10)
        { month_now = (now_dt.Year.ToString()) + "0" + (now_dt.Month.ToString()); }
        else { month_now = (now_dt.Year.ToString()) + (now_dt.Month.ToString()); }
        if ((now_dt.Month - 1) < 10)
        { month_last = (now_dt.Year.ToString()) + "0" + ((now_dt.Month - 1).ToString()); }
        else { month_last = (now_dt.Year.ToString()) + ((now_dt.Month - 1).ToString()); }
        lastday_month_now = (DateTime.DaysInMonth(now_dt.Year, now_dt.Month)).ToString();
        lastday_month_last = (DateTime.DaysInMonth(now_dt.Year, (now_dt.Month - 1))).ToString();

        if (ddlPeriod.SelectedValue != month_last && ddlPeriod.SelectedValue != month_now)
         {
             ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Period Can be Only this Month or Last Month','Please Select Accordingly!','warning');", true);
             dtDrIOtherDelDate_CalendarExtender.SelectedDate = System.DateTime.ParseExact(bll.vLookUp("select dbo.fn_getLatestDate()"), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
             dtDrIOtherDelDate.Text = bll.vLookUp("select dbo.fn_getLatestDate()");
             dtDrIOtherDelDate.Enabled = false;
             return;
         }
        dtDrIOtherDelDate.Enabled = true;
        dtDrIOtherDelDate_CalendarExtender.Enabled = true;
        if (ddlPeriod.SelectedValue == month_last)
         {
             dtDrIOtherDelDate_CalendarExtender.StartDate = System.DateTime.ParseExact( ("01/" + ((now_dt.Month - 1).ToString()) +"/" + (now_dt.Year.ToString())), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
             dtDrIOtherDelDate_CalendarExtender.EndDate = System.DateTime.ParseExact((lastday_month_last + "/" + ((now_dt.Month - 1).ToString()) + "/" + (now_dt.Year.ToString())), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
             dtDrIOtherDelDate_CalendarExtender.SelectedDate = System.DateTime.ParseExact(("01/" + ((now_dt.Month - 1).ToString()) + "/" + (now_dt.Year.ToString())), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
             dtDrIOtherDelDate.Text = "01/" + ((now_dt.Month - 1).ToString()) +"/" + (now_dt.Year.ToString());
         }
        if (ddlPeriod.SelectedValue == month_now)
         {
             dtDrIOtherDelDate_CalendarExtender.StartDate = System.DateTime.ParseExact(("01/" + (now_dt.Month.ToString()) + "/" + (now_dt.Year.ToString())), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
             dtDrIOtherDelDate_CalendarExtender.EndDate = System.DateTime.ParseExact((lastday_month_now + "/" + (now_dt.Month.ToString()) + "/" + (now_dt.Year.ToString())), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
             dtDrIOtherDelDate_CalendarExtender.SelectedDate = System.DateTime.ParseExact(("01/" + (now_dt.Month.ToString()) + "/" + (now_dt.Year.ToString())), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
             dtDrIOtherDelDate.Text = "01/" + (now_dt.Month.ToString()) + "/" + (now_dt.Year.ToString());
         }
    }
     protected void ddllocTo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddllocTo.SelectedIndex == ddllocFrom.SelectedIndex && ddllocTo.SelectedIndex != 0)
         {
             ddllocFrom.SelectedIndex = 0;
             ddllocTo.SelectedIndex = 0;
             ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Location TO and From Can Not be The Same','Please Select Other Location!','warning');", true);
             return;
         }
    }

     protected void ddllocFrom_SelectedIndexChanged(object sender, EventArgs e)
     {
         if (ddllocFrom.SelectedIndex == ddllocTo.SelectedIndex && ddllocFrom.SelectedIndex != 0)
         {
             ddllocFrom.SelectedIndex = 0;
              ddllocTo.SelectedIndex = 0;
             ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Location From and To Can Not be The Same','Please Select Other Location!','warning');", true);
             return;
         }
     }

    protected void btnSubmit_Click(object sender, EventArgs e)
    { 
        List<cArrayList> arr1 = new List<cArrayList>();
        if (ddlDriver.SelectedValue.ToString() == "Select")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Driver is not Selected ','Please Select Driver!','warning');", true);
            return;
        }
        else
        { arr1.Add((new cArrayList("@driver_cd", ddlDriver.SelectedValue.ToString()))); }

        if (ddllocFrom.SelectedValue.ToString() == "Select")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Location From is not Selected ','Please Select Location From!','warning');", true);
            return;
        }
        { arr1.Add((new cArrayList("@locfrom", ddllocFrom.SelectedValue.ToString()))); }

        if ( ddllocTo.SelectedValue.ToString() == "Select")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Location To  is not Selected ','Please Select Location To!','warning');", true);
            return;
        }
        else
        { arr1.Add((new cArrayList("@locto", ddllocTo.SelectedValue.ToString()))); }

        if (ddlReason.SelectedValue.ToString() == "Select")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Reason is not Selected ','Please Select Reason!','warning');", true);
            return;
        }
        else 
        { arr1.Add((new cArrayList("@reasn_cd", ddlReason.SelectedValue.ToString()))); }
        arr1.Add((new cArrayList("@Period", ddlPeriod.SelectedValue.ToString())));
        arr1.Add((new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString())));

        if (dtDrIOtherDelDate.Enabled == false)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Delivery Date is not Selected ','Please Select Delivery Date!','warning');", true);
            return;
        }
        else
        { arr1.Add((new cArrayList("@delivery_dt", DateTime.ParseExact(dtDrIOtherDelDate.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)))); }
       
        if (txRemark.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Remark is Empty','Please Write a Remark!','warning');", true);
            return;
        }
        else
        {
            arr1.Add((new cArrayList("@remark", txRemark.Text)));
           
        }
        arr1.Add((new cArrayList("@createdby", Request.Cookies["usr_id"].Value.ToString())));
        SqlDataReader rs = null;
        bll.vTdriver_Otherdelivery_Ins(arr1,ref rs);
        while (rs.Read())
        {
             hdids.Value = rs["ids"].ToString();
          
        } rs.Close();
   
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Data Submitted Successfully!','','success');", true);
        btnSubmit.Enabled = false;
        btnPrint.Visible = true;
        btnNew.Visible = true;
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {     
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opf", "openreport('fm_report2.aspx?src=DrOtherDel&ids="+hdids.Value.ToString()+"');", true);
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_DriverOtherDeliveryReport.aspx");
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_DriverOtherDelivery.aspx");
    }

}
