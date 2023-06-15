using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class lookupcustomer : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { 
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@rdcust", Request.QueryString["ct"]));
            arr.Add(new cArrayList("@val", Request.QueryString["val"]));
            bll.vBindingGridToSp(ref grd, "sp_tmst_customer_getbytype", arr);
            
        }
    }
    protected void grd_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Label lbcustcode = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbcustcode");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "clos", "window.opener.RefreshData('"+lbcustcode.Text+"');window.close();", true);
    }
}