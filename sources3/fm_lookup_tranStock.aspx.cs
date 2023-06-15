using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_lookup_tranStock : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btsearch_Click(object sender, EventArgs e)
    {
        try
        {

            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@trnstkNo", txsearch.Text));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grd, "sp_tbltrnstock_get", arr);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_mstrps");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    protected void grd_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Label lbtrnstkNo = (Label)grd.SelectedRow.FindControl("lbtrnstkNo");
            //Label lbinvtype = (Label)grd.SelectedRow.FindControl("lbinvtype");

            Session["lootrnstkNo"] = lbtrnstkNo.Text;
            //Session["lootrnstkinvtype"] = lbinvtype.Text;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "updpnl", "closewin()", true);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_mstrps");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
}