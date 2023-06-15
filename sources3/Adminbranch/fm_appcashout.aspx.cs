using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_fm_appcashout : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingGridToSp(ref grd, "sp_tcashregout_getbystatus");
        }
    }
    protected void grd_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grd.EditIndex = e.NewEditIndex;
        bll.vBindingGridToSp(ref grd, "sp_tcashregout_getbystatus");
        DropDownList cbapproval = (DropDownList)grd.Rows[e.NewEditIndex].FindControl("cbapproval");
        bll.vBindingFieldValueToCombo(ref cbapproval, "cashout_sta_id");
        Label lbcashoutno = (Label)grd.Rows[e.NewEditIndex].FindControl("lbcashoutno");
    }
    protected void grd_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        Label lbcashoutno = (Label)grd.Rows[e.RowIndex].FindControl("lbcashoutno");
        DropDownList cbapproval = (DropDownList)grd.Rows[e.RowIndex].FindControl("cbapproval");
        arr.Add(new cArrayList("@casregout_cd", lbcashoutno.Text));
        arr.Add(new cArrayList("@cashout_sta_id", cbapproval.SelectedValue.ToString()));
        bll.vUpdateCashRegisterByCashStaID(arr);
        grd.EditIndex = -1;
        bll.vBindingGridToSp(ref grd, "sp_tcashregout_getbystatus");

    }
    protected void grd_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grd.EditIndex = -1;
        bll.vBindingGridToSp(ref grd, "sp_tcashregout_getbystatus");
    }
}