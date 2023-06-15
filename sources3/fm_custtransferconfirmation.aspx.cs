using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_custtransferconfirmation : System.Web.UI.Page
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
                bll.vBindingGridToSp(ref grd, "sp_tcustomer_transfer_getbystatus", arr);
            }
            catch (Exception ex)
            {
                Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
                bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_custtransferconfirmation");
                Response.Redirect("fm_ErrorPage.aspx");
            }
        }
    }

    protected void grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grd.PageIndex = e.NewPageIndex;
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grd, "sp_tcustomer_transfer_getbystatus", arr);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_custtransferconfirmation");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void grd_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            List <cArrayList> arr = new List<cArrayList>();
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grd.Rows[index];
            Label lbtrfno = (Label)grd.Rows[index].FindControl("lbtrfno");
            Label lbsalespoint = (Label)grd.Rows[index].FindControl("lbsalespoint");
            if (e.CommandName == "approve")
            {
                if (bll.nCheckAccess("trfcustomer", Request.Cookies["usr_id"].Value.ToString()) == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','Approve / Reject Customer Transfer...','warning');", true);
                    return;
                }
                arr.Clear();
                arr.Add(new cArrayList("@trf_no", lbtrfno.Text));
                arr.Add(new cArrayList("@custrf_sta_id", "A"));
                arr.Add(new cArrayList("@updatedBy", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@UpdatMethod", "App"));
                arr.Add(new cArrayList("@salespointcd", lbsalespoint.Text));
                bll.vUpdateCustomerTransferByStatus(arr);
            }
            else if (e.CommandName == "reject")
            {
                if (bll.nCheckAccess("trfcustomer", Request.Cookies["usr_id"].Value.ToString()) == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','Approve / Reject Customer Transfer...','warning');", true);
                    return;
                }
                arr.Clear();
                arr.Add(new cArrayList("@trf_no", lbtrfno.Text));
                arr.Add(new cArrayList("@custrf_sta_id", "E"));
                arr.Add(new cArrayList("@updatedBy", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@UpdatMethod", "App"));
                arr.Add(new cArrayList("@salespointcd", lbsalespoint.Text));
                bll.vUpdateCustomerTransferByStatus(arr);
            }
            arr.Clear();
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grd, "sp_tcustomer_transfer_getbystatus", arr);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_custtransferconfirmation");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

}