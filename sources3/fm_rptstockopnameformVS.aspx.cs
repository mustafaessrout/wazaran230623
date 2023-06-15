using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class fm_rptstockopnameformVS : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                lbstockno.Text = Request.QueryString["qsstockno"];
                lbdate.Text = Request.QueryString["qsdate"];
                lbwhs_cd.Text = Request.QueryString["qswhs_cd"];

                List<cArrayList> arr = new List<cArrayList>();
                arr.Add(new cArrayList("@dtdate", lbdate.Text));
                arr.Add(new cArrayList("@whs_cd", lbwhs_cd.Text));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vUpdatetblrptstockopnameformVS(arr);
                bll.vBindingGridToSp(ref grd, "sp_tblrptstockopnameformVS_get");

            }
            catch (Exception ex)
            {
                Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
                bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_rptstockopnameformVS");
                Response.Redirect("fm_ErrorPage.aspx");
            }
            
        }
    }

   
    
    protected void btclose_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "cl", "window.close();", true);
    }
    protected void bttoExcel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=tblrptstockopnameformVS.xls");
            Response.ContentType = "application/excel";
            System.IO.StringWriter sw = new System.IO.StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            grd.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_rptstockopnameformVS");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /*Tell the compiler that the control is rendered
         * explicitly by overriding the VerifyRenderingInServerForm event.*/
    }
}