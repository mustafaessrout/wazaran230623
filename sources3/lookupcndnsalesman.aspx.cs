using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class lookupcndnsalesman : System.Web.UI.Page
{
    cbll bll =new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@cndn_sta_id", "W"));
            bll.vBindingGridToSp(ref grd, "sp_tcndn_salesman_getbystatus", arr);
        }
    }

    protected void grd_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Label lbcndncode = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbcndncode");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), 
            "window.opener.SelectData('"+lbcndncode.Text+"');window.close();", true);
    }
}