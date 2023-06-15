using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class lookup_deposit : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { 
            //string sDepCode = Request.QueryString["id"];
            //List<cArrayList> arr = new List<cArrayList>();
            //arr.Add(new cArrayList("@dep_cd", sDepCode));
            bll.vBindingGridToSp(ref grd, "sp_tpayment_deposit_get");
        }
    }
    protected void grd_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Label lbdepcode = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbdepcode");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "CLS", "window.opener.GetDeposit('"+lbdepcode.Text+"');window.close();", true);
    }
}