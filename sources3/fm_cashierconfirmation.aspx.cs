using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_cashierconfirmation : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                List<cArrayList> arr = new List<cArrayList>();
                bll.vBindingGridToSp(ref grdcash, "sp_tcashregister_closing_getbystatus");
            }
            catch (Exception ex)
            {
                Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
                bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_cashierconfirmation");
                Response.Redirect("fm_ErrorPage.aspx");
            }        
        }
    }

    protected void grdcash_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdcash.PageIndex = e.NewPageIndex;
            bll.vBindingGridToSp(ref grdcash, "sp_tcashregister_closing_getbystatus");

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_cashierconfirmation");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    protected void grdcash_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grdcash.Rows[index];
            Label lbchclosingno = (Label)grdcash.Rows[index].FindControl("lbchclosingno");
            Label lbsalespoint = (Label)grdcash.Rows[index].FindControl("lbsalespoint");

            if (e.CommandName == "approve")
            {
                if (bll.nCheckAccess("cashierho", Request.Cookies["usr_id"].Value.ToString()) == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','Confirm this Closing Cashier','warning');", true);
                    return;
                }
                arr.Clear();
                arr.Add(new cArrayList("@chclosingno", lbchclosingno.Text));
                arr.Add(new cArrayList("@salespointcd", lbsalespoint.Text));
                arr.Add(new cArrayList("@status", "A"));
                bll.vUpdateCashregisterClosingAcknowledge(arr);
            }
            if (e.CommandName == "reject")
            {
                if (bll.nCheckAccess("cashierho", Request.Cookies["usr_id"].Value.ToString()) == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','Reject this Closing Cashier','warning');", true);
                    return;
                }
                arr.Clear();
                arr.Add(new cArrayList("@chclosingno", lbchclosingno.Text));
                arr.Add(new cArrayList("@salespointcd", lbsalespoint.Text));
                arr.Add(new cArrayList("@status", "R"));
                bll.vUpdateCashregisterClosingAcknowledge(arr);
            }
            arr.Clear();
            bll.vBindingGridToSp(ref grdcash, "sp_tcashregister_closing_getbystatus", arr);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_cashierconfirmation");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
}