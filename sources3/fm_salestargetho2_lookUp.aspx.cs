using AppClassTools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_salestargetho2_lookUp : System.Web.UI.Page
{
    cbll bll = new cbll();
    Utitlity ut = new Utitlity();
    cdal cdl = new cdal();
    AppClass app = new AppClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            string[] period = bll.vLookUp("select top(1) DATEADD(day, -1, system_dt) AS newDateAdd  from tclosing_log where salespointcd = '" + ddlBranch.SelectedValue.Split('|')[1] + "' and closing_typ = 'M' order by closing_dt desc").Substring(0, 10).Split('/');
            string lastmonth = string.Empty;
            if (period[0].Length == 1)
            {
                lastmonth = "0" + period[0];
            }
            else { lastmonth =   period[0]; }
            string newPeriod = period[2].Trim() +lastmonth;
            lblLastMonthClosing.Text = newPeriod;
            lblUserSelectedPeriod.Text = Convert.ToString(Request.QueryString["period"]);
            bindControl();
        }
    }

    private void bindControl()
    {

    }

    protected void btsearch_Click(object sender, EventArgs e)
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            //arr.Add(new cArrayList("@period", Convert.ToString(Request.QueryString["period"])));
            //arr.Add(new cArrayList("@salespointcd", Convert.ToString(ddlBranch.SelectedValue.Split('|')[1])));

            if(Convert.ToInt32(lblLastMonthClosing.Text)>= Convert.ToInt32(lblUserSelectedPeriod.Text)){
                app.BootstrapAlert(lblMsg, "Please select correct period", app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
                return;
            }
            bll.vSyncSalesTarget(arr, Convert.ToString(ddlBranch.SelectedValue.Split('|')[0]));
            app.BootstrapAlert(lblMsg, "Sync successfully", app.alertType = AppClassTools.AppClass.BootstrapAlertType.Success, true);
        }
        catch (Exception ex)
        {
            app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
            ut.Logs("", "Sales Target", "Sales Target Head Office", "fm_salestargetho2", "cblevel_SelectedIndexChanged", "Exception", ex.Message + ex.InnerException);
        }
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        string[] period = bll.vLookUp("select top(1) DATEADD(day, -1, system_dt) AS newDateAdd  from tclosing_log where salespointcd = '" + ddlBranch.SelectedValue.Split('|')[1] + "' and closing_typ = 'M' order by closing_dt desc").Substring(0, 10).Split('/');
        string newPeriod = period[2] + period[0];
        lblLastMonthClosing.Text = newPeriod;
        lblUserSelectedPeriod.Text = Convert.ToString(Request.QueryString["period"]);
    }
}