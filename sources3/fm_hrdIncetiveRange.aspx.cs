using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_hrdIncetiveRange : System.Web.UI.Page
{
    cbll bll = new cbll();
    cdal cdl = new cdal();
    Utitlity ut = new Utitlity();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGroup();
        }
    }

    void BindGroup() {
        List<cArrayList> arr = new List<cArrayList>();
         DataTable dt = new DataTable();
         dt = cdl.GetValueFromSP("sp_tmst_IncetiveRange_get", arr);
        if (dt.Rows.Count > 0)
        {
            grd.DataSource = dt;
            grd.DataBind();
        }
        else
        {
            grd.DataSource = null;
            grd.DataBind();
        }
    }
    protected void grd_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grd.EditIndex = -1;
        BindGroup();
    }
    protected void grd_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grd.EditIndex = e.NewEditIndex;
        BindGroup();
    }
    protected void grd_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        HiddenField hdfincetiveRangeID = (HiddenField)grd.Rows[e.RowIndex].FindControl("hdfincetiveRangeID");
        TextBox txminRange = (TextBox)grd.Rows[e.RowIndex].FindControl("txminRange");
        TextBox txmaxRange = (TextBox)grd.Rows[e.RowIndex].FindControl("txmaxRange");
        TextBox txincetiveAmount = (TextBox)grd.Rows[e.RowIndex].FindControl("txincetiveAmount");

        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@incetiveRangeID", hdfincetiveRangeID.Value));
        arr.Add(new cArrayList("@minRange", Convert.ToDecimal(txminRange.Text)));
        arr.Add(new cArrayList("@maxRange", Convert.ToDecimal(txmaxRange.Text)));
        arr.Add(new cArrayList("@incetiveAmount", Convert.ToDecimal(txincetiveAmount.Text)));
        bll.vUpdateIncetiveRange(arr);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Data has been saved successfully','Successfully','success');", true);
        grd.EditIndex = -1;
        BindGroup();
    }
}