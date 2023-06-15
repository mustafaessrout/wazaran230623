using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_pendingcashregout : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@cashout_sta_id", "N"));
            bll.vBindingGridToSp(ref grd, "sp_tcashregout_dtl_getbystatus", arr);
        }
    }
    protected void grd_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Label lbcashregout = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbcashregout");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Reminding approval has sent via SMS/Email','"  + lbcashregout.Text + "','info');", true);
    }
}