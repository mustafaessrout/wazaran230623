using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_cashoutconfirmation : System.Web.UI.Page
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
                bll.vBindingGridToSp(ref grdcash, "sp_tcashout_request_getbypending", arr);
            }
            catch (Exception ex)
            {
                Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
                bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_cashoutconfirmation");
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
            bll.vBindingGridToSp(ref grdcash, "sp_tcashout_request_getbypending", arr);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_cashoutconfirmation");
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
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_cashoutconfirmation");
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
            Label lbcashout_cd = (Label)grdcash.Rows[index].FindControl("lbcashout_cd");
            Label lbsalespoint = (Label)grdcash.Rows[index].FindControl("lbsalespoint");
            Label lbcashout_dt = (Label)grdcash.Rows[index].FindControl("lbcashout_dt");
            Label lbtotamt = (Label)grdcash.Rows[index].FindControl("lbtotamt");

            if (e.CommandName == "approve")
            {
                if (bll.nCheckAccess("cashout", Request.Cookies["usr_id"].Value.ToString()) == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','Approve / Reject Cashout Request','warning');", true);
                    return;
                }

                string sClosingCashier = "", typeCashout = "";
                double dCashierAmt = 0, dAmt = 0;

                sClosingCashier = bll.vLookUp("select 1 from tcashregister_closing where acknowledge in ('N','Y') and salespointcd='" + lbsalespoint.Text + "' and chclosing_dt='" + lbcashout_dt.Text + "'");

                if (sClosingCashier.ToString() == "1")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Cashier Already closing or pending in Approval!','warning');", true);
                    return;
                }

                // Add Block If Cashier Balance less than Amount 

                dCashierAmt = double.Parse(bll.vLookUp("select dbo.fn_getcashierbalance('" + lbsalespoint.Text + "')"));
                dAmt = double.Parse(lbtotamt.Text);

                typeCashout = bll.vLookUp("select top 1 b.inout from tcashout_request a join tmst_itemcashout b on a.itemco_cd=b.itemco_cd where cashout_cd = '"+lbcashout_cd.Text+"'");

                if (typeCashout == "O")
                {
                    if ((dCashierAmt - dAmt) < 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Balance Cashier  : " + dCashierAmt.ToString() + " , Not Enough to approve this cashout / expense!','warning');", true);
                        return;
                    }
                }

                //// 
                

                arr.Clear();
                arr.Add(new cArrayList("@cashout_cd", lbcashout_cd.Text));
                arr.Add(new cArrayList("@salespointcd", lbsalespoint.Text));
                arr.Add(new cArrayList("@cashout_sta_id", "A"));
                bll.vUpdateCashoutRequestByStatus(arr);
            }
            else if (e.CommandName == "reject")
            {
                if (bll.nCheckAccess("cashierho", Request.Cookies["usr_id"].Value.ToString()) == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','Approve / Reject Cashout Request','warning');", true);
                    return;
                }
                arr.Clear();
                arr.Add(new cArrayList("@cashout_cd", lbcashout_cd.Text));
                arr.Add(new cArrayList("@salespointcd", lbsalespoint.Text));
                arr.Add(new cArrayList("@cashout_sta_id", "R"));
                bll.vUpdateCashoutRequestByStatus(arr);
            }
            arr.Clear();
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grdcash, "sp_tcashout_request_getbypending", arr);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_cashoutconfirmation");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
}