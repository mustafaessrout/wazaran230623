using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_appdisc : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@disc_sta_id", "W"));
            bll.vBindingGridToSp(ref grdapp, "sp_tmst_discount_get", arr);
        }
    }
    protected void grdapp_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grdapp.EditIndex = e.NewEditIndex;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@disc_sta_id", "W"));
        bll.vBindingGridToSp(ref grdapp, "sp_tmst_discount_get", arr);
        DropDownList cbstatus = (DropDownList)grdapp.Rows[e.NewEditIndex].FindControl("cbstatus");
        arr.Clear();
        arr.Add(new cArrayList("@hiddendata", 0));
        arr.Add(new cArrayList("@fld_nm", "disc_sta_id"));
        bll.vBindingFieldValueToCombo(ref cbstatus, arr);
    }
    protected void grdapp_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        Label lbdisccode = (Label)grdapp.Rows[e.RowIndex].FindControl("lbdisccode");
        DropDownList cbstatus = (DropDownList)grdapp.Rows[e.RowIndex].FindControl("cbstatus");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@disc_cd", lbdisccode.Text));
        arr.Add(new cArrayList("@disc_sta_id", cbstatus.SelectedValue.ToString()));
        bll.vUpdateMstDiscount(arr);
        grdapp.EditIndex = -1;
        arr.Clear();
        arr.Add(new cArrayList("@disc_sta_id", "W"));
        bll.vBindingGridToSp(ref grdapp, "sp_tmst_discount_get", arr);
    }
    protected void grdapp_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        grdapp.EditIndex = -1;
        arr.Add(new cArrayList("@disc_sta_id", "W"));
        bll.vBindingGridToSp(ref grdapp, "sp_tmst_discount_get", arr);
    }
    protected void grdapp_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Label lbdisccode = (Label)grdapp.Rows[e.NewSelectedIndex].FindControl("lbdisccode");
        //Response.Redirect("fm_reqdiscount.aspx?dc=" + lbdisccode.Text);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opx", "openwindow('fm_discountinfo.aspx?dc=" + lbdisccode.Text + "');", true);
    }
}