using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_goodreceived_brn : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));  
            bll.vBindingComboToSpWithEmptyChoosen(ref cbdepo, "sp_tmst_warehouse_get", "whs_cd", "whs_nm", arr);
        }
    }

    protected void cbdepo_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@whs_cd_to", cbdepo.SelectedValue));
        arr.Add(new cArrayList("@sta_id", "A"));
        bll.vBindingGridToSp(ref grd, "sp_tinternal_transfer_getbyreceive", arr);
    }

    protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow) {
            Label lbtransferno = (Label)e.Row.FindControl("lbtransferno");
            GridView grddetail = (GridView)e.Row.FindControl("grddetail");
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@trf_no", lbtransferno.Text));
            bll.vBindingGridToSp(ref grddetail, "sp_tinternal_transfer_dtl_getbynumber", arr);
        }
    }

    protected void btpost_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        GridViewRow _row = (GridViewRow)btn.NamingContainer;
        Label lbtransferno = (Label)_row.FindControl("lbtransferno");
        DropDownList cbstatus = (DropDownList)_row.FindControl("cbaction");

        if (cbstatus.SelectedValue == string.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                "sweetAlert('Please select status !','Status selection','warning');", true);
            return;
        }
        if (cbstatus.SelectedValue == "R")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), 
                "sweetAlert('You can only received !','For this time can not reject','warning');", true);
            return;
        }

        string _sql = "update tinternal_transfer set sta_id='"+cbstatus.SelectedValue+"' where trf_no='"+lbtransferno.Text+"'";
        bll.vExecuteSQL(_sql);
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@whs_cd_to", cbdepo.SelectedValue));
        arr.Add(new cArrayList("@sta_id", "A"));
        bll.vBindingGridToSp(ref grd, "sp_tinternal_transfer_getbyreceive", arr);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
              "sweetAlert('Received warehouse has succeeded !','"+lbtransferno.Text+"','success');", true);
    }
}