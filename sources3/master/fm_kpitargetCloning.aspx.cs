using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_kpitargetCloning : System.Web.UI.Page
{
    cbll bll = new cbll();
    Utitlity ut = new Utitlity();
    cdal cdl = new cdal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //sp_driverIncentiveCloning
            //sp_DriverTagetbyPeriod_get
            //select* from thrd_kpi_target where  period = '201807'
            //select* from trpt_hrd_kpiresult where period = '201807'
            //select* from trpt_hrd_kpiresultdtl where period = '201807'
            //select* from thrd_CalculateIncetive where  period = '201807'
            //select* from thrd_CalIncetiveDtl where CalIncetiveID in (select   CalIncetiveID from thrd_CalculateIncetive where period = '201807'   )
            //select* from thrd_IncetiveRangeDtl where CalIncetiveID in (select   CalIncetiveID from thrd_CalculateIncetive where  period = '201807'   )

            //Only above Table using in Form 
            List<cArrayList> arr = new List<cArrayList>();
            bll.vBindingComboToSp(ref cbperiod, "sp_tmst_period_get", "period_cd", "period_nm");
            bll.vBindingComboToSp(ref cbperiodTo, "sp_tmst_period_get", "period_cd", "period_nm");
            cbperiod.SelectedValue = bll.sGetControlParameter("period");
            bll.vBindingComboToSp(ref cbsalespoint, "sp_tmst_salespoint_get", "salespointcd", "salespoint_nm");
            cbsalespoint_SelectedIndexChanged(sender, e);
            arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));


            arr.Clear();
            arr.Add(new cArrayList("@level_no", 2));
            vBindingGrid();
        }
    }
   
    void vBindingGrid()
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@period", cbperiodTo.SelectedValue.ToString()));
        arr.Add(new cArrayList("@salespointcd",cbsalespoint.SelectedValue.ToString()));
        arr.Add(new cArrayList("@level_cd", "1"));
        arr.Add(new cArrayList("@job_title_cd", "5"));
        bll.vBindingGridToSp(ref grd, "sp_DriverTagetbyPeriod_get", arr);
    }

   
    protected void btnew_Click(object sender, EventArgs e)
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            string returnValue = string.Empty;

            arr.Add(new cArrayList("@periodFrom", cbperiod.SelectedValue.ToString()));
            arr.Add(new cArrayList("@periodTo", cbperiodTo.SelectedValue.ToString()));
            arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
            arr.Add(new cArrayList("@createdBY", Request.Cookies["usr_id"].Value));

            int checkRecord = Convert.ToInt32(bll.vLookUp("select count(*) from thrd_kpi_target where  period = '" + cbperiodTo.SelectedValue.ToString() 
                + "' and salespointcd = '" + cbsalespoint.SelectedValue.ToString() + "' and level_cd ='1' and job_title_cd ='5'"   ));

            if (checkRecord > 0) {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Target already available','Target already available','warning');", true);
            }
            else {

                bll.vDriverIncentiveCloning(arr, ref returnValue);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Target saved successfully','Target saved successfully','success');", true);
                string[] retArr = returnValue.Split('|');

                string err = "N/A";

                if (returnValue.Split('|').Count() > 1)
                {

                }
            }
            
            vBindingGrid();
        }
        catch (Exception ex)
        {

            ut.Logs("", "Driver Incentive", "Target Colone", "fm_HRDPACloning", "btnsave_Click", "Exception", ex.Message + ex.InnerException);
            throw;
        }
    }
    protected void grd_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        //vBindingGrdDtl();
    }

    protected void cbsalespoint_SelectedIndexChanged(object sender, EventArgs e)
    {
        vBindingGrid();
    }

    protected void grd_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void cbperiodTo_SelectedIndexChanged(object sender, EventArgs e)
    {
        vBindingGrid();
    }

    public class SyncResult
    {
        public string BranchName { get; set; }
        public string SyncName { get; set; }
        public bool isSyncSuccess { get; set; }
        public string error { get; set; }
    }
}