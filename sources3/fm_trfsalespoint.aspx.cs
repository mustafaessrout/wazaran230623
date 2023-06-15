using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_trfsalespoint : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                List<cArrayList> arr = new List<cArrayList>();
                bll.vBindingSalespointToCombo(ref cbfrom);
                bll.vBindingSalespointToCombo(ref cbto);
                cbfrom.Enabled = false;
                cbfrom.SelectedValue = Request.Cookies["sp"].Value.ToString();
                bttransfer.Visible = false;
            }
            catch (Exception ex)
            {
                Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
                bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_trfsalespoint");
                Response.Redirect("fm_ErrorPage.aspx");
            }
        }
    }

    protected void bttransfer_Click(object sender, EventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_trfsalespoint");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    protected void btchecking_Click(object sender, EventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_trfsalespoint");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
}