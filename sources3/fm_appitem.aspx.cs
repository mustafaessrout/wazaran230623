using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_appitem : System.Web.UI.Page
{
    cbll bll = new cbll();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@item_sta_id", "W"));
            bll.vBindingGridToSp(ref grd, "sp_tmst_item_get_by_sta", arr);
        }
    }
    protected void grd_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grd.EditIndex = e.NewEditIndex;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@item_sta_id", "W"));
        bll.vBindingGridToSp(ref grd, "sp_tmst_item_get_by_sta", arr);
        DropDownList cbstatus = (DropDownList)grd.Rows[e.NewEditIndex].FindControl("cbstatus");
        arr.Clear();
        arr.Add(new cArrayList("@hiddendata", 0));
        arr.Add(new cArrayList("@fld_nm", "item_sta_id"));
        bll.vBindingFieldValueToCombo(ref cbstatus,arr);
    }
    protected void grd_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grd.EditIndex = -1;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@item_sta_id", "W"));
        bll.vBindingGridToSp(ref grd, "sp_tmst_item_get_by_sta", arr);
    }
    protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbbranded = (Label)e.Row.FindControl("lbbranded");
            Label lbitemcode = (Label)e.Row.FindControl("lbitemcode");
            lbbranded.Text = bll.sGetBranded(lbitemcode.Text);
        }
    }
    protected void grd_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        Label lbitemcode = (Label) grd.Rows[e.RowIndex].FindControl("lbitemcode");
        DropDownList cbstatus = (DropDownList)grd.Rows[e.RowIndex].FindControl("cbstatus");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@item_cd", lbitemcode.Text));
        arr.Add(new cArrayList("@item_sta_id", cbstatus.SelectedValue.ToString()));
        bll.vUpdateMstItem(arr);
        grd.EditIndex = -1;
        arr.Clear();
        arr.Add(new cArrayList("@item_sta_id", "W"));
        bll.vBindingGridToSp(ref grd, "sp_tmst_item_get_by_sta", arr);
      
    }
    protected void grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        grd.PageIndex = e.NewPageIndex;
        arr.Add(new cArrayList("@item_sta_id", "W"));
        bll.vBindingGridToSp(ref grd, "sp_tmst_item_get_by_sta", arr);
      
    }
    protected void btrefresh_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@item_sta_id", "W"));
        bll.vBindingGridToSp(ref grd, "sp_tmst_item_get_by_sta", arr);
    }
}