using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_mst_employeeVehicleBranch : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@salespointcd", HttpContext.Current.Request.Cookies["sp"].Value));
            bll.vBindingGridToSp(ref grd, "sp_tmst_vehicle_get", arr);
        }
    }

    protected void grd_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        HiddenField hdfemp_cd = (HiddenField)grd.Rows[e.RowIndex].FindControl("hdfemp_cd");
        HiddenField hdfvhc_cd = (HiddenField)grd.Rows[e.RowIndex].FindControl("hdfvhc_cd");

        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@emp_cd", hdfemp_cd.Value));
        arr.Add(new cArrayList("@vhc_cd", hdfvhc_cd.Value));
        bll.vDelVehicle(arr);

        arr.Clear();
        arr.Add(new cArrayList("@salespointcd", HttpContext.Current.Request.Cookies["sp"].Value));
        bll.vBindingGridToSp(ref grd, "sp_tmst_vehicle_get", arr);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('delete successfully','" + hdfemp_cd.Value + "','success');", true);
    }
}