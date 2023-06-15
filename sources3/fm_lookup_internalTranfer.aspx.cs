using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_lookup_internalTranfer : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                bll.vBindingFieldValueToComboByQryWithEmptyChoosen(ref cbstatus, "sta_id" , "sta_id");
                //cbstatus.SelectedValue = "N";
                //cbstatus_SelectedIndexChanged(sender, e);

            }
            catch (Exception ex)
            {
                Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
                bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_lookup_internalTransfer");
                Response.Redirect("fm_ErrorPage.aspx");
            }
        }
    }
    protected void btsearch_Click(object sender, EventArgs e)
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@trf_no", txsearch.Text));
            arr.Add(new cArrayList("@sta_id", cbstatus.SelectedValue.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grd, "sp_tinternal_transfer_get", arr);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_lookup_internalTransfer");
            Response.Redirect("fm_ErrorPage.aspx");
        }
        finally
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
        }
    }

    protected void grd_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            Label lbtrf_no = (Label)grd.SelectedRow.FindControl("lbtrf_no");

            Session["lootrf_no"] = lbtrf_no.Text;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "updpnl", "closewin()", true);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_lookup_internalTransfer");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {

            grd.PageIndex = e.NewPageIndex;
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@trf_no", txsearch.Text));
            arr.Add(new cArrayList("@sta_id", cbstatus.SelectedValue.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grd, "sp_tinternal_transfer_get", arr);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_lookup_internalTransfer");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    protected void cbstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        btsearch_Click(sender, e);
    }
}