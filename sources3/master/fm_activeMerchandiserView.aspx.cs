using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppClassTools;

public partial class fm_activeMerchandiserView : System.Web.UI.Page
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
            bll.vBindingComboToSp(ref cbsalespoint, "sp_tmst_salespoint_get", "salespointcd", "salespoint_nm");
            cbsalespoint_SelectedIndexChanged(sender, e);
        }
    }

    protected void cbsalespoint_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
        arr.Add(new cArrayList("@period",ddlPeriod.SelectedValue.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_Merchandiser_getActive", arr);
    }
}