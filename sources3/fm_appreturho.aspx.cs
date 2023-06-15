using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_appreturho : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@retho_sta_id", "N"));
            bll.vBindingGridToSp(ref grd, "sp_tmst_returho_get", arr);
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tablePageCopy", "tablePageCopy();", true);
    }
    protected void grd_RowEditing(object sender, GridViewEditEventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        grd.EditIndex = e.NewEditIndex;
        arr.Add(new cArrayList("@retho_sta_id", "N"));
        bll.vBindingGridToSp(ref grd, "sp_tmst_returho_get", arr);
        DropDownList cbstatus = (DropDownList)grd.Rows[e.NewEditIndex].FindControl("cbstatus");
        DropDownList cbaction_plan = (DropDownList)grd.Rows[e.NewEditIndex].FindControl("cbaction_plan");
        arr.Clear();
        arr.Add(new cArrayList("@hiddendata", "0"));
        arr.Add(new cArrayList("@fld_nm", "retho_sta_id"));
        bll.vBindingFieldValueToCombo(ref cbstatus ,arr);
        arr.Clear();
        arr.Add(new cArrayList("@hiddendata", "0"));
        arr.Add(new cArrayList("@fld_nm", "action_plan"));
        bll.vBindingFieldValueToCombo(ref cbaction_plan, arr);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tablePageCopy", "tablePageCopy();", true);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "editTable", "editTable();", true);

    }
    protected void grd_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        Label lbrethono = (Label)grd.Rows[e.RowIndex].FindControl("lbreturhono");
        DropDownList cbstatus = (DropDownList)grd.Rows[e.RowIndex].FindControl("cbstatus");
        DropDownList cbaction_plan = (DropDownList)grd.Rows[e.RowIndex].FindControl("cbaction_plan");
        TextBox dtdeadline_dt = (TextBox)grd.Rows[e.RowIndex].FindControl("dtdeadline_dt");

        DateTime ddate = DateTime.ParseExact(dtdeadline_dt.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        if (dtdeadline_dt.Text.Equals(DBNull.Value) || dtdeadline_dt.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al2", "sweetAlert('Warning','Please fill Dateline Date  !','warning');", true);
            return;
        }
        arr.Add(new cArrayList("@returho_no", lbrethono.Text));
        arr.Add(new cArrayList("@retho_sta_id", cbstatus.SelectedValue.ToString()));
        arr.Add(new cArrayList("@action_plan", cbaction_plan.SelectedValue.ToString()));
        arr.Add(new cArrayList("@deadline_dt", ddate));
        bll.vUpdateMstReturHo(arr);
        grd.EditIndex = -1;
        arr.Clear();
        arr.Add(new cArrayList("@retho_sta_id", "N"));
        bll.vBindingGridToSp(ref grd, "sp_tmst_returho_get", arr);
    }
    protected void grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd.PageIndex = e.NewPageIndex;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@retho_sta_id", "N"));
        bll.vBindingGridToSp(ref grd, "sp_tmst_returho_get", arr);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tablePageCopy", "tablePageCopy();", true);
    }

}