using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_invreceived : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@do_sta_id", "B"));
            bll.vBindingGridToSp(ref grd, "sp_tmst_do_get", arr);
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "tablePageCopy", "tablePageCopy()", true);
    }
    protected void grd_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grd.EditIndex = e.NewEditIndex;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@do_sta_id", "B"));
        bll.vBindingGridToSp(ref grd, "sp_tmst_do_get", arr);
        DropDownList cbstatus = (DropDownList)grd.Rows[e.NewEditIndex].FindControl("cbstatus");
        bll.vBindingFieldValueToCombo(ref cbstatus, "do_sta_id");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "editTable", "editTable();", true);
    }
    protected void grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd.PageIndex = e.NewPageIndex;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@do_sta_id", "B"));
        bll.vBindingGridToSp(ref grd, "sp_tmst_do_get", arr);
        
    }
}