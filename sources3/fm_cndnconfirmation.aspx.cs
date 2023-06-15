using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_cndnconfirmation : System.Web.UI.Page
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
                bll.vBindingGridToSp(ref grdcash, "sp_tacc_cndn_getbypending", arr);
            }
            catch (Exception ex)
            {
                Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
                bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_cndnconfirmation");
                Response.Redirect("fm_ErrorPage.aspx");
            }        
        }
    }

    protected void grdcash_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdcash.PageIndex = e.NewPageIndex;
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grdcash, "sp_tacc_cndn_getbypending", arr);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_cndnconfirmation");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    protected void grdcash_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_cndnconfirmation");
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
            Label lbcndn_cd = (Label)grdcash.Rows[index].FindControl("lbcndn_cd");
            Label lbsalespoint = (Label)grdcash.Rows[index].FindControl("lbsalespoint");
            Label lbcndn_dt = (Label)grdcash.Rows[index].FindControl("lbcndn_dt");
            Label lbtotamt = (Label)grdcash.Rows[index].FindControl("lbtotamt");

            if (e.CommandName == "approve")
            {
                if (bll.nCheckAccess("cndn", Request.Cookies["usr_id"].Value.ToString()) == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(),Guid.NewGuid().ToString(), "sweetAlert('You dont have access','Approve / Reject CNDN Request','warning');", true);
                    return;
                }


                arr.Clear();
                arr.Add(new cArrayList("@cndn_cd", lbcndn_cd.Text));
                arr.Add(new cArrayList("@salespointcd", lbsalespoint.Text));
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@cndn_sta_id", "A"));
                bll.vUpdateCNDNRequestByStatus(arr);
            }
            else if (e.CommandName == "reject")
            {
                if (bll.nCheckAccess("cndn", Request.Cookies["usr_id"].Value.ToString()) == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','Approve / Reject CNDN Request','warning');", true);
                    return;
                }
                arr.Clear();
                arr.Add(new cArrayList("@cndn_cd", lbcndn_cd.Text));
                arr.Add(new cArrayList("@salespointcd", lbsalespoint.Text));
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@cndn_sta_id", "R"));
                bll.vUpdateCNDNRequestByStatus(arr);
            }
            arr.Clear();
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grdcash, "sp_tacc_cndn_getbypending", arr);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_cndnconfirmation");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
}