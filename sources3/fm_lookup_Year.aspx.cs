using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_lookup_Year : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btsearch_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@YearCD", txsearch.Text));
        bll.vBindingGridToSp(ref grd, "sp_Year_get", arr);

    }
    protected void btok_Click(object sender, EventArgs e)
    {
       
    }
    protected void grd_SelectedIndexChanged(object sender, EventArgs e)
    {
        Label lbYearCD = (Label)grd.SelectedRow.FindControl("lbYearCD");

        Session["looYearYearCD"] = lbYearCD.Text;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "updpnl", "closewin()", true);
    }
}