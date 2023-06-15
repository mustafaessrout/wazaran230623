using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_app_stockaddloss : System.Web.UI.Page
{
    cbll bll = new cbll();
    decimal totalQty = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindinggrd();
        }
    }

    private void bindinggrd()
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        bll.vBindingGridToSp(ref grdtrf, "sp_tbltrnstock_getaddloss", arr);
    }

    protected void grdtrfdtl_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbqty = (Label)e.Row.FindControl("lbqty");
            decimal qty;
            if (lbqty != null)
                qty = decimal.Parse(lbqty.Text);
            else
                qty = 0;
            totalQty = totalQty + qty;
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblTotalqty = (Label)e.Row.FindControl("lblTotalqty");
            lblTotalqty.Text = totalQty.ToString();
        }
    }

    protected void grdtrf_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdtrf.PageIndex = e.NewPageIndex;
        bindinggrd();
    }

    protected void grdtrf_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Label lbtrnstkno = (Label)grdtrf.Rows[e.NewSelectedIndex].FindControl("lbtrnstkno");
        Label lbsalespoint = (Label)grdtrf.Rows[e.NewSelectedIndex].FindControl("lbsalespoint");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@trnstkno", lbtrnstkno.Text));
        arr.Add(new cArrayList("@salespointcd", lbsalespoint.Text));
        bll.vBindingGridToSp(ref grdtrfdtl, "sp_tbltrnstock_getaddloss_dtl", arr);
    }

    protected void grdtrf_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grdtrf.Rows[index];
            Label lbtrnstkno = (Label)grdtrf.Rows[index].FindControl("lbtrnstkno");
            Label lbsalespoint = (Label)grdtrf.Rows[index].FindControl("lbsalespoint");

            if (e.CommandName == "approve")
            {
                if (bll.nCheckAccess("stockaddloss", Request.Cookies["usr_id"].Value.ToString()) == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','Approve / Reject Stok Add | Loss | Destroy','warning');", true);
                    return;
                }
                arr.Clear();
                arr.Add(new cArrayList("@trnstkno", lbtrnstkno.Text));
                arr.Add(new cArrayList("@salespointcd", lbsalespoint.Text));
                arr.Add(new cArrayList("@sta_id", "A"));
                bll.vUpdateTblTrnStockApprove(arr);
            }
            else if (e.CommandName == "reject")
            {
                if (bll.nCheckAccess("stockaddloss", Request.Cookies["usr_id"].Value.ToString()) == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','Approve / Reject Stok Add | Loss | Destroy','warning');", true);
                    return;
                }
                arr.Clear();
                arr.Add(new cArrayList("@trnstkno", lbtrnstkno.Text));
                arr.Add(new cArrayList("@salespointcd", lbsalespoint.Text));
                arr.Add(new cArrayList("@sta_id", "L"));
                bll.vUpdateTblTrnStockApprove(arr);
            }
            bindinggrd();
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_app_stockaddloss");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
}