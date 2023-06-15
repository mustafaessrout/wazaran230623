using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_doconfirmation : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                List<cArrayList> arr = new List<cArrayList>();
                bll.vBindingGridToSp(ref grddo, "sp_tmst_do_confirmation");
            }
            catch (Exception ex)
            {
                Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
                bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_doconfirmation");
                Response.Redirect("fm_ErrorPage.aspx");
            }
        }
    }

    protected void grddo_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grddo.PageIndex = e.NewPageIndex;
            bll.vBindingGridToSp(ref grddo, "sp_tmst_do_confirmation");

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_doconfirmation");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    protected void grddo_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void grddo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            string status_type = "";
            List <cArrayList> arr = new List<cArrayList>();
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grddo.Rows[index];
            Label lbdono = (Label)grddo.Rows[index].FindControl("lbdono");
            Label lbsalespoint = (Label)grddo.Rows[index].FindControl("lbsalespoint");
            status_type = bll.vLookUp("select (case when b.whs_typ='P' then 'L' else 'D' end) as status from tmst_do a inner join tmst_warehouse b on a.whs_cd=b.whs_cd where a.do_no=@'" + lbdono.Text + "'");
            if (e.CommandName == "approve")
            {
                if (bll.nCheckAccess("doho", Request.Cookies["usr_id"].Value.ToString()) == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','Approve / Reject Cashier Deposit to HO','warning');", true);
                    return;
                }
                arr.Clear();
                arr.Add(new cArrayList("@do_no", lbdono.Text));
                arr.Add(new cArrayList("@salespointcd", lbsalespoint.Text));
                arr.Add(new cArrayList("@do_sta_id", status_type));
                bll.vUpdateMstDo2(arr);
            }
            else if (e.CommandName == "reject")
            {
                if (bll.nCheckAccess("doho", Request.Cookies["usr_id"].Value.ToString()) == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','Approve / Reject Cashier Deposit to HO','warning');", true);
                    return;
                }
                arr.Clear();
                arr.Add(new cArrayList("@do_no", lbdono.Text));
                arr.Add(new cArrayList("@salespointcd", lbsalespoint.Text));
                arr.Add(new cArrayList("@do_sta_id", status_type));
                bll.vUpdateMstDo2(arr);
            }
            arr.Clear();
            bll.vBindingGridToSp(ref grddo, "sp_tmst_do_confirmation", arr);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_doconfirmation");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
}