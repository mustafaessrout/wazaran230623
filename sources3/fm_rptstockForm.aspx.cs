using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class fm_rptstockForm : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lbwhs_nm.Text = Request.QueryString["qswhs_nm"];
            lbbin_nm.Text = Request.QueryString["qsbin_nm"];
            lbdatefr.Text = Request.QueryString["qsdatefr"];
            lbdateto.Text = Request.QueryString["qsdateto"];

            //List<cArrayList> arr = new List<cArrayList>();
            //arr.Add(new cArrayList("@dtdate", lbdate.Text));
            //arr.Add(new cArrayList("@whs_cd", lbwhs_cd.Text));
            //bll.vUpdatetblrptstockopnameformVS(arr);
            List<cArrayList> lParamStockForm = (List<cArrayList>)Session["lParamStockForm"];
            bll.vBindingGridToSp(ref grd, "sp_rptStock_form", lParamStockForm);
            
        }
    }

   
    
    protected void btclose_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "cl", "window.close();", true);
    }
    protected void bttoExcel_Click(object sender, EventArgs e)
    {
        Response.ClearContent();
        Response.AddHeader("content-disposition", "attachment; filename=rptStock_form.xls");
        Response.ContentType = "application/excel";
        System.IO.StringWriter sw = new System.IO.StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        grd.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /*Tell the compiler that the control is rendered
         * explicitly by overriding the VerifyRenderingInServerForm event.*/
    }
}