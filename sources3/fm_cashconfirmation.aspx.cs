using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_cashconfirmation : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                List<cArrayList> arr = new List<cArrayList>();
                bll.vBindingGridToSp(ref grdcash, "sp_tmst_payment_getbyadmin");
            }
            catch (Exception ex)
            {
                Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
                bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_cashconfirmation");
                Response.Redirect("fm_ErrorPage.aspx");
            }        
        }
    }

    protected void grdcash_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdcash.PageIndex = e.NewPageIndex;
            bll.vBindingGridToSp(ref grdcash, "sp_tmst_payment_getbyadmin");

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_cashconfirmation");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    protected void grdcash_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType != DataControlRowType.DataRow) return;
            DropDownList cbbank = (DropDownList)e.Row.FindControl("cbbank");
            bll.vBindingComboToSp(ref cbbank, "sp_tmst_bankaccount_get", "acc_no", "bank_desc");

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_cashconfirmation");
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
            Label lbpaymentno = (Label)grdcash.Rows[index].FindControl("lbpaymentno");
            Label lbsalespoint = (Label)grdcash.Rows[index].FindControl("lbsalespoint");
            Label lbpayment_dt = (Label)grdcash.Rows[index].FindControl("lbpayment_dt");
            Label lbtotamt = (Label)grdcash.Rows[index].FindControl("lbtotamt");
            DropDownList cbbank = (DropDownList)grdcash.Rows[index].FindControl("cbbank");

            if (e.CommandName == "approve")
            {
                if (bll.nCheckAccess("cashierho", Request.Cookies["usr_id"].Value.ToString()) == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','Approve / Reject Cashier Deposit to HO','warning');", true);
                    return;
                }

                string sClosingCashier = "", typeCashout = "";
                double dCashierAmt = 0, dAmt = 0;

                sClosingCashier = bll.vLookUp("select 1 from tcashregister_closing where acknowledge in ('N','Y') and salespointcd='" + lbsalespoint.Text + "' and chclosing_dt='" + lbpayment_dt.Text + "'");

                if (sClosingCashier.ToString() == "1")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Cashier Already closing or pending in Approval!','warning');", true);
                    return;
                }

                // Add Block If Cashier Balance less than Amount 

                dCashierAmt = double.Parse(bll.vLookUp("select dbo.fn_getcashierbalance('" + lbsalespoint.Text + "')"));
                dAmt = double.Parse(lbtotamt.Text);

                typeCashout = bll.vLookUp("select top 1 b.inout from tcashout_request a join tmst_itemcashout b on a.itemco_cd=b.itemco_cd where cashout_cd = '" + lbpaymentno.Text + "'");

                if (typeCashout == "O")
                {
                    if ((dCashierAmt - dAmt) < 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Balance Cashier : " + dCashierAmt.ToString() + " , Not Enough to approve this bank deposit!','warning');", true);
                        return;
                    }
                }

                    
                arr.Clear();
                arr.Add(new cArrayList("@payment_no", lbpaymentno.Text));
                arr.Add(new cArrayList("@salespointcd", lbsalespoint.Text));
                arr.Add(new cArrayList("@accnoto", cbbank.SelectedValue.ToString()));
                arr.Add(new cArrayList("@dep_sta_id", "H"));
                bll.vUpdateMstPaymentBank(arr);
            }
            else if (e.CommandName == "reject")
            {
                if (bll.nCheckAccess("cashierho", Request.Cookies["usr_id"].Value.ToString()) == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','Approve / Reject Cashier Deposit to HO','warning');", true);
                    return;
                }
                arr.Clear();
                arr.Add(new cArrayList("@payment_no", lbpaymentno.Text));
                arr.Add(new cArrayList("@salespointcd", lbsalespoint.Text));
                arr.Add(new cArrayList("@dep_sta_id", "R"));
                bll.vUpdateMstPaymentBank(arr);
            }
            arr.Clear();
            bll.vBindingGridToSp(ref grdcash, "sp_tmst_payment_getbyadmin", arr);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_cashconfirmation");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
}