using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class promotor_lookupexhibitinttrf : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@exhibit_cd", Request.Cookies["exh_cd"].Value.ToString()));
            bll.vBindingGridToSp(ref grd, "sp_texhibition_internaltrf_get", arr);
        }
    }
    protected void grd_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Label lbtrfno = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbtrfno");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "window.opener.RefreshData('"+lbtrfno.Text+"');window.close();", true);
    }
}