using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_stockout_app : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@sample_sta_id", "N"));
            bll.vBindingGridToSp(ref grd, "sp_tsample_stock_getbystatus", arr);
        }
    }

    protected void grd_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grd.EditIndex = e.NewEditIndex;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@sample_sta_id", "N"));
        bll.vBindingGridToSp(ref grd, "sp_tsample_stock_getbystatus", arr);
        DropDownList cbstatus = (DropDownList)grd.Rows[e.NewEditIndex].FindControl("cbstatus");
        bll.vBindingFieldValueToComboByQryWithEmptyChoosen(ref cbstatus, "sample_sta_id","sample_sta_id_app");
    }

    protected void grd_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        Label lbsamplecode = (Label)grd.Rows[e.RowIndex].FindControl("lbsamplecode");
        DropDownList cbstatus = (DropDownList)grd.Rows[e.RowIndex].FindControl("cbstatus");

        string _sql = "update tstock_sample set sample_sta_id='"+cbstatus.SelectedValue+"' where sample_cd='"+lbsamplecode.Text+"'";
        bll.vExecuteSQL(_sql);

        grd.EditIndex = -1;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@sample_sta_id", "N"));
        bll.vBindingGridToSp(ref grd, "sp_tsample_stock_getbystatus", arr);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), 
            "sweetAlert('Sample code has been proceeded !','"+lbsamplecode.Text+"','success');", true);

    }

    protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbsamplecode = (Label)e.Row.FindControl("lbsamplecode");
            GridView grddetail = (GridView)e.Row.FindControl("grddetail");
            arr.Clear();
            arr.Add(new cArrayList("@sample_cd", lbsamplecode.Text));
            bll.vBindingGridToSp(ref grddetail, "sp_tsamplestock_dtl_get", arr);
            foreach (LinkButton button in e.Row.Cells[5].Controls.OfType<LinkButton>())
            {
                if ((button.CommandName == "Edit") || (button.CommandName == "Update"))
                {
                    button.Attributes.Add("OnClick", "ShowProgress();");
                }
            }
        }
    }
}