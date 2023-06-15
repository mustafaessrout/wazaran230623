using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using AjaxControlToolkit;
using System.IO;
using System.Data;
using System.Drawing;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
public partial class fm_ErrorLog : System.Web.UI.Page
{
    cbll bll = new cbll();
    cdal cd = new cdal();
    Utitlity ut = new Utitlity();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //ReloadControl();
        }
    }



    //private void ReloadControl()
    //{
    //    try
    //    {
    //        bll.vBindingComboToSp(ref ddlSalesPoint, "sp_tmst_salespoint_get", "salespointcd", "salespoint_nm");
    //        ddlSalesPoint.SelectedValue = Convert.ToString(Request.Cookies["sp"].Value.ToString());
    //        txstart_dt.Text = DateTime.Now.ToString("dd-MMM-yyyy");
    //        BindGrid();
    //    }
    //    catch (Exception ex)
    //    {
    //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Error occured during Reload control','error');", true);
    //        ut.Logs("", "Sanad Booking", "Booking Sanad Branch", "Booking Sanad Branch", "ReloadControl", "Exception", ex.Message + ex.InnerException);
    //    }
    //}

    protected void ddlSalesPoint_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
          //  BindGrid();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Error occured during Reload control','error');", true);
            ut.Logs("", "Sanad Booking", "Booking Sanad Branch", "Booking Sanad Branch", "ReloadControl", "Exception", ex.Message + ex.InnerException);
        }
    }

    //private void BindGrid()
    //{
    //    try
    //    {
    //        string fileName = Convert.ToString(txstart_dt.Text) + "_" + Convert.ToString(ddlSalesPoint.SelectedValue) + ".txt";
    //        string fileDetails = ut.ReadFiles(Convert.ToDateTime(txstart_dt.Text), fileName);

    //        string result = fileDetails.Replace(@"}]", "},");
    //        result = result.Replace(@"[{", "{");
    //        //result = result.Replace(@"},", "");

    //        result = result.Remove(result.Length - 1);
    //        result = result.Remove(result.Length - 1);
    //        result = result.Remove(result.Length - 1);
    //        result = result.Remove(result.Length - 1);

    //        result = "[" + result + "}]";

    //        var objResponse1 = JsonConvert.DeserializeObject<List<Utitlity.Data>>(result);


    //        grdErrorLog.DataSource = objResponse1;
    //        grdErrorLog.DataBind();
    //    }
    //    catch (Exception ex)
    //    {
    //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Error occured during Binding Data','error');", true);
    //        ut.Logs("", "Sanad Booking", "Booking Sanad Branch", "Booking Sanad Branch", "BindGrid", "Exception", ex.Message + ex.InnerException);
    //    }
    //}

    protected void OnRowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(grdErrorLog, "Select$" + e.Row.RowIndex);
            e.Row.ToolTip = "Click to select this row.";
        }
    }


    protected void grdErrorLog_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdErrorLog.PageIndex = e.NewPageIndex;
        //BindGrid();
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
    //    try
    //    { //BindGrid(); }
    //    catch (Exception ex)
    //    {
    //        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Error occured during saving','error');", true);
    //    }
    }

    protected void OnSelectedIndexChanged(object sender, EventArgs e)
    {
        string dataKeys = grdErrorLog.SelectedRow.Cells[0].Text;
        //Accessing TemplateField Column controls
        //BindControl(dataKeys);

        foreach (GridViewRow row in grdErrorLog.Rows)
        {
            if (row.RowIndex == grdErrorLog.SelectedIndex)
            {
                row.BackColor = ColorTranslator.FromHtml("#A1DCF2");
                row.ToolTip = string.Empty;
            }
            else
            {
                row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                row.ToolTip = "Click to select this row.";
            }
        }
    }

    protected void grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdErrorLog.PageIndex = e.NewPageIndex;
       // BindGrid();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
          //  ReloadControl();
        }
        catch (Exception ex)
        {
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Error occured during saving','error');", true);
        }
    }
}