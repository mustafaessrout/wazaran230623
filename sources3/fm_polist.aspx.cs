using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_polist : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingGridToSp(ref grdpo, "sp_tmst_po_get");
        }
    }
    protected void btadd_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_po.aspx");
    }
    protected void grdpo_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Label lbpono = (Label)grdpo.Rows[e.NewSelectedIndex].FindControl("lbpono");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@po_no", lbpono.Text));
        bll.vBindingGridToSp(ref grddtl, "sp_tpo_dtl_get", arr);
    }
}