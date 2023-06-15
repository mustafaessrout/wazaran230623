using AppClassTools;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Web;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SyncBranch : System.Web.UI.Page
{
    AppClass app = new AppClass();
    cbll bll = new cbll();
    cdal cdl = new cdal();
    Utitlity ut = new Utitlity();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //ddlSync_SelectedIndexChanged(sender, e);
            dvPeriod.Visible = false;

            bll.vBindingComboToSp(ref ddlPeriod, "sp_tmst_period_get", "period_cd", "period_nm");
            string period = Convert.ToString(DateTime.Now.Year) + (Convert.ToString(DateTime.Now.Month).Length == 1 ? ("0" + Convert.ToString(DateTime.Now.Month)) : Convert.ToString(DateTime.Now.Month));
            ddlPeriod.SelectedValue = period;
       

            bll.vBindingFieldValueToCombo(ref ddlSync, "Sync_Type");
            System.Data.SqlClient.SqlDataReader rs = null;
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vGetMstSalespoint(arr, ref rs);
            Ping pg = new Ping();
            PingReply reply = pg.Send("172.16.1.18");
            bool status = reply.Status == IPStatus.Success;
            if (status == false)
            {
                lblHOStat.Text = "disconnected";
                dvHOStatusValue.Style.Add("box-shadow", "inset 0 0 5px rgba(200, 236, 214, 0.71)");
                dvHOStatusValue.Style.Add("background", "radial-gradient(#e74c3c 80%,#a7f1c6)");
                hdfHOConnected.Value = "false";
            }
            else {
                lblHOStat.Text = "connected";
                dvHOStatusValue.Style.Add("box-shadow", "inset 0 0 5px rgba(200, 236, 214, 0.71)");
                dvHOStatusValue.Style.Add("background", "radial-gradient(#2ecc71 80%,#a7f1c6)");
                hdfHOConnected.Value = "true"; 
            }
            while (rs.Read())
            {
                lblBranchName.Text = rs["salespoint_nm"].ToString();
            }
            rs.Close();

            hdfBaranchID.Value = Convert.ToString(Request.Cookies["sp"].Value.ToString());
            hdfUserID.Value = Convert.ToString(Request.Cookies["usr_id"].Value.ToString());
           hdfSyncID.Value = Convert.ToString(ddlSync.SelectedValue);
        }

    }
    protected void OnRowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(grdSync, "Select$" + e.Row.RowIndex);
            e.Row.ToolTip = "Click to select this row.";
        }
    }

    protected void grdSync_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
       grdSync.PageIndex = e.NewPageIndex;
       // BindGridSIM();
    }

    protected void OnSelectedIndexChanged(object sender, EventArgs e)
    {
        string dataKeys =grdSync.SelectedRow.Cells[0].Text;
    }

    protected void btSearch_Click(object sender, EventArgs e)
    {
        try
        {
        }
        catch (Exception ex)
        {
            app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
            ut.Logs("", "Appraisal", "Target Driver", "fm_targetdriver", "Page_Load", "Exception", ex.Message + ex.InnerException);
        }
    }
    protected void btShowAll_Click(object sender, EventArgs e)
    {
        try
        {
        }
        catch (Exception ex)
        {
            app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
            ut.Logs("", "Appraisal", "Target Driver", "fm_targetdriver", "Page_Load", "Exception", ex.Message + ex.InnerException);
        }
    }

    protected void btSearchHO_Click(object sender, EventArgs e)
    {
        try
        {
            Ping pg = new Ping();
            PingReply reply = pg.Send("172.16.1.18");
            bool status = reply.Status == IPStatus.Success;
            if (status == false)
            {
                dvHOStatusValue.Style.Add("background-color", "red");
                hdfHOConnected.Value = "false";
            }
            else
            {
                dvHOStatusValue.Style.Add("background-color", "green");
                hdfHOConnected.Value = "true";
            }
        }
        catch (Exception ex)
        {
            app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
            ut.Logs("", "Appraisal", "Target Driver", "fm_targetdriver", "Page_Load", "Exception", ex.Message + ex.InnerException);
        }
    }
    protected void ddlSync_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlSync.SelectedValue) == 3 || Convert.ToInt32(ddlSync.SelectedValue) == 301 
            || Convert.ToInt32(ddlSync.SelectedValue) == 9 || Convert.ToInt32(ddlSync.SelectedValue) == 12 || Convert.ToInt32(ddlSync.SelectedValue) == 302 || Convert.ToInt32(ddlSync.SelectedValue) == 303)
        {
            dvPeriod.Visible = true;
        }
        else { dvPeriod.Visible = false; }
    }
}