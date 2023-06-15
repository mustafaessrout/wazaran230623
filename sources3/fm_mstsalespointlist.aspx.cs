using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_mstsalespointlist : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingGridToSp(ref grdsp, "sp_tmst_salespoint_get");
        }
    }
    protected void grdsp_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdsp.PageIndex = e.NewPageIndex;
        bll.vBindingGridToSp(ref grdsp, "sp_tmst_salespoint_get");
    }
    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_mstsalespointentry.aspx");
    }
    protected void grdsp_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        TextBox txsalespoint_nm = (TextBox)grdsp.Rows[e.RowIndex].FindControl("txsalespoint_nm");
        TextBox txsalespoint_sn = (TextBox)grdsp.Rows[e.RowIndex].FindControl("txsalespoint_sn");

        if (txsalespoint_nm.Text.Trim() != "" && txsalespoint_sn.Text.Trim() != "")
        {
            List<cArrayList> arr = new List<cArrayList>();
            HiddenField hdsalespointcd = (HiddenField)grdsp.Rows[e.RowIndex].FindControl("hdsalespointcd");
            arr.Add(new cArrayList("@salespointcd", hdsalespointcd.Value.ToString()));
            arr.Add(new cArrayList("@salespoint_nm", txsalespoint_nm.Text));
            arr.Add(new cArrayList("@salespoint_sn", txsalespoint_sn.Text));
            bll.vUpdatsalespointgrd(arr);
            grdsp.EditIndex = -1;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('The record has been updated','Sucessfully...','success');", true);//by Othman 30-8-15        
            bll.vBindingGridToSp(ref grdsp, "sp_tmst_salespoint_get");

        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Please insert ','Sales point name and Short name...','error');", true);//by Othman 30-8-15
        }
    }
    protected void grdsp_RowCancelingEdit1(object sender, GridViewCancelEditEventArgs e)
    {
        grdsp.EditIndex = -1;
        bll.vBindingGridToSp(ref grdsp, "sp_tmst_salespoint_get");
    }
    protected void grdsp_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grdsp.EditIndex = e.NewEditIndex;
        bll.vBindingGridToSp(ref grdsp, "sp_tmst_salespoint_get");
    }
}