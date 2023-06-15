using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_SkipPrioirtyItemsLog : System.Web.UI.Page
{
    cbll bll = new cbll();
    Utitlity ut = new Utitlity();
    cdal cdl = new cdal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                bll.vBindingComboToSp(ref cbperiod, "sp_tmst_period_get", "period_cd", "period_nm");
                bll.vBindingComboToSp(ref cbsalespoint, "sp_tmst_salespoint_get", "salespointcd", "salespoint_nm");
                string period = Convert.ToString(DateTime.Now.Year) + (Convert.ToString(DateTime.Now.Month).Length == 1 ? ("0" + Convert.ToString(DateTime.Now.Month)) : Convert.ToString(DateTime.Now.Month));
                cbperiod.SelectedValue = period;

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Error occured during pageload','error');", true);
                ut.Logs("", "Skip Prioirty Item Log", "Skip Prioirty Item Log Head Office", "fm_SkipPrioirtyItemsLog", "PageLoad", "Exception", ex.Message + ex.InnerException);
            }
        }
    }


    protected void cbsalespoint_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid();
    }
    
    protected void cbperiod_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid();
    }

    void BindGrid() {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salespointcd",cbsalespoint.SelectedValue));
        arr.Add(new cArrayList("@period",cbperiod.SelectedValue));
        bll.vBindingGridToSp(ref grd, "sp_SkipPrioirtyItemsLog", arr);
    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        //&salespointcd=" + cbsalespoint.SelectedValue + "
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport('/fm_report2.aspx?src=SkipPrioirtyItemsLog&period=" + cbperiod.SelectedValue + "');", true);
    }
}