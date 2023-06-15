using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppClassTools;

public partial class fm_activeMerchandiserCloning : System.Web.UI.Page
{
    cbll bll = new cbll();
    Utitlity ut = new Utitlity();
    cdal cdl = new cdal();

    AppClass app = new AppClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingComboToSp(ref ddlPeriodFrom, "sp_tmst_period_get", "period_cd", "period_nm");
            bll.vBindingComboToSp(ref ddlPeriodTo, "sp_tmst_period_get", "period_cd", "period_nm");
            bll.vBindingComboToSp(ref cbsalespoint, "sp_tmst_salespoint_get", "salespointcd", "salespoint_nm");
            cbsalespoint_SelectedIndexChanged(sender, e);
            ddlPeriodTo_SelectedIndexChanged(sender, e);
        }
    }

    protected void cbsalespoint_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Clear();
        arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
        arr.Add(new cArrayList("@period", ddlPeriodTo.SelectedValue.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_Merchandiser_getActive", arr);
    }

 
    protected void grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }


    protected void btUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            // here we check that data already use / duplicate

            List<cArrayList> arr = new List<cArrayList>();

            arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue));
            arr.Add(new cArrayList("@periodFrom", ddlPeriodFrom.SelectedValue));
            arr.Add(new cArrayList("@periodTo", ddlPeriodTo.SelectedValue));
            bll.vInsertActiveMerchandiserCloneHO(arr);
            cbsalespoint_SelectedIndexChanged(sender, e);
            app.BootstrapAlert(lblMsg, "Data cloning successfully", app.alertType = AppClassTools.AppClass.BootstrapAlertType.Success, true);
        }
        catch (Exception ex)
        {
            app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);

            ut.Logs("", "Merchandiser Incentive HO", "Merchandiser Active Cloning", "fm_activeMerchandiserCloning", "btUpdate_Click", "Exception", ex.Message + ex.InnerException);
        }
    }

    protected void btView_Click(object sender, EventArgs e)
    {

    }

    protected void ddlPeriodTo_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Clear();
        arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
        arr.Add(new cArrayList("@period", ddlPeriodTo.SelectedValue.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_Merchandiser_getActive", arr);
    }
}