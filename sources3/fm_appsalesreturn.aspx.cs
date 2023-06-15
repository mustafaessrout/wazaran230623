using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_appsalesreturn : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();        
            arr.Add(new cArrayList("@retur_sta_id", "N"));
            bll.vBindingGridToSp(ref grdapp, "sp_tsalesreturn_getbystatus", arr);
        }
    }
    protected void grdapp_RowEditing(object sender, GridViewEditEventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        grdapp.EditIndex = e.NewEditIndex;
        arr.Add(new cArrayList("@retur_sta_id", "N"));
        bll.vBindingGridToSp(ref grdapp, "sp_tsalesreturn_getbystatus", arr);
        DropDownList cbapp = (DropDownList)grdapp.Rows[e.NewEditIndex].FindControl("cbapp");
        bll.vBindingFieldValueToCombo(ref cbapp, "retur_sta_id",false);
    }
    protected void grdapp_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        Label lbreturno = (Label)grdapp.Rows[e.RowIndex].FindControl("lbretur_no");

        string sPrice = bll.vLookUp("select dbo.fn_checkreturpricebyno('"+lbreturno.Text+"')");
        if (sPrice != "ok")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('"+sPrice+"','"+lbreturno.Text+"','warning');", true);
            return;
        }
        Label lbretur_no = (Label)grdapp.Rows[e.RowIndex].FindControl("lbretur_no");
        DropDownList cbapp = (DropDownList)grdapp.Rows[e.RowIndex].FindControl("cbapp");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@retur_no", lbretur_no.Text));
        arr.Add(new cArrayList("@retur_sta_id", cbapp.SelectedValue.ToString()));
        bll.vUpdateTsalesReturn(arr);
        arr.Clear();
        grdapp.EditIndex = -1;
        arr.Add(new cArrayList("@retur_sta_id", "N"));
        bll.vBindingGridToSp(ref grdapp, "sp_tsalesreturn_getbystatus", arr);
    }
    protected void grdapp_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        Label lbretur_no = (Label)grdapp.Rows[e.RowIndex].FindControl("retur_no");
        grdapp.EditIndex = -1;
        arr.Add(new cArrayList("@retur_sta_id", "N"));
        bll.vBindingGridToSp(ref grdapp, "sp_tsalesreturn_getbystatus", arr);
    }
    protected void grdapp_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
      
    }
    protected void grdapp_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        grdapp.PageIndex = e.NewPageIndex;
        arr.Add(new cArrayList("@retur_sta_id", "N"));
        bll.vBindingGridToSp(ref grdapp, "sp_tsalesreturn_getbystatus", arr);
    }
    protected void grdapp_SelectedIndexChanging1(object sender, GridViewSelectEventArgs e)
    {
        Label lbreturnno = (Label)grdapp.Rows[e.NewSelectedIndex].FindControl("lbretur_no");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@retur_no", lbreturnno.Text));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        bll.vBindingGridToSp(ref grddtl, "sp_tsalesreturn_dtl_get", arr);
    }
}