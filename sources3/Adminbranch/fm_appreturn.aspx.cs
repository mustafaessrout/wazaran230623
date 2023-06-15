using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Adminbranch_fm_appreturn : System.Web.UI.Page
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
        bll.vBindingGridToSp(ref grd, "sp_tsalesreturn_getbystatusnew");
    }
    protected void grd_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grd.EditIndex = e.NewEditIndex;
        vInitGrid();
        DropDownList cbstatus = (DropDownList)grd.Rows[e.NewEditIndex].FindControl("cbstatus");
        bll.vBindingFieldValueToCombo(ref cbstatus, "retur_sta_id");
    }
    protected void grd_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        Label lbreturno = (Label)grd.Rows[e.RowIndex].FindControl("lbreturno");
        DropDownList cbstatus = (DropDownList)grd.Rows[e.RowIndex].FindControl("cbstatus");
        string sCheck = bll.vLookUp("select dbo.fn_checkreturnprice('"+lbreturno.Text+"')");
        if (sCheck != "ok")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('There is item still zero price','Pls feedback to branch for price','warning');", true);
            return;
        }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@retur_no", lbreturno.Text));
        arr.Add(new cArrayList("@retur_sta_id", cbstatus.SelectedValue.ToString()));
        bll.vUpdateSalesReturnByStatus2(arr);
        grd.EditIndex = -1;
        vInitGrid();
    }

    protected void grd_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grd.EditIndex = -1;
        vInitGrid();
    }
}