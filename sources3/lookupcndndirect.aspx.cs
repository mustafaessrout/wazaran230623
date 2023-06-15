using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class lookupcndndirect : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingFieldValueToCombo(ref cbstatus, "cndn_sta_id");
            cbstatus_SelectedIndexChanged(sender, e);
        }
    }

    protected void cbstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@cndn_sta_id", cbstatus.SelectedValue));
        bll.vBindingGridToSp(ref grd, "sp_tmst_cndnemployee_get", arr);
    }

    protected void grd_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Label lbcndnno = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbcndnno");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "window.opener.selectcn('"+lbcndnno.Text+"');window.close();", true);
    }
}