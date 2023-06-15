using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class lookupcashout2 : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingFieldValueToCombo(ref cbstatus, "cashout_sta_id", false);
            vInitGrid();
        }
    }

    void vInitGrid()
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@cashout_sta_id", cbstatus.SelectedValue.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_tcashout_request_getbystatus2",arr);
    }
    protected void cbstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        vInitGrid();
    }
    protected void grd_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Label lbcashoutcode = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbcashoutcode");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "window.opener.DataSelected('"+lbcashoutcode.Text+"');window.close();", true);
    }
}