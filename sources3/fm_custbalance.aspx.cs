using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_custbalance : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            bll.vBindingComboToSp(ref cbperiod, "sp_getperiod", "period", "ymtName");
        }
    }
    protected void cbperiod_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@period", cbperiod.SelectedValue.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_tmonthend_custbalance", arr);
    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opr", "openreport('fm_report2.aspx?src=cb&prd=" + cbperiod.SelectedValue.ToString() + "');", true);
    }
}