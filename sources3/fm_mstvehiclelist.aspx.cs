using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_mstvehiclelist : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@salespointcd",Request.Cookies["sp"].Value));
            bll.vBindingGridToSp(ref grdvehicle, "sp_tmst_vehicle_get",arr);
        }
    }
    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_mstvehicles.aspx");
    }
    protected void grdvehicle_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grdvehicle.EditIndex = e.NewEditIndex;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
        bll.vBindingGridToSp(ref grdvehicle, "sp_tmst_vehicle_get", arr);
        DropDownList cbemployee = (DropDownList)grdvehicle.Rows[e.NewEditIndex].FindControl("cbemployee");
        DropDownList cbstatus = (DropDownList)grdvehicle.Rows[e.NewEditIndex].FindControl("cbstatus");
        arr.Clear();
        //arr.Add(new cArrayList("@job_sta_id", "D"));
        bll.vBindingComboToSp(ref cbemployee, "sp_tmst_employee_get2", "emp_cd", "emp_nm");
        bll.vBindingFieldValueToCombo(ref cbstatus, "vhc_status");
    }
    protected void grdvehicle_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        Label lbvhccode = (Label)grdvehicle.Rows[e.RowIndex].FindControl("lbvhccode");
        DropDownList cbemployee = (DropDownList)grdvehicle.Rows[e.RowIndex].FindControl("cbemployee");
        DropDownList cbstatus = (DropDownList)grdvehicle.Rows[e.RowIndex].FindControl("cbstatus");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@vhc_cd", lbvhccode.Text));
        arr.Add(new cArrayList("@emp_cd", cbemployee.SelectedValue.ToString()));
        arr.Add(new cArrayList("@vhc_status", cbstatus.SelectedValue.ToString()));
        bll.vUpdateMstVehicle(arr);
        grdvehicle.EditIndex=-1;
        arr.Clear();
         arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
        bll.vBindingGridToSp(ref grdvehicle, "sp_tmst_vehicle_get", arr);
    }
    protected void grdvehicle_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        grdvehicle.EditIndex = -1;
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
        bll.vBindingGridToSp(ref grdvehicle, "sp_tmst_vehicle_get", arr);
    }

    protected void grdvehicle_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        grdvehicle.PageIndex = e.NewPageIndex;
        bll.vBindingGridToSp(ref grdvehicle, "sp_tmst_vehicle_get", arr);
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport('fm_report2.aspx?src=msveh&salespointcd=" + Request.Cookies["sp"].Value + "');", true);
    }
}