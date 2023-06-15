using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using DocumentFormat.OpenXml.Spreadsheet;
//using DocumentFormat.OpenXml.Packaging;
//using DocumentFormat.OpenXml;
using System.Data;
//using System.IO.Packaging;
using System.IO;
public partial class fm_dsr : System.Web.UI.Page
{
    cbll bll = new cbll();
    //   protected override void Render(HtmlTextWriter writer) {
    //    // Ensure that the control is nested in a server form.
    //    if (Page != null) {
    //        Page.VerifyRenderingInServerForm();
    //    }
    //    base.Render(writer);
    //}
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabErr", "tabErr();", true);
    }
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        HttpCookie cok;
        cok = HttpContext.Current.Request.Cookies["sp"];
        List<string> lcust = new List<string>();
        List<cArrayList> arr = new List<cArrayList>();
        string scust = string.Empty;
        System.Data.SqlClient.SqlDataReader rs = null;
        //arr.Add(new cArrayList("@cust_cd", prefixText));
        arr.Add(new cArrayList("@salesman_cd", prefixText));
        arr.Add(new cArrayList("@salespointcd", cok.Value.ToString()));
        bll.vSearchdriver(arr, ref rs);
        //bll.vSearchMstCustomer(arr, ref rs);
        while (rs.Read())
        {
            scust = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["emp_cd"].ToString() + "-" + rs["emp_nm"].ToString(), rs["emp_cd"].ToString());
            lcust.Add(scust);
        }
        rs.Close();

        return (lcust.ToArray());
    }
    //  protected void Button1_Click(object sender, EventArgs e)
    // {
    //MemoryStream ms = new MemoryStream();
    //SpreadsheetDocument xl = SpreadsheetDocument.Create(ms, SpreadsheetDocumentType.Workbook);
    //WorkbookPart wbp = xl.AddWorkbookPart();
    //WorksheetPart wsp = wbp.AddNewPart<WorksheetPart>();
    //Workbook wb = new Workbook();
    //FileVersion fv = new FileVersion();
    //fv.ApplicationName = "Microsoft Office Excel";
    //Worksheet ws = new Worksheet();

    //First cell
    //SheetData sd = new SheetData();
    //Row r1 = new Row() { RowIndex = (UInt32Value)1u };
    //Cell c1 = new Cell();
    //c1.DataType = CellValues.String;
    //c1.CellValue = new CellValue("some value");
    //r1.Append(c1);

    // Second cell
    //Cell c2 = new Cell();
    //c2.CellReference = "C1";
    //c2.DataType = CellValues.String;
    //c2.CellValue = new CellValue("other value");
    //r1.Append(c2);
    //sd.Append(r1);

    //third cell
    //Row r2 = new Row() { RowIndex = (UInt32Value)2u };
    //Cell c3 = new Cell();
    //c3.DataType = CellValues.String;
    //c3.CellValue = new CellValue("some string");
    //r2.Append(c3);
    //sd.Append(r2);
    //Row r3 = new Row() { RowIndex = (UInt32Value)1u };
    //Cell c4 = new Cell();
    //c4.DataType = CellValues.String;
    //c4.CellValue = new CellValue("some string");
    //r3.Append(c4);
    //sd.Append(r3);


    //ws.Append(sd);
    //wsp.Worksheet = ws;
    //wsp.Worksheet.Save();
    //Sheets sheets = new Sheets();
    //Sheet sheet = new Sheet();
    //sheet.Name = "first sheet";
    //sheet.SheetId = 1;
    //sheet.Id = wbp.GetIdOfPart(wsp);
    //sheets.Append(sheet);
    //wb.Append(fv);
    //wb.Append(sheets);

    //xl.WorkbookPart.Workbook = wb;
    //xl.WorkbookPart.Workbook.Save();
    //xl.Close();
    //string fileName = "testOpenXml.xlsx";
    //Response.Clear();
    //byte[] dt = ms.ToArray();

    //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
    //Response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}", fileName));
    //Response.BinaryWrite(dt);
    //Response.End(); 
    // Response.ClearContent();
    //Response.AddHeader("content-disposition", "attachment; filename=rptStock_form.xls");
    //Response.ContentType = "application/excel";
    //System.IO.StringWriter sw = new System.IO.StringWriter();
    //HtmlTextWriter htw = new HtmlTextWriter(sw);
    //grd.AllowSorting = false;
    //grd.AllowPaging = false;
    //grd.RenderControl(htw);
    //Response.Write(sw.ToString());
    //Response.End();
    // }

    protected void btgenerate_Click(object sender, EventArgs e)
    {
        
        //DateTime dtpayp1 = DateTime.ParseExact(dtstart.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        //string dt1 = dtpayp1.Year.ToString() + "-" + dtpayp1.Month.ToString("00") + "-" + dtpayp1.Day.ToString("00");
        //List<cArrayList> arr = new List<cArrayList>();
        //arr.Add(new cArrayList("@startdate", dt1));
        //arr.Add(new cArrayList("@startdate1", dt1));
        //arr.Add(new cArrayList("@driver_cd", hdsalesman_cd.Value));
        //bll.vinsertinsintivedriver(arr);
        string drv_nm, sdatefr, sdateto,drv_cd;
        sdatefr = dtstart.Text;
        sdateto = dtstart.Text;
        drv_nm = txsalesman.Text;
        drv_cd = hdsalesman_cd.Value.ToString();
        //Session["lParamstockForm"] = arr;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "popupwindow('fm_driverinsentivep.aspx?driver_nm=" + drv_nm + "&driver_cd=" + drv_cd + "&qsdatefr=" + sdatefr + "&qsdateto=" + sdateto + "');", true);
    }
    protected void brnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_dsr.aspx");
    }
    protected void btreport_Click(object sender, EventArgs e)
    {
        if (txsalesman.Text=="")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You must insert driver name','Driver Name Error !!','warning');", true);
            return;
        }

        if (dtstart.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You must choose Date','Date Error !!','warning');", true);
            return;
        }
        txsalesman.Enabled = false;
        DateTime dtpayp1 = DateTime.ParseExact(dtstart.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        string dt1 = dtpayp1.Year.ToString() + "-" + dtpayp1.Month.ToString("00") + "-" + dtpayp1.Day.ToString("00");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@startdate", dt1));
        arr.Add(new cArrayList("@startdate1",dt1));
        arr.Add(new cArrayList("@driver_cd", hdsalesman_cd.Value));
        arr.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
        Session["lParamDSR"] = arr;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "go", "openreport('fm_report2.aspx?src=DSR');", true);
    }
}