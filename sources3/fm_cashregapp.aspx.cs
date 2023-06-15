using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_cashregapp : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@cashout_sta_id", "A"));
            bll.vBindingGridToSp(ref grd, "sp_tcashregout_get", arr);
        }
    }
    protected void grd_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Label lbcasregoutcode = (Label) grd.Rows[e.NewSelectedIndex].FindControl("lbcasregoutcode");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@cashregout_cd", lbcasregoutcode.Text));
        bll.vBindingGridToSp(ref grddtl, "sp_tcashregout_dtl_get", arr);

    }
    protected void grd_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_cashregisterentry.aspx");
    }
}