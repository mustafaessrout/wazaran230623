using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Adminbranch_fm_confirmpayment : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            vBindingGrid();
        }
    }
    protected void grd_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grd.EditIndex = e.NewEditIndex;
        vBindingGrid();
        DropDownList cbstatus = (DropDownList)grd.Rows[e.NewEditIndex].FindControl("cbstatus");
        bll.vBindingFieldValueToCombo(ref cbstatus, "dep_sta_id");
    }

    void vBindingGrid()
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@dep_sta_id", "N"));
        bll.vBindingGridToSp(ref grd, "sp_tbank_deposit_getbystatus", arr);
    }
    protected void grd_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        DropDownList cbstatus = (DropDownList)grd.Rows[e.RowIndex].FindControl("cbstatus");
        HiddenField hdids = (HiddenField)grd.Rows[e.RowIndex].FindControl("hdids");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@ids", hdids.Value.ToString()));
        arr.Add(new cArrayList("@dep_sta_id", cbstatus.SelectedValue.ToString()));
        bll.vUpdateBankDepositByStatus(arr);
        grd.EditIndex = -1;
        vBindingGrid();
    }
    protected void grd_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grd.EditIndex = -1;
        vBindingGrid();
    }
}