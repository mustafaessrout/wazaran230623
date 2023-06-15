using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_lookup2 : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@usr_id",Request.Cookies["usr_id"].Value.ToString()));
            bll.vDelWrkFreeItem(arr);
            bll.vBindingGridToSp(ref grd, "sp_tmst_item_get");
        }
    }
    protected void btsearch_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@item_cd", txsearch.Text));
        arr.Add(new cArrayList("@item_nm", txsearch.Text));
        bll.vBindingGridToSp(ref grd, "sp_tmst_item_search");
    }
    protected void btsubmit_Click(object sender, EventArgs e)
    {
       //  ScriptManager.RegisterStartupScript(Page, Page.GetType(), "cl", "window.close()", true);
       //  ScriptManager.RegisterStartupScript(Page, Page.GetType(), "xd", "window.opener.updpnl()", true);
       // ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "TakeRefresh()", true);
    }
    protected void btadd_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        foreach (GridViewRow row in grd.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chk = (CheckBox)row.FindControl("chselect");
                if (chk.Checked)
                {
                    Label lbitemcode = (Label)row.FindControl("lbitemcode");
                    Label lbitemname = (Label)row.FindControl("lbitemname");
                    Label lbarabic = (Label)row.FindControl("lbarabic");
                    Label lbsize = (Label)row.FindControl("lbsize");
                    arr.Clear();
                    arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                    arr.Add(new cArrayList("@item_cd", lbitemcode.Text));
                    arr.Add(new cArrayList("@item_arabic", lbarabic.Text));
                    arr.Add(new cArrayList("@item_nm", lbitemname.Text));
                   // arr.Add(new cArrayList("@size", lbsize.Text));
                    bll.vInsertWrkFreeItem(arr);
                }
            }
        }
      //  ScriptManager.RegisterStartupScript(Page, Page.GetType(), "rf", "window.opener.updpnl()", true);
      //  ScriptManager.RegisterStartupScript(Page, Page.GetType(), "cl", "window.close()", true);
         arr.Clear(); arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grddtl, "sp_twrk_freeitem_get", arr);
    }
    protected void btdel_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        foreach (GridViewRow row in grddtl.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chk = (CheckBox)row.FindControl("chdel");
                if (chk.Checked)
                {
                    Label lbitemcode = (Label)row.FindControl("lbitemcode");
                    arr.Clear();
                    arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                    arr.Add(new cArrayList("@item_cd", lbitemcode.Text));
                   // arr.Add(new cArrayList("@item_arabic", lbarabic.Text));
                   // arr.Add(new cArrayList("@item_nm", lbitemname.Text));
                    bll.vDelWrkLookup(arr);
                }
            }
        }
        arr.Clear(); arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grddtl, "sp_twrk_lookup2_get", arr);
    }
    protected void btsubmit_Click1(object sender, EventArgs e)
    {

    }
    protected void grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd.PageIndex = e.NewPageIndex;
        bll.vBindingGridToSp(ref grd, "sp_tmst_item_get");
    }
}