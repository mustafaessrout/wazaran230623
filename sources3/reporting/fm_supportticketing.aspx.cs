using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class reporting_fm_supportticketing : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            bll.vBindingComboToSp(ref cbsalespoint, "sp_tmst_salespoint_get", "salespointcd", "salespoint_nm");
            vBindingGrid();
        }
    }

    void vBindingGrid()
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@status", (cbstatus.SelectedValue.ToString())));
        arr.Add(new cArrayList("@salespointcd", (cbsalespoint.SelectedValue.ToString())));
        bll.vBindingGridToSp(ref grdticket, "sp_supporting_get", arr);
    }

    protected void cbstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        vBindingGrid();
    }

    protected void cbsalespoint_SelectedIndexChanged(object sender, EventArgs e)
    {
        vBindingGrid();
    }

    protected void grdticket_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        if (e.CommandName == "view")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grdticket.Rows[index];
            Label lbno = (Label)grdticket.Rows[index].FindControl("lbno");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openwindow('fm_supporting_detail.aspx?no=" + lbno.Text + "');", true);
        }
    }

    protected void grdticket_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdticket.PageIndex = e.NewPageIndex;
        vBindingGrid();
    }
}