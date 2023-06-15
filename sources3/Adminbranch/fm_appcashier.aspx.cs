using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_fm_appcashier : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingGridToSp(ref grd, "tcashregister_closing_getbyack");
        }
    }
    protected void grd_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grd.EditIndex = e.NewEditIndex;
        bll.vBindingGridToSp(ref grd, "tcashregister_closing_getbyack");
        DropDownList cboack = (DropDownList)grd.Rows[e.NewEditIndex].FindControl("cback");
        bll.vBindingFieldValueToCombo(ref cboack, "acknowledge");
    }
    protected void grd_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grd.EditIndex = -1;
        bll.vBindingGridToSp(ref grd, "tcashregister_closing_getbyack");
    }
    protected void grd_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        DropDownList cback = (DropDownList)grd.Rows[e.RowIndex].FindControl("cback");
        Label lbclosingno = (Label)grd.Rows[e.RowIndex].FindControl("lbchclosingno");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@chclosingno", lbclosingno.Text));
        arr.Add(new cArrayList("@acknowledge", cback.SelectedValue.ToString()));
        bll.vUpdateCashRegisterClosingAck(arr);
        grd.EditIndex = -1;
        bll.vBindingGridToSp(ref grd, "tcashregister_closing_getbyack");
    }
}