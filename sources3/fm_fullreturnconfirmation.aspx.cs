using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_fullreturnconfirmation : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                List<cArrayList> arr = new List<cArrayList>();
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vBindingGridToSp(ref grdre, "sp_tInvoice_fullReturn_confirmation", arr);
            }
            catch (Exception ex)
            {
                Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
                bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_fullreturnconfirmation");
                Response.Redirect("fm_ErrorPage.aspx");
            }
        }
    }

    protected void grdre_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdre.PageIndex = e.NewPageIndex;
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grdre, "sp_tInvoice_fullReturn_confirmation", arr);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_fullreturnconfirmation");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    protected void grdre_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void grdre_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            List <cArrayList> arr = new List<cArrayList>();
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grdre.Rows[index];
            Label lbreturno = (Label)grdre.Rows[index].FindControl("lbreturno");
            Label lbsalespoint = (Label)grdre.Rows[index].FindControl("lbsalespoint");
            if (e.CommandName == "approve")
            {
                if (bll.nCheckAccess("fullreturbr", Request.Cookies["usr_id"].Value.ToString()) == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','Approve / Reject Sales Return from Customer','warning');", true);
                    return;
                }
                arr.Clear();
                arr.Add(new cArrayList("@invIFR_no", lbreturno.Text));
                arr.Add(new cArrayList("@can_sta_id", "A"));
                arr.Add(new cArrayList("@updatedBy", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@rejectInfo", null));
                arr.Add(new cArrayList("@UpdatMethod", "App"));
                arr.Add(new cArrayList("@salespointcd", lbsalespoint.Text));
                bll.vUpdateFullSalesreturnByStatus(arr);
            }
            else if (e.CommandName == "reject")
            {
                if (bll.nCheckAccess("fullreturbr", Request.Cookies["usr_id"].Value.ToString()) == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','Approve / Reject Sales Return from Customer','warning');", true);
                    return;
                }
                arr.Clear();
                arr.Add(new cArrayList("@invIFR_no", lbreturno.Text));
                arr.Add(new cArrayList("@can_sta_id", "R"));
                arr.Add(new cArrayList("@updatedBy", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@rejectInfo", null));
                arr.Add(new cArrayList("@UpdatMethod", "App"));
                arr.Add(new cArrayList("@salespointcd", lbsalespoint.Text));
                bll.vUpdateFullSalesreturnByStatus(arr);
            }
            arr.Clear();
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grdre, "sp_tInvoice_fullReturn_confirmation", arr);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_fullreturnconfirmation");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

}