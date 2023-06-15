using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_paymentreceipt_ho_confirm : System.Web.UI.Page
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
                bll.vBindingGridToSp(ref grd, "sp_tmst_payment_ho_confirm_get", arr);
            }
            catch (Exception ex)
            {
                Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
                bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_paymentreceipt_ho_confirm");
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
            bll.vBindingGridToSp(ref grd, "sp_tmst_payment_ho_confirm_get",arr);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_paymentreceipt_ho_confirm");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    protected void grd_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {

            List<cArrayList> arr = new List<cArrayList>();
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grd.Rows[index];
            Label lbpaymentno = (Label)grd.Rows[index].FindControl("lbpaymentno");
            Label lbinv_no = (Label)grd.Rows[index].FindControl("lbinv_no");
            Label lbsalespoint = (Label)grd.Rows[index].FindControl("lbsalespoint");
            Label lbpayment_dt = (Label)grd.Rows[index].FindControl("lbpayment_dt");
            Label lbtotamt = (Label)grd.Rows[index].FindControl("lbtotamt");

            if (e.CommandName == "received")
            {
                if (bll.nCheckAccess("paymentho", Request.Cookies["usr_id"].Value.ToString()) == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','Receipt Payment from HO','warning');", true);
                    return;
                }

                double dBalance = 0, dPayment = 0;
                dPayment = double.Parse(lbtotamt.Text.ToString());
                dBalance = double.Parse(bll.vLookUp("select isnull(balance,0) from tdosales_invoice where inv_no='" + lbinv_no.Text + "'"));

                if (dBalance < dPayment || dBalance == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Balance Invoice cant applied !!!','Receipt Payment from HO','warning');", true);
                    return;
                }

                // Insert Payment to Branch
                arr.Clear();
                arr.Add(new cArrayList("@payment_no", lbpaymentno.Text));
                arr.Add(new cArrayList("@inv_no", lbinv_no.Text));
                arr.Add(new cArrayList("@salespointcd", lbsalespoint.Text));
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@sta_id", "C"));
                bll.vUpdateMstPaymentHOConfirm(arr);
            }
            else if (e.CommandName == "reject")
            {
                if (bll.nCheckAccess("paymentho", Request.Cookies["usr_id"].Value.ToString()) == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','Receipt Payment from HO','warning');", true);
                    return;
                }

                arr.Clear();
                arr.Add(new cArrayList("@payment_no", lbpaymentno.Text));
                arr.Add(new cArrayList("@inv_no", lbinv_no.Text));
                arr.Add(new cArrayList("@salespointcd", lbsalespoint.Text));
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@sta_id", "L"));
                bll.vUpdateMstPaymentHOConfirm(arr);
            }

            arr.Clear();
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grd, "sp_tmst_payment_ho_confirm_get", arr);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_paymentreceipt_ho_confirm");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
}