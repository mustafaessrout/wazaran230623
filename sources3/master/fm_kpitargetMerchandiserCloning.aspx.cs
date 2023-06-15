using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class master_fm_kpitargetMerchandiserCloning : System.Web.UI.Page
{
    cbll bll = new cbll();
    cdal cdl = new cdal();
    Utitlity ut = new Utitlity();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindControl();
        }
    }
    private void BindControl()
    {
        bll.vBindingComboToSp(ref cbsalespoint, "sp_tmst_salespoint_get", "salespointcd", "salespoint_nm");
        bll.vBindingComboToSp(ref cbperiod, "sp_tmst_period_get", "period_cd", "period_nm");
        bll.vBindingComboToSp(ref cbperiodTo, "sp_tmst_period_get", "period_cd", "period_nm");
    }
    protected void cbperiodTo_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@period", cbperiodTo.SelectedValue.ToString()));
        arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
        arr.Add(new cArrayList("@level_cd", "1"));
        arr.Add(new cArrayList("@job_title_cd", "12"));
        bll.vBindingGridToSp(ref grd, "sp_DriverTagetbyPeriod_get", arr);
    }

    protected void btsave_Click(object sender, EventArgs e)
    {
        int checkRecord = Convert.ToInt32(bll.vLookUp("select count(*) from thrd_kpi_target where  period = '" + cbperiodTo.SelectedValue.ToString() + "' and salespointcd = '" + cbsalespoint.SelectedValue.ToString() + "'"
             + " and level_cd ='1' and job_title_cd ='12'"));

        if (checkRecord > 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Target already available','Target already available','warning');", true);
        }
        else
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@toPeriod", cbperiodTo.SelectedValue.ToString()));
            arr.Add(new cArrayList("@fromPeriod", cbperiod.SelectedValue.ToString()));
            arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
            //bll.vMerchandiserIncentiveCloning(arr);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Target saved successfully','Target saved successfully','success');", true);
            arr.Clear();
            arr.Add(new cArrayList("@period", cbperiodTo.SelectedValue.ToString()));
            arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
            arr.Add(new cArrayList("@level_cd", "1"));
            arr.Add(new cArrayList("@job_title_cd", "12"));
            bll.vBindingGridToSp(ref grd, "sp_DriverTagetbyPeriod_get", arr);
        }
    }
}