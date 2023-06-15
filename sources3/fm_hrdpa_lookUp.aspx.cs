using AppClassTools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_hrdpa_lookUp : System.Web.UI.Page
{
    cbll bll = new cbll();
    Utitlity ut = new Utitlity();
    cdal cdl = new cdal();
    AppClass app = new AppClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Under Progress.
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
            string salespoint = Convert.ToString(Request.Cookies["sp"].Value);

            DataTable dtSalespoint = new DataTable();

            List<cArrayList> drSalespoint = new List<cArrayList>();
            drSalespoint.Add(new cArrayList("@salespoint_nm", Convert.ToString(salespoint)));
            dtSalespoint = cdl.GetValueFromSP("sp_tmst_salespoint_search", drSalespoint);

            List<cArrayList> arr = new List<cArrayList>();
            bll.vSyncSalesTarget(arr, Convert.ToString(ddlBranch.SelectedValue));
            app.BootstrapAlert(lblMsg, "Sync successfully", app.alertType = AppClassTools.AppClass.BootstrapAlertType.Success, true);
        }
        catch (Exception ex)
        {
            app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
            ut.Logs("", "Sales Target", "Sales Target Head Office", "fm_salestargetho2", "cblevel_SelectedIndexChanged", "Exception", ex.Message + ex.InnerException);
        }
    }
}