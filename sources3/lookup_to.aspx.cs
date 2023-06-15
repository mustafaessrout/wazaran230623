using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class lookup_to : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingFieldValueToCombo(ref cbstatus, "so_sta_id");
            cbstatus_SelectedIndexChanged(sender, e);
          //  bll.vBindingGridToSp(ref grd, "sp_tmst_salesorder_get");
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix();", true);
    }

    void vBindingGrid()
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@so_sta_id", cbstatus.SelectedValue.ToString()));
        if (searchbox.Text == "") { arr.Add(new cArrayList("@so_cd", null)); }
        else { arr.Add(new cArrayList("@so_cd", searchbox.Text.ToString())); }
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_tmst_salesorder_get", arr);
    }
    protected void grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd.PageIndex = e.NewPageIndex;
        vBindingGrid();
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix();", true);
        //bll.vBindingGridToSp(ref grd, "sp_tmst_salesorder_get", arr);
    }
    protected void cbstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "slrt", "dvshow.setAttribute('class','divmsg');", true);
      //  img.CssClass = "divmsg";
        vBindingGrid();
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix();", true);
     //   img.CssClass = "divhid";
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "slrt", "dvshow.setAttribute('class','divhid');", true);
    }

    protected void searchboxbtn_Click(object sender, EventArgs e)
    {
        vBindingGrid();
    }
}