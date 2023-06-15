using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class lookupinvreturnApproval : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                bll.vBindingFieldValueToCombo(ref cbInvoiceType, "can_sta_id");
                BindGrid();
            }
            catch (Exception ex)
            {
                Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
                bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : lookupinvreturnApproval");
                Response.Redirect("fm_ErrorPage.aspx");
            }
        }
    }
    protected void grd_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        //Label lbinvoiceno = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbinvoiceno");
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "clo", "window.opener.InvSelected('"+lbinvoiceno.Text+"');window.close();", true);
    }
    protected void grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd.PageIndex = e.NewPageIndex;
        BindGrid();
    }

    void BindGrid()
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@can_sta_id", Convert.ToString(cbInvoiceType.SelectedValue)));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grd, "sp_tdosales_invoice_getApproval", arr);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : lookupinvreturnApproval");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void cbInvoiceType_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid();
    }
}