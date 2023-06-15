using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_fm_appcanvas : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingGridToSp(ref grd, "sp_tmst_canvasorder_getbyinfo");
        }
    }
    protected void grd_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grd.EditIndex = e.NewEditIndex;
        bll.vBindingGridToSp(ref grd, "sp_tmst_canvasorder_getbyinfo");
        
    }
    protected void grd_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        TextBox txreason = (TextBox)grd.Rows[e.RowIndex].FindControl("txreason");
        DropDownList cbstatus = (DropDownList)grd.Rows[e.RowIndex].FindControl("cbstatus");
        Label lbsocd = (Label)grd.Rows[e.RowIndex].FindControl("lbsocd");
        //arr.Add(new cArrayList("@reasn", txreason.Text));
        arr.Add(new cArrayList("@so_cd", lbsocd.Text));
        //arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@app_sta_id", cbstatus.SelectedValue.ToString()));
        bll.vUpdateCanvasOrderInfoByAppStaID(arr);
        arr.Clear();
        arr.Add(new cArrayList("@executedby", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@reasn", txreason.Text));
        arr.Add(new cArrayList("@audit_typ", "U"));
        arr.Add(new cArrayList("@audit_object", "tcanvasorder_info"));
        bll.vInsertAuditTrail(arr);
        grd.EditIndex = -1;
        bll.vBindingGridToSp(ref grd, "sp_tmst_canvasorder_getbyinfo");
    }
    protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if ((e.Row.RowType == DataControlRowType.DataRow) && (e.Row.RowState == DataControlRowState.Edit))
        {
            DropDownList cbstatus = (DropDownList)e.Row.FindControl("cbstatus");
            bll.vBindingFieldValueToCombo(ref cbstatus, "app_sta_id");
        }
    }
}