using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Adminbranch_fm_rps : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
           
            bll.vBindingFieldValueToCombo(ref cbday, "day_cd");
            arr.Add(new cArrayList("@qry_cd", "SalesJob"));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        
            bll.vBindingComboToSp(ref cbsalesman, "sp_tmst_employee_getbyqry", "emp_cd", "emp_desc",arr);
            vBindingGrid();

        }
    }
    protected void cbsalesman_SelectedIndexChanged(object sender, EventArgs e)
    {
        vBindingGrid();
    }

    protected void vBindingGrid()
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salesman_cd", cbsalesman.SelectedValue.ToString()));
        arr.Add(new cArrayList("@day_cd", cbday.SelectedValue.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_trps_dtl_get", arr);
    }
    protected void cbday_SelectedIndexChanged(object sender, EventArgs e)
    {
        vBindingGrid();
    }
    protected void grd_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grd.EditIndex = e.NewEditIndex;
        vBindingGrid();
    }
    protected void grd_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lbday = (Label)grd.Rows[e.RowIndex].FindControl("lbday");
        HiddenField hdcust = (HiddenField)grd.Rows[e.RowIndex].FindControl("hdcust");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@emp_cd", cbsalesman.SelectedValue.ToString()));
        arr.Add(new cArrayList("@day_cd", lbday.Text));
        arr.Add(new cArrayList("@cust_cd", hdcust.Value.ToString()));
        bll.vDelRpsDtl(arr);
        grd.EditIndex = -1;
        vBindingGrid();
    }
}