using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_lookup : System.Web.UI.Page
{
    cbll bll = new cbll();
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            bll.vDelWrkLookup(arr);
            arr.Clear();
            arr.Add(new cArrayList("@level_no", "1"));
            bll.vBindingComboToSp(ref cbbranded, "sp_tmst_product_get", "prod_cd", "prod_nm", arr);
            cbbranded_SelectedIndexChanged(sender, e);
            
        }
    }
    protected void cbbranded_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@level_no", "2"));
        arr.Add(new cArrayList("@prod_cd_parent", cbbranded.SelectedValue.ToString()));
        bll.vBindingComboToSp(ref cbproduct, "sp_tmst_product_get", "prod_cd", "prod_nm", arr);
    }
    protected void btsearch_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@prod_cd", cbproduct.SelectedValue.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_tmst_item_get", arr);
    }
    protected void btsubmit_Click(object sender, EventArgs e)
    {
        //ScriptManager.RegisterStartupScript(Page, this.GetType(), "myscript", "window.close()",true);
    }
    protected void grd_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
          
        }
    }
    protected void grd_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vDelWrkLookup(arr);
        Label lbitemcode = (Label)grd.SelectedRow.FindControl("lbitemcode");
        Label lbitemname = (Label)grd.SelectedRow.FindControl("lbitemname");
        Label lbarabic = (Label)grd.SelectedRow.FindControl("lbarabic");
        arr.Add(new cArrayList("@item_cd", lbitemcode.Text));
        arr.Add(new cArrayList("@item_nm", lbitemname.Text));
        arr.Add(new cArrayList("@item_arabic", lbarabic.Text));
        bll.vInsertWrkFreeItem(arr);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "xd", "window.opener.updpnl()", true);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "cls", "window.close()", true);
    }
    protected void grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        grd.PageIndex = e.NewPageIndex;
        arr.Add(new cArrayList("prod_cd", cbproduct.SelectedValue.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_tmst_item_get",arr);
    }
    protected void UpdatePanel2_Unload(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "xxx", "window.opener.refresh()", true);
    }
}