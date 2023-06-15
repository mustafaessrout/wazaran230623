using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class lookupsample : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btsearch_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@sample_cd", txsearch.Text));
        bll.vBindingGridToSp(ref grd, "sp_tstock_sample_search", arr);
    }

    protected void grd_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Label lbsamplecode = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbsamplecode");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), 
                "window.opener.SelectData('"+lbsamplecode.Text+"');window.close();", true);
    }
}