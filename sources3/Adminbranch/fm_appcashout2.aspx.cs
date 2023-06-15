using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Adminbranch_fm_appcashout2 : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            vInitGrid();
        }
    }

    void vInitGrid()
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@cashout_sta_id", "N"));
        bll.vBindingGridToSp(ref grd, "sp_tcashout_request_getbystatus", arr);
    }
    protected void grd_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grd.EditIndex = e.NewEditIndex;
        vInitGrid();
        DropDownList cbstatus = (DropDownList)grd.Rows[e.NewEditIndex].FindControl("cbstatus");
        bll.vBindingFieldValueToCombo(ref cbstatus, "cashout_sta_id");
    }
    protected void grd_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        DropDownList cbstatus = (DropDownList)grd.Rows[e.RowIndex].FindControl("cbstatus");
        Label lbcashoutcode = (Label)grd.Rows[e.RowIndex].FindControl("lbcashoutcode");
        arr.Add(new cArrayList("@cashout_sta_id", cbstatus.SelectedValue.ToString()));
        arr.Add(new cArrayList("@cashout_cd", lbcashoutcode.Text));
        bll.vUpdateCashoutRequestByStatus(arr);
        grd.EditIndex = -1;
        vInitGrid();
    }
}