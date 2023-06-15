using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_apporder : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingGridToSp(ref grd, "sp_tmst_salesorder_getbyinfo");
        }
    }
    protected void grd_RowEditing(object sender, GridViewEditEventArgs e)
    {
        
        grd.EditIndex = e.NewEditIndex;
        bll.vBindingGridToSp(ref grd, "sp_tmst_salesorder_getbyinfo");
        DropDownList cbstatus = (DropDownList)grd.Rows[e.NewEditIndex].FindControl("cbstatus");
        bll.vBindingFieldValueToCombo(ref cbstatus, "app_sta_id");
    }
    protected void grd_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grd.EditIndex = - 1;
        bll.vBindingGridToSp(ref grd, "sp_tmst_salesorder_getbyinfo");
    }
    protected void grd_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        Label lbsocd = (Label)grd.Rows[e.RowIndex].FindControl("lbsocd");
        DropDownList cbstatus = (DropDownList)grd.Rows[e.RowIndex].FindControl("cbstatus");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@so_cd", lbsocd.Text));
        arr.Add(new cArrayList("@app_sta_id", cbstatus.SelectedValue.ToString()));
        bll.vUpdateSalesOrderInfoByStatus(arr);
        grd.EditIndex = -1;
        bll.vBindingGridToSp(ref grd, "sp_tmst_salesorder_getbyinfo");
    }
}