using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class lookupcndncustomer : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
         
            bll.vBindingFieldValueToComboWithChoosen(ref cbstatus, "cndncust_sta_id");
        }
    }

    protected void cbstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@cndncust_sta_id", cbstatus.SelectedValue));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
        bll.vBindingGridToSp(ref grd, "sp_tmst_cndncustomer_getbystatus", arr);
    }

    protected void grd_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Label lbcndnnumber = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbcndnnumber");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), 
            "window.opener.SelectData('"+lbcndnnumber.Text+"');window.close();", true);
    }
}