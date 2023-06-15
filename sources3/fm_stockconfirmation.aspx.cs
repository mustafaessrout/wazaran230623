using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_stockconfirmation : System.Web.UI.Page
{
    cbll bll = new cbll();
    decimal totalQty = 0;
    decimal totalQtyCtn = 0, totalQtyPcs = 0;

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
        arr.Add(new cArrayList("@sta_id", "W"));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_app_stockconfirm_get", arr);
    }

    protected void btapprove_Click(object sender, EventArgs e)
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            foreach (GridViewRow row in grd.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chk = (CheckBox)row.FindControl("chk");

                    if (chk.Checked)
                    {
                        HiddenField hdsalespoint = (HiddenField)row.FindControl("hdsalespoint");
                        Label lbstock_dt = (Label)row.FindControl("lbstock_dt");

                        arr.Clear();
                        arr.Add(new cArrayList("@salespointcd", hdsalespoint.Value.ToString()));
                        arr.Add(new cArrayList("@stock_dt", lbstock_dt.Text));
                        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                        arr.Add(new cArrayList("@sta_id", "C"));
                        bll.vUpdateAppStockConfirmation(arr);
                    }
                }
            }
            bindinggrd();
            grddtl.DataSource = null;
            grddtl.DataBind();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alr", "sweetAlert('Stock Confirm has been approved','Stock Confirmation','success');", true);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : app_stockconfirmation");
            Response.Redirect("fm_ErrorPage.aspx");
        }
        
    }

    protected void btcancel_Click(object sender, EventArgs e)
    {
        try
        {

            List<cArrayList> arr = new List<cArrayList>();
            foreach (GridViewRow row in grd.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chk = (CheckBox)row.FindControl("chk");
                    if (chk.Checked)
                    {
                        HiddenField hdsalespoint = (HiddenField)row.FindControl("hdsalespoint");
                        Label lbstock_dt = (Label)row.FindControl("lbstock_dt");

                        arr.Clear();
                        arr.Add(new cArrayList("@salespointcd", hdsalespoint.Value.ToString()));
                        arr.Add(new cArrayList("@stock_dt", lbstock_dt.Text));
                        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                        arr.Add(new cArrayList("@sta_id", "N"));
                        bll.vUpdateAppStockConfirmation(arr);
                    }
                }
            }
            bindinggrd();
            grddtl.DataSource = null;
            grddtl.DataBind();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alr", "sweetAlert('Stock Confirm has been rejected','Stock Confirmation','success');", true);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : app_stockconfirmation");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    
    protected void grddtl_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {   
            HiddenField hdqty_ctn = (HiddenField)e.Row.FindControl("hdqty_ctn");
            HiddenField hdqty_pcs = (HiddenField)e.Row.FindControl("hdqty_pcs");
            Label lbitemcode = (Label)e.Row.FindControl("lbitemcode");

            totalQtyCtn = totalQtyCtn + decimal.Parse(hdqty_ctn.Value);
            totalQtyPcs = totalQtyPcs + decimal.Parse(hdqty_pcs.Value);
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblTotalqty = (Label)e.Row.FindControl("lblTotalqty");
            lblTotalqty.Text = totalQtyCtn.ToString() + " CTN, " + totalQtyPcs.ToString() + " PCS";
        }

    }

    protected void grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd.PageIndex = e.NewPageIndex;
        bindinggrd();
    }

    protected void grd_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        HiddenField hdsalespoint = (HiddenField)grd.Rows[e.NewSelectedIndex].FindControl("hdsalespoint");
        Label lbstock_dt = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbstock_dt");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@stock_dt", lbstock_dt.Text));
        arr.Add(new cArrayList("@salespointcd", hdsalespoint.Value.ToString()));
        bll.vBindingGridToSp(ref grddtl, "sp_app_stockconfirm_dtl_get", arr);
    }
}