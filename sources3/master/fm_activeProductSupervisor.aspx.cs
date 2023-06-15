using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppClassTools;

public partial class fm_activeProductSupervisor : System.Web.UI.Page
{
    cbll bll = new cbll();
    Utitlity ut = new Utitlity();
    cdal cdl = new cdal();

    AppClass app = new AppClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingComboToSp(ref ddlPeriod, "sp_tmst_period_get", "period_cd", "period_nm");
            ddlPeriod_SelectedIndexChanged(sender, e);
        }
    }

    protected void ddlPeriod_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        //arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
        arr.Add(new cArrayList("@period", ddlPeriod.SelectedValue.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_ProductSupervisor_getbyjobtitle", arr);
    }



    protected void grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }


    protected void btUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            bool isSave = false;
            foreach (GridViewRow row in grd.Rows)
            {
                var period = ddlPeriod.SelectedValue;
                Label lbprodcd = (Label)row.FindControl("lblEmpno");
                CheckBox chkIsTeamLeader = (CheckBox)row.FindControl("chkIsTeamLeader");
                CheckBox chkCanDelete = (CheckBox)row.FindControl("chkCanDelete");
                if (chkCanDelete.Checked == true)
                {
                    arr.Clear();
                    arr.Add(new cArrayList("@emp_cd", lbprodcd.Text));
                    arr.Add(new cArrayList("@isTeamLeader", chkIsTeamLeader.Checked));
                    arr.Add(new cArrayList("@period", ddlPeriod.SelectedValue));
                    if (chkIsTeamLeader.Checked)
                    {
                        arr.Add(new cArrayList("@job_title_cd", "22"));
                    }
                    else { arr.Add(new cArrayList("@job_title_cd", "12")); }
                    arr.Add(new cArrayList("@level_cd", "1"));
                    bll.vInsertActiveMerchandiserHO(arr);
                }
                else
                {
                    arr.Clear();
                    arr.Add(new cArrayList("@emp_cd", lbprodcd.Text));
                    arr.Add(new cArrayList("@period", ddlPeriod.SelectedValue));
                    bll.vDeleteActiveMerchandiserHO(arr);
                }

                ddlPeriod_SelectedIndexChanged(sender, e);
                app.BootstrapAlert(lblMsg, "Data updated successfully", app.alertType = AppClassTools.AppClass.BootstrapAlertType.Success, true);
                arr.Clear();
                bll.vBindingGridToSp(ref grd, "sp_Merchandiser_getbyjobtitle", arr);
            }
        }
        catch (Exception ex)
        {
            app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);

            ut.Logs("", "Merchandiser Incentive HO", "Merchandiser Active", "fm_activeMerchandiser", "btUpdate_Click", "Exception", ex.Message + ex.InnerException);
        }
    }

    protected void btView_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_activeMerchandiserView.aspx", false);
    }

   
}